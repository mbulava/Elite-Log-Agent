using DW.ELA.Interfaces;
using DW.ELA.Interfaces.Settings;
using System.Windows.Forms;

namespace DW.ELA.Controller
{
    public class AbstractSettingsControl : UserControl, ISettingsController
    {
        public AbstractSettingsControl()
        {
        }

        /// <summary>
        /// Gets or sets reference to temporary instance of Settings existing in settings form
        /// </summary>
        public GlobalSettings GlobalSettings { get; set; }

        public virtual void SaveSettings()
        {
        }
    }
}
