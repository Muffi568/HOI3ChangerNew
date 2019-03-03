using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData {
	public class BundleData {
		private List<Country> countries;
		private List<WeaponCategory> categories;
		private List<WeaponModel> models;
		private List<Technology> technologies;
		private List<Technologynames> technologynames;
		public BundleData(string path) {
			GlobalInfos global = GlobalInfos.getInstance();
			global.setPath(path);
			countries = Country.getCountries();
			categories = WeaponCategory.getWeaponCategories();
			models = WeaponModel.getWeaponModels(countries, categories);
			technologies = Technology.getTechnologies(categories);
			technologynames = Technologynames.getTechnologynames(countries, technologies);
		}
	}
}
