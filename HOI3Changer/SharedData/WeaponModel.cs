using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SharedData {
	public class WeaponModel : Information {
		private Country _country;
		private WeaponCategory _weaponCategory;
		private string _stage;
		public string Stage {
			get { return _stage; }
			set {
				if (_stage != value) {
					_stage = value;
					setShortcut();
				}
			}
		}
		public Country Country {
			get { return _country; }
			set {
				if (_country != value) {
					_country = value;
					setShortcut();
				}
			}
		}
		public WeaponCategory WeaponCategory {
			get { return _weaponCategory; }
			set {
				if (_weaponCategory != value) {
					_weaponCategory = value;
					setShortcut();
				}
			}
		}
		public List<string[]> Skills { get; set; } = new List<string[]>();

		private void setShortcut() {
			if (_country != null && WeaponCategory != null)
				Shortcut = _country.Shortcut + "_" + WeaponCategory.Shortcut + "_" + Stage;
		}
		public static ObservableCollection<WeaponModel> getWeaponModels(ObservableCollection<Country> countries, ObservableCollection<WeaponCategory> categories) {
			ObservableCollection<WeaponModel> ret = new ObservableCollection<WeaponModel>();
			GlobalInfos global = GlobalInfos.getInstance();
			TextFile modelFile = global.getPath(@"localisation\models.csv");
			if (modelFile == null)
				return null;
            string[] modelLines = modelFile.Lines;
			//Parallel.For(1, modelLines.Length, i => { 
            for (int i = 1; i < modelLines.Length; i++) {
				string line = modelLines[i];
				if (line.StartsWith("#"))
					continue;
				string[] lineparts = line.Split(';');
                WeaponModel c = new WeaponModel();
                string name = lineparts[0];
                string[] nameparts = name.Split('_');
                c.Country = countries.First(x => x.Shortcut == nameparts[0]);
                string Weapontyp = "";
                for (int j = 1; j < nameparts.Length - 1; j++) {
                    Weapontyp += nameparts[j];
                    if (j != nameparts.Length - 2)
                        Weapontyp += "_";
                }
                if (categories.Any(x => x.Shortcut == Weapontyp)) {
                    c.WeaponCategory = categories.First(x => x.Shortcut == Weapontyp);
                    c.Stage = nameparts[nameparts.Length - 1];
                    c.Shortcut = name;
                    c.English = lineparts[1];
                    c.French = lineparts[2];
                    c.German = lineparts[3];
                    c.Spanish = lineparts[5];
                    ret.Add(c);
                }
            }
			return ret;
		}
	}
}
