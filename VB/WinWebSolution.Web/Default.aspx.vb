Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web.UI
Imports System.Drawing
Imports System.Web.UI.WebControls

Imports DevExpress.ExpressApp.Templates
Imports DevExpress.ExpressApp.Web
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Web.TestScripts
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Web.Templates.ActionContainers

Partial Public Class [Default]
	Inherits System.Web.UI.Page
	Implements IWindowTemplate, IViewSiteTemplate, ISupportViewControlAdding
	Private contextMenu As ContextActionsMenu
	Private Function GetApplicationInfoString() As String
		Dim aboutInfo As New AboutInfo(WebApplication.Instance)
		Return aboutInfo.GetAboutInfoString()
	End Function
	Private Sub ToolsRoundPanel_PreRender(ByVal sender As Object, ByVal e As EventArgs)
		Dim isVisible As Boolean = False
		For Each control As Control In ToolsRoundPanel.Controls
			If TypeOf control Is WebActionContainer AndAlso (CType(control, WebActionContainer)).HasActiveActions Then
				isVisible = True
				Exit For
			End If
		Next control
		ToolsRoundPanel.Visible = isVisible
	End Sub
	Protected Overrides Sub InitializeCulture()
		MyBase.InitializeCulture()
		WebApplication.Instance.InitializeCulture()
	End Sub
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		AddHandler ToolsRoundPanel.PreRender, AddressOf ToolsRoundPanel_PreRender
		contextMenu = New ContextActionsMenu(Me, "RecordEdit", "ObjectsCreation", "ListView")
		Header.Title = WebApplication.Instance.Title
		'ApplicationTitle.Text = WebApplication.Instance.Title;
		'ApplicationTitle.NavigateUrl = Request.ApplicationPath + "/";
		NavigationTabsActionContainer.ShowImages = True
		Copyright.Text = GetApplicationInfoString()
		LogoImage.ImageUrl = String.Format("~/App_Themes/{0}/Xaf/Logo.png", Theme)
		WebApplication.Instance.CreateControls(Me)
	End Sub
	Protected Sub Page_Prerender(ByVal sender As Object, ByVal e As EventArgs)
		If TestScriptsManager.EasyTestEnabled Then
			ViewCaptionLabel.Attributes(EasyTestTagHelper.TestField) = "FormCaption"
			ViewCaptionLabel.Attributes(EasyTestTagHelper.TestControlClassName) = JSLabelTestControl.ClassName
		End If
	End Sub
	Public Overrides Sub Dispose()
		If contextMenu IsNot Nothing Then
			contextMenu.Dispose()
			contextMenu = Nothing
		End If
		MyBase.Dispose()
	End Sub

	#Region "IFrameTemplate Members"

	Public ReadOnly Property DefaultContainer() As IActionContainer Implements DevExpress.ExpressApp.Templates.IFrameTemplate.DefaultContainer
		Get
			Return ViewPresentationActionContainer
		End Get
	End Property

	Public Function GetContainers() As ICollection(Of IActionContainer) Implements DevExpress.ExpressApp.Templates.IFrameTemplate.GetContainers
		Dim result As List(Of IActionContainer) = New List(Of IActionContainer)()
		result.AddRange(contextMenu.Containers)
		result.AddRange(New IActionContainer() { NavigationTabsActionContainer, NavigationLinksActionContainer, SearchActionContainer, VerticalNewActionContainer, ContextObjectsCreationActionContainer, RecordsNavigationContainer, ListViewDataManagementActionContainer, ViewPresentationActionContainer, TopRecordEditActionContainer, DiagnosticActionContainer, VerticalToolsActionContainer, ViewsHistoryNavigationContainer, SecurityActionContainer })
		Return result.ToArray()
	End Function

	Public Sub SetView(ByVal view As DevExpress.ExpressApp.View) Implements DevExpress.ExpressApp.Templates.IFrameTemplate.SetView
		ViewSite.Controls.Clear()
		If view IsNot Nothing Then
			contextMenu.CreateControls(view)
			view.CreateControls()
			RaiseEvent ViewControlAdding(Me, EventArgs.Empty)
			ViewSite.Controls.Add(CType(view.Control, Control))
			ViewCaptionLabel.Text = view.Caption

			Dim imageInfo As ImageInfo = ImageLoader.Instance.GetLargeImageInfo(view.Info.GetAttributeValue("ImageName"))
			If imageInfo.IsEmpty Then
				ViewImage.Visible = False
			Else
				ViewImage.ImageUrl = imageInfo.ImageUrl
			End If
			Dim colorString As String = view.Info.GetAttributeValue("BackColor")
			'if(!string.IsNullOrEmpty(colorString)) {
			'	try {
			'		KnownColor kColor = (KnownColor)Enum.Parse(typeof(KnownColor), colorString);
			'		//ViewTitle.BackColor = Color.FromKnownColor(kColor);
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

	Public Sub SetCaption(ByVal caption As String) Implements IWindowTemplate.SetCaption
		If String.IsNullOrEmpty(caption) Then
			Header.Title = caption
		Else
			Header.Title = System.Web.HttpUtility.HtmlEncode(caption)
		End If
	End Sub

	Public Sub SetStatus(ByVal statusMessages As System.Collections.Generic.ICollection(Of String)) Implements IWindowTemplate.SetStatus
		InfoMessagesPanel.Text = String.Join("<br>", New List(Of String)(statusMessages).ToArray())
	End Sub
	Private isSizeable_Renamed As Boolean
	Public Property IsSizeable() As Boolean Implements IWindowTemplate.IsSizeable
		Get
			Return isSizeable_Renamed
		End Get
		Set(ByVal value As Boolean)
			isSizeable_Renamed = value
		End Set
	End Property
	Public ReadOnly Property ViewSiteControl() As Object Implements IViewSiteTemplate.ViewSiteControl
		Get
			Return ViewSite
		End Get
	End Property
	Public Event ViewControlAdding As EventHandler Implements ISupportViewControlAdding.ViewControlAdding
	#End Region
End Class
