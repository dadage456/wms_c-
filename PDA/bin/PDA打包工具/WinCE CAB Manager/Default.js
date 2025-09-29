/****************************************************************************
 *
 * This is the default JScript file for use with WinCE CAB Manager - modify
 * as needed.  When you are finished, click 'Script / Load' on the WinCE CAB
 * Manager application menu to load and run the script.
 *
 ***************************************************************************/

// Include Scripts ----------------------------------------------------------
IncludeScript( "CeCabMgr_Types.js" );


/////////////////////////////////////////////////////////////////////////////
// Implementation
/////////////////////////////////////////////////////////////////////////////

// TODO: Add Your Code Here!
alert( "Script is loaded and events are running!" );


/////////////////////////////////////////////////////////////////////////////
// Application Events
/////////////////////////////////////////////////////////////////////////////

// BeforeApplicationShutDown ------------------------------------------------
//
// Description: Occurs before the application shuts down.
//
// Note: This event is not actually valid in scripts, because the script
// document will be closed before this event occurs.  If you require this
// event, you should consider implementing your code in a COM Add-In instead
// of a script.
//
// Parameters: None
//
function Application::BeforeApplicationShutDown()
{
}


// DocumentOpen --------------------------------------------------------------
//
// Description: Occurs after a docuement is opened.
//
// Parameters:
//	oDocument As CeCabManagerLib.Document
//
function Application::DocumentOpen( oDocument )
{
}


// BeforeDocumentClose -------------------------------------------------------
//
// Description: Occurs before a document is closed.
//
// Parameters:
//	oDocument As CeCabManagerLib.Document
//
function Application::BeforeDocumentClose( oDocument )
{
}


// DocumentSave --------------------------------------------------------------
//
// Description: Occurs after a document has been saved.
//
// Parameters:
//	oDocument As CeCabManagerLib.Document
//
function Application::DocumentSave( oDocument )
{
}


// NewDocument ---------------------------------------------------------------
//
// Description: Occurs after a new document has been created.
//
// Parameters:
//	oDocument As CeCabManagerLib.Document
//
function Application::NewDocument( oDocument )
{
}


// WindowActivate ------------------------------------------------------------
//
// Description: Occurs after a window is activated.
//
// Parameters:
//	oWindow As CeCabManagerLib.Window
//
function Application::WindowActivate( oWindow )
{
}


// WindowDeactivate ----------------------------------------------------------
//
// Description: Occurs after a window is deactivated.
//
// Parameters:
//	oWindow As CeCabManagerLib.Window
//
function Application::WindowDeactivate( oWindow )
{
}


// FilesDropped --------------------------------------------------------------
//
// Description: Occurs when files are dropped onto the application window.
//
// Parameters:
//	oDocument As CeCabManagerLib.Document
//	oFilePaths As CeCabManagerLib.Paths
//	bHandled As Boolean
//
function Application::FilesDropped( oDocument, oFilePaths, bHandled )
{
}


/////////////////////////////////////////////////////////////////////////////
// CeCabinetMgr Events
/////////////////////////////////////////////////////////////////////////////

// ProviderChanged -----------------------------------------------------------
//
// Description: Occurs when the provider name is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::ProviderChanged( oCabinet )
{
}


// AppNameChanged ------------------------------------------------------------
//
// Description: Occurs when the application name is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::AppNameChanged( oCabinet )
{
}


// ProcessorTypeChanged ------------------------------------------------------
//
// Description: Occurs when the processor type supported is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::ProcessorTypeChanged( oCabinet )
{
}


// InstallDirChanged ---------------------------------------------------------
//
// Description: Occurs when the default installation directory is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::InstallDirChanged( oCabinet )
{
}


// UnsupportedPlatformsChanged -----------------------------------------------
//
// Description: Occurs when the list of unsupported platforms is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::UnsupportedPlatformsChanged( oCabinet )
{
}


// VersionMinChanged ---------------------------------------------------------
//
// Description: Occurs when the minimum OS version required by the application
//              is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::VersionMinChanged( oCabinet )
{
}


// VersionMaxChanged ---------------------------------------------------------
//
// Description: Occurs when the maximum OS version required by the application
//              is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::VersionMaxChanged( oCabinet )
{
}


// BuildMinChanged -----------------------------------------------------------
//
// Description: Occurs when the minimum OS build required by the application
//              is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::BuildMinChanged( oCabinet )
{
}


// BuildMaxChanged -----------------------------------------------------------
//
// Description: Occurs when the maximum OS build required by the application
//              is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::BuildMaxChanged( oCabinet )
{
}


// SetupDllAdded -------------------------------------------------------------
//
// Description: Occurs after a Setup DLL is added to the cabinet.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::SetupDllAdded( oCabinet )
{
}


// SetupDllExtracted ---------------------------------------------------------
//
// Description: Occurs after the Setup DLL has been extracted.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::SetupDllExtracted( oCabinet, Path )
{
}


// BeforeSetupDllRemove ------------------------------------------------------
//
// Description: Occurs before a Setup DLL is removed from the cabinet.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::BeforeSetupDllRemove( oCabinet )
{
}


// FileAdded -----------------------------------------------------------------
//
// Description: Occurs after a file is added to the cabinet.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oFile As CeCabinetLib.CeFile
//
function CeCabinetMgr::FileAdded( oCabinet, oFile )
{
}


// FileChanged ---------------------------------------------------------------
//
// Description: Occurs when a file//s properties are changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oFile As CeCabinetLib.CeFile
//	Item As CeCabinetLib.PropChangeType
//
function CeCabinetMgr::FileChanged( oCabinet, oFile, Item )
{
}


