# AGENTS.md

## Build Commands

```bash
# Build
dotnet build -c Release

# Package（快速打包；CI 用 Scripts/build.ps1 -SkipBuild -SkipVersion，命名只在 build.ps1 维护）
powershell -ExecutionPolicy Bypass -File Scripts/pack-zip.ps1
```

## Project Overview

QuickLook plugin for previewing PAG (Portable Animated Graphics) files. Uses WebView2 + libpag WebAssembly for rendering.

## Key Files

- `Plugin.cs` - IViewer entry point, handles .pag file detection
- `PagViewerPanel.xaml.cs` - WebView2 panel, manages lifecycle and theme
- `Resources/Web/pag-player.html` - Player UI (HTML/CSS/JS)
- `Resources/Web/libpag.min.js` + `libpag.wasm` - PAG Web SDK

## Architecture

```
Plugin.cs (IViewer)
  └── PagViewerPanel (WPF UserControl)
        └── WebView2
              └── pag-player.html
                    └── libpag.min.js + libpag.wasm (WebAssembly)
```

## CI / Release

- push `master` / PR / 手动：构建 + 打包，上传 Artifact（14 天），**不**发 Release
- tag `v*`：构建打包后自动创建 GitHub Release，资产为 `QuickLook.Plugin.PagViewer.qlplugin`
- 打包命名唯一维护在 `Scripts/build.ps1`；workflow 调用它，**禁止**在 workflow 内联第二份打包逻辑
- 发版只需 `git tag vX.Y.Z && git push --tags`，无需手动上传

## Conventions

- Target framework: .NET Framework 4.6.2
- Namespace: `QuickLook.Plugin.PagViewer`
- Follow official QuickLook plugin structure (see VideoViewer, ImageViewer)
- XAML files at project root (not in subdirectories)
- Web assets in `Resources/Web/`
- Native P/Invoke stubs in `Native/`
