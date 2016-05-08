;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; NSIS installer script for TrailerBuilder ;
; (http://nsis.sourceforge.net) ;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

!define PRODUCT_NAME "Trailer Builder"
!define PRODUCT_VERSION '0.6'
!define PRODUCT_GROUP "Trailer Builder"
!define PRODUCT_PUBLISHER "Trailer Builder"
!define PRODUCT_WEB_SITE "http://www.essien.de"
!define PRODUCT_DIR_REGKEY "Software\TrailerBuilder"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define PRODUCT_ID "{DCC71D3F-C6EE-441f-82DE-0C2EC41420AF}"
!define BRAND_TXT "Trailer Builder Development Ltd"

!include MUI.nsh
!include Library.nsh
	
;;;;;;;;;;;;;;;;;;;;;;;;;
; General configuration ;
;;;;;;;;;;;;;;;;;;;;;;;;;

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile TrailerBuilder-setup-${PRODUCT_VERSION}.exe
InstallDir "$PROGRAMFILES\TrailerBuilder"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" "Install_Dir"
!ifdef NSIS_LZMA_COMPRESS_WHOLE
SetCompressor lzma
!else
SetCompressor /SOLID lzma
!endif
;ShowInstDetails show
;ShowUnInstDetails show
SetOverwrite ifnewer
CRCCheck on

InstType "Normal"
InstType "Full"

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; NSIS Modern User Interface configuration ;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

; MUI 1.67 compatible ------
  !include "MUI.nsh"

; MUI Settings
  !define MUI_ABORTWARNING
  !define MUI_ICON "setup.ico"
  !define MUI_UNICON "setup.ico"
  !define MUI_COMPONENTSPAGE_SMALLDESC
  !define MUI_HEADERIMAGE
  !define MUI_HEADERIMAGE_BITMAP toplogo.bmp
  !define MUI_HEADERIMAGE_RIGHT
  !define MUI_WELCOMEFINISHPAGE_BITMAP sideimage.bmp

	BrandingText /TRIMRIGHT "${BRAND_TXT}"
	
; Installer pages
  ; Welcome page
    !define MUI_WELCOMEPAGE_TITLE_3LINES
    !insertmacro MUI_PAGE_WELCOME
  ; License page
    !insertmacro MUI_PAGE_LICENSE "License.rtf"
  ; Components page
    !insertmacro MUI_PAGE_COMPONENTS
  ; Directory page
    !insertmacro MUI_PAGE_DIRECTORY
  ; Instfiles page
    !insertmacro MUI_PAGE_INSTFILES
  ; Finish page
    !define MUI_FINISHPAGE_RUN "$INSTDIR\TrailerBuilder.exe"
    !define MUI_FINISHPAGE_SHOWREADME "$INSTDIR\README.txt"
    !define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED
   ; !define MUI_FINISHPAGE_LINK "Visit the TrailerBuilder Website"
   ; !define MUI_FINISHPAGE_LINK_LOCATION "http://www.TrailerBuilder.tv"
    !define MUI_FINISHPAGE_NOREBOOTSUPPORT
    !insertmacro MUI_PAGE_FINISH

; Uninstaller pages
    !insertmacro MUI_UNPAGE_CONFIRM
    !insertmacro MUI_UNPAGE_COMPONENTS
    !insertmacro MUI_UNPAGE_INSTFILES
    !insertmacro MUI_UNPAGE_FINISH

; Language files
  !insertmacro MUI_LANGUAGE "English" # first language is the default language
  !insertmacro MUI_LANGUAGE "French"
  !insertmacro MUI_LANGUAGE "German"
  !insertmacro MUI_LANGUAGE "Spanish"
  !insertmacro MUI_LANGUAGE "SimpChinese"
  !insertmacro MUI_LANGUAGE "TradChinese"
  !insertmacro MUI_LANGUAGE "Japanese"
  !insertmacro MUI_LANGUAGE "Korean"
  !insertmacro MUI_LANGUAGE "Italian"
  !insertmacro MUI_LANGUAGE "Dutch"
  !insertmacro MUI_LANGUAGE "Danish"
  !insertmacro MUI_LANGUAGE "Swedish"
  !insertmacro MUI_LANGUAGE "Norwegian"
  !insertmacro MUI_LANGUAGE "Finnish"
  !insertmacro MUI_LANGUAGE "Greek"
  !insertmacro MUI_LANGUAGE "Russian"
  !insertmacro MUI_LANGUAGE "Portuguese"
  !insertmacro MUI_LANGUAGE "Arabic"

!insertmacro MUI_RESERVEFILE_LANGDLL

; Reserve files
  !insertmacro MUI_RESERVEFILE_INSTALLOPTIONS

; MUI end ------

;;;;;;;;;;;;;;;;;;;
; Extension lists ;
;;;;;;;;;;;;;;;;;;;

!macro MacroOtherExtensions _action
  !insertmacro ${_action} ".videoshow"
!macroend

!macro MacroAllExtensions _action
  !insertmacro MacroOtherExtensions ${_action}
!macroend

;;;;;;;;;;;;;;;;;;;;;;;;;;
; File type associations ;
;;;;;;;;;;;;;;;;;;;;;;;;;;

Function RegisterExtension
  ; back up old value for extension $R0 (eg. ".opt")
  ReadRegStr $1 HKCR "$R0" ""
  StrCmp $1 "" NoBackup
    StrCmp $1 "TrailerBuilder$R0" "NoBackup"
    WriteRegStr HKCR "$R0" "VideoShow.backup" $1
NoBackup:
  WriteRegStr HKCR "$R0" "" "TrailerBuilder$R0"
  ReadRegStr $0 HKCR "TrailerBuilder$R0" ""
  WriteRegStr HKCR "TrailerBuilder$R0" "" "TrailerBuilder media file"
  WriteRegStr HKCR "TrailerBuilder$R0\shell" "" "Play"
  WriteRegStr HKCR "TrailerBuilder$R0\shell\Play\command" "" '"$INSTDIR\TrailerBuilder.exe" "%1"'
  WriteRegStr HKCR "TrailerBuilder$R0\DefaultIcon" "" '"$INSTDIR\TrailerBuilder.exe",0'
FunctionEnd

Function un.RegisterExtension
  ;start of restore script
  ReadRegStr $1 HKCR "$R0" ""
  StrCmp $1 "TrailerBuilder$R0" 0 NoOwn ; only do this if we own it
    ReadRegStr $1 HKCR "$R0" "TrailerBuilder.backup"
    StrCmp $1 "" 0 Restore ; if backup="" then delete the whole key
      DeleteRegKey HKCR "$R0"
    Goto NoOwn
Restore:
      WriteRegStr HKCR "$R0" "" $1
      DeleteRegValue HKCR "$R0" "TrailerBuilder.backup"
NoOwn:
    DeleteRegKey HKCR "TrailerBuilder$R0" ;Delete key with association settings
FunctionEnd

!macro RegisterExtensionSection EXT
  Section /o ${EXT}
    SectionIn 2 3
    Push $R0
    StrCpy $R0 ${EXT}
    Call RegisterExtension
    Pop $R0
  SectionEnd
!macroend

!macro UnRegisterExtensionSection EXT
  Push $R0
  StrCpy $R0 ${EXT}
  Call un.RegisterExtension
  Pop $R0
!macroend

!macro WriteRegStrSupportedTypes EXT
  WriteRegStr HKCR Applications\TrailerBuilder.exe\SupportedTypes ${EXT} ""
!macroend

;;;;;;;;;;;;;;;;;;;;;;;;
; Context menu entries ;
;;;;;;;;;;;;;;;;;;;;;;;;

!macro AddContextMenu EXT
  WriteRegStr HKCR ${EXT}\shell\PlayWithTrailerBuilder "" "Play with TrailerBuilder"
  WriteRegStr HKCR ${EXT}\shell\PlayWithTrailerBuilder\command "" '$INSTDIR\TrailerBuilder "%1"'

  WriteRegStr HKCR ${EXT}\shell\AddToPlaylistTrailerBuilder "" "Add to TrailerBuilder's Playlist"
  WriteRegStr HKCR ${EXT}\shell\AddToPlaylistTrailerBuilder\command "" '$INSTDIR\TrailerBuilder.exe --add "%1"'
!macroend

!macro DeleteContextMenu EXT
  DeleteRegKey HKCR ${EXT}\shell\PlayWithTrailerBuilder
  DeleteRegKey HKCR ${EXT}\shell\AddToPlaylistTrailerBuilder
!macroend

;;;;;;;;;;;;;;;;;;;;;;;;;;
; Delete prefs and cache ;
;;;;;;;;;;;;;;;;;;;;;;;;;;

!macro delprefs
  SectionIn 2 3
  StrCpy $0 0
  !define Index 'Line${__LINE__}'
  "${Index}-Loop:"
  ; FIXME
  ; this will loop through all the logged users and "virtual" windows users
  ; (it looks like users are only present in HKEY_USERS when they are logged in)
    ClearErrors
    EnumRegKey $1 HKU "" $0
    StrCmp $1 "" "${Index}-End"
    IntOp $0 $0 + 1
    ReadRegStr $2 HKU "$1\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders" AppData
    StrCmp $2 "" "${Index}-Loop"
    RMDir /r "$2\TrailerBuilder"
    Goto "${Index}-Loop"
  "${Index}-End:"
  !undef Index
!macroend

;;;;;;;;;;;;;;;;;;;;;;
; Installer sections ;
;;;;;;;;;;;;;;;;;;;;;;

Section "Main (required)" SEC01
  SectionIn 1 2 3 RO
  SetShellVarContext all
  SetOutPath "$INSTDIR"

  File  "..\bin\Release\TrailerBuilder.exe"
  File  "..\bin\Release\AxInterop.VIDEOEDITORLib.dll"
  File  "..\bin\Release\Interop.VIDEOEDITORLib.dll"
  File  *.txt
  File  *.rtf
	
#	!insertmacro InstallLib REGDLL SHARED REBOOT_NOTPROTECTED "deps\msvbvm60.dll" "$SYSDIR\msvbvm60.dll" "$SYSDIR"

  File "..\..\VideoEditor\ReleaseMinDependency\VideoEditor.dll"
  File "Deps\vcredist_x86.exe"

SectionEnd

Section "Start Menu Shortcut" SEC02a
  SectionIn 1 2 3
  CreateDirectory "$SMPROGRAMS\Trailer Builder"
  CreateShortCut "$SMPROGRAMS\Trailer Builder\Start Trailer Builder.lnk" \
    "$INSTDIR\TrailerBuilder.exe"		
SectionEnd

Section "Desktop Shortcut" SEC02b
  SectionIn 1 2 3
  CreateShortCut "$DESKTOP\Trailer Builder.lnk" \
    "$INSTDIR\TrailerBuilder.exe"
SectionEnd


; Installer section descriptions
!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC01} \
    "TrailerBuilder Television"
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC02a} \
    "Adds icons to your start menu for easy access"
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC02b} \
    "Adds icon to your desktop for easy access"
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC05} \
    "Add context menu items ('Play With TrailerBuilder' and 'Add To TrailerBuilder's Playlist')"
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC06} \
    "Sets TrailerBuilder as the default application for the specified file type"
