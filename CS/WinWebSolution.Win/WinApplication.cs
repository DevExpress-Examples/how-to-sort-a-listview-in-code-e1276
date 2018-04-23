using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp;

namespace WinWebSolution.Win {
    public partial class WinWebSolutionWindowsFormsApplication : WinApplication {
        public WinWebSolutionWindowsFormsApplication() {
            InitializeComponent();
        }

        private void WinWebSolutionWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
			e.Updater.Update();
			e.Handled = true;
#else
            e.Updater.Update();
            e.Handled = true;
#endif
        }
    }
}
