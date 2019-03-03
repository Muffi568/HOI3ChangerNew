using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedData {
	public class Country:Information {

		public static List<Country> getCountries() {
			GlobalInfos global = GlobalInfos.getInstance();
			TextFile countryFile = global.getPath(@"\localisation\countries.csv");
			if (countryFile == null)
				return null;
			List<Country> ret = new List<Country>();
            string[] countryLines = countryFile.Lines;
			for (int i = 1; i < countryLines.Length; i++) {
				string line = countryLines[i];
				string[] lineparts = line.Split(';');
                if (lineparts[0].StartsWith("#"))
                    continue;
				Country c = new Country();
				c.Shortcut = lineparts[0];
				c.English = lineparts[1];
				c.French = lineparts[2];
				c.German = lineparts[3];
				c.Spanish = lineparts[5];
				ret.Add(c);
			}
			return ret;
		}
	}
}
