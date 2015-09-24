using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace livegui
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private List<string> list = new List<string>();
        private int slctd = -1;
        private int slctdTmp = -1;
        private string ListKey = "";

        public EditWindow(string List)
        {
            InitializeComponent();

            ListKey = List;
        }

        private void CheckUrlCount()
        {
            if (lb_urls.Items.Count > 0 && lb_urls.SelectedIndex == -1)
                lb_urls.SelectedIndex = 0;

            if (lb_urls.SelectedIndex != -1)
            {
                btn_add.IsEnabled = true;
                btn_edit.IsEnabled = true;
                btn_delete.IsEnabled = true;
            }

            else
            {
                btn_add.IsEnabled = false;
                btn_edit.IsEnabled = false;
                btn_delete.IsEnabled = false;
            }
        }

        private void txt_urlCount_Loaded(object sender, RoutedEventArgs e)
        {
            list = Properties.Settings.Default[ListKey].ToString().Split('|').ToList();
            lb_urls.ItemsSource = list;

            txt_urlCount.Text = list.Count + " " + (list.Count() == 1 ? "Item" : "Items");
        }

        private void lb_urls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_urls.SelectedIndex != -1)
            {
                btn_add.IsEnabled = true;
                btn_edit.IsEnabled = true;
                btn_delete.IsEnabled = true;
                tb_url.Text = lb_urls.SelectedItem.ToString();
            }

            slctd = lb_urls.SelectedIndex;
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            list.RemoveAt(lb_urls.SelectedIndex);
            lb_urls.ItemsSource = null;
            lb_urls.ItemsSource = list;
            tb_url.Text = "";

            CheckUrlCount();

            Properties.Settings.Default[ListKey] = String.Join("|", list);
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            list.Add(tb_url.Text);

            lb_urls.ItemsSource = null;
            lb_urls.ItemsSource = list;
            tb_url.Text = "";

            CheckUrlCount();

            Properties.Settings.Default[ListKey] = String.Join("|", list);
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            slctdTmp = slctd;
            list[slctd] = tb_url.Text;

            lb_urls.ItemsSource = null;
            lb_urls.ItemsSource = list;
            lb_urls.SelectedIndex = slctdTmp;
            tb_url.Text = "";

            CheckUrlCount();

            Properties.Settings.Default[ListKey] = String.Join("|", list);
        }

        private void tb_url_KeyUp(object sender, KeyEventArgs e)
        {
            if (tb_url.Text.Length > 0)
                btn_add.IsEnabled = true;

            else
                btn_add.IsEnabled = false;
        }

        private void pluginsPara_Loaded(object sender, RoutedEventArgs e)
        {
            pluginsPara.NavigateUri = new Uri(MainWindow.livestreamerPlugins);
        }

        private void pluginsPara_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
