# Void

> **The minimalist Windows deep-work launcher and context guard.** Boots your chosen task, locks your workspace, and keeps you from wandering off.

[![Void Launcher Overview](media/overview.png)](media/walkthrough.mp4)

*Click the preview card above to watch the walkthrough video.*

---

## Why I Built This

When starting a difficult engineering task, the temptation to check email, open social media tabs, or reorganize files is highest in the first 10 minutes. 

Void is a lightweight C# WPF desktop launcher. You pick your target project, and Void opens the required tools, suppresses background notifications, and floats a low-profile session HUD. If you switch to an unapproved distraction window, Void gently guides you back to your primary task.

## Standout Features

- **Instant Workspace Boot:** Launches your IDE, terminal profiles, and project documents in a single keystroke.
- **Context Guard:** Low-overhead Windows window hook monitors active process handles and redirects wandering focus.
- **Ambient Desktop HUD:** Subtle, semi-transparent progress indicator that keeps you aware of your sprint timing without breaking immersion.

## Visuals & Architecture

| Session Launcher Card | Architecture Overview |
| :--- | :--- |
| ![Void HUD](media/overview.png) | ![Poster Preview](media/poster.png) |

Watch the explainer video: **[media/walkthrough.mp4](media/walkthrough.mp4)**.

## Building & Running

```bash
git clone https://github.com/Invictine/Void.git
cd Void
dotnet build -c Release
dotnet run --project Void.WPF
```
