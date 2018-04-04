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
            StartDate = ev.Start.ToString("dd/MM/yyyy");
            EndDate = ev.End.ToString("dd/MM/yyyy");
            TimeDuration = parseTimeDuration(ev.Start, ev.End);

            AttendeeList = ev.Attendees.Select(e => e.People).Select(att => att.DisplayName).ToList();
            OwnerList = ev.Owners.Select(e => e.People).Select(own => own.DisplayName).ToList();
            LocationList = ev.Locations.Select(e => e.Location).Select(loc => loc.Name).ToList();
        }


        public string parseTimeDuration(DateTime Start, DateTime End)
        {
            var startTime = Start.ToShortTimeString();
            var endTime = End.ToShortTimeString();

            return startTime + " - " + endTime;
        }
    }
}
