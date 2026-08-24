using System;
using System.IO;
using System.Runtime.InteropServices;

namespace QuickLook.Plugin.PagViewer.Native
{
    internal static class PagDllResolver
    {
        private static bool _initialized;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetDllDirectory(string lpPathName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        public static bool TryInitialize()
        {
            if (_initialized) return true;

            var dllDir = FindPagDllDirectory();
            if (dllDir == null) return false;

            SetDllDirectory(dllDir);

            var pagDll = Path.Combine(dllDir, "pag.dll");
            var handle = LoadLibrary(pagDll);
            if (handle == IntPtr.Zero) return false;

            _initialized = true;
            return true;
        }

        private static string FindPagDllDirectory()
        {
            var candidates = new[]
            {
                // PAGViewer default install paths
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PAGViewer"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "PAGViewer"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PAGViewer"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PAGViewer"),
                // Plugin's own directory (bundled DLL)
                Path.GetDirectoryName(typeof(PagDllResolver).Assembly.Location)
            };

            foreach (var dir in candidates)
            {
                if (dir == null) continue;
                if (File.Exists(Path.Combine(dir, "pag.dll")))
                    return dir;
            }

            // Search PATH environment variable
            var pathEnv = Environment.GetEnvironmentVariable("PATH");
            if (pathEnv != null)
            {
                foreach (var dir in pathEnv.Split(';'))
                {
                    if (string.IsNullOrWhiteSpace(dir)) continue;
                    if (File.Exists(Path.Combine(dir.Trim(), "pag.dll")))
                        return dir.Trim();
                }
            }

            return null;
        }
    }
}
