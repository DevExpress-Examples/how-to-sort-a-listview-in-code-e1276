using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;

namespace WinWebSolution.Module {
    public class Updater : ModuleUpdater {
        public Updater(Session session, Version currentDBVersion) : base(session, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            Issue obj1 = new Issue(Session);
            obj1.Subject = "Issue 3";
            obj1.UpdateModifiedOn();
            obj1.Save();
            Issue obj2 = new Issue(Session);
            obj2.Subject = "Issue 2";
            obj2.Save();
            obj2.UpdateModifiedOn(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1));
            Issue obj3 = new Issue(Session);
            obj3.Subject = "Issue 1";
            obj3.Save();
            obj3.UpdateModifiedOn(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2));
        }
    }
}
