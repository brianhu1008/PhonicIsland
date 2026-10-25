@echo off
cd /d "%~dp0Build\PhonicsIsland"
if not exist PhonicsIsland.exe (
    echo PhonicsIsland.exe not found. Build it first from Unity: Phonics Island ^> Build Windows EXE
    pause
    exit /b 1
)
start "" PhonicsIsland.exe
