using System;
using backend.Models;

namespace backend.Extensions
{
    /**
     * Used for formatting DateTime to correct Modal Time
     */
    public class EventInformation

    {

        public string StartDate { get; set;}

        public string EndDate { get; set;}

        public Event Event { get; set;}

        public string TimeDuration { get; set; }

        public EventInformation(Event ev)
        {
           Event = ev;
           StartDate = ev.Start.ToString("MM/dd/yyyy");
           EndDate = ev.End.ToString("MM/dd/yyyy");
           TimeDuration = parseTimeDuration(ev.Start, ev.End);
        }


        public string parseTimeDuration(DateTime Start, DateTime End)
        {
            var StartTime = Start.ToShortTimeString();
            var EndTime = End.ToShortTimeString();

            return StartTime + " - " + EndTime;
        }
    }

}
