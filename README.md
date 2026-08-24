# QuickLook.Plugin.PagViewer

A [QuickLook](https://github.com/QL-Win/QuickLook) plugin for previewing PAG (Portable Animated Graphics) files on Windows.

## Features

- **Real-time PAG playback** using [libpag](https://github.com/Tencent/libpag) WebAssembly engine
- **Playback controls**: Play/Pause, Loop toggle, Progress bar with seek
- **Volume control**: Mute button, volume slider, mouse wheel adjustment
- **Canvas zoom**: Ctrl+Scroll to zoom (0.1x ~ 5x), left-click drag to pan, double-click to reset
- **Background switcher**: Default / Checkerboard / White / Black / Custom color (right-click for picker)
- **Auto-hide UI**: Control bar and top toolbar fade out after 1.5s of inactivity
- **Theme support**: Adapts to QuickLook Dark/Light theme
- **Segoe Fluent Icons**: Consistent with official QuickLook plugins

## Requirements

- Windows 10/11
- [QuickLook](https://github.com/QL-Win/QuickLook) 4.x
- [WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/) (usually pre-installed on Windows 10/11)

## Installation

1. Download `QuickLook.Plugin.PagViewer.qlplugin` from Releases
2. Select the file in Explorer and press Space to preview
3. Click "Install"
4. Restart QuickLook

## Build

### Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download) or Visual Studio 2022
- Windows 10/11

### Build from source

```bash
git clone https://github.com/your-username/QuickLook.Plugin.PagViewer.git
cd QuickLook.Plugin.PagViewer
dotnet build -c Release
```

### Package as .qlplugin

```powershell
cd Scripts
.\pack-zip.ps1
```

The output `QuickLook.Plugin.PagViewer.qlplugin` will be in the project root.

## Project Structure

```
├── Plugin.cs                      # IViewer implementation
├── PagViewerPanel.xaml(.cs)       # WebView2-based PAG player panel
├── Native/                        # P/Invoke stubs (for future native libpag)
│   ├── LibPagNative.cs
│   ├── LibPagTypes.cs
│   └── PagDllResolver.cs
├── Resources/Web/                 # Web assets
│   ├── libpag.min.js              # PAG Web SDK
│   ├── libpag.wasm                # PAG WebAssembly engine
│   └── pag-player.html            # Player UI
├── Properties/
│   └── AssemblyInfo.cs
├── Scripts/
│   └── pack-zip.ps1               # Packaging script
├── QuickLook.Plugin.PagViewer.csproj
├── QuickLook.Plugin.Metadata.Base.config
└── QuickLook.Plugin.Metadata.config
```

## Keyboard & Mouse Shortcuts

| Action | Shortcut |
|--------|----------|
| Play / Pause | Click button |
| Seek | Click/drag progress bar |
| Loop toggle | Click button |
| Volume | Mouse wheel |
| Mute | Click button |
| Zoom in/out | Ctrl + Scroll |
| Pan canvas | Left-click drag on canvas |
| Reset view | Double-click canvas |
| Background picker | Right-click background button |

## Credits

- [QuickLook](https://github.com/QL-Win/QuickLook) by QL-Win
- [libpag](https://github.com/Tencent/libpag) by Tencent
- UI design references official QuickLook plugins (VideoViewer, ImageViewer, LottieFilesViewer)

## License

GPL-3.0
