# Changelog

## [1.2.0] - 2026-08-24

### Added
- Performance monitoring: FPS display in top-left corner with color coding
- Progress bar color changes based on render pressure (green/yellow/red)
- Sample PAG files for testing (from Tencent/libpag official assets)
- Issue templates (bug report, feature request)
- Pull request template
- Version management script (update-version.ps1)

### Changed
- Simplified performance display (removed OSD overlay)
- FPS shown below dimensions info (green ≥50, yellow ≥30, red <30)
- Progress bar color reflects flushTime (green ≤10ms, yellow ≤20ms, red >20ms)

### Fixed
- btnPerf declaration order causing applyI18n error
- i18n default translations fallback

## [1.1.0] - 2026-08-24

### Added
- i18n bilingual support (Chinese/English) from Translations.config
- Metadata panel (dimensions, duration, FPS, layers, images, texts)
- Save image as PNG (saves next to PAG file)
- Copy file to clipboard
- Window drag (click background area to drag)
- Canvas zoom (Ctrl+Scroll, 0.1x ~ 5x)
- Canvas pan (left-click drag)
- Double-click to reset view
- Background switcher (default/checkerboard/white/black/custom color)
- Volume control (button, slider, mouse wheel)
- Loop toggle button
- Toast notifications
- WebView2 availability check with download fallback
- DPI change handling
- GitHub Actions CI/CD workflow

### Changed
- UI redesigned to match official VideoViewer style
- Segoe Fluent Icons for all buttons
- Auto-hide control bar (show on mouse move, fade after 1.5s)
- Progress bar thin line style with hover thumb
- Settings persisted via localStorage

### Fixed
- WebGL canvas frame capture (preserveDrawingBuffer)
- Transparent PNG export (remove black background)
- Dispose NullReferenceException
- Build exclusion for QuickLook subdirectory

## [1.0.0] - 2026-08-24

### Added
- Initial release
- PAG file preview using WebView2 + libpag WebAssembly
- Basic playback controls (play/pause, progress bar)
- QuickLook plugin structure (IViewer implementation)
