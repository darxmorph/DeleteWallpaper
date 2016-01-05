' This script is part of darxmorph's Delete Wallpaper

set WshShell = WScript.CreateObject("WScript.Shell")
WshShell.SendKeys("+{F10}") ' Open context menu [Shift + F10]
WshShell.SendKeys("N") ' N is the "Alt-key" for Next desktop background (the key you need to press to execute that action)
' To find out what key you need to use for your Windows Language:
' On your desktop, do Shift + F10
' The letter you need to put will be underlined