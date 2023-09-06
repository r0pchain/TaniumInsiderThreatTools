$BLV = Get-BitLockerVolume -MountPoint "C:"

$KeyID = $BLV.KeyProtector | Where-Object {$PSItem.KeyProtectorType -eq "TPMPin"}

Remove-BitLockerKeyProtector -MountPoint "C:" -KeyProtectorID $KeyID.KeyProtectorID

Add-BitLockerKeyProtector -MountPoint "C:" -TPMProtector

