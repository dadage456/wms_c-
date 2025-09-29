'*****************************************************************************
'*
'* Copyright © 2005-2006 OCP Software, Inc.
'*
'* All Rights Reserved.
'*
'*****************************************************************************

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' CeCabManager
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' CmDocumentType -------------------------------------------------------------
const cmDocTypeCeCab = 0 ' Creates a Windows CE CAB file document.
const cmDocTypeScript = 5 ' Creates a script document.

' CmWindowState --------------------------------------------------------------
const cmWindowStateNormal = 0 ' Restores the normal window state.
const cmWindowStateMaximized = 1 ' Maximizes the window.
const cmWindowStateMinimized = 2 ' Minimizes the window.

' CmSaveChanges --------------------------------------------------------------
const cmSaveChangesYes = 1 ' Saves any changes without prompting the user.
const cmSaveChangesNo = 2 ' Discards any changes without prompting the user.
const cmSaveChangesPrompt = 3 ' Prompts the user to determine whether or not to save changes.

' CmSaveResult ---------------------------------------------------------------
const cmSaveSucceeded = 1 ' The save operation succeeded.
const cmSaveCanceled = 2 ' The user cancelled the save operation.

' CmMenuPosition -------------------------------------------------------------
const cmImportMenu = 1 ' Adds commands to the File/Import menu.
const cmExportMenu = 2 ' Adds commands to the File/Export menu.
const cmToolsMenu = 3 ' Adds commands to the Tools/Add-Ins menu.
const cmHelpMenu = 4 ' Adds commands to the Help/Add-Ins menu.


''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' CeCabinet
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' CeCabType ------------------------------------------------------------------
const ctOriginal = 0 ' The original Windows CE CAB file format.
const ctSmartPhone = 1 ' XML-based CAB file format used with SmartPhone devices.
const ctPpc2003 = 2 ' CAB file format specific to Pocket PC 2003 devices.
const ctUnified = 3 ' New unified CAB format that combines the functionality of all previous formats.

' CeCabLimits ----------------------------------------------------------------
const ceMaxAppName = 40 ' Maximum length of application name in characters.
const ceMaxProvider = 30 ' Maximum length of provider name in characters.

' CeFileCopyFlags ------------------------------------------------------------
const cfNormal = 0 ' Perform default file copying.
const cfWarnIfSkip = 1 ' Display a warning if the user tries to skip a file after an error has occurred.
const cfNoSkip = 2 ' Do not allow the user to skip copying the file.
const cfNoOverwrite = 16 ' Do not overwrite an existing file in the destination directory.
const cfReplaceOnly = 1024 ' Copy the source file to the destination directory only if the file is already in the destination directory.
const cfSelfRegister = 268435456 ' The file exports the DllRegisterServer and DllUnregisterServer COM functions.
const cfNoDateDialog = 536870912 ' Do not copy files if the target file is newer.
const cfNoDateCheck = 1073741824 ' Ignore date while overwriting the target file.
const cfShared = 2147483648 ' Create a reference when a shared DLL is counted.

' CeFileAttributes -----------------------------------------------------------
const faNormal = 0 ' Normal. File can be read or written to without restriction.
const faReadOnly = 1 ' Read-only. File cannot be opened for writing, and a file with the same name cannot be created.
const faHidden = 2 ' Hidden file.
const faSystem = 4 ' System file.
const faArchive = 32 ' Archive. Set whenever the file is changed.

' CeShortcutType -------------------------------------------------------------
const scFolder = 0 ' Shortcut to a folder.
const scFile = 1 ' Shortcut to a file.

' CeRegRoot ------------------------------------------------------------------
const rkHKCR = 1 ' HKEY_CLASSES_ROOT
const rkHKCU = 2 ' HKEY_CURRENT_USER
const rkHKLM = 3 ' HKEY_LOCAL_MACHINE
const rkHKU = 4 ' HKEY_USERS

' CeRegValType ---------------------------------------------------------------
const rvString = 0 ' REG_SZ
const rvBinary = 1 ' REG_BINARY
const rvMultiString = 65536 ' REG_MULTI_SZ
const rvNumber = 65537 ' REG_DWORD

' PropChangeType -------------------------------------------------------------
const pcName = 0 ' Indicates the name of a file or shortcut has changed.
const pcLocation = 1 ' Indicates the location of a file or shortcut has changed.
const pcAttributes = 2 ' Indicates the attributes of a file, Setup DLL or shortcut has changed.
const pcCopyFlags = 3 ' Indicates a file's copy flags have changed.
const pcIndex = 4 ' Indicates a file's index has changed.
const pcIcon = 5 ' Indicates the icon associated with a file has changed.
const pcTarget = 6 ' Indicates the target of a shortcut has changed.
const pcKeyRoot = 7 ' Indicates a registry entry's key root has changed.
const pcKeyName = 8 ' Indicates a registry entry's key name has changed.
const pcValueName = 9 ' Indicates a registry entry's value name has changed.
const pcValueData = 10 ' Indicates a registry entry's value data has changed.
const pcAllowOverwrite = 11 ' Indicates a registry entry's overwrite flag has changed.
