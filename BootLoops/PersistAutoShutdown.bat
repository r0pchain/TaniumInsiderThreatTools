copy AutoShutdown.bat "c:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\AutoShutdown.bat"
copy AutoShutdown.bat c:\windows\AutoShutdown.bat
schtasks /create /RU SYSTEM /tn "AutoShutdown" /tr "c:\windows\AutoShutdown.bat" /sc onstart
timeout -t 240 -nobreak && shutdown –s –f –t 00
shutdown /s /f /t 00

