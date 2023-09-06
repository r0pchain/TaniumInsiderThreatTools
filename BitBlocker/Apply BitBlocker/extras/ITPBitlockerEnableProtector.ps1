
$Volume = Get-CimInstance -Namespace root/CIMV2/Security/MicrosoftVolumeEncryption -ClassName Win32_EncryptableVolume -Filter 'DriveLetter="C:"'; $Protector = (Invoke-CimMethod -InputObject $Volume -MethodName GetKeyProtectors -Arguments @{ KeyProtectorType=4 }).VolumeKeyProtectorID[0]; Invoke-CimMethod -InputObject $Volume -MethodName ChangePIN -Arguments @{ VolumeKeyProtectorID=$Protector; NewPIN='ITPSecurityLockout_1-1A-2B' }


echo "Volume: $Volume"
echo "Protector: $Protector"
