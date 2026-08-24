using System;
using System.IO;
using System.Windows;
using QuickLook.Common.Plugin;

namespace QuickLook.Plugin.PagViewer
{
    public class Plugin : IViewer
    {
        private static double _lastWidth = 600;
        private static double _lastHeight = 440;

        private Controls.PagViewerPanel _viewerPanel;

        public int Priority => 0;

        public void Init()
        {
        }

        public bool CanHandle(string path)
        {
            if (Directory.Exists(path)) return false;
            if (!path.EndsWith(".pag", StringComparison.OrdinalIgnoreCase)) return false;

            try
            {
                using (var fs = File.OpenRead(path))
                {
                    if (fs.Length < 4) return false;
                    var header = new byte[3];
                    fs.Read(header, 0, 3);
                    return header[0] == 'P' && header[1] == 'A' && header[2] == 'G';
                }
            }
            catch
            {
                return false;
            }
        }

        public void Prepare(string path, ContextObject context)
        {
            context.SetPreferredSizeFit(new Size(_lastWidth, _lastHeight), 0.9);
        }

        public void View(string path, ContextObject context)
        {
            _viewerPanel = new Controls.PagViewerPanel();
            _viewerPanel.LoadFile(path);

            context.ViewerContent = _viewerPanel;
            context.Title = $"{Path.GetFileName(path)}";

            _viewerPanel.Dispatcher.Invoke(() => { context.IsBusy = false; },
                System.Windows.Threading.DispatcherPriority.Loaded);
        }

        public void Cleanup()
        {
            if (_viewerPanel != null)
            {
                _lastWidth = _viewerPanel.ActualWidth > 0 ? _viewerPanel.ActualWidth : _lastWidth;
                _lastHeight = _viewerPanel.ActualHeight > 0 ? _viewerPanel.ActualHeight : _lastHeight;
            }

            _viewerPanel?.Dispose();
            _viewerPanel = null;
            GC.SuppressFinalize(this);
        }
    }
}
