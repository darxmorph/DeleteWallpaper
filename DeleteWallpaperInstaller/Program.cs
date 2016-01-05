using System;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Windows.Forms;
using DeleteWallpaperInstaller.Natives;

namespace DeleteWallpaperInstaller
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
            string uiTitle = pName + " Installer";
            // Ask users if they're sure
            if (MessageBox.Show("Are you sure you want to install " + pName + "?", uiTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                Environment.Exit(0);
            string pFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), pName);
            if (Directory.Exists(pFolder))
            {
                MessageBox.Show("ERROR: Program is already installed.\nYou can uninstall it from Control Panel", uiTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            // Make program folder
            Directory.CreateDirectory(pFolder);
            // Copy files
            using (Stream uninstaller = File.Create(Path.Combine(pFolder, "uninstall.exe")), delbgvbs = File.Create(Path.Combine(pFolder, "delbg.vbs")), trashfileexe = File.Create(Path.Combine(pFolder, "TrashFile.exe")), win7vbs = File.Create(Path.Combine(pFolder, "win7.vbs")), win8vbs = File.Create(Path.Combine(pFolder, "win8.vbs")), hackmevbs = File.Create(Path.Combine(pFolder, "hackme.vbs")), nextwpvbs = File.Create(Path.Combine(pFolder, "nextwp.vbs")))
            {
                Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".Resources.uninstall.exe").CopyTo(uninstaller);
                Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".Resources.delbg.vbs").CopyTo(delbgvbs);
                Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".Resources.TrashFile.exe").CopyTo(trashfileexe);
                Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".Resources.win7.vbs").CopyTo(win7vbs);
                Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".Resources.win8.vbs").CopyTo(win8vbs);
                Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".Resources.hackme.vbs").CopyTo(hackmevbs);
                Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".Resources.nextwp.vbs").CopyTo(nextwpvbs);
            }
            // Add Desktop context menu association
            RegistryKey desk = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Classes\DesktopBackground\Shell\" + pName);
            desk.SetValue("", "Delete current desktop wallpaper", RegistryValueKind.String);
            desk.SetValue("icon", "imageres.dll,84");
            desk.Close();
            RegistryKey deskcmd = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Classes\DesktopBackground\Shell\" + pName + @"\command");
            deskcmd.SetValue("", "wscript.exe " + Path.Combine(pFolder,"delbg.vbs"), RegistryValueKind.String);
            deskcmd.Close();
            // Add Uninstall Registry Entry
            RegistryKey uninstall = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\" + pName);
            uninstall.SetValue("DisplayName", pName, RegistryValueKind.String);
            uninstall.SetValue("DisplayVersion", "1.0.0.0", RegistryValueKind.String);
            uninstall.SetValue("DisplayIcon", "imageres.dll, 108", RegistryValueKind.String);
            uninstall.SetValue("UninstallString", Path.Combine(pFolder, "uninstall.exe"), RegistryValueKind.String);
            uninstall.SetValue("EstimatedSize", 40, RegistryValueKind.DWord);
            uninstall.SetValue("NoModify", 1, RegistryValueKind.DWord);
            uninstall.SetValue("NoRepair", 1, RegistryValueKind.DWord);
            uninstall.SetValue("Publisher", "Henry", RegistryValueKind.String);
            uninstall.SetValue("HelpLink", "github.com/darxmorph", RegistryValueKind.String);
            uninstall.Close();
            // Refresh Desktop
            ShellHelper.SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
            // Tell them it's done
            MessageBox.Show("Done\nYou can uninstall this later from Control Panel - Uninstall or change a program", uiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
