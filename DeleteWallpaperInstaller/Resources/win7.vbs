' This script is part of darxmorph's Delete Wallpaper
' I just modified for my own purposes
' As script is not mine, I've left the info from the original authors

'"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'Copyright © 2010 Ramesh Srinivasan. All rights reserved.
'Filename	 : WPTargetDir.vbs
'Description : Opens the target folder of current Desktop Background in Windows 7
'Author   	 : Ramesh Srinivasan, Microsoft MVP (Windows)
'Homepage 	 : http://www.winhelponline.com/blog
'Created 	 : Feb 02, 2010
'"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
Set WshShell = WScript.CreateObject("WScript.Shell")
Set fso = CreateObject("Scripting.FileSystemObject")
strCurWP = ""

On Error Resume Next
strCurWP = WshShell.RegRead("HKCU\Software\Microsoft\Internet Explorer\Desktop\General\WallpaperSource")
On Error Goto 0

If Trim(strCurWP) = "" Then
	MsgBox "No Wallpaper selected for Desktop Slideshow",16,"Delete Wallpaper - Error"
Else
	If fso.FileExists(strCurWP) Then
		    CreateObject("Shell.Application").ShellExecute "hackme.vbs", chr(34) & strCurWP & chr(34), scriptFolder, "open", 1
	Else
		MsgBox "Cannot find file: " & strCurWP,16,"Delete Wallpaper - Error"
	End If
End If

Function scriptFolder
    strScriptPath = Wscript.ScriptFullName
    Set objFile = fso.GetFile(strScriptPath)
    scriptFolder = fso.GetParentFolderName(objFile)
End Function