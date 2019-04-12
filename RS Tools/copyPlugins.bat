@echo off
echo Copying all plugins:
copy /y "ChatBoxLootAlarm\bin\Debug\ChatBoxLootAlarm.dll" "RS Tools\bin\Debug\Plugins"
copy /y "SlayerTaskCompletionAlarm\bin\Debug\SlayerTaskCompletionAlarm.dll" "RS Tools\bin\Debug\Plugins"
copy /y "LowHealthAlarm\bin\Debug\LowHealthAlarm.dll" "RS Tools\bin\Debug\Plugins"
pause