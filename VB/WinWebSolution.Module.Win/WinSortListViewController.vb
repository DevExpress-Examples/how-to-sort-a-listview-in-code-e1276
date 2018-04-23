Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Win.Editors

Namespace WinWebSolution.Module.Win
	Public Class WinSortListViewController
		Inherits SortListViewControllerBase
		Protected Overrides Sub OnViewControlsCreated()
			MyBase.OnViewControlsCreated()
			Dim gridListEditor As GridListEditor = TryCast(View.Editor, GridListEditor)
			If gridListEditor IsNot Nothing Then
				gridListEditor.GridView.OptionsCustomization.AllowSort = False
			End If
		End Sub
	End Class
End Namespace
