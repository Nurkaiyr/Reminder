using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerWithReminder
{
    public class Reminder
    {
        public Guid Id { get; set; }
        public string NotificationText { get; set; }
        public DateTime NotifyTime { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
