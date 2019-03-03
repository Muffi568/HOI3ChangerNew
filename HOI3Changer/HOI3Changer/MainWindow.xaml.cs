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
using SharedData;

namespace HOI3Changer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string _path = @"F:\Program Files (x86)\Paradox Interactive\Hearts of Iron III";
        private string _path = @"D:\Program Files (x86)\GOG Galaxy\Games\Hearts of Iron III";
        public MainWindow()
        {
            InitializeComponent();
			BundleData bundle = new BundleData(_path);

        }
    }
}
