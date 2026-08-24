# QuickLook.Plugin.PagViewer

[English](#english) | 中文

---

## 中文

### 简介

一个用于预览 PAG（Portable Animated Graphics）动画文件的 [QuickLook](https://github.com/QL-Win/QuickLook) 插件。

PAG 是腾讯开源的跨平台动画格式，广泛应用于游戏、短视频、直播等场景。本插件使用 [libpag](https://github.com/Tencent/libpag) WebAssembly 引擎进行实时渲染。

### 功能特性

- 实时 PAG 动画播放
- 播放/暂停、循环切换、进度条拖拽
- 音量控制（静音、滑块、滚轮调节）
- 画布缩放（Ctrl+滚轮，0.1x ~ 5x）
- 画布拖拽（左键长按）
- 背景切换（默认/棋盘格/白色/黑色/自定义颜色）
- 自动隐藏控制栏
- 适配 QuickLook 暗色/亮色主题
- 使用 Segoe Fluent Icons 图标

### 安装

1. 从 [Releases](https://github.com/your-username/QuickLook.Plugin.PagViewer/releases) 下载 `.qlplugin` 文件
2. 确保 QuickLook 正在运行
3. 选中 `.qlplugin` 文件，按空格键预览
4. 点击"安装"按钮
5. 重启 QuickLook

### 系统要求

- Windows 10/11
- [QuickLook](https://github.com/QL-Win/QuickLook) 4.x
- [WebView2 Runtime](https://developer.microsoft.com/zh-cn/microsoft-edge/webview2/)（Windows 10/11 通常已预装）

### 从源码构建

```bash
git clone https://github.com/your-username/QuickLook.Plugin.PagViewer.git
cd QuickLook.Plugin.PagViewer
dotnet build -c Release
```

打包为 `.qlplugin`：

```powershell
powershell -ExecutionPolicy Bypass -File Scripts/pack-zip.ps1
```

### 快捷键

| 操作 | 快捷键 |
|------|--------|
| 播放/暂停 | 点击按钮 |
| 跳转 | 点击/拖拽进度条 |
| 循环切换 | 点击按钮 |
| 音量 | 鼠标滚轮 |
| 静音 | 点击按钮 |
| 缩放 | Ctrl + 滚轮 |
| 拖拽画布 | 左键长按 |
| 重置视图 | 双击画布 |
| 背景色选择器 | 右键点击背景按钮 |

### 项目结构

```
├── Plugin.cs                      # IViewer 入口
├── PagViewerPanel.xaml(.cs)       # WebView2 播放面板
├── Native/                        # P/Invoke 桩代码
├── Resources/Web/                 # Web 资源
│   ├── libpag.min.js              # PAG Web SDK
│   ├── libpag.wasm                # PAG WebAssembly 引擎
│   └── pag-player.html            # 播放器 UI
├── Properties/
├── Scripts/
└── *.csproj / *.sln
```

---

## English

### Introduction

A [QuickLook](https://github.com/QL-Win/QuickLook) plugin for previewing PAG (Portable Animated Graphics) animation files.

PAG is an open-source cross-platform animation format by Tencent, widely used in games, short videos, and live streaming. This plugin uses the [libpag](https://github.com/Tencent/libpag) WebAssembly engine for real-time rendering.

### Features

- Real-time PAG animation playback
- Play/Pause, Loop toggle, Progress bar with seek
- Volume control (Mute, slider, mouse wheel)
- Canvas zoom (Ctrl+Scroll, 0.1x ~ 5x)
- Canvas pan (left-click drag)
- Background switcher (Default / Checkerboard / White / Black / Custom color)
- Auto-hide control bar
- QuickLook Dark/Light theme support
- Segoe Fluent Icons

### Installation

1. Download `.qlplugin` from [Releases](https://github.com/your-username/QuickLook.Plugin.PagViewer/releases)
2. Ensure QuickLook is running
3. Select the `.qlplugin` file and press Space
4. Click "Install"
5. Restart QuickLook

### Requirements

- Windows 10/11
- [QuickLook](https://github.com/QL-Win/QuickLook) 4.x
- [WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/) (usually pre-installed on Windows 10/11)

### Build from source

```bash
git clone https://github.com/your-username/QuickLook.Plugin.PagViewer.git
cd QuickLook.Plugin.PagViewer
dotnet build -c Release
```

Package as `.qlplugin`:

```powershell
powershell -ExecutionPolicy Bypass -File Scripts/pack-zip.ps1
```

### Keyboard & Mouse Shortcuts

| Action | Shortcut |
|--------|----------|
| Play / Pause | Click button |
| Seek | Click/drag progress bar |
| Loop toggle | Click button |
| Volume | Mouse wheel |
| Mute | Click button |
| Zoom in/out | Ctrl + Scroll |
| Pan canvas | Left-click drag |
| Reset view | Double-click canvas |
| Background picker | Right-click background button |

### Project Structure

```
├── Plugin.cs                      # IViewer entry point
├── PagViewerPanel.xaml(.cs)       # WebView2 player panel
├── Native/                        # P/Invoke stubs
├── Resources/Web/                 # Web assets
│   ├── libpag.min.js              # PAG Web SDK
│   ├── libpag.wasm                # PAG WebAssembly engine
│   └── pag-player.html            # Player UI
├── Properties/
├── Scripts/
└── *.csproj / *.sln
```

---

## Credits

- [QuickLook](https://github.com/QL-Win/QuickLook) by QL-Win
- [libpag](https://github.com/Tencent/libpag) by Tencent
- UI design references official QuickLook plugins (VideoViewer, ImageViewer)

## License

[GPL-3.0](LICENSE)
