﻿namespace DW.ELA.Interfaces.Events
{
    using Newtonsoft.Json;

    public class CommunityGoal : LogEvent
    {
        [JsonProperty("CurrentGoals")]
        public CommunityGoalRecord[] CurrentGoals { get; set; }
    }
}
