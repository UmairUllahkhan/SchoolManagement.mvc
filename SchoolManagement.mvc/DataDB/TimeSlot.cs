using System;
using System.Collections.Generic;

namespace SchoolManagement.mvc.DataDB
{
    public partial class TimeSlot
    {
        public int TimeId { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public int? RoomId { get; set; }

        public virtual RoomTable? Room { get; set; }
    }
}
