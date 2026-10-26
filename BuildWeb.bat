@echo off
rem Builds the web version into docs\ (GitHub Pages serves it). Close the Unity editor first.
cd /d "%~dp0"
echo Building the web version... (this can take a few minutes)
"C:\Program Files\Unity\Hub\Editor\6000.6.7f1\Editor\Unity.exe" -batchmode -quit -projectPath "%~dp0." -buildTarget WebGL -executeMethod PhonicsIsland.BuildGame.BuildWeb -logFile "%~dp0Logs\buildweb.log"
if errorlevel 1 (
    echo Build failed. See Logs\buildweb.log
    pause
    exit /b 1
)
echo Done. The web version is in docs\
