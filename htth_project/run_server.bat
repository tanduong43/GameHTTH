@echo off
title HTTH Game Server
chcp 65001 >nul
color 0A
cd /d "%~dp0"

echo ===================================================
echo [1/3] Dang tat cac tien trinh Java Server cu...
echo ===================================================
taskkill /F /IM java.exe 2>nul
ping -n 2 127.0.0.1 >nul

echo.
echo ===================================================
echo [2/3] Dang bien dich va cap nhat file JAR...
echo ===================================================
if not exist "target\classes" mkdir "target\classes"

javac -encoding UTF-8 -cp "target\htth-project-Truongbk-1.0-jar-with-dependencies.jar" -d "target\classes" src\main\java\core\*.java src\main\java\client\*.java src\main\java\map\*.java src\main\java\template\*.java src\main\java\database\*.java src\main\java\io\*.java src\main\java\event\*.java src\main\java\activities\*.java 2>nul

if errorlevel 1 (
    echo Bien dich bang powershell...
    powershell -Command "javac -encoding UTF-8 -cp 'target\htth-project-Truongbk-1.0-jar-with-dependencies.jar' -d target\classes (Get-ChildItem -Path 'src\main\java' -Filter '*.java' -Recurse | Select-Object -ExpandProperty FullName)"
)

jar uf "target\htth-project-Truongbk-1.0-jar-with-dependencies.jar" -C target\classes .
echo Cap nhat file JAR thanh cong!

:run_loop
echo.
echo ===================================================
echo [3/3] Dang khoi dong HTTH Server...
echo ===================================================
java -Xms512m -Xmx2048m -Dfile.encoding=UTF-8 -jar target\htth-project-Truongbk-1.0-jar-with-dependencies.jar

echo.
echo ===================================================
echo [!] Server da dong (Bao tri hoac tat tien trinh).
echo Tu dong khoi dong lai sau 5 giay... (Nhan Ctrl+C de thoat han)
echo ===================================================
ping -n 6 127.0.0.1 >nul
goto run_loop
