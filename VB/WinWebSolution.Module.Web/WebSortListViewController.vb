Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Web.Editors.ASPx

Namespace WinWebSolution.Module.Win
	Public Class WinSortRootListViewController
		Inherits SortListViewControllerBase
		Protected Overrides Sub OnViewControlsCreated()
			MyBase.OnViewControlsCreated()
			Dim gridListEditor As ASPxGridListEditor = TryCast(View.Editor, ASPxGridListEditor)
			If gridListEditor IsNot Nothing Then
				gridListEditor.Grid.SettingsBehavior.AllowSort = False
			End If
		End Sub
	End Class
End Namespace