// FileExtracted -------------------------------------------------------------
//
// Description: Occurs after a file has been extracted.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oFile As CeCabinetLib.CeFile
//	sPath As String
//
function CeCabinetMgr::FileExtracted( oCabinet, oFile, sPath )
{
}


// BeforeFileRemove ----------------------------------------------------------
//
// Description: Occurs just before a file is removed from the cabinet.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oFile As CeCabinetLib.CeFile
//
function CeCabinetMgr::BeforeFileRemove( oCabinet, oFile )
{
}


// ShortcutAdded -------------------------------------------------------------
//
// Description: Occurs after a shortcut is added to the cabinet.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oShortcut As CeCabinetLib.CeShortcut
//
function CeCabinetMgr::ShortcutAdded( oCabinet, oShortcut )
{
}


// ShortcutChanged -----------------------------------------------------------
//
// Description: Occurs when a shortcut//s properties are changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oShortcut As CeCabinetLib.CeShortcut
//	Item As CeCabinetLib.PropChangeType
//
function CeCabinetMgr::ShortcutChanged( oCabinet, oShortcut, Item )
{
}


// BeforeShortcutRemove ------------------------------------------------------
//
// Description: Occurs just before a shortcut is removed from the cabinet.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oShortcut As CeCabinetLib.CeShortcut
//
function CeCabinetMgr::BeforeShortcutRemove( oCabinet, oShortcut )
{
}


// RegEntryAdded -------------------------------------------------------------
//
// Description: Occurs after a registry entry is added to the cabinet.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oRegEntry As CeCabinetLib.CeRegEntry
//
function CeCabinetMgr::RegEntryAdded( oCabinet, oRegEntry )
{
}


// RegEntryChanged -----------------------------------------------------------
//
// Description: Occurs when a registry entry//s properties are changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oRegEntry As CeCabinetLib.CeRegEntry
//	Item As CeCabinetLib.PropChangeType
//
function CeCabinetMgr::RegEntryChanged( oCabinet, oRegEntry, Item )
{
}


// BeforeRegEntryRemove ------------------------------------------------------
//
// Description: Occurs just before a registry entry is removed from the 
//              cabinet.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oRegEntry As CeCabinetLib.CeRegEntry
//
function CeCabinetMgr::BeforeRegEntryRemove( oCabinet, oRegEntry )
{
}


// ReadOnlyChanged -----------------------------------------------------------
//
// Description: Occurs when the Read Only property is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::ReadOnlyChanged( oCabinet )
{
}


// Saved ---------------------------------------------------------------------
//
// Description: Occurs after a cabinet file has been saved.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::Saved( oCabinet )
{
}


// SetupDllRemoved -----------------------------------------------------------
//
// Description: Occurs after a Setup DLL is removed from the cabinet.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::SetupDllRemoved( oCabinet )
{
}


// SetupDllChanged -----------------------------------------------------------
//
// Description: Occurs when the Setup DLL//s properties are changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	Item As CeCabinetLib.PropChangeType
//
function CeCabinetMgr::SetupDllChanged( oCabinet, Item )
{
}


// FileRemoved ---------------------------------------------------------------
//
// Description: Occurs after a file has been removed from the cabinet, but
//              before the object is released.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oFile As CeCabinetLib.CeFile
//
function CeCabinetMgr::FileRemoved( oCabinet, oFile )
{
}


// ShortcutRemoved -----------------------------------------------------------
//
// Description: Occurs after a shortcut has been removed from the cabinet, but
//              before the object is released.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oShortcut As CeCabinetLib.CeShortcut
//
function CeCabinetMgr::ShortcutRemoved( oCabinet, oShortcut )
{
}


// RegEntryRemoved -----------------------------------------------------------
//
// Description: Occurs after a registry entry has been removed from the
//              cabinet, but before the object is released.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//	oRegEntry As CeCabinetLib.CeRegEntry
//
function CeCabinetMgr::RegEntryRemoved( oCabinet, oRegEntry )
{
}


// AllowUninstallChanged -----------------------------------------------------
//
// Description: Occurs when the AllowUninstall property of a SmartPhone CAB
//              file is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::AllowUninstallChanged( oCabinet )
{
}


// PreXMLChanged -------------------------------------------------------------
//
// Description: Occurs when the PreXML property of a SmartPhone CAB file is
//              changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::PreXMLChanged( oCabinet )
{
}


// PostXMLChanged ------------------------------------------------------------
//
// Description: Occurs when the PostXML property of a SmartPhone CAB file is
//              changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::PostXMLChanged( oCabinet )
{
}


// PlatformMinChanged --------------------------------------------------------
//
// Description: Occurs when the PlatformMin property of a Pocket PC 2003 CAB
//              file is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::PlatformMinChanged( oCabinet )
{
}


// PlatformMaxChanged --------------------------------------------------------
//
// Description: Occurs when the PlatformMax property of a Pocket PC 2003 CAB
//              file is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::PlatformMaxChanged( oCabinet )
{
}


// PlatformNameChanged -------------------------------------------------------
//
// Description: Occurs when the PlatformName property of a Pocket PC 2003 CAB
//              file is changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::PlatformNameChanged( oCabinet )
{
}


// CompressionLevelChanged ---------------------------------------------------
//
// Description: Occurs when the CompressionLevel property of a CAB file is
//              changed.
//
// Parameters:
//	oCabinet As CeCabinetLib.Cabinet
//
function CeCabinetMgr::CompressionLevelChanged( oCabinet )
{
}


// SaveProgress --------------------------------------------------------------
//
// Description: Occurs repeatedly while a file is being saved.
//
// Parameters:
//	oCabinet As CeCabinetLib.CeCabinet
//	PercentSaved As Long
//
function CeCabinetMgr::SaveProgress( oCabinet, PercentSaved )
{
}
