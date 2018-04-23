Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp

Namespace WinWebSolution.Module
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal os As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(os, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			Dim obj1 As Issue = ObjectSpace.CreateObject(Of Issue)()
			obj1.Subject = "Issue 3"
			obj1.UpdateModifiedOn()
			obj1.Save()
			Dim obj2 As Issue = ObjectSpace.CreateObject(Of Issue)()
			obj2.Subject = "Issue 2"
			obj2.Save()
			obj2.UpdateModifiedOn(New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1))
			Dim obj3 As Issue = ObjectSpace.CreateObject(Of Issue)()
			obj3.Subject = "Issue 1"
			obj3.Save()
			obj3.UpdateModifiedOn(New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2))
		End Sub
	End Class
End Namespace
