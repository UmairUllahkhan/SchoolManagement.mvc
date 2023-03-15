using System;
using System.Collections.Generic;

namespace SchoolManagement.mvc.DataDB
{
    public partial class RoomTable
    {
        public RoomTable()
        {
            TimeSlots = new HashSet<TimeSlot>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;

        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
    }
}
