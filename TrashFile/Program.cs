using System;
using System.IO;
using TrashFile.Natives;
using static TrashFile.Natives.ShellHelper;
using TrashFile.Symlinks;
using Microsoft.VisualBasic.FileIO;

namespace TrashFile
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Parse int from string argument to TrashFile.Program.whatToDo
            whatToDo action = (whatToDo)int.Parse(args[0]);
            // Check if parsed action is valid (exists in enum)
            bool isActionValid = Enum.IsDefined(typeof(whatToDo), action);
            
            // If it isn't, exit (with ErrorCode 1)
            if (!(isActionValid))
                Environment.Exit(1);

            // Check if has correct number of arguments for each action
            switch (action)
            {
                case whatToDo.DeleteShell:
                    if (!(args.Length == 2))
                        Environment.Exit(1);
                    break;
                case whatToDo.Trash:
                    if (!(args.Length == 2))
                        Environment.Exit(1);
                    break;
                case whatToDo.DeleteIO:
                    if (!(args.Length == 2))
                        Environment.Exit(1);
                    break;
                case whatToDo.Move:
                    if (!(args.Length == 3))
                        Environment.Exit(1);
                    break;
            }

            // Get variables from arguments
            string file = args[1];
            string filePath = Path.GetFullPath(file);
            string whereToMove = null;

            if (action == whatToDo.Move) {
                whereToMove = args[2];
                if (!(Directory.Exists(whereToMove)))
                    Environment.Exit(2);
            }

            // Check if file exists
            if (!(File.Exists(filePath)))
                Environment.Exit(2);

            // Check if the file is a symlink
            bool isSymlink = SymbolicLink.Exists(filePath);

            if (isSymlink)
            {
                // If it is, remove/move real file
                string realFilePath = SymbolicLink.GetTarget(filePath);
                if (File.Exists(realFilePath))
                {
                    switch (action)
                    {
                        case whatToDo.DeleteShell:
                            ShellFileOperations.Delete(realFilePath);
                            break;
                        case whatToDo.Trash:
                            FileSystem.DeleteFile(realFilePath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
                            break;
                        case whatToDo.DeleteIO:
                            File.Delete(realFilePath);
                            break;
                        case whatToDo.Move:
                            File.Move(realFilePath, Path.Combine(whereToMove, Path.GetFileName(realFilePath)));
                            break;
                    }
                }
                // Moving symlinks wouldn't make sense
                File.Delete(filePath);
            }
            else {
                // File isn't a symlink
                switch (action)
                {
                    case whatToDo.DeleteShell:
                        ShellFileOperations.Delete(filePath);
                        break;
                    case whatToDo.Trash:
                        FileSystem.DeleteFile(filePath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
                        break;
                    case whatToDo.DeleteIO:
                        File.Delete(filePath);
                        break;
                    case whatToDo.Move:
                        File.Move(filePath, Path.Combine(whereToMove, Path.GetFileName(filePath)));
                        break;
                }
            }
            // Next Wallpaper (this is now done by nextwp.vbs)
            /*
            CultureInfo ci = CultureInfo.CurrentUICulture; // Get user language
            // (obsolete)TODO: check if ci is English (in any variant)
            Shell32.Shell objShell = new Shell32.Shell();
            objShell.ToggleDesktop();
            // Simulately press Shift + F10 to open Desktop context menu
            System.Windows.Forms.SendKeys.SendWait("+{F10}");
            // Simulately press shortkey N to execute the “Next desktop background”
            System.Windows.Forms.SendKeys.SendWait("{N}");
            System.Threading.Thread.Sleep(50);
            objShell.ToggleDesktop();
            */
        }
        private enum whatToDo
        {
            DeleteShell, // 0
            Trash, // 1
            DeleteIO, // 2
            Move, // 3
        }
    }
    public static class ShellFileOperations {
        public static void Delete(string path)
        {
            SHFILEOPSTRUCT shf = new SHFILEOPSTRUCT();
            shf.wFunc = FO_DELETE;
            shf.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
            shf.pFrom = path;
            SHFileOperation(ref shf);
        }
    }
}
