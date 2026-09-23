@echo off
dir /s /b src\main\java\*.java > sources.txt
"C:\Program Files\Common Files\Oracle\Java\javapath\javac.exe" -encoding UTF-8 -cp "target/classes;target/htth-project-Truongbk-1.0-jar-with-dependencies.jar;lib/*" -d target/classes @sources.txt
if %errorlevel% neq 0 (
    echo Compilation Failed
    exit /b %errorlevel%
)
"C:\Program Files\Android\Android Studio\jbr\bin\jar.exe" uf target/htth-project-Truongbk-1.0-jar-with-dependencies.jar -C target/classes .
del sources.txt
echo BUILD_OK
