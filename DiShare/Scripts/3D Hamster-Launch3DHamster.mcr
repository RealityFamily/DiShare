macroScript Launch3DHamster
category:"3D Hamster"
toolTip:"Launch 3D Hamster"
buttontext:"3D Hamster"
icon: #("3DHamster",1)
(
	local HamsterRootFolder
	local message = "Не могу найти 3D Hamster.\nПопробуйте переустановить программу."
	try(
		local key
		registry.openKey HKEY_CURRENT_USER ("Software\\3D Hamster") accessRights:#readOnly key:&key			
		registry.queryValue key "InstallPath" value:&HamsterRootFolder expand:true	
		registry.closeKey key
	)catch( messageBox ("Ошибка запуска 3D Hamster.\n" + (registry.getLastError()) as string))

	if HamsterRootFolder != undefined then (
		local hamster = (HamsterRootFolder + @"\Library.exe")

		if doesFileExist hamster then (ShellLaunch hamster "")
		else messagebox message
	)
	else messagebox message
)