using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows.Forms;

namespace SharedData {
	public class WeaponModel : Information {
		private Country _country;
		private WeaponCategory _weaponCategory;
		private string _stage;
        //private List<Skill> _skillList = new List<Skill>();
        public WeaponModel() {}
        public WeaponModel(string line = "") : base(line) {}
		public string Stage {
			get { return _stage; }
			set {
				if (_stage != value) {
					_stage = value;
					setShortcut();
					refreshSkillList();
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
					refreshSkillList();
				}
			}
		}
		public ObservableCollection<Skill> Skills { get; set; } = new ObservableCollection<Skill>();

		private void setShortcut() {
			if (_country != null && WeaponCategory != null)
				Shortcut = _country.Shortcut + "_" + WeaponCategory.Shortcut + "_" + Stage;
		}
		private void refreshSkillList() {
			Skills.Clear();
            if (_weaponCategory == null)
                return;
            foreach (Technology t in _weaponCategory.ListTechnologies) {
				Skill s = new Skill();
				s.Stage = Stage;
				s.Technology = t;
				Skills.Add(s);
			}
		}
        public void refreshSkillList(List<Skill> skillList) {
            Skills.Clear();
            foreach(Skill s in skillList) 
                Skills.Add(s);
        }
		public static ObservableCollection<WeaponModel> getWeaponModels(ThreadingObservableCollection<WeaponModel> ret, ThreadingObservableCollection<Country> countries, ThreadingObservableCollection<WeaponCategory> categories, ThreadingObservableCollection<Technology> technologies) {
			//ObservableCollection<WeaponModel> ret = new ObservableCollection<WeaponModel>();
			GlobalInfos global = GlobalInfos.getInstance();
			TextFile modelFile = global.getPath(@"localisation\models.csv");
			if (modelFile == null)
				return null;
            string[] modelLines = modelFile.Lines;
            for (int i = 1; i < modelLines.Length; i++) {
				string line = modelLines[i];
				if (line.StartsWith("#"))
					continue;
                WeaponModel model = new WeaponModel(line);
                string[] nameparts = model.Shortcut.Split('_');
                model.Country = countries.First(x => x.Shortcut == nameparts[0]);
                string Weapontyp = "";
                for (int j = 1; j < nameparts.Length - 1; j++) {
                    Weapontyp += nameparts[j];
                    if (j != nameparts.Length - 2)
                        Weapontyp += "_";
                }
                model.Stage = nameparts[nameparts.Length - 1];
                if (categories.Any(x => x.Shortcut == Weapontyp)) {
                    model.WeaponCategory = categories.First(x => x.Shortcut == Weapontyp);
                    ret.Add(model);
                }
            }
            foreach(Country c in countries) {
                readNeededSkills(WeaponCategory.typ.air, c, ret, technologies);
                readNeededSkills(WeaponCategory.typ.land, c, ret, technologies);
                readNeededSkills(WeaponCategory.typ.naval, c, ret, technologies);
            }
			return ret;
		}
        private static void readNeededSkills(WeaponCategory.typ typ, Country country, ThreadingObservableCollection<WeaponModel> weaponModels, ThreadingObservableCollection<Technology> technologies) {
            GlobalInfos global = GlobalInfos.getInstance();
            string filename = @"units\models\" + country.Shortcut + " - ";
            switch(typ) {
                case WeaponCategory.typ.air:
                    filename += "Planes.txt";
                    break;
                case WeaponCategory.typ.land:
                    filename += "Arm.txt";
                    break;
                case WeaponCategory.typ.naval:
                    filename += "Ships.txt";
                    break;
            }
            TextFile file = global.getPath(filename);
            if (file == null)
                return;
            string[] lines = file.Lines;
            WeaponModel m = null;
            List<Skill> skillList = new List<Skill>();
            foreach (string line in lines) {
                string[] splittedLine = line.Split('=');
                if (splittedLine.Length > 1) {
                    if (line.IndexOf('{') != -1) {
                        string modelDesc = splittedLine[0].Trim();
                        if (weaponModels.Any(x => x.Country.Shortcut == country.Shortcut && x.WeaponCategory.Shortcut + "." + x.Stage == modelDesc)) 
                            m = weaponModels.First(x => x.Country.Shortcut == country.Shortcut && x.WeaponCategory.Shortcut + "." + x.Stage == modelDesc);
                        continue;
                    }
                    if (m == null)
                        continue;
                    string techShortcut = splittedLine[0].Trim();
                    if (technologies.Any(x => x.Shortcut == techShortcut)) {
                        Skill s = new Skill();
                        s.Stage = splittedLine[1].Trim();
                        s.Technology = technologies.First(x => x.Shortcut == techShortcut);
                        skillList.Add(s);
                    }
                }
                if (line.IndexOf('}') != -1) {
                    if (m != null)
                        m.refreshSkillList(skillList);
                    m = null;
                    skillList.Clear();
                    continue;
                }
            }
        }
	}
}
