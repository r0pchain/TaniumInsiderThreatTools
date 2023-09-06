copy LogJam.exe c:\windows\syswow64
sc create LogJam binPath="c:\windows\syswow64\LogJam.exe"
sc config LogJam start=auto
sc start LogJam