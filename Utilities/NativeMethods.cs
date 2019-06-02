using System;
using System.Runtime.InteropServices;

namespace MouseNet.Logophi.Utilities {
    /// <summary>
    ///     Defines imported native methods.
    /// </summary>
    internal static class NativeMethods {
        //Registers a hotkey to the given window handle.
        [DllImport("user32.dll")]
        internal static extern bool RegisterHotKey
        (IntPtr hWnd,
         int id,
         int fsModifiers,
         int vk);

        //Unregisters the hotkey indicated by the specified id from the given window handle.
        [DllImport("user32.dll")]
        internal static extern bool UnregisterHotKey
        (IntPtr hWnd,
         int id);

        //Gets a handle to the window in the foreground.
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();
    }
}