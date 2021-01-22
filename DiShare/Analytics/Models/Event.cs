using System;

namespace DiShare.Analytics.Models
{
    public class Event
    {
        public string Id { get; set; }

        public string Category { get; set; }

        public string Action { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        public string Version { get; set; }

        public DateTime Created { get; set; }

        public bool IsSynchronized { get; set; }
    }
}
