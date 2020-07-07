# Load WinSCP .NET assembly
Add-Type -Path "$PSScriptRoot\WinSCPnet.dll"
function FileTransferProgress {
param($e)
Write-Progress `
-Activity "Uploading" -Status ("{0:P0} complete:" -f $e.OverallProgress) `
-PercentComplete ($e.OverallProgress * 100)
Write-Progress `
-Id 1 -Activity $e.FileName -Status ("{0:P0} complete:" -f $e.FileProgress) `
-PercentComplete ($e.FileProgress * 100)
}
# Set up session options
$sessionOptions = New-Object WinSCP.SessionOptions -Property @{
Protocol              = [WinSCP.Protocol]::Sftp
HostName              = "raspberrypi.local"
UserName              = "pi"
Password              = "achuchitos"
SshHostKeyFingerprint = "ssh-ed25519 255 rBvR+ZM500gmyRfcKEbsSEtpsgKG+Px3YKRhnRL+tfA="
}
$session = New-Object WinSCP.Session
try {
# Will continuously report progress of transfer
$session.add_FileTransferProgress( { FileTransferProgress($_) } )
# Connect
$session.Open($sessionOptions)
try {
$session.ExecuteCommand("killall Hydraled").Check();
}
catch {
Write-Host 'didnt kill Hydraled because it wasnt running '
}
Start-Process dotnet -ArgumentList 'publish -r linux-arm' -Wait -NoNewWindow -WorkingDirectory $PSScriptRoot
#$result = $session.PutFiles("$PSScriptRoot/Hydraled/bin\Debug\netcoreapp3.1\linux-arm\publish\*", "Documents/HydrateProject/HelloWorldRemote/").Check();
#Write-Host $result
$session.ExecuteCommand("chown pi /home/pi/Documents/HydrateProject/HelloWorldRemote -R").Check();
$session.ExecuteCommand("chmod 777 /home/pi/Documents/HydrateProject/HelloWorldRemote -R").Check();
$session.ExecuteCommand("/home/pi/Documents/HydrateProject/HelloWorldRemote/Hydraled").Check();
}
finally {
$session.Dispose()
}
#/home/pi/Documents/Hydrate Project/HelloWorldRemote