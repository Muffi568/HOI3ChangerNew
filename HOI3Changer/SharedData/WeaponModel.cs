using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedData {
	class WeaponModel : Information {
		private string Stage { get; set; }
		private Country Country { get; set; }
		private WeaponCategory WeaponCategory { get; set; }
		private List<string[]> Skills { get; set; } = new List<string[]>();

		public static List<WeaponModel> getWeaponModels(List<Country> countries, List<WeaponCategory> categories) {
			List<WeaponModel> ret = new List<WeaponModel>();
			GlobalInfos global = GlobalInfos.getInstance();
			TextFile modelFile = global.getPath(@"localisation\models.csv");
			if (modelFile == null)
				return null;
            string[] modelLines = modelFile.Lines;
            for (int i = 1; i < modelLines.Length; i++) {
				string line = modelLines[i];
                string[] lineparts = line.Split(';');
                if (lineparts[0].StartsWith("#"))
                    continue;
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