!insertmacro MUI_FUNCTION_DESCRIPTION_END

Function .onInit
  ReadRegStr $R0  ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" \
  "UninstallString"
  StrCmp $R0 "" done
 
  MessageBox MB_YESNO|MB_ICONEXCLAMATION \
  "TrailerBuilder has already been installed. $\nDo you want to remove \
  the previous version before installing $(^Name) ?" \
  IDNO done
  
  ;Run the uninstaller
  ;uninst:
    ClearErrors
    ExecWait '$R0 _?=$INSTDIR' ;Do not copy the uninstaller to a temp file
  done:
  !insertmacro MUI_LANGDLL_DISPLAY
FunctionEnd

Section -Post
	
  ExecWait '"$INSTDIR\vcredist_x86.exe" /q:a /c:"msiexec /i vcredist.msi /qn /l*v %temp%\vcredist_x86.log'

  RegDLL "$INSTDIR\VideoEditor.dll"

  WriteUninstaller "$INSTDIR\uninstall.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "InstallDir" $INSTDIR
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "Version" "${VERSION}"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\TrailerBuilder.exe"

  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" \
    "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" \
    "UninstallString" "$INSTDIR\uninstall.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" \
    "DisplayIcon" "$INSTDIR\VideoShow.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" \
    "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" \
    "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" \
    "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

