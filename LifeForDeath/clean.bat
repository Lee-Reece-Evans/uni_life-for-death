@echo off
echo %cd%
echo "this will delete some files within the directory above!  Make sure unity is not running!"
pause
echo "are you sure you would like to do this?"
pause
rd /s /q Library
rd /s /q Temp
del /s /q /f *.csproj
del /s /q /f *.pidb
del /s /q /f *.unityproj
del /s /q /f *.DS_Store
del /s /q /f *.sln
del /s /q /f *.userprefs
rd /s /q .vs
rd /s /q obj
echo "done."
pause