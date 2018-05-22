using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimerWithReminder
{
    /// <summary>
    /// Логика взаимодействия для CreateNotification.xaml
    /// </summary>
    public partial class CreateNotification : Window
    {
        private MainWindow MainWindow { get; set; }

        public CreateNotification(MainWindow main)
        {
            this.MainWindow = main;
            InitializeComponent();
        }

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            if (ValidateNotification())
            {
                Reminder newReminder = new TimerWithReminder.Reminder
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    NotificationText = ReminderText.Text
                                        .Substring(0, ReminderText.Text.Length - 2),
                    NotifyTime = (DateTime)NotifyDateTime.Value
                };

                using (var context = new ReminderContext())
                {
                    context.Reminders.Add(newReminder);
                    context.SaveChanges();
                }

                MainWindow.UpdateTimers(newReminder);
                MainWindow.UpdateReminder();
                this.Close();
            }
        }

        public bool ValidateNotification()
        {
            if (NotifyDateTime.Value == null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Выберите дату напоминания!");
                return false;
            }
            if (NotifyDateTime.Value < DateTime.Now)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Нельзя выбрать прошедшую дату");
                return false;
            }

            if (ReminderText.Text == "\r\n" || ReminderText.Text == "")
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Введите текст напоминания!");
                return false;
            }

            return true;
        }
    }
}
