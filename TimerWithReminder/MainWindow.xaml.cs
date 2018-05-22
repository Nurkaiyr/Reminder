using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimerWithReminder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICollection<Timer> Timers { get; set; }
        public MainWindow()
        {
            Timers = new List<Timer>();
            UpdateTimers();
            InitializeComponent();
            UpdateReminder();
        }

        public void UpdateTimers(Reminder reminder=null)
        {
            if (reminder == null)
            {
                using (var context = new ReminderContext())
                {
                    foreach (var ntf in context.Reminders.Where(n => n.NotifyTime > DateTime.Now))
                    {
                        Timers.Add(new Timer(new TimerCallback(Notify), ntf.NotificationText,
                                    GetMilliseconds(ntf.NotifyTime), Timeout.Infinite));
                    }
                }
            }
            else
            {
                Timers.Add(new Timer(new TimerCallback(Notify), reminder.NotificationText,
                                    GetMilliseconds(reminder.NotifyTime), Timeout.Infinite));
            }
        }

        public void UpdateReminder()
        {
            using (var context = new ReminderContext())
            {
                NotificationList.ItemsSource = context.Reminders
                            .OrderByDescending(x => x.CreateTime).ToList();
            }
        }


        // Показать напоминание
        public void Notify(object reminder)
        {
            MessageBox.Show(reminder.ToString());
        }

        public int GetMilliseconds(DateTime time)
        {
            double diff = (time - DateTime.Now).TotalMilliseconds;

            if (diff < 0)
            {
                return 0;
            }

            return (int)diff;
        }

        // Удалить
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (NotificationList.SelectedIndex == -1)
                return;

            Reminder notificationToDelete = NotificationList.Items[NotificationList.SelectedIndex] as Reminder;

            using (var context = new ReminderContext())
            {
                context.Entry(notificationToDelete).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

            UpdateReminder();
        }

        // Создать
        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            Window createWindow = new CreateNotification(this);
            createWindow.ShowDialog();
        }
    }
}
