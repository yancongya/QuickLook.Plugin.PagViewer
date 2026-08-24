using System;
using System.Runtime.InteropServices;

namespace QuickLook.Plugin.PagViewer.Native
{
    internal static class LibPagNative
    {
        private const string DllName = "pag";

        // --- File operations ---

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pag_file_load(byte[] bytes, int length, string filePath);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int pag_file_get_num_texts(IntPtr file);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int pag_file_get_num_images(IntPtr file);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long pag_file_get_duration(IntPtr file);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pag_file_set_time_stretch_mode(IntPtr file, PagTimeStretchMode mode);

        // --- Player operations ---

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pag_player_create();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pag_player_set_composition(IntPtr player, IntPtr composition);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pag_player_set_surface(IntPtr player, IntPtr surface);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double pag_player_get_progress(IntPtr player);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pag_player_set_progress(IntPtr player, double progress);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool pag_player_flush(IntPtr player);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long pag_player_get_duration(IntPtr player);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern PagScaleMode pag_player_get_scale_mode(IntPtr player);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pag_player_set_scale_mode(IntPtr player, PagScaleMode scaleMode);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool pag_player_get_cache_enable(IntPtr player);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pag_player_set_cache_enable(IntPtr player, bool cacheEnable);

        // --- Surface operations ---

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pag_surface_make_offscreen(int width, int height);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool pag_surface_read_pixels(IntPtr surface, PagColorType colorType,
            PagAlphaType alphaType, IntPtr dstPixels, int dstRowBytes);

        // --- Object lifecycle ---

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void pag_release(IntPtr obj);
    }
}
