Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata

Namespace WinWebSolution.Module
	<DefaultClassOptions, DeferredDeletion(False)> _
	Public Class TestObject
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _name As String
		Public Property Name() As String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", _name, value)
			End Set
		End Property
	End Class
End Namespace