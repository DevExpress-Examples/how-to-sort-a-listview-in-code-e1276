Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.Persistent.Base
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web.Layout

Partial Public Class NestedFrameControl
	Inherits System.Web.UI.UserControl
	Implements IFrameTemplate, IViewImageAndCaptionProvider
	Private contextMenu As ContextActionsMenu
	Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
		MyBase.OnPreRender(e)
	End Sub
	Protected Overrides Sub OnLoad(ByVal e As EventArgs)
		MyBase.OnLoad(e)
	End Sub
	Protected Overrides Sub OnInit(ByVal e As EventArgs)
		MyBase.OnInit(e)
	End Sub
	Public Sub New()
		contextMenu = New ContextActionsMenu(Me, "RecordEdit", "ListView")
	End Sub
	Public Overrides Sub Dispose()
		For Each container As IActionContainer In GetContainers()
			container.Dispose()
		Next container
		If contextMenu IsNot Nothing Then
			contextMenu.Dispose()
			contextMenu = Nothing
		End If
		MyBase.Dispose()
	End Sub
	Private ReadOnly Property ViewImageUrl() As String Implements IViewImageAndCaptionProvider.ViewImageUrl
		Get
			Return ViewImage.ImageUrl
		End Get
	End Property
	Private ReadOnly Property ViewCaption() As String Implements IViewImageAndCaptionProvider.ViewCaption
		Get
			Return ViewCaptionLabel.Text
		End Get
	End Property
	Public Event ViewInfoChanged As EventHandler(Of EventArgs) Implements IViewImageAndCaptionProvider.ViewInfoChanged
	#Region "OnViewInfoChanged"
	Private Sub OnViewInfoChanged()
		RaiseEvent ViewInfoChanged(Me, EventArgs.Empty)
	End Sub
	#End Region
	#Region "IFrameTemplate Members"
	Public ReadOnly Property DefaultContainer() As IActionContainer Implements IFrameTemplate.DefaultContainer
		Get
			Return ViewContainer
		End Get
	End Property
	Public Function GetContainers() As ICollection(Of IActionContainer) Implements IFrameTemplate.GetContainers
		Dim result As List(Of IActionContainer) = New List(Of IActionContainer)()
		If contextMenu IsNot Nothing Then
			result.AddRange(contextMenu.Containers)
		End If
		result.AddRange(New IActionContainer() { ContextObjectsCreationActionContainer, DiagnosticActionContainer, RecordEditContainer, ViewContainer, FiltersActionContainer })
		Return result
	End Function
	Public Sub SetView(ByVal view As DevExpress.ExpressApp.View) Implements IFrameTemplate.SetView
		ViewSite.Controls.Clear()
		If view IsNot Nothing Then
			contextMenu.CreateControls(view)
			view.CreateControls()
			ViewSite.Controls.Add(CType(view.Control, Control))
			ViewCaptionLabel.Text = view.Caption
			Dim imageInfo As ImageInfo = ImageLoader.Instance.GetImageInfo(view.Info.GetAttributeValue("ImageName"))
			If imageInfo.IsEmpty Then
				ViewImage.Visible = False
				ViewImage.ImageUrl = Nothing
			Else
				ViewImage.ImageUrl = imageInfo.ImageUrl
			End If
			OnViewInfoChanged()
		End If
	End Sub
	Public Sub SetSettings(ByVal settingsNode As DictionaryNode)
	End Sub
	Public Sub ReloadSettings()
	End Sub
	Public Sub SaveSettings()
	End Sub
	#End Region
End Class
