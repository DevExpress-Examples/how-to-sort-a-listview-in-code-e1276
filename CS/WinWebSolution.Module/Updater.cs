using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp;

namespace WinWebSolution.Module {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace os, Version currentDBVersion) : base(os, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            Issue obj1 = ObjectSpace.CreateObject<Issue>();
            obj1.Subject = "Issue 3";
            obj1.UpdateModifiedOn();
            obj1.Save();
            Issue obj2 = ObjectSpace.CreateObject<Issue>();
            obj2.Subject = "Issue 2";
            obj2.Save();
            obj2.UpdateModifiedOn(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1));
            Issue obj3 = ObjectSpace.CreateObject<Issue>();
            obj3.Subject = "Issue 1";
            obj3.Save();
            obj3.UpdateModifiedOn(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2));
        }
    }
}
