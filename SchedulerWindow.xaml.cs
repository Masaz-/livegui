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

namespace livegui
{
    /// <summary>
    /// Interaction logic for Scheduler.xaml
    /// </summary>
    public partial class SchedulerWindow : Window
    {
        public string Hours { get; set; }
        public string Minutes { get; set; }
        public int Action { get; set; }
        public bool ScheduleActive { get; set; }

        public SchedulerWindow()
        {
            Hours = "00";
            Minutes = "00";
            Action = 0;
            ScheduleActive = false;
            InitializeComponent();
        }

        public SchedulerWindow(int Hour, int Minute, int Actiond)
        {
            Hours = Hour.ToString().PadLeft(2, '0');
            Minutes = Minute.ToString().PadLeft(2, '0');
            Action = Actiond;
            ScheduleActive = true;

            InitializeComponent();
        }

        private void combo_hours_Loaded(object sender, RoutedEventArgs e)
        {
            int index = 0;

            for (int i = 0; i < 24; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = i.ToString().PadLeft(2, '0');

                combo_hours.Items.Add(cbi);

                if (cbi.Content.ToString() == Hours)
                    index = i;
            }

            combo_hours.SelectedIndex = index;
        }

        private void combo_minutes_Loaded(object sender, RoutedEventArgs e)
        {
            int index = 0;

            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0)
                {
                    ComboBoxItem cbi = new ComboBoxItem();
                    cbi.Content = i.ToString().PadLeft(2, '0');
                    combo_minutes.Items.Add(cbi);

                    if (cbi.Content.ToString() == Minutes)
                        index = i;
                }
            }

            combo_minutes.SelectedIndex = index;
        }

        private void combo_start_Loaded(object sender, RoutedEventArgs e)
        {
            combo_start.SelectedIndex = Action;
        }

        private void btn_active_Click(object sender, RoutedEventArgs e)
        {
            ScheduleActive = true;
            Close();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ScheduleActive = false;
            Close();
        }
    }
}
