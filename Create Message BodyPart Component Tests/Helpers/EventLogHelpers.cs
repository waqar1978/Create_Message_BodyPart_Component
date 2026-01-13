using System.Diagnostics;

namespace Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests.Helpers
{
    public static class EventLogHelpers
    {
        public static void CleanupEventLog()
        {
            EventLog ev = new EventLog("Application");
            ev.Clear();
        }

        public static bool CheckForInfoEvent(int eventLogCategory)
        {
            return (CheckForEvent(EventLogEntryType.Information, eventLogCategory));
        }

        public static bool CheckForWarningEvent(int eventLogCategory)
        {
            return (CheckForEvent(EventLogEntryType.Warning, eventLogCategory));
        }

        public static bool CheckForErrorEvent(int eventLogCategory)
        {
            return (CheckForEvent(EventLogEntryType.Error, eventLogCategory));
        }

        public static bool CheckForEvent(EventLogEntryType eventLogEntryType, int eventLogCategory)
        {
            EventLog el = new EventLog("Application");
            EventLogEntryCollection elCollection = el.Entries;

            foreach (EventLogEntry elEntry in elCollection)
            {
                if ((elEntry.InstanceId == eventLogCategory) && (elEntry.EntryType == eventLogEntryType))
                {
                    return (true);
                }
            }

            return (false);
        }
    }
}