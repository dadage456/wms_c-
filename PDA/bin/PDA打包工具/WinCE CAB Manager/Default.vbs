'****************************************************************************
'*
'* This is the default VBScript file for use with WinCE CAB Manager - modify
'* as needed.  When you are finished, click 'Script / Load' on the WinCE CAB
'* Manager application menu to load and run the script.
'*
'****************************************************************************

' Include Scripts -----------------------------------------------------------
IncludeScript "CeCabMgr_Types.vbs"


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Implementation
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' TODO: Add Your Code Here!
MsgBox "Script is loaded and events are running!"


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Application Events
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' BeforeApplicationShutDown -------------------------------------------------
'
' Description: Occurs before the application shuts down.
'
' Note: This event is not actually valid in scripts, because the script
' document will be closed before this event occurs.  If you require this
' event, you should consider implementing your code in a COM Add-In instead
' of a script.
'
' Parameters: None
'
Sub Application_BeforeApplicationShutDown()
	' TODO: Add your implementation here
End Sub


' DocumentOpen --------------------------------------------------------------
'
' Description: Occurs after a docuement is opened.
'
' Parameters:
'	oDocument As CeCabManagerLib.Document
'
Sub Application_DocumentOpen( oDocument )
	' TODO: Add your implementation here
End Sub


' BeforeDocumentClose -------------------------------------------------------
'
' Description: Occurs before a document is closed.
'
' Parameters:
'	oDocument As CeCabManagerLib.Document
'
Sub Application_BeforeDocumentClose( oDocument )
	' TODO: Add your implementation here
End Sub


' DocumentSave --------------------------------------------------------------
'
' Description: Occurs after a document has been saved.
'
' Parameters:
'	oDocument As CeCabManagerLib.Document
'
Sub Application_DocumentSave( oDocument )
	' TODO: Add your implementation here
End Sub


' NewDocument ---------------------------------------------------------------
'
' Description: Occurs after a new document has been created.
'
' Parameters:
'	oDocument As CeCabManagerLib.Document
'
Sub Application_NewDocument( oDocument )
	' TODO: Add your implementation here
End Sub


' WindowActivate ------------------------------------------------------------
'
' Description: Occurs after a window is activated.
'
' Parameters:
'	oWindow As CeCabManagerLib.Window
'
Sub Application_WindowActivate( oWindow )
	' TODO: Add your implementation here
End Sub


' WindowDeactivate ----------------------------------------------------------
'
' Description: Occurs after a window is deactivated.
'
' Parameters:
'	oWindow As CeCabManagerLib.Window
'
Sub Application_WindowDeactivate( oWindow )
	' TODO: Add your implementation here
End Sub


' FilesDropped --------------------------------------------------------------
'
' Description: Occurs when files are dropped onto the application window.
'
' Parameters:
'	oDocument As CeCabManagerLib.Document
'	oFilePaths As CeCabManagerLib.Paths
'	bHandled As Boolean
'
Sub Application_FilesDropped( oDocument, oFilePaths, ByRef bHandled )
	' TODO: Add your implementation here
End Sub


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' CeCabinetMgr Events
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' ProviderChanged -----------------------------------------------------------
'
' Description: Occurs when the provider name is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_ProviderChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' AppNameChanged ------------------------------------------------------------
'
' Description: Occurs when the application name is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_AppNameChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' ProcessorTypeChanged ------------------------------------------------------
'
' Description: Occurs when the processor type supported is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_ProcessorTypeChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' InstallDirChanged ---------------------------------------------------------
'
' Description: Occurs when the default installation directory is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_InstallDirChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' UnsupportedPlatformsChanged -----------------------------------------------
'
' Description: Occurs when the list of unsupported platforms is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_UnsupportedPlatformsChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' VersionMinChanged ---------------------------------------------------------
'
' Description: Occurs when the minimum OS version required by the application
'              is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_VersionMinChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' VersionMaxChanged ---------------------------------------------------------
'
' Description: Occurs when the maximum OS version required by the application
'              is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_VersionMaxChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' BuildMinChanged -----------------------------------------------------------
'
' Description: Occurs when the minimum OS build required by the application
'              is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_BuildMinChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' BuildMaxChanged -----------------------------------------------------------
'
' Description: Occurs when the maximum OS build required by the application
'              is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_BuildMaxChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' SetupDllAdded -------------------------------------------------------------
'
' Description: Occurs after a Setup DLL is added to the cabinet.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_SetupDllAdded( oCabinet )
	' TODO: Add your implementation here
