﻿namespace DW.ELA.Plugin.EDDN
{
    using System.Threading.Tasks;
    using DW.ELA.Plugin.EDDN.Model;

    public interface IEddnApiFacade
    {
        Task PostEventAsync(EddnEvent events);
    }
}