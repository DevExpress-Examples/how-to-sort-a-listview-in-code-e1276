using System;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;

namespace WinWebSolution.Module {
    public partial class SortListViewController : ViewController {
        public SortListViewController() {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(TestObject);
        }
        protected override void OnActivated() {
            base.OnActivated();
            ListView lv = (ListView)View;
            string propertyName = "Name";
            SortingDirection direction = SortingDirection.Descending;
            if (lv.Model.UseServerMode) {
                XpoServerModeGridObjectDataSource serverCollection = (XpoServerModeGridObjectDataSource)lv.CollectionSource.Collection;
                serverCollection.SortInfo.Add(new XpoServerModeGridSortInfo(new OperandProperty(propertyName), direction, View.ObjectSpace.Session.GetClassInfo(View.ObjectTypeInfo.Type)));
            } else {
                XPBaseCollection regularCollection = (XPBaseCollection)lv.CollectionSource.Collection;
                regularCollection.Sorting.Add(new SortProperty(propertyName, direction));
            }
        }
    }
}
