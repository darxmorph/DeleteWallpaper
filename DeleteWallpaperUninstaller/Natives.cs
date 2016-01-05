using System;
using System.Runtime.InteropServices;

namespace DeleteWallpaperUninstaller.Natives
{
    public static class ShellHelper
    {
        [DllImport("shell32.dll")]
        public static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);
    }
}
