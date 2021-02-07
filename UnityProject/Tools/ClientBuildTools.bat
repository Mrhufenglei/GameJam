cd Tools
java -Dfile.encoding=UTF-8 -jar ExcelClientBuildTool.jar .\xlsx true
pause
xcopy export\datas ..\Assets\Resources\LocalModel/i/e/y
pause
xcopy export\LocalModels ..\Assets\Script\Logic\LocalModels/i/e/y
pause