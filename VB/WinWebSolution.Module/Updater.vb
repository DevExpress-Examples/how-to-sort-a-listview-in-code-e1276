Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl

Namespace WinWebSolution.Module
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal session As Session, ByVal currentDBVersion As Version)
			MyBase.New(session, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			Dim obj1 As New Issue(Session)
			obj1.Subject = "Issue 3"
			obj1.UpdateModifiedOn()
			obj1.Save()
			Dim obj2 As New Issue(Session)
			obj2.Subject = "Issue 2"
			obj2.Save()
			obj2.UpdateModifiedOn(New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1))
			Dim obj3 As New Issue(Session)
			obj3.Subject = "Issue 1"
			obj3.Save()
			obj3.UpdateModifiedOn(New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2))
		End Sub
	End Class
End Namespace
