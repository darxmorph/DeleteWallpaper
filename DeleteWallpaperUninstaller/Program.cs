using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Forms;
using DeleteWallpaperUninstaller.Natives;

namespace DeleteWallpaperUninstaller
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string pName = "DeleteWallpaper";
            string uiTitle = pName + " Uninstaller";
            // Ask users if they're sure
            if (MessageBox.Show("Are you sure you want to uninstall " + pName + "?", uiTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                Environment.Exit(0);
            string pFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), pName);
            if (!Directory.Exists(pFolder))
            {
                MessageBox.Show("ERROR: Program is not installed", uiTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            // Remove Desktop context menu association
            Registry.CurrentUser.DeleteSubKeyTree(@"SOFTWARE\Classes\DesktopBackground\Shell\" + pName);
            // Remove Uninstall Registry Entry
            Registry.CurrentUser.DeleteSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\" + pName);
            // Refresh Desktop
            ShellHelper.SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
            // Self-delete entire dir
            ProcessStartInfo Info = new ProcessStartInfo();
            Info.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Info.Arguments = "/C PING 1.1.1.1 -n 1 -w 800 & RD /S /Q \"" + pFolder + "\"& >%temp%\\temp.vbs echo MsgBox \"" + pName + " was successfully uninstalled\",\"64\",\"" + uiTitle + "\" & call %temp%\\temp.vbs & del /f /q %temp%\\temp.vbs";
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";
            Process.Start(Info);
        }
    }
}
