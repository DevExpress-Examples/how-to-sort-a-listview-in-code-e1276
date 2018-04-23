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
            lv.CollectionSource.CollectionChanged += new EventHandler(CollectionSource_CollectionChanged);
        }
        void CollectionSource_CollectionChanged(object sender, EventArgs e) {
            ListView lv = (ListView)View;
            string propertyName = "Name";
            SortingDirection direction = SortingDirection.Descending;
            if (!lv.Model.UseServerMode) {
                XPBaseCollection regularCollection = (XPBaseCollection)lv.CollectionSource.Collection;
                regularCollection.Sorting.Add(new SortProperty(propertyName, direction));
            }
        }
    }
}
