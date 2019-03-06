using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedData {
	public class Country:Information {

		public static ObservableCollection<Country> getCountries() {
			GlobalInfos global = GlobalInfos.getInstance();
			TextFile countryFile = global.getPath(@"\localisation\countries.csv");
			if (countryFile == null)
				return null;
			ObservableCollection<Country> ret = new ObservableCollection<Country>();
            string[] countryLines = countryFile.Lines;
			//Parallel.For(1, countryLines.Length, i => {
			for (int i = 1; i < countryLines.Length; i++) {
				string line = countryLines[i];
				if (line.StartsWith("#"))
					continue;
				string[] lineparts = line.Split(';');
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
