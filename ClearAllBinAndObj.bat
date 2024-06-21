@echo off
setlocal

:: Find and delete all 'bin' directories
for /d /r %%d in (bin) do (
    if exist "%%d" (
        echo Deleting directory: %%d
        rd /s /q "%%d"
    )
)

:: Find and delete all 'obj' directories
for /d /r %%d in (obj) do (
    if exist "%%d" (
        echo Deleting directory: %%d
        rd /s /q "%%d"
    )
)

echo All 'bin' and 'obj' directories have been deleted.
endlocal
pause
