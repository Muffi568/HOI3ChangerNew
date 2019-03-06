using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedData {
    public class Technologyname : Information {
        public string Stage { get; set; }
        public Country Country { get; set; }
        public Technology Technology { get; set; }
        public static ObservableCollection<Technologyname> getTechnologynames(ObservableCollection<Country> countries, ObservableCollection<Technology> technologies) {
			ObservableCollection<Technologyname> ret = new ObservableCollection<Technologyname>();
            GlobalInfos global = GlobalInfos.getInstance();
            bool isstart = true;
            TextFile technameFile = global.getPath(@"\localisation\technology.csv");
			if (technameFile == null)
				return null;
            string[] technameLines = technameFile.Lines;
            //for (int i = 0; i < technameLines.Length; i++) {
            //    string line = technameLines[i];
			foreach(string line in technameLines) { 
                string[] lineparts = line.Split(';');
                if (isstart) {
                    if (lineparts[0] == "# Tech Names") 
                        isstart = false;
                } else {
                    if (lineparts[0] == "# Building Names")
                        break;
                    else {
                        if (line.StartsWith("#"))
                            continue;
                        Technologyname t = new Technologyname();
                        string name = lineparts[0];
                        string[] nameparts = name.Split('_');
                        if (!countries.Any(x => x.Shortcut == nameparts[0]))
                            continue;
                        t.Country = countries.First(x => x.Shortcut == nameparts[0]);
                        string Technology = "";
                        for (int j = 1; j < nameparts.Length - 1; j++) {
                            Technology += nameparts[j];
                            if (j != nameparts.Length - 2)
                                Technology += "_";
                        }
                        if (!technologies.Any(x => x.Shortcut == Technology))
                            continue;
                        t.Technology = technologies.First(x => x.Shortcut == Technology);
                        t.Stage = (nameparts[nameparts.Length - 1]);
                        t.Shortcut = lineparts[0];
                        t.English = lineparts[1];
                        t.French = lineparts[2];
                        t.German = lineparts[3];
                        t.Spanish = lineparts[5];
                        ret.Add(t);
                    }
                }
            }
            return ret;
        }
    }
}
