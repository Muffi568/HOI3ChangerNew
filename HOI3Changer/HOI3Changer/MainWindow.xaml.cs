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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using SharedData;
using System.IO;

namespace HOI3Changer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string _path = @"F:\Program Files (x86)\Paradox Interactive\Hearts of Iron III";
        private string _path = @"D:\Program Files (x86)\GOG Galaxy\Games\Hearts of Iron III";
        public BundleData BundleData { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _path = Properties.Settings.Default.HOI3Path;
            if (!Directory.Exists(_path)) {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.Description = "Select a valid HOI3 directory!";
                folderBrowser.ShowDialog();
                _path = folderBrowser.SelectedPath;
                Properties.Settings.Default.HOI3Path = _path;
                Properties.Settings.Default.Save();
            }
            if (!Directory.Exists(_path))
                return;
            BundleData = new BundleData(_path, Dispatcher);
            weaponModelControl.BundleData = BundleData;
			//dataGrid.ItemsSource = bundle.Models;
			DataContext = BundleData;
        }
    }
}
