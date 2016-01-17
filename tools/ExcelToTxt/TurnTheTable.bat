echo off
TurnTheTable.py 

echo.
echo 将生成的txt文件copy到游戏工程中
xcopy /e/a/y   out   ..\..\Assets\Resources\Data\ 

::打开目标目录
::start ..\..\Assets\Resources\Data\

pause