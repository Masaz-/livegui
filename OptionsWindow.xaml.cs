using System.Windows;

namespace livegui
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }

        private void check_closeLauncher_Loaded(object sender, RoutedEventArgs e)
        {
            check_closeLauncher.IsChecked = Properties.Settings.Default.closeLauncher == true ? true : false;
        }

        private void check_alarmFocus_Loaded(object sender, RoutedEventArgs e)
        {
            check_alarmFocus.IsChecked = Properties.Settings.Default.alarmFocus == true ? true : false;
        }

        private void check_useMediaPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            check_useMediaPlayer.IsChecked = Properties.Settings.Default.useCustomMediaPlayer == true ? true : false;

            if (check_useMediaPlayer.IsChecked == true)
            {
                tb_mediaPlayerLocation.IsEnabled = true;
                btn_selectMediaPlayerLocation.IsEnabled = true;
            }

            else
            {
                tb_mediaPlayerLocation.IsEnabled = false;
                btn_selectMediaPlayerLocation.IsEnabled = false;
            }
        }

        private void tb_mediaPlayerLocation_Loaded(object sender, RoutedEventArgs e)
        {
            tb_mediaPlayerLocation.Text = Properties.Settings.Default.mediaPlayerLocation;
        }

        private void tb_livestreamerLocation_Loaded(object sender, RoutedEventArgs e)
        {
            tb_livestreamerLocation.Text = Properties.Settings.Default.livestreamerLocation;
        }

        private void tb_launchParameters_Loaded(object sender, RoutedEventArgs e)
        {
            tb_launchParameters.Text = Properties.Settings.Default.launchParameters;
        }

        private void check_closeLauncher_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.closeLauncher = check_closeLauncher.IsChecked == true ? true : false;
        }

        private void check_alarmFocus_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.alarmFocus = check_alarmFocus.IsChecked == true ? true : false;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reload();
            Close();
        }

        private void check_useMediaPlayer_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.useCustomMediaPlayer = check_useMediaPlayer.IsChecked == true ? true : false;

            if (check_useMediaPlayer.IsChecked == true)
            {
                tb_mediaPlayerLocation.IsEnabled = true;
                btn_selectMediaPlayerLocation.IsEnabled = true;
            }

            else
            {
                tb_mediaPlayerLocation.IsEnabled = false;
                btn_selectMediaPlayerLocation.IsEnabled = false;
            }
        }

        private void btn_selectMediaPlayerLocation_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.InitialDirectory = Properties.Settings.Default.livestreamerLocation;
            dialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            dialog.Multiselect = false;
            dialog.Title = "Set Media Player location";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_mediaPlayerLocation.Text = dialog.FileName;
                Properties.Settings.Default.mediaPlayerLocation = dialog.FileName;
            }
        }

        private void btn_selectLivestreamerLocation_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.InitialDirectory = Properties.Settings.Default.livestreamerLocation;
            dialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            dialog.Multiselect = false;
            dialog.Title = "Set Livestreamer location";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_livestreamerLocation.Text = dialog.FileName;
                Properties.Settings.Default.livestreamerLocation = dialog.FileName;
            }
        }

        private void tb_mediaPlayerLocation_GotFocus(object sender, RoutedEventArgs e)
        {
            btn_selectMediaPlayerLocation_Click(sender, e);
        }

        private void tb_livestreamerLocation_GotFocus(object sender, RoutedEventArgs e)
        {
            btn_selectLivestreamerLocation_Click(sender, e);
        }

        private void tb_mediaPlayerLocation_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.mediaPlayerLocation = tb_mediaPlayerLocation.Text;
        }

        private void tb_livestreamerLocation_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.livestreamerLocation = tb_livestreamerLocation.Text;
        }

        private void tb_launchParameters_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.launchParameters = tb_launchParameters.Text;
        }

        private void launchParaLink_Loaded(object sender, RoutedEventArgs e)
        {
            launchParaLink.NavigateUri = new System.Uri(MainWindow.livestreamerParams);
        }

        private void launchParaLink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
