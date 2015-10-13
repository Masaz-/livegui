using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace livegui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> urls = new List<string>();
        private List<string> plugins = new List<string>();
        private List<string> qualities = new List<string>();

        // Livestreamer URLs
        public static string livestreamerUrl = "http://docs.livestreamer.io/"; 
        public static string livestreamerParams = "http://docs.livestreamer.io/cli.html";
        public static string livestreamerPlugins = "http://docs.livestreamer.io/plugin_matrix.html";

        public DispatcherTimer Timer = new DispatcherTimer();
        public bool ScheduleSet = false;
        public DateTime ScheduleDT = new DateTime();
        public int Action = 0;

        public MainWindow()
        {
            InitializeComponent();
            Timer.Tick += Timer_Tick;

            urls = Properties.Settings.Default.urls.ToString().Split('|').ToList();
            plugins = Properties.Settings.Default.plugins.ToString().Split('|').ToList();
            qualities = Properties.Settings.Default.qualities.ToString().Split('|').ToList();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Stop();
            ScheduleSet = false;
            lbl_scheduleText.Content = "";

            if (Action == 0)
                LaunchStream();

            else if (Action == 1)
            {
                System.Media.SystemSounds.Hand.Play();

                if (Properties.Settings.Default.alarmFocus)
                   Activate();

                MessageBox.Show("Event is starting!", "Alert!");
            }
        }

        private void cb_url_Loaded(object sender, RoutedEventArgs e)
        {
            cb_url.Focus();

            cb_url.ItemsSource = null;
            cb_url.ItemsSource = urls;
        }

        private void cb_quality_Loaded(object sender, RoutedEventArgs e)
        {
            cb_quality.ItemsSource = null;
            cb_quality.ItemsSource = qualities;

            if (cb_quality.Items.Count > 0)
                cb_quality.SelectedIndex = 0;
        }

        private void SaveUrl()
        {
            bool addUrl = true;

            if (cb_url.Text.Trim() == "")
                return;

            foreach (string s in urls)
            {
                if (s == cb_url.Text)
                    addUrl = false;
            }

            if (addUrl)
            {
                urls.Add(cb_url.Text);

                urls = urls.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

                cb_url.ItemsSource = null;
                cb_url.ItemsSource = urls;

                Properties.Settings.Default.urls = string.Join("|", urls);
                Properties.Settings.Default.Save();
            }
        }

        private void SaveQuality()
        {
            bool addQuality = true;

            if (cb_quality.Text.Trim() == "")
                return;

            foreach (string q in qualities)
            {
                if (q == cb_quality.Text)
                    addQuality = false;
            }

            if (addQuality)
            {
                ComboBoxItem cbi = new ComboBoxItem();

                cbi.Content = cb_quality.Text;
                qualities.Add(cb_quality.Text);

                Properties.Settings.Default.qualities = string.Join("|", qualities);
                Properties.Settings.Default.Save();
            }
        }

        private void LaunchStream()
        {
            if (cb_url.Text.Length > 2 && plugins.Any(x => cb_url.Text.Contains(x)))
            {
                string url = cb_url.Text;
                string quality = cb_quality.Text.ToLower();

                url = url.Replace("http://www.", "");
                url = url.Replace("https://www.", "");
                url = url.Replace("www.", "");

                try
                {
                    string args = "";

                    ProcessStartInfo startInfo = new ProcessStartInfo(Properties.Settings.Default.livestreamerLocation);

                    startInfo.WindowStyle = ProcessWindowStyle.Minimized;
  
                    if (Properties.Settings.Default.useCustomMediaPlayer)
                        args += " --player=\"" + Properties.Settings.Default.mediaPlayerLocation + "\"";

                    if (String.IsNullOrWhiteSpace(Properties.Settings.Default.launchParameters) == false)
                        args += " " + Properties.Settings.Default.launchParameters;

                    startInfo.Arguments = args + " " + url + " " + quality;

                    Process.Start(startInfo);

                    if (Properties.Settings.Default.closeLauncher)
                        Close();

                    SaveQuality();
                    SaveUrl();
                }

                catch (Exception general)
                {
                    MessageBox.Show(general.StackTrace, general.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            else
            {
                MessageBox.Show("No plugin found for URL " + cb_url.Text + "! You can try adding base URL to plugin list.", "Can't open stream!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_launch_Click(object sender, RoutedEventArgs e)
        {
            LaunchStream();
        }

        private void cb_url_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LaunchStream();
        }

        private void Scheduler_Click(object sender, RoutedEventArgs e)
        {
            SchedulerWindow schedule;

            if (ScheduleSet)
                schedule = new SchedulerWindow(ScheduleDT.Hour, ScheduleDT.Minute, Action);

            else
                schedule = new SchedulerWindow();

            schedule.Closing += schedule_Closing;
            schedule.ShowDialog();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow settings = new OptionsWindow();
            settings.ShowDialog();
        }

        private void schedule_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SchedulerWindow s = (SchedulerWindow)sender;

            if (s.ScheduleActive == true)
            {
                int hours = Int32.Parse(((ComboBoxItem)s.combo_hours.SelectedItem).Content.ToString());
                int minutes = Int32.Parse(((ComboBoxItem)s.combo_minutes.SelectedItem).Content.ToString());

                Action = s.combo_start.SelectedIndex;

                ScheduleSet = true;
                ScheduleDT = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, 0);

                if (ScheduleDT.Hour < DateTime.Now.Hour)
                    ScheduleDT = ScheduleDT.AddDays(2);

                Timer.Interval = new TimeSpan(ScheduleDT.Ticks - DateTime.Now.Ticks);
                Timer.Start();

                lbl_scheduleText.Content = "Schedule set to " + (ScheduleDT.Date > DateTime.Now ? "tomorrow" : "today") + " " + ((ComboBoxItem)s.combo_hours.SelectedItem).Content.ToString() + ":" + ((ComboBoxItem)s.combo_minutes.SelectedItem).Content.ToString();
            }

            else
            {
                lbl_scheduleText.Content = "";
                Timer.Stop();
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            using (AboutBox box = new AboutBox())
            {
                box.ShowDialog();
            }
        }

        private void Aboutlivestreamer_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(livestreamerUrl);
        }

        private void OrganizeUrls_Click(object sender, RoutedEventArgs e)
        {
            EditWindow edit = new EditWindow("urls");
            edit.Closing += Edit_Closing;
            edit.ShowDialog(); 
        }

        private void OrganizePlugins_Click(object sender, RoutedEventArgs e)
        {
            EditWindow edit = new EditWindow("plugins");
            edit.Closing += Edit_Closing;
            edit.ShowDialog();
        }

        private void OrganizeQualities_Click(object sender, RoutedEventArgs e)
        {
            EditWindow edit = new EditWindow("qualities");
            edit.Closing += Edit_Closing;
            edit.ShowDialog();
        }

        private void Edit_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            urls = Properties.Settings.Default.urls.ToString().Split('|').ToList();
            plugins = Properties.Settings.Default.plugins.ToString().Split('|').ToList();
            qualities = Properties.Settings.Default.qualities.ToString().Split('|').ToList();

            cb_url.ItemsSource = null;
            cb_url.ItemsSource = urls;

            cb_quality.ItemsSource = null;
            cb_quality.ItemsSource = qualities;

            if (cb_quality.Items.Count > 0)
                cb_quality.SelectedIndex = 0;
        }
    }
}
