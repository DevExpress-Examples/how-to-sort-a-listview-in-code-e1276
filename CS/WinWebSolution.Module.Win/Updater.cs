using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp;

namespace WinWebSolution.Module.Win {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace os, Version currentDBVersion) : base(os, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
        }
    }
}
