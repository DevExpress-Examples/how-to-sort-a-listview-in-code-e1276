using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Web.Editors.ASPx;

namespace WinWebSolution.Module.Win {
    public class WinSortRootListViewController : SortListViewControllerBase {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            ASPxGridListEditor gridListEditor = View.Editor as ASPxGridListEditor;
            if (gridListEditor != null) {
                gridListEditor.Grid.SettingsBehavior.AllowSort = false;
            }
        }
    }
}