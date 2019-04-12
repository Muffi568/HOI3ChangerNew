using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace SharedData {
    public class WeaponCategory : Information {
        public List<Technology> ListTechnologies { get; set; } = new List<Technology>();
        public enum typ { land, naval, air };
        private typ _Typ;
        public void setTyp(string Typ) {
            if (Typ == typ.land.ToString()) {
                _Typ = typ.land;
            }
            if (Typ == typ.naval.ToString()) {
                _Typ = typ.naval;
            }
            if (Typ == typ.air.ToString()) {
                _Typ = typ.air;
            }
        }
        public typ getTyp() {
            return _Typ;
        }
        public WeaponCategory(string line) : base(line) { }
        public WeaponCategory() { }
        public static void/*ObservableCollection<WeaponCategory>*/ getWeaponCategories(ThreadingObservableCollection<WeaponCategory> ret) {
			//ObservableCollection<WeaponCategory> ret = new ObservableCollection<WeaponCategory>();
            bool unitNames = false;
            GlobalInfos global = GlobalInfos.getInstance();
            TextFile unitFile = global.getPath(@"\localisation\units.csv");
            if (unitFile == null)
                return;
            string[] typParts;// = line.Split(';');
            string[] unitLines = unitFile.Lines;
            for (int i = 0; i < unitLines.Length; i++) {
                string line = unitLines[i];
                if (!line.StartsWith("#") && unitNames) {
                    //if (!lineparts[0].EndsWith("short")) {
                        WeaponCategory c = new WeaponCategory(line);
                        string name = c.Shortcut;
                        TextFile typFile = global.getPath(@"units\" + name + ".txt");
                        if (typFile == null)
                            typFile = global.getPath(@"units\" + name.Split('_')[0] + ".txt");
                        //if (typFile == null) {
                        //    string[] typpart = name.Split('_');
                        //    typFile = global.getPath(@"units\" + typpart[0] + "_" + typpart[1] + ".txt");
                        //}
                        if (typFile == null)
                            continue;
                        string[] typLines = typFile.Lines;
                        Regex r;
                        foreach (string typLine in typLines) {
                            r = new Regex("type");
                            if (r.IsMatch(typLine)) {
                                typParts = typLine.Split('=');
                                c.setTyp(typParts[1].Trim());
                                break;
                            }
                        }
                        ret.Add(c);
                    //}
                } else if (line.StartsWith("# Unit Names")) {
                    unitNames = true;
                    i++;
                } else if (unitNames)
                    break;
            }
        }
    }
}
