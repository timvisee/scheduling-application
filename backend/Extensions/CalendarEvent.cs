using System;
using System.Collections.Generic;
using backend.Models;

namespace backend.Extensions
{
    /**
     * Used for formatting DateTime to correct Modal Time
     */
    public class CalendarEvent

    {
        public CalendarEvent(Event ev)
        {
            id = ev.Id;
            start = ev.Start;
            end = ev.End;
            description = ev.Description;
            title = ev.Title;
        }

        /**
         * Lowercase variables for the FullCalendar plugin
         */
        public int id { get; }
        public DateTime start { get; }
        public DateTime end { get; }
        public string title { get; }
        public string description { get; }
    }
}
