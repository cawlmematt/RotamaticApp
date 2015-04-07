using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotamaticApp.Models
{
    public class FullCalendarEvent
    {
        /* Uniquely identifies the given event. Different instances of repeating events 
         * should all have the same id. */
        public string Id { get; set; }

        public string Title { get; set; }
        public bool AllDay { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        /* A URL that will be visited when this event is clicked by the user. 
         * For more information on controlling this behavior, see the eventClick 
         * callback. */
        public string Url { get; set; }

        // A CSS class (or array of classes) that will be attached to this event's element.
        public string ClassName { get; set; }
    }
}