;;;;;;;;;;;;;;;;;;;;;;;;
; Uninstaller sections ;
;;;;;;;;;;;;;;;;;;;;;;;;

Section "Uninstall" SEC91
  SectionIn 1 2 3 RO
  SetShellVarContext all

  UnRegDLL "$INSTDIR\ActiveFirewall.dll"
  Delete /REBOOTOK "$INSTDIR\ActiveFirewall.dll"

  RMDir "$SMPROGRAMS\Trailer Builder"
  RMDir /r "$SMPROGRAMS\Trailer Builder"
  RMDir /r $INSTDIR
  DeleteRegKey HKLM "Software\Trailer Builder"

  DeleteRegKey HKLM \
    "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"

  Delete "$DESKTOP\Start Trailer Builder.lnk"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd

Section /o "un.Delete preferences and cache" SEC92
  !insertmacro delprefs
SectionEnd

; Uninstaller section descriptions
!insertmacro MUI_UNFUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC91} \
    "Uninstall TrailerBuilder and all its components"
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC92} \
    "Deletes TrailerBuilder preferences and cache files"
!insertmacro MUI_UNFUNCTION_DESCRIPTION_END

;Function un.onUninstSuccess
;  HideWindow
;  MessageBox MB_ICONINFORMATION|MB_OK \
;    "$(^Name) was successfully removed from your computer."
;FunctionEnd

Function un.onInit
  !insertmacro MUI_LANGDLL_DISPLAY
FunctionEnd
