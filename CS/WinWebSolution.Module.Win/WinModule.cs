using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using DevExpress.ExpressApp;

namespace WinWebSolution.Module.Win {
    [ToolboxItemFilter("Xaf.Platform.Win")]
    public sealed partial class WinWebSolutionWindowsFormsModule : ModuleBase {
        public WinWebSolutionWindowsFormsModule() {
            InitializeComponent();
        }
    }
}
