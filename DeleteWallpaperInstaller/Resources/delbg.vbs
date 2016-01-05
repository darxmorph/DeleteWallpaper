' This script is part of darxmorph's Delete Wallpaper
' Determine Windows 7 / Windows 8 / 8.1 / 10

Set objShell = CreateObject("WScript.Shell")
Set objFSO = CreateObject("Scripting.FileSystemObject")
objShell.CurrentDirectory = scriptFolder

strComputer = "."
Set objWMIService = GetObject("winmgmts:\\" & strComputer & "\root\cimv2")        
Set colOperatingSystem = objWMIService.ExecQuery("Select * from Win32_OperatingSystem")

For Each objOperatingSystem in colOperatingSystem
ver = objOperatingSystem.Version
Next

' Windows 10
If InStr(1, ver, "10") = 1 Then
    Windows8AndHigher
	WScript.Quit
End If

' Windows 8.1
If InStr(1, ver, "6.3") = 1 Then
    Windows8AndHigher
	WScript.Quit
End If

' Windows 8
If InStr(1, ver, "6.2") = 1 Then
    Windows8AndHigher
	WScript.Quit
End If

' Windows 7
If InStr(1, ver, "6.1") = 1 Then
    Windows7
	WScript.Quit
End If

WScript.Quit

Sub Windows7
  objShell.Run "win7.vbs",0,false
End Sub

Sub Windows8AndHigher
  objShell.Run "win8.vbs",0,false
End Sub

Function scriptFolder
    strScriptPath = Wscript.ScriptFullName
    Set objFile = objFSO.GetFile(strScriptPath)
    scriptFolder = objFSO.GetParentFolderName(objFile)
End Function