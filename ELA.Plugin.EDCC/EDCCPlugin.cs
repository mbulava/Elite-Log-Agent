using DW.ELA.Interfaces;
using DW.ELA.Interfaces.Events;
using DW.ELA.Interfaces.Settings;
using DW.ELA.Utility;
using MoreLinq;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Timers;

namespace ELA.Plugin.EDCC
{
    public sealed class EDCCPlugin : IPlugin, IObserver<JournalEvent>
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        private readonly ConcurrentQueue<JournalEvent> EventQueue = new ConcurrentQueue<JournalEvent>();
        private readonly System.Timers.Timer flushTimer = new System.Timers.Timer
        { 
            AutoReset = false, 
        };

        public Commander CurrentCommander { get; private set; }

        public EDCCSettings MySettings { get; private set; }

        #region IPlugin Implementation
        public string PluginName => "EDCC";

        public string PluginId => "EDCC Event Tracker";

        public void FlushQueue()
        {
            try
            {
                var events = new List<JournalEvent>();
                while (EventQueue.TryDequeue(out var @event))
                    events.Add(@event);
                if (events.Count > 0)
                    FlushEvents(events);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while processing events");
            }
            finally
            {
                flushTimer.Start();
            }
        }

       
        public IObserver<JournalEvent> GetLogObserver() => this;


        public ISettingsController GetPluginSettingsControl(GlobalSettings settings) => new EDCCSettingsControl()
        {
            GlobalSettings = settings,
            SaveSettingsFunc = SaveSettings
        };

        private void SaveSettings(GlobalSettings settings, EDCCSettings myValues) =>
            new PluginSettingsFacade<EDCCSettings>(PluginId).SetPluginSettings(settings, 
                new EDCCSettings() 
                { 
                    ConnectionString = myValues.ConnectionString,
                });

        public void OnSettingsChanged(object sender, EventArgs e) => ReloadSettings();

        #endregion

        #region IObserver<JournalEvent> Implementation
        void IObserver<JournalEvent>.OnCompleted() => FlushQueue();

        void IObserver<JournalEvent>.OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        void IObserver<JournalEvent>.OnNext(JournalEvent value)
        {
            if(value is Commander commanderEvent)
            {
                FlushQueue();
                CurrentCommander = commanderEvent;
            }
        }

        #endregion



        private void FlushEvents(List<JournalEvent> events)
        {
            try
            {
                foreach (var e in events)
                    WriteEvent(e);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private void ReloadSettings()
        {
            //throw new NotImplementedException();
        }


        private async void WriteEvent(JournalEvent journalEvent, CancellationToken cancellationToken = default)
        {
            try
            {
                using(var con = new SqlConnection(MySettings.ConnectionString))
                {
                    await con.OpenAsync(cancellationToken);
                    var cmd = con.CreateCommand();
                    cmd.CommandText = "usp_API_EventItem_Insert";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Timestamp", journalEvent.Timestamp);
                    cmd.Parameters.AddWithValue("@EventType", journalEvent.Event);
                    cmd.Parameters.AddWithValue("@Raw", journalEvent.Raw.ToString());

                    await cmd.ExecuteNonQueryAsync(cancellationToken);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //public class CommanderData
        //{
        //    public readonly string Name;
        //    public readonly string FrontierID;

        //    public CommanderData(string commanderName, string commanderFid)
        //    {
        //        Name = commanderName;
        //        FrontierID = commanderFid;
        //    }

        //    public override string ToString() => $"{Name} - {FrontierID}";
        //}

    }
}
