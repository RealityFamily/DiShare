(	
	local HamsterRootFolder
	try(
		local key
		registry.openKey HKEY_CURRENT_USER ("Software\\3D Hamster") accessRights:#readOnly key:&key
		registry.queryValue key "InstallPath" value:&HamsterRootFolder expand:true	
		registry.closeKey key
	)catch()
	
	if HamsterRootFolder != undefined then (
		local HamsterScript =  (HamsterRootFolder + "\Scripts\3dhamster.ms")
		if doesFileExist HamsterScript then filein HamsterScript
	)
)