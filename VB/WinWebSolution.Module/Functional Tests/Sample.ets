;#DropDB WinWebSolutionEasyTest

#Application WinWebSolutionWin
#Application WinWebSolutionWeb

*Action Navigation(User)

*ProcessRecord
 Full Name = ''

#IfDef WinWebSolutionWeb
*Action Edit
#EndIf
 
*FillForm
 Full Name = Test User
 
*Action Cancel

#IfDef WinWebSolutionWin
*HandleDialog
 Respond = Yes
#EndIf

*CheckFieldValues
 Full Name = ''
