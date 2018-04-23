Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp

Namespace WinWebSolution.Module.Web
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal os As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(os, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
		End Sub
	End Class
End Namespace
