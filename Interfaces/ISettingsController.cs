using DW.ELA.Interfaces.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace DW.ELA.Interfaces
{
    public interface ISettingsController
    {
        GlobalSettings GlobalSettings { get; set; }

        void SaveSettings();
    }
}
