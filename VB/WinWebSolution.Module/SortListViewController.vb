Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.Helpers

Namespace WinWebSolution.Module
	Partial Public Class SortListViewController
		Inherits ViewController
		Public Sub New()
			InitializeComponent()
			RegisterActions(components)
			TargetViewType = ViewType.ListView
			TargetObjectType = GetType(TestObject)
		End Sub
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			Dim lv As ListView = CType(View, ListView)
			Dim propertyName As String = "Name"
			Dim direction As SortingDirection = SortingDirection.Descending
			If lv.Model.UseServerMode Then
				Dim serverCollection As XpoServerModeGridObjectDataSource = CType(lv.CollectionSource.Collection, XpoServerModeGridObjectDataSource)
				serverCollection.SortInfo.Add(New XpoServerModeGridSortInfo(New OperandProperty(propertyName), direction, View.ObjectSpace.Session.GetClassInfo(View.ObjectTypeInfo.Type)))
			Else
				Dim regularCollection As XPBaseCollection = CType(lv.CollectionSource.Collection, XPBaseCollection)
				regularCollection.Sorting.Add(New SortProperty(propertyName, direction))
			End If
		End Sub
	End Class
End Namespace
