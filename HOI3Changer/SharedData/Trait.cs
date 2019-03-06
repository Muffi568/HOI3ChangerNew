using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedData {
	public class Trait:Information {

		public static List<Trait> getTraits() {
			List<Trait> ret = new List<Trait>();
			GlobalInfos global = GlobalInfos.getInstance();
			//string typpath = global.getPath() + @"\units\";
			TextFile unitFile = global.getPath(@"\localisation\units.csv");
			if (unitFile == null)
				return null;
			string[] unitLines = unitFile.Lines;
			bool traitNames = false;
			for (int i = 0; i < unitLines.Length; i++) {
				string line = unitLines[i];
				string[] lineparts = line.Split(';');
				if (!lineparts[0].StartsWith("#") && traitNames) {
					Regex r = new Regex("short");
					if (!r.IsMatch(lineparts[0])) {
						Trait c = new Trait();
						string name = lineparts[0];
						c.Shortcut = name;
						c.English = lineparts[1];
						c.French = lineparts[2];
						c.German = lineparts[3];
						c.Spanish = lineparts[5];
						ret.Add(c);
					}
				} else if (lineparts[0] == "# Trait Names") {
					traitNames = true;
					i++;
				} else if (lineparts[0] == "# Unit Names") 
					break;
			}
			return ret;
		}
	}
}
