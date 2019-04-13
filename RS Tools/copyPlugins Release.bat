@echo off
echo Copying all plugins:
copy /y "ChatBoxLootAlarm\bin\Release\ChatBoxLootAlarm.dll" "RS Tools\bin\Release\Plugins"
copy /y "SlayerTaskCompletionAlarm\bin\Release\SlayerTaskCompletionAlarm.dll" "RS Tools\bin\Release\Plugins"
copy /y "LowHealthAlarm\bin\Release\LowHealthAlarm.dll" "RS Tools\bin\Release\Plugins"
copy /y "LobbyPause\bin\Release\LobbyPause.dll" "RS Tools\bin\Release\Plugins"
copy /y "EmptyThroneRoomDivinationAlarm\bin\Release\EmptyThroneRoomDivinationAlarm.dll" "RS Tools\bin\Release\Plugins"
copy /y "EmptyInventoryAlert\bin\Release\EmptyInventoryAlert.dll" "RS Tools\bin\Release\Plugins"
pause