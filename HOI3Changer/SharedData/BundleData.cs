using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;

namespace SharedData {
	public class BundleData {
		public ThreadingObservableCollection<Country> Countries { get; set; }
        public ThreadingObservableCollection<WeaponCategory> Categories { get; set; }
        public ThreadingObservableCollection<WeaponModel> Models { get; set; }
		public ThreadingObservableCollection<Technology> Technologies { get; set; }
        public ThreadingObservableCollection<Technologyname> Technologynames { get; set; }
        public BundleData(string path, Dispatcher dispatcher) {
			GlobalInfos global = GlobalInfos.getInstance();
			global.setPath(path);
            Countries = new ThreadingObservableCollection<Country>(dispatcher);
            Categories = new ThreadingObservableCollection<WeaponCategory>(dispatcher);
            Models = new ThreadingObservableCollection<WeaponModel>(dispatcher);
            Technologies = new ThreadingObservableCollection<Technology>(dispatcher);
            Technologynames = new ThreadingObservableCollection<Technologyname>(dispatcher);
            if (dispatcher != null) {
                Thread t = new Thread(tasks);
                t.Start();
            } else
                tasks();
		}
        public void tasks() {
            /*Countries = */Country.getCountries(Countries);
            WeaponCategory.getWeaponCategories(Categories);
            /*Technologies = */Technology.getTechnologies(Technologies, Categories);
            /*Technologynames = */Technologyname.getTechnologynames(Technologynames, Countries, Technologies);
            /*Models = */WeaponModel.getWeaponModels(Models, Countries, Categories, Technologies);
            Models.Add(new WeaponModel());
            System.Diagnostics.Debug.WriteLine("Fertig");
        }
	}
}
