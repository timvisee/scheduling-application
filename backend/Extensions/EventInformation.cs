using System;
using System.Collections.Generic;
using backend.Models;

namespace backend.Extensions
{
    /**
     * Used for formatting DateTime to correct Modal Time
     */
    public class EventInformation

    {
        public string EventTitle { get; }
        public string EventDescription { get; }

        public string StartDate { get; }
        public string EndDate { get; }
        public string TimeDuration { get; }
//        public List<string> AttendeeList { get; }

        public EventInformation(Event ev)
        {
            EventTitle = ev.Title;
            EventDescription = ev.Description;
            StartDate = ev.Start.ToString("MM/dd/yyyy");
            EndDate = ev.End.ToString("MM/dd/yyyy");
            TimeDuration = parseTimeDuration(ev.Start, ev.End);

            // TODO: Add locations (tested with attendees)
//            if (ev.Attendees != null) {
//                foreach (var att in ev.Attendees)
//                {
//                    AttendeeList.Add(att.People.DisplayName);
//                }
//            }
        }


        public string parseTimeDuration(DateTime Start, DateTime End)
        {
            var startTime = Start.ToShortTimeString();
            var endTime = End.ToShortTimeString();

            return startTime + " - " + endTime;
        }
    }
}
