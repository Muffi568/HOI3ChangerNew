using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData {
	public class BundleData {
		private ObservableCollection<Country> countries;
		private ObservableCollection<WeaponCategory> categories;
		private ObservableCollection<WeaponModel> models;
		private ObservableCollection<Technology> technologies;
		private ObservableCollection<Technologyname> technologynames;
		public ObservableCollection<Country> Countries { get { return countries; } }
		public ObservableCollection<WeaponCategory> Categories { get { return categories; } }
		public ObservableCollection<WeaponModel> Models { get { return models; } }
		public ObservableCollection<Technology> Technologies { get { return technologies; } }
		public ObservableCollection<Technologyname> Technologynames { get { return technologynames; } }
		public BundleData(string path) {
			GlobalInfos global = GlobalInfos.getInstance();
			global.setPath(path);
			countries = Country.getCountries();
			categories = WeaponCategory.getWeaponCategories();
			models = WeaponModel.getWeaponModels(countries, categories);
			technologies = Technology.getTechnologies(categories);
			technologynames = Technologyname.getTechnologynames(countries, technologies);
		}
	}
}
