/****************************************************************************
 *
 * Copyright © 2005-2006 OCP Software, Inc.
 *
 * All Rights Reserved.
 *
 ***************************************************************************/

/////////////////////////////////////////////////////////////////////////////
// CeCabManager
/////////////////////////////////////////////////////////////////////////////

// CmDocumentType -----------------------------------------------------------
cmDocTypeCeCab = 0; // Creates a Windows CE CAB file document.
cmDocTypeScript = 5; // Creates a script document.

// CmWindowState ------------------------------------------------------------
cmWindowStateNormal = 0; // Restores the normal window state.
cmWindowStateMaximized = 1; // Maximizes the window.
cmWindowStateMinimized = 2; // Minimizes the window.

// CmSaveChanges ------------------------------------------------------------
cmSaveChangesYes = 1; // Saves any changes without prompting the user.
cmSaveChangesNo = 2; // Discards any changes without prompting the user.
cmSaveChangesPrompt = 3; // Prompts the user to determine whether or not to save changes.

// CmSaveResult -------------------------------------------------------------
cmSaveSucceeded = 1; // The save operation succeeded.
cmSaveCanceled = 2; // The user cancelled the save operation.

// CmMenuPosition -----------------------------------------------------------
cmImportMenu = 1; // Adds commands to the File/Import menu.
cmExportMenu = 2; // Adds commands to the File/Export menu.
cmToolsMenu = 3; // Adds commands to the Tools/Add-Ins menu.
cmHelpMenu = 4; // Adds commands to the Help/Add-Ins menu.


/////////////////////////////////////////////////////////////////////////////
// CeCabinet
/////////////////////////////////////////////////////////////////////////////

// CeCabType ----------------------------------------------------------------
ctOriginal = 0; // The original Windows CE CAB file format.
ctSmartPhone = 1; // XML-based CAB file format used with SmartPhone devices.
ctPpc2003 = 2; // CAB file format specific to Pocket PC 2003 devices.
ctUnified = 3; // New unified CAB format that combines the functionality of all previous formats.

// CeCabLimits --------------------------------------------------------------
ceMaxAppName = 40; // Maximum length of application name in characters.
ceMaxProvider = 30; // Maximum length of provider name in characters.

// CeFileCopyFlags ----------------------------------------------------------
cfNormal = 0; // Perform default file copying.
cfWarnIfSkip = 1; // Display a warning if the user tries to skip a file after an error has occurred.
cfNoSkip = 2; // Do not allow the user to skip copying the file.
cfNoOverwrite = 16; // Do not overwrite an existing file in the destination directory.
cfReplaceOnly = 1024; // Copy the source file to the destination directory only if the file is already in the destination directory.
cfSelfRegister = 268435456; // The file exports the DllRegisterServer and DllUnregisterServer COM functions.
cfNoDateDialog = 536870912; // Do not copy files if the target file is newer.
cfNoDateCheck = 1073741824; // Ignore date while overwriting the target file.
cfShared = 2147483648; // Create a reference when a shared DLL is counted.

// CeFileAttributes ---------------------------------------------------------
faNormal = 0; // Normal. File can be read or written to without restriction.
faReadOnly = 1; // Read-only. File cannot be opened for writing, and a file with the same name cannot be created.
faHidden = 2; // Hidden file.
faSystem = 4; // System file.
faArchive = 32; // Archive. Set whenever the file is changed.

// CeShortcutType -----------------------------------------------------------
scFolder = 0; // Shortcut to a folder.
scFile = 1; // Shortcut to a file.

// CeRegRoot ----------------------------------------------------------------
rkHKCR = 1; // HKEY_CLASSES_ROOT
rkHKCU = 2; // HKEY_CURRENT_USER
rkHKLM = 3; // HKEY_LOCAL_MACHINE
rkHKU = 4; // HKEY_USERS

// CeRegValType -------------------------------------------------------------
rvString = 0; // REG_SZ
rvBinary = 1; // REG_BINARY
rvMultiString = 65536; // REG_MULTI_SZ
rvNumber = 65537; // REG_DWORD

// PropChangeType -----------------------------------------------------------
pcName = 0; // Indicates the name of a file or shortcut has changed.
pcLocation = 1; // Indicates the location of a file or shortcut has changed.
pcAttributes = 2; // Indicates the attributes of a file, Setup DLL or shortcut has changed.
pcCopyFlags = 3; // Indicates a file//s copy flags have changed.
pcIndex = 4; // Indicates a file//s index has changed.
pcIcon = 5; // Indicates the icon associated with a file has changed.
pcTarget = 6; // Indicates the target of a shortcut has changed.
pcKeyRoot = 7; // Indicates a registry entry//s key root has changed.
pcKeyName = 8; // Indicates a registry entry//s key name has changed.
pcValueName = 9; // Indicates a registry entry//s value name has changed.
pcValueData = 10; // Indicates a registry entry//s value data has changed.
pcAllowOverwrite = 11; // Indicates a registry entry//s overwrite flag has changed.
