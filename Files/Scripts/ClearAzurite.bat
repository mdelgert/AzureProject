set folder="%userprofile%\AppData\Local\Temp\Azurite"
cd /d %folder%
for /F "delims=" %%i in ('dir /b') do (rmdir "%%i" /s/q || del "%%i" /s/q)