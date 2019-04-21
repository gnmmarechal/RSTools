@echo off
echo Copying all plugins:
copy /y "ChatBoxLootAlarm\bin\Debug\ChatBoxLootAlarm.dll" "RS Tools\bin\Debug\Plugins"
copy /y "SlayerTaskCompletionAlarm\bin\Debug\SlayerTaskCompletionAlarm.dll" "RS Tools\bin\Debug\Plugins"
copy /y "LowHealthAlarm\bin\Debug\LowHealthAlarm.dll" "RS Tools\bin\Debug\Plugins"
copy /y "LobbyPause\bin\Debug\LobbyPause.dll" "RS Tools\bin\Debug\Plugins"
copy /y "EmptyThroneRoomDivinationAlarm\bin\Debug\EmptyThroneRoomDivinationAlarm.dll" "RS Tools\bin\Debug\Plugins"
copy /y "EmptyInventoryAlert\bin\Debug\EmptyInventoryAlert.dll" "RS Tools\bin\Debug\Plugins"
::copy /y "OverlayTestPlugin\bin\Debug\OverlayTestPlugin.dll" "RS Tools\bin\Debug\Plugins"
pause