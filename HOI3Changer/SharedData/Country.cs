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
        public Country(string line) : base(line) { }
        public Country() { }
		public static ObservableCollection<Country> getCountries(ThreadingObservableCollection<Country> ret) {
			GlobalInfos global = GlobalInfos.getInstance();
			TextFile countryFile = global.getPath(@"\localisation\countries.csv");
			if (countryFile == null)
				return null;
			//ObservableCollection<Country> ret = new ObservableCollection<Country>();
            var countryLines = countryFile.Lines.Where(x => !x.StartsWith("#"));
			for (int i = 1; i < countryLines.Count(); i++) {
				Country c = new Country(countryLines.ElementAt(i));
				ret.Add(c);
			}
			return ret;
		}
	}
}
