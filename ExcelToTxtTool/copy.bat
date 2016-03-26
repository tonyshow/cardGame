echo off
color 02


::copy .cs文件======================================================================================================================================
if exist out\Script\CardDetail.cs   (xcopy  /y   out\Script\CardDetail.cs  ..\Assets\Resources\Script\Configuration\) 

::copy .txt文件======================================================================================================================================
if exist out\configs\Card.txt      (xcopy  /y   out\configs\Card.txt     ..\Assets\Resources\configs\)

pause