End Sub


' SetupDllExtracted ---------------------------------------------------------
'
' Description: Occurs after the Setup DLL has been extracted.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_SetupDllExtracted( oCabinet, Path )
	' TODO: Add your implementation here
End Sub


' BeforeSetupDllRemove ------------------------------------------------------
'
' Description: Occurs before a Setup DLL is removed from the cabinet.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_BeforeSetupDllRemove( oCabinet )
	' TODO: Add your implementation here
End Sub


' FileAdded -----------------------------------------------------------------
'
' Description: Occurs after a file is added to the cabinet.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oFile As CeCabinetLib.CeFile
'
Sub CeCabinetMgr_FileAdded( oCabinet, oFile )
	' TODO: Add your implementation here
End Sub


' FileChanged ---------------------------------------------------------------
'
' Description: Occurs when a file's properties are changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oFile As CeCabinetLib.CeFile
'	Item As CeCabinetLib.PropChangeType
'
Sub CeCabinetMgr_FileChanged( oCabinet, oFile, Item )
	' TODO: Add your implementation here
End Sub


' FileExtracted -------------------------------------------------------------
'
' Description: Occurs after a file has been extracted.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oFile As CeCabinetLib.CeFile
'	sPath As String
'
Sub CeCabinetMgr_FileExtracted( oCabinet, oFile, sPath )
	' TODO: Add your implementation here
End Sub


' BeforeFileRemove ----------------------------------------------------------
'
' Description: Occurs just before a file is removed from the cabinet.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oFile As CeCabinetLib.CeFile
'
Sub CeCabinetMgr_BeforeFileRemove( oCabinet, oFile )
	' TODO: Add your implementation here
End Sub


' ShortcutAdded -------------------------------------------------------------
'
' Description: Occurs after a shortcut is added to the cabinet.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oShortcut As CeCabinetLib.CeShortcut
'
Sub CeCabinetMgr_ShortcutAdded( oCabinet, oShortcut )
	' TODO: Add your implementation here
End Sub


' ShortcutChanged -----------------------------------------------------------
'
' Description: Occurs when a shortcut's properties are changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oShortcut As CeCabinetLib.CeShortcut
'	Item As CeCabinetLib.PropChangeType
'
Sub CeCabinetMgr_ShortcutChanged( oCabinet, oShortcut, Item )
	' TODO: Add your implementation here
End Sub


' BeforeShortcutRemove ------------------------------------------------------
'
' Description: Occurs just before a shortcut is removed from the cabinet.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oShortcut As CeCabinetLib.CeShortcut
'
Sub CeCabinetMgr_BeforeShortcutRemove( oCabinet, oShortcut )
	' TODO: Add your implementation here
End Sub


' RegEntryAdded -------------------------------------------------------------
'
' Description: Occurs after a registry entry is added to the cabinet.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oRegEntry As CeCabinetLib.CeRegEntry
'
Sub CeCabinetMgr_RegEntryAdded( oCabinet, oRegEntry )
	' TODO: Add your implementation here
End Sub


' RegEntryChanged -----------------------------------------------------------
'
' Description: Occurs when a registry entry's properties are changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oRegEntry As CeCabinetLib.CeRegEntry
'	Item As CeCabinetLib.PropChangeType
'
Sub CeCabinetMgr_RegEntryChanged( oCabinet, oRegEntry, Item )
	' TODO: Add your implementation here
End Sub


