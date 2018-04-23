Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Web.UI

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Web

Partial Public Class DialogPage
	Inherits System.Web.UI.Page
	Implements IWindowTemplate, ILookupPopupFrameTemplate
	Private contextMenu As ContextActionsMenu
	Protected Overrides Sub InitializeCulture()
		If WebApplication.Instance IsNot Nothing Then
			WebApplication.Instance.InitializeCulture()
		End If
	End Sub
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		contextMenu = New ContextActionsMenu(Me, "ListView")
		WebApplication.Instance.CreateControls(Me)
	End Sub
	Public Overrides Sub Dispose()
		If contextMenu IsNot Nothing Then
			contextMenu.Dispose()
			contextMenu = Nothing
		End If
		MyBase.Dispose()
	End Sub

	#Region "IFrameTemplate Members"
	Private isSizeable_Renamed As Boolean
	Public ReadOnly Property DefaultContainer() As IActionContainer Implements DevExpress.ExpressApp.Templates.IFrameTemplate.DefaultContainer
		Get
			Return Nothing
		End Get
	End Property
	Public Function GetContainers() As ICollection(Of IActionContainer) Implements DevExpress.ExpressApp.Templates.IFrameTemplate.GetContainers
		Dim result As List(Of IActionContainer) = New List(Of IActionContainer)()
		result.AddRange(contextMenu.Containers)
		result.AddRange(New IActionContainer() { Me.SearchActionContainer, Me.ObjectsCreationActionContainer, Me.PopupActions, Me.DiagnosticActionContainer})
		Return result
	End Function
	Public Sub SetView(ByVal view As DevExpress.ExpressApp.View) Implements DevExpress.ExpressApp.Templates.IFrameTemplate.SetView
		Me.ViewSite.Controls.Clear()
		If view IsNot Nothing Then
			contextMenu.CreateControls(view)
			view.CreateControls()
			ViewSite.Controls.Add(CType(view.Control, Control))
			Me.ViewCaptionLabel.Text = view.Caption
			Header.Title = view.Caption & " - " & WebApplication.Instance.Title
			Dim imageInfo As ImageInfo = ImageLoader.Instance.GetLargeImageInfo(view.Info.GetAttributeValue("ImageName"))
			If imageInfo.IsEmpty Then
				ViewImage.Visible = False
			Else
				ViewImage.ImageUrl = imageInfo.ImageUrl
			End If
			If TypeOf view Is DetailView Then
				SearchActionContainer.Visible = False
				ObjectsCreationActionContainer.Visible = False
			End If
			'string colorString = view.Info.GetAttributeValue("BackColor");
			'if(!string.IsNullOrEmpty(colorString)) {
			'	try {
			'		KnownColor kColor = (KnownColor)Enum.Parse(typeof(KnownColor), colorString);
			'		ViewTitle.BackColor = Color.FromKnownColor(kColor);
			'	}
			'	catch(Exception) { }
			'}
		End If
	End Sub
	Public Sub SetSettings(ByVal settingsNode As DictionaryNode)
	End Sub
	Public Sub ReloadSettings()
	End Sub
	Public Sub SaveSettings()
	End Sub
	Public Property IsSizeable() As Boolean Implements IWindowTemplate.IsSizeable
		Get
			Return isSizeable_Renamed
		End Get
		Set(ByVal value As Boolean)
			isSizeable_Renamed = value
		End Set
	End Property
	Public Sub SetCaption(ByVal caption As String) Implements IWindowTemplate.SetCaption
	End Sub
	Public Sub SetStatus(ByVal statusMessages As System.Collections.Generic.ICollection(Of String)) Implements IWindowTemplate.SetStatus
	End Sub

	#End Region

	#Region "ILookupPopupFrameTemplate Members"

	Public Property IsSearchEnabled() As Boolean Implements ILookupPopupFrameTemplate.IsSearchEnabled
		Get
			Return SearchActionContainer.Visible
		End Get
		Set(ByVal value As Boolean)
			SearchActionContainer.Visible = value
		End Set
	End Property

	Public Sub SetStartSearchString(ByVal searchString As String) Implements ILookupPopupFrameTemplate.SetStartSearchString
	End Sub

	#End Region
End Class
