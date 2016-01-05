' This script is part of darxmorph's Delete Wallpaper

' Control code (do not edit)
If Not WScript.Arguments.Count = 1 Then
  WScript.Quit
End If
wallpaperFilePath = WScript.Arguments(0)

whereToMove = "C:\Users\Henry\Desktop\deletedwallpapers"

Const action = 1
' 0 = DeleteShell [EXPERIMENTAL/NOT RECOMMENDED]: Delete file using Win32 shell. Will trash or remove depending on your Windows settings (right click Recycle Bin, properties to find out)
' 1 = Trash: Will send file to Recycle bin using VB/.NET's FileSystem.DeleteFile()
' 2 = DeleteIO: Will PERMANENTLY erase file using C#/.NET's File.Delete()
' 3 = Move: Will move file to the path specified by whereToMove. Path must exist.

Const tryNextWallpaper = True
' True/False
' Self-Explanatory: Tries to skip to next wallpaper
' Non-English systems: please take a look to nextwp.vbs and edit keystrokes as needed
' (better explained in file)

' Do action
Select Case action
 Case 0
  arguments = action & chr(32) & chr(34) & wallpaperFilePath & chr(34)
 Case 1
  arguments = action & chr(32) & chr(34) & wallpaperFilePath & chr(34)
 Case 2
  arguments = action & chr(32) & chr(34) & wallpaperFilePath & chr(34)
 Case 3
  Set objFSO = CreateObject("Scripting.FileSystemObject")
  If Not objFSO.FolderExists(whereToMove) Then
   MsgBox "whereToMove folder does not exist",16,"DeleteWallpaper - Error"
   WScript.Quit
  End If
  arguments = action & chr(32) & chr(34) & wallpaperFilePath & chr(34) & chr(32) & chr(34) & whereToMove & chr(34)
  Case Else
   MsgBox "action is invalid",16,"DeleteWallpaper - Error"
   WScript.Quit
End Select

' chr(32) = space ( )
' chr(34) = double quotes (")

CreateObject("Shell.Application").ShellExecute "TrashFile.exe", arguments, scriptFolder, "open", 1

If tryNextWallpaper = True Then
 CreateObject("Shell.Application").ShellExecute "wscript.exe", "nextwp.vbs", scriptFolder, "open", 1
End If