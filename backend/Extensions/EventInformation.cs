using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<string> AttendeeList { get; set; }
        public List<string> LocationList { get; set; }
        public List<string> OwnerList { get; set; }

        public EventInformation(Event ev)
        {
            EventTitle = ev.Title;
            EventDescription = ev.Description;
            StartDate = ev.Start.ToString("MM/dd/yyyy");
            EndDate = ev.End.ToString("MM/dd/yyyy");
            TimeDuration = parseTimeDuration(ev.Start, ev.End);

            var tempAttList = new List<string>();
            var tempLocList = new List<string>();

            foreach (var att in ev.Attendees.Select(e => e.People))
            {
                tempAttList.Add(att.DisplayName);
            }

            foreach (var loc in ev.Locations.Select(e => e.Location))
            {
                tempLocList.Add(loc.Name);
            }

            AttendeeList = tempAttList;
            LocationList = tempLocList;
        }


        public string parseTimeDuration(DateTime Start, DateTime End)
        {
            var startTime = Start.ToShortTimeString();
            var endTime = End.ToShortTimeString();

            return startTime + " - " + endTime;
        }
    }
}
