https://docs.github.com/en/actions/hosting-your-own-runners/adding-self-hosted-runners

# Go to this url
https://github.com/ISSAOnline/issa-online-data/settings/actions/runners/new

# Create a folder under the drive root
$ mkdir actions-runner; 
cd actions-runner# Download the latest runner package

$ Invoke-WebRequest -Uri https://github.com/actions/runner/releases/download/v2.295.0/actions-runner-win-x64-2.295.0.zip -OutFile actions-runner-win-x64-2.295.0.zip# Optional: Validate the hash

$ if((Get-FileHash -Path actions-runner-win-x64-2.295.0.zip -Algorithm SHA256).Hash.ToUpper() -ne 'bd448c6ce36121eeb7f71c2c56025c1a05027c133b3cff9c7094c6bfbcc1314f'.ToUpper()){ throw 'Computed checksum did not match' }# Extract the installer

$ Add-Type -AssemblyName System.IO.Compression.FileSystem ; [System.IO.Compression.ZipFile]::ExtractToDirectory("$PWD/actions-runner-win-x64-2.295.0.zip", "$PWD")