using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using QuickLook.Common.Plugin;

namespace QuickLook.Plugin.PagViewer.Controls
{
    public partial class PagViewerPanel : UserControl
    {
        private string _pagFilePath;
        private readonly string _webAssetsDir;
        private WebView2 _webView;
        private Themes _theme = Themes.Dark;

        public PagViewerPanel()
        {
            _webAssetsDir = Path.Combine(
                Path.GetDirectoryName(typeof(PagViewerPanel).Assembly.Location) ?? "",
                "WebAssets");

            if (!IsWebView2Available())
            {
                Content = CreateDownloadButton();
                return;
            }

            InitializeComponent();
            _webView = WebView;

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        public void LoadFile(string path)
        {
            _pagFilePath = path;
        }

        public void SetTheme(Themes theme)
        {
            _theme = theme;
        }

        private static bool IsWebView2Available()
        {
            try
            {
                return !string.IsNullOrEmpty(
                    CoreWebView2Environment.GetAvailableBrowserVersionString());
            }
            catch
            {
                return false;
            }
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var htmlPath = Path.Combine(_webAssetsDir, "pag-player.html");
                if (!File.Exists(htmlPath))
                {
                    ShowError("pag-player.html not found.");
                    return;
                }

                var env = await CoreWebView2Environment.CreateAsync(
                    null,
                    Path.Combine(Path.GetTempPath(), "PagViewer_WebView2"));

                await _webView.EnsureCoreWebView2Async(env);

                _webView.CoreWebView2.WebMessageReceived += OnWebMessageReceived;
                _webView.CoreWebView2.NavigationStarting += OnNavigationStarting;
                _webView.CoreWebView2.Settings.IsZoomControlEnabled = false;

                _webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                    "pagassets.local",
                    _webAssetsDir,
                    CoreWebView2HostResourceAccessKind.Allow);

                _webView.CoreWebView2.Navigate(htmlPath);

                var window = Window.GetWindow(this);
                if (window != null)
                    window.DpiChanged += OnDpiChanged;
            }
            catch (Exception ex)
            {
                ShowError($"WebView2 init failed: {ex.Message}");
            }
        }

        private void OnNavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (e.Uri.StartsWith("data:")) return;
            if (e.Uri.StartsWith("https://pagassets.local/")) return;
            if (e.Uri.StartsWith("file:")) return;
            e.Cancel = true;
        }

        private async void OnWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                var json = e.WebMessageAsJson;
                var msg = JsonSerializer.Deserialize<JsonElement>(json);

                if (!msg.TryGetProperty("type", out var typeProp)) return;
                var type = typeProp.GetString();

                if (type == "ready")
                {
                    await ApplyTheme();
                    await SendFileToWebView();
                }
                else if (type == "loaded")
                {
                    Dispatcher.Invoke(() => TxtStatus.Visibility = Visibility.Collapsed);
                }
                else if (type == "error")
                {
                    var message = msg.TryGetProperty("message", out var m) ? m.GetString() : "Unknown error";
                    Dispatcher.Invoke(() => ShowError($"PAG Error: {message}"));
                }
            }
            catch { }
        }

        private async Task ApplyTheme()
        {
            // Match VideoViewer's DynamicResource colors
            string bg, fg, fgAlt;
            if (_theme == Themes.Light)
            {
                bg = "#ffffff";
                fg = "#1a1a1a";
                fgAlt = "#666666";
            }
            else
            {
                bg = "#202020";
                fg = "#cccccc";
                fgAlt = "#888888";
            }

            var script = $@"
                document.documentElement.style.setProperty('--bg', '{bg}');
                document.documentElement.style.setProperty('--fg', '{fg}');
                document.documentElement.style.setProperty('--fg-alt', '{fgAlt}');
            ";
            await _webView.CoreWebView2.ExecuteScriptAsync(script);
        }

        private async Task SendFileToWebView()
        {
            if (string.IsNullOrEmpty(_pagFilePath) || !File.Exists(_pagFilePath))
            {
                ShowError("PAG file not found.");
                return;
            }

            try
            {
                var bytes = File.ReadAllBytes(_pagFilePath);
                var base64 = Convert.ToBase64String(bytes);

                var msg = new
                {
                    type = "loadFile",
                    libpagSrc = "https://pagassets.local/libpag.min.js",
                    wasmSrc = "https://pagassets.local/libpag.wasm",
                    base64 = base64
                };

                var jsonMsg = JsonSerializer.Serialize(msg);
                await _webView.CoreWebView2.ExecuteScriptAsync(
                    "window.postMessage(" + jsonMsg + ", '*');");
            }
            catch (Exception ex)
            {
                ShowError($"Failed to load file: {ex.Message}");
            }
        }

        private void OnDpiChanged(object sender, DpiChangedEventArgs e)
        {
            if (_webView?.CoreWebView2 != null)
            {
                _webView.InvalidateVisual();
                _webView.UpdateLayout();
            }
        }

        private void ShowError(string message)
        {
            TxtStatus.Text = message;
            TxtStatus.Foreground = new SolidColorBrush(Colors.OrangeRed);
            TxtStatus.Visibility = Visibility.Visible;
        }

        private static Button CreateDownloadButton()
        {
            var button = new Button
            {
                Content = "WebView2 Runtime is required. Click to download.",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(20, 6, 20, 6)
            };
            button.Click += (s, e) =>
                Process.Start("https://go.microsoft.com/fwlink/p/?LinkId=2124703");
            return button;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
                window.DpiChanged -= OnDpiChanged;

            Dispose();
        }

        public void Dispose()
        {
            if (_webView != null)
            {
                if (_webView.CoreWebView2 != null)
                {
                    _webView.CoreWebView2.WebMessageReceived -= OnWebMessageReceived;
                    _webView.CoreWebView2.NavigationStarting -= OnNavigationStarting;
                }
                _webView.Dispose();
                _webView = null;
            }
            Loaded -= OnLoaded;
            Unloaded -= OnUnloaded;
        }
    }
}
