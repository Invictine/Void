using System.Windows;
using System.Diagnostics;
namespace VoidApp;
public partial class App : Application
{
    private Mutex? instance;
    private LauncherWindow? launcher;
    protected override void OnStartup(StartupEventArgs e)
    {
        if (e.Args.Length is 4 or 5 && e.Args[0] == "--recover")
        {
            // A separate instance waits for the UI process, including a forced termination.
            try
            {
                using var parent = Process.GetProcessById(int.Parse(e.Args[1]));
                using var ready = EventWaitHandle.OpenExisting(e.Args[3]);
                ready.Set();
                if (parent.StartTime.ToUniversalTime().Ticks == long.Parse(e.Args[2])) parent.WaitForExit();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or System.ComponentModel.Win32Exception or WaitHandleCannotBeOpenedException) { }
            finally
            {
                if (e.Args.Length == 5) DisplaySession.Restore(e.Args[4]);
                TaskbarSession.Restore();
            }
            Shutdown();
            return;
        }
        instance = new Mutex(true, "Local\\Void.FocusShell", out bool first);
        if (!first)
        {
            var existing = NativeMethods.FindWindow(null, "Void");
            if (existing != 0) NativeMethods.PostMessage(existing, 0x8002, 0, 0);
            Shutdown(); return;
        }
        DispatcherUnhandledException += (_, args) =>
        {
            TaskbarSession.Restore(); args.Handled = true;
            MessageBox.Show("Void stopped safely: " + args.Exception.Message, "Void");
            Shutdown(1);
        };
        AppDomain.CurrentDomain.UnhandledException += (_, _) => TaskbarSession.Restore();
        SessionEnding += (_, _) => TaskbarSession.Restore();
        var home = new MainWindow();
        MainWindow = home;
        // Reserve recovery shortcuts without showing the fullscreen window.
        new System.Windows.Interop.WindowInteropHelper(home).EnsureHandle();
        launcher = new LauncherWindow(home.OpenHome, home.QuickLaunch, home.GetRecentPresets, () => home.ExitApplication());
        home.ResidentRequested += () => launcher.Show();
        home.FocusStarted += () => launcher.Hide();
        launcher.Show();
    }
    protected override void OnExit(ExitEventArgs e)
    {
        if (TaskbarSession.Active) TaskbarSession.Restore();
        instance?.Dispose();
        base.OnExit(e);
    }
}
