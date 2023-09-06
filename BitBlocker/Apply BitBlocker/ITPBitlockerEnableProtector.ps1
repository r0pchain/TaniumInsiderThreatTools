$PinString = ConvertTo-SecureString "ITPSecurityLockout_1-1A-2B" -AsPlainText -Force
Add-BitLockerKeyProtector -MountPoint "C:" -Pin $PinString -TPMandPinProtector