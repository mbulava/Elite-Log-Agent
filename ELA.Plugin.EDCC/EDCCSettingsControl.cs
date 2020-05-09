using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DW.ELA.Controller;
using DW.ELA.Interfaces.Settings;

namespace ELA.Plugin.EDCC
{
    public partial class EDCCSettingsControl : AbstractSettingsControl
    {
        public EDCCSettingsControl()
        {
            InitializeComponent();
        }

        public Action<GlobalSettings, EDCCSettings> SaveSettingsFunc { private get; set; }
    }
}
