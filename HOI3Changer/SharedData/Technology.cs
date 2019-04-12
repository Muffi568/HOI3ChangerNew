using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedData {
	public class Technology : Information {
		public string Typ { get; set; }
		public bool OnceTech { get; set; } = true;
		private List<WeaponCategory> WeaponCategories { get; set; } = new List<WeaponCategory>();
        public Technology(string line) : base(line) { }
        public Technology() { }
		public static ObservableCollection<Technology> getTechnologies(ThreadingObservableCollection<Technology> ret, ThreadingObservableCollection<WeaponCategory> weaponCategories) {
			GlobalInfos global = GlobalInfos.getInstance();
			TextFile techFile = global.getPath(@"\localisation\technology.csv");
			if (techFile == null)
				return null;
			bool isstart = true;
			string typ = "";
			//ObservableCollection<Technology> ret = new ObservableCollection<Technology>();
            string[] techLines = techFile.Lines;
            Regex infantryReg = new Regex("Infantry Techs");
            Regex descReg = new Regex("_desc");
			for (int i = 0; i < techLines.Length; i++) {
				string line = techLines[i];
				string[] lineparts = line.Split(';');
				if (isstart) {
					if (infantryReg.IsMatch(lineparts[0])) {
						isstart = false;
						typ = "_Infantry Technologies.txt";
					}
				} else if (!line.StartsWith("#") && i != 0) {
					if (!descReg.IsMatch(lineparts[0])) {
						Technology tech = new Technology(line);
						tech.Typ = typ;
						findTyps_new(tech, weaponCategories);
						ret.Add(tech);
					}
				} else {
					string typline = lineparts[0].Split('#')[1].Trim();
					switch (typline) {
						case "Artillery Techs":
							typ = "ArtilleryTechnologies.txt";
							break;
						case "Tech Names":
							i = techLines.Length;
							break;
						case "Naval Techs":
							typ = "Naval Technologies.txt";
							break;
						case "Armour Techs":
							typ = "Armour Technologies.txt";
							break;
						case "Aircraft Techs":
							typ = "Aircraft Technology.txt";
							break;
						case "Industry Techs":
							typ = "_Industry Technologies.txt";
							break;
						case "Secret Weapons":
							typ = "Secret Weapons.txt";
							break;
						case "Theoretical Research":
							typ = "Theories.txt";
							break;
						case "Land Doctrine Techs":
							typ = "Land Doctrines.txt";
							break;
						case "Naval Doctrine Techs":
							typ = "Naval Doctrines.txt";
							break;
						case "Air Doctrine Techs":
							typ = "Aircraftz Doctrines.txt";
							break;
					}
				}
			}
			return ret;
		}
		private static void findTyps_new(Technology t, ObservableCollection<WeaponCategory> weaponCategories) {
			GlobalInfos global = GlobalInfos.getInstance();
			TextFile typFile = global.getPath(@"technologies\" + t.Typ);
			if (typFile == null)
				return;
			int breckets = -1;
			string aktTech = "";
			List<string> List_Lines = new List<string>();
            string[] typLines = typFile.Lines;
            for(int i = 0; i < typLines.Length; i++) {
                string line = typLines[i];
				Regex r = new Regex("\\b" + t.Shortcut + "( |=)");
				if (!r.IsMatch(line))
					continue;
				aktTech = t.Shortcut;
				while (breckets != 0) {
					if (line != "" && line != "\t" && line != "\t\t") {
						if (line.IndexOf('{') != -1) {
							if (breckets != -1)
								breckets++;
							else
								breckets = 1;
						}
						if (line.IndexOf('}') != -1 && aktTech != "") 
							breckets--;
						List_Lines.Add(line.Split('=')[0].Trim());
					}
                    i++;
                    if(i < typLines.Length)
                        line = typLines[i];
				}
				if (List_Lines.Count > 0)
					break;
			}
			int tmpBreckets = 0;
			bool allow = false;
			foreach(string line in List_Lines) { 
				if (line.IndexOf('{') != -1) {
					if (breckets != -1)
						breckets++;
					else
						breckets = 1;
				}
				if (line.IndexOf('}') != -1) 
					breckets--;
				if (!allow) {
					Regex r = new Regex("additional_offset");
					if (r.IsMatch(line))
						t.OnceTech = false;
					else {
						r = new Regex("allow = {");
						if (r.IsMatch(line)) {
							allow = true;
							tmpBreckets = breckets;
						} else if (weaponCategories.Any(x => x.Shortcut == line)) {
							WeaponCategory wt = weaponCategories.First(x => x.Shortcut == line);
							wt.ListTechnologies.Add(t);
							t.WeaponCategories.Add(wt);
						}
					}
				}
				if (tmpBreckets == breckets)
					allow = false;
			}
		}
	}
}
