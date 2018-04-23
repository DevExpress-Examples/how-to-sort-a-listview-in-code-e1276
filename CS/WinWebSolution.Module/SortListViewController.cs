using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.NodeWrappers;
using DevExpress.Data;

namespace WinWebSolution.Module {
    public abstract class SortListViewControllerBase : ViewController<ListView> {
        public SortListViewControllerBase() {
            TargetObjectType = typeof(Issue);
        }
        protected override void OnActivated() {
            base.OnActivated();
            ColumnInfoNodeWrapper columnInfo = View.Model.Columns.FindColumnInfo("ModifiedOn");
            if (columnInfo != null) {
                columnInfo.SortIndex = 0;
                columnInfo.SortOrder = ColumnSortOrder.Descending;
            }
        }
    }
    [DefaultClassOptions]
    public class Issue : BaseObject {
        public Issue(Session session) : base(session) { }
        [Persistent("ModifiedOn"), ValueConverter(typeof(UtcDateTimeConverter))]
        protected DateTime _ModifiedOn = DateTime.Now;
        [PersistentAlias("_ModifiedOn")]
        [Custom("EditMask", "G")]
        [Custom("DisplayFormat", "{0:G}")]
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