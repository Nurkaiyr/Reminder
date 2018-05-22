using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace TimerWithReminder
{
    public class ReminderContext:DbContext
    {
        public ReminderContext() : base("ReminderDB")
        {
            Database.SetInitializer(new ReminderDBInitializer());
        }
        public DbSet<Reminder> Reminders { get; set; }
    }
    public class ReminderDBInitializer : CreateDatabaseIfNotExists<ReminderContext>
    {
        protected override void Seed(ReminderContext context)
        {
            List<Reminder> reminders = new List<Reminder>
            {
                new Reminder
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    NotificationText = "Напоминание 1",
                    NotifyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                              DateTime.Now.Hour, DateTime.Now.Minute + 2, 0)
                },
                new Reminder
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    NotificationText = "Напоминание 2",
                    NotifyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                              DateTime.Now.Hour, DateTime.Now.Minute + 1, 0)
                }
            };

            context.Reminders.AddRange(reminders);
            base.Seed(context);
        }
    }
}
