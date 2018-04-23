using System;
using DevExpress.Xpo;
using DevExpress.Data;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;

namespace WinWebSolution.Module {
    public abstract class SortListViewControllerBase : ViewController<ListView> {
        public SortListViewControllerBase() {
            TargetObjectType = typeof(Issue);
        }
        protected override void OnActivated() {
            base.OnActivated();
            string propertyName = "ModifiedOn";
            bool demoFlag = true;
            //Dennis: This code applies a client side sorting.
            if (demoFlag) {
                IModelColumn columnInfo = ((IModelList<IModelColumn>)View.Model.Columns)[propertyName];
                if (columnInfo != null) {
                    columnInfo.SortIndex = 0;
                    columnInfo.SortOrder = ColumnSortOrder.Descending;
                }
            }
            else {
                //Dennis: This code is used for the server side sorting.
                if (((IModelList<IModelSortProperty>)View.Model.Sorting)[propertyName] == null) {
                    IModelSortProperty sortProperty = View.Model.Sorting.AddNode<IModelSortProperty>(propertyName);
                    sortProperty.Direction = SortingDirection.Descending;
                    sortProperty.PropertyName = propertyName;
                }
            }
        }
    }
    [DefaultClassOptions]
    public class Issue : BaseObject {
        public Issue(Session session) : base(session) { }
        [Persistent("ModifiedOn"), ValueConverter(typeof(UtcDateTimeConverter))]
        protected DateTime _ModifiedOn = DateTime.Now;
        [PersistentAlias("_ModifiedOn")]
        [ModelDefault("EditMask", "G")]
        [ModelDefault("DisplayFormat", "{0:G}")]
        public DateTime ModifiedOn {
            get { return _ModifiedOn; }
        }
        internal virtual void UpdateModifiedOn() {
            UpdateModifiedOn(DateTime.Now);
        }
        internal virtual void UpdateModifiedOn(DateTime date) {
            _ModifiedOn = date;
            Save();
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue) {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == "Subject" || propertyName == "Description") {
                UpdateModifiedOn();
            }
        }
        private string _Subject;
        public string Subject {
            get { return _Subject; }
            set { SetPropertyValue("Subject", ref _Subject, value); }
        }
        private string _Description;
        public string Description {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }
    }
}