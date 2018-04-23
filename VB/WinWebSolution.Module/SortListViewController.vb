Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.NodeWrappers
Imports DevExpress.Data

Namespace WinWebSolution.Module
	Public MustInherit Class SortListViewControllerBase
		Inherits ViewController(Of ListView)
		Public Sub New()
			TargetObjectType = GetType(Issue)
		End Sub
		Protected Overrides Overloads Sub OnActivated()
			MyBase.OnActivated()
			Dim columnInfo As ColumnInfoNodeWrapper = View.Model.Columns.FindColumnInfo("ModifiedOn")
			If columnInfo IsNot Nothing Then
				columnInfo.SortIndex = 0
				columnInfo.SortOrder = ColumnSortOrder.Descending
			End If
		End Sub
	End Class
	<DefaultClassOptions> _
	Public Class Issue
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		<Persistent("ModifiedOn"), ValueConverter(GetType(UtcDateTimeConverter))> _
		Protected _ModifiedOn As DateTime = DateTime.Now
		<PersistentAlias("_ModifiedOn"), Custom("EditMask", "G"), Custom("DisplayFormat", "{0:G}")> _
		Public ReadOnly Property ModifiedOn() As DateTime
			Get
				Return _ModifiedOn
			End Get
		End Property
		Friend Overridable Sub UpdateModifiedOn()
			UpdateModifiedOn(DateTime.Now)
		End Sub
		Friend Overridable Sub UpdateModifiedOn(ByVal [date] As DateTime)
			_ModifiedOn = [date]
			Save()
		End Sub
		Protected Overrides Overloads Sub OnChanged(ByVal propertyName As String, ByVal oldValue As Object, ByVal newValue As Object)
			MyBase.OnChanged(propertyName, oldValue, newValue)
			If propertyName = "Subject" OrElse propertyName = "Description" Then
				UpdateModifiedOn()
			End If
		End Sub
		Private _Subject As String
		Public Property Subject() As String
			Get
				Return _Subject
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Subject", _Subject, value)
			End Set
		End Property
		Private _Description As String
		Public Property Description() As String
			Get
				Return _Description
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Description", _Description, value)
			End Set
		End Property
	End Class
End Namespace