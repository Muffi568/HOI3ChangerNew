using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SharedData {
    class WeaponCategory : Information {
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
        public static List<WeaponCategory> getWeaponCategories() {
            List<WeaponCategory> ret = new List<WeaponCategory>();
            bool unitNames = false;
            GlobalInfos global = GlobalInfos.getInstance();
            TextFile unitFile = global.getPath(@"\localisation\units.csv");
            if (unitFile == null)
                return null;
            string[] unitLines = unitFile.Lines;
            for (int i = 0; i < unitLines.Length; i++) {
                string line = unitLines[i];
                string[] lineparts = line.Split(';');
                if (!lineparts[0].StartsWith("#") && unitNames) {
                    Regex r = new Regex("short");
                    if (!r.IsMatch(lineparts[0])) {
                        WeaponCategory c = new WeaponCategory();
                        string name = lineparts[0];
                        c.Shortcut = name;
                        c.English = lineparts[1];
                        c.French = lineparts[2];
                        c.German = lineparts[3];
                        c.Spanish = lineparts[5];
                        TextFile typFile = global.getPath(@"units\" + name + ".txt");
                        if (typFile == null)
                            typFile = global.getPath(@"units\" + name.Split('_')[0] + ".txt");
                        if (typFile == null) {
                            string[] typpart = name.Split('_');
                            typFile = global.getPath(@"units\" + typpart[0] + "_" + typpart[1] + ".txt");
                        }
                        if (typFile == null)
                            continue;
                        string[] typLines = typFile.Lines;
                        foreach (string typLine in typLines) {
                            r = new Regex("type");
                            if (r.IsMatch(typLine)) {
                                lineparts = typLine.Split('=');
                                c.setTyp(lineparts[1].Trim());
                                break;
                            }
                        }
                        ret.Add(c);
                    }
                } else if (lineparts[0] == "# Unit Names") {
                    unitNames = true;
                    i++;
                } else if (unitNames)
                    break;
            }
            return ret;
        }
    }
}
