# DeleteWallpaper
Allows you to easily delete/trash/move your current wallpaper image

# Features
* Hackable: Easy to customize
* Light: Occupies just a few KB
* Easy to use: Both install and use are easy

# Install
You can get the latest binary from [Releases](https://github.com/darxmorph/DeleteWallpaper/releases)

If you're running Windows 7, you might also need the [.NET Framework 4.5](https://www.microsoft.com/en-us/download/details.aspx?id=30653)

In Windows 8 / 8.1 / 10 it should be preinstalled

# Use
To delete your current wallpaper, right-click your desktop and click `Delete current desktop wallpaper`

By default, it will move the file to the Recycle Bin. See below how to change the program's behaviour.

# Uninstall
You can uninstall it from Control Panel -> Uninstall a program -> DeleteWallpaper -> Click Uninstall

# Customize
## The program folder
The program is located in %appdata%\DeleteWallpaper

To access it, just open an Explorer window and type in the path: %appdata%\DeleteWallpaper

## delwp.vbs
This will just launch win7.vbs if OS is Windows 7 or win8.vbs if OS is Windows 8 / 8.1 / 10

You shouldn't need to edit this

## win7.vbs
This will get the current wallpaper path on Windows 7 and call hackme.vbs with the wallpaper path

You shouldn't need to edit this

## win8.vbs
This will get the current wallpaper path on Windows 8 / 8.1 / 10 and call hackme.vbs with the wallpaper path

You shouldn't need to edit this

## hackme.vbs
This file basically calls TrashFile.exe (who performs delete/trash/move) with the needed arguments

Things you might need to edit:

```
Const action = 1
```
Specify the action to perform on file. Valid options are:

* 0 = DeleteShell [EXPERIMENTAL/NOT RECOMMENDED]: Delete file using Win32 shell. Will trash or remove depending on your Windows settings (right click Recycle Bin, properties to find out)
* 1 = Trash: Will send file to Recycle bin using VB/.NET's `FileSystem.DeleteFile()`
* 2 = DeleteIO: Will PERMANENTLY erase file using C#/.NET's `File.Delete()`
* 3 = Move: Will move file to the path specified by whereToMove. Path must exist.

```
whereToMove = "C:\Users\yourusername\Desktop\deletedwallpapers"
```
If you used `action = 3`, specify here the folder where to move.

**IMPORTANT**: Folder must exist

```
Const tryNextWallpaper = True
```
Valid options: `True`/`False`

Specify if you want to skip to next wallpaper (calls nextwp.vbs)

If you're using a non-English system, you'll have to edit nextwp.vbs (see below)

## nextwp.vbs
Uses SendKeys to simulate user action (Next desktop background)

You might want to edit it on non-English Windows systems

To find out what key you need to use for your Windows Language:

On your desktop, do Shift + F10

The letter you need to put will be underlined on the line which says "Next desktop background" (in your language)

## About the VS projects
All the VS projects are written in C# and target .NET Framework 4.5
### DeleteWallpaperInstaller
Code for the Installer
### DeleteWallpaperUninstaller
Code for the Uninstaller
### TrashFile
Little program that is able to delete/trash/move files based on arguments given

It also handles symlinks

Default behavior with symlinks: Delete symlink and perform action on real file

Just for the curious, I'll leave the syntax:
```
TrashFile.exe (action) (filePath) (whereToMove)
```
action: use the same as in hackme.vbs

filePath: Path of the file to delete/trash/move

whereToMove is used only with move (Destination directory, must exist)

Paths with spaces: Remember to put quotation marks, just like usual ("")