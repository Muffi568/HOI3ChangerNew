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

namespace HOI3Changer {
    /// <summary>
    /// Interaktionslogik für WeaponModelControl.xaml
    /// </summary>
    public partial class WeaponModelControl : UserControl {
        private BundleData _bundleData;
        public BundleData BundleData {
            get { return _bundleData; }
            set {
                _bundleData = value;
                DataContext = BundleData;
            }
        }
        public WeaponModelControl() {
            InitializeComponent();
        }

        private void dataGridModels_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            dataGridSkills.DataContext = dataGridModels.SelectedItem;
        }

        private void butDelete_Click(object sender, RoutedEventArgs e) {
            var selectedItem = dataGridModels.SelectedItem;
            while (selectedItem != null) {
                BundleData.Models.Remove((WeaponModel)selectedItem);
                selectedItem = dataGridModels.SelectedItem;
            }
        }

        private void butAdd_Click(object sender, RoutedEventArgs e) {
            if (BundleData.Models.Count > 0)
                dataGridModels.ScrollIntoView(BundleData.Models.Last());
            BundleData.Models.Add(new WeaponModel());
        }
    }
}