' BeforeRegEntryRemove ------------------------------------------------------
'
' Description: Occurs just before a registry entry is removed from the 
'              cabinet.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oRegEntry As CeCabinetLib.CeRegEntry
'
Sub CeCabinetMgr_BeforeRegEntryRemove( oCabinet, oRegEntry )
	' TODO: Add your implementation here
End Sub


' ReadOnlyChanged -----------------------------------------------------------
'
' Description: Occurs when the Read Only property is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_ReadOnlyChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' Saved ---------------------------------------------------------------------
'
' Description: Occurs after a cabinet file has been saved.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_Saved( oCabinet )
	' TODO: Add your implementation here
End Sub


' SetupDllRemoved -----------------------------------------------------------
'
' Description: Occurs after a Setup DLL is removed from the cabinet.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_SetupDllRemoved( oCabinet )
	' TODO: Add your implementation here
End Sub


' SetupDllChanged -----------------------------------------------------------
'
' Description: Occurs when the Setup DLL's properties are changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	Item As CeCabinetLib.PropChangeType
'
Sub CeCabinetMgr_SetupDllChanged( oCabinet, Item )
	' TODO: Add your implementation here
End Sub


' FileRemoved ---------------------------------------------------------------
'
' Description: Occurs after a file has been removed from the cabinet, but
'              before the object is released.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oFile As CeCabinetLib.CeFile
'
Sub CeCabinetMgr_FileRemoved( oCabinet, oFile )
	' TODO: Add your implementation here
End Sub


' ShortcutRemoved -----------------------------------------------------------
'
' Description: Occurs after a shortcut has been removed from the cabinet, but
'              before the object is released.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oShortcut As CeCabinetLib.CeShortcut
'
Sub CeCabinetMgr_ShortcutRemoved( oCabinet, oShortcut )
	' TODO: Add your implementation here
End Sub


' RegEntryRemoved -----------------------------------------------------------
'
' Description: Occurs after a registry entry has been removed from the
'              cabinet, but before the object is released.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'	oRegEntry As CeCabinetLib.CeRegEntry
'
Sub CeCabinetMgr_RegEntryRemoved( oCabinet, oRegEntry )
	' TODO: Add your implementation here
End Sub


' AllowUninstallChanged -----------------------------------------------------
'
' Description: Occurs when the AllowUninstall property of a SmartPhone CAB
'              file is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_AllowUninstallChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' PreXMLChanged -------------------------------------------------------------
'
' Description: Occurs when the PreXML property of a SmartPhone CAB file is
'              changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_PreXMLChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' PostXMLChanged ------------------------------------------------------------
'
' Description: Occurs when the PostXML property of a SmartPhone CAB file is
'              changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_PostXMLChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' PlatformMinChanged --------------------------------------------------------
'
' Description: Occurs when the PlatformMin property of a Pocket PC 2003 CAB
'              file is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_PlatformMinChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' PlatformMaxChanged --------------------------------------------------------
'
' Description: Occurs when the PlatformMax property of a Pocket PC 2003 CAB
'              file is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_PlatformMaxChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' PlatformNameChanged -------------------------------------------------------
'
' Description: Occurs when the PlatformName property of a Pocket PC 2003 CAB
'              file is changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_PlatformNameChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' CompressionLevelChanged ---------------------------------------------------
'
' Description: Occurs when the CompressionLevel property of a CAB file is
'              changed.
'
' Parameters:
'	oCabinet As CeCabinetLib.Cabinet
'
Sub CeCabinetMgr_CompressionLevelChanged( oCabinet )
	' TODO: Add your implementation here
End Sub


' SaveProgress --------------------------------------------------------------
'
' Description: Occurs repeatedly while a file is being saved.
'
' Parameters:
'	oCabinet As CeCabinetLib.CeCabinet
'	PercentSaved As Long
'
Sub CeCabinetMgr_SaveProgress( oCabinet, PercentSaved )
	' TODO: Add your implementation here
End Sub
