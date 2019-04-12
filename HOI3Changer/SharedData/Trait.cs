using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedData {
	public class Trait:Information {
        public Trait(string line) : base(line) { }
		public static List<Trait> getTraits() {
			List<Trait> ret = new List<Trait>();
			GlobalInfos global = GlobalInfos.getInstance();
			//string typpath = global.getPath() + @"\units\";
			TextFile unitFile = global.getPath(@"\localisation\units.csv");
			if (unitFile == null)
				return null;
			var unitLines = unitFile.Lines;
			bool traitNames = false;
            /*for (int i = 0; i < unitLines.Count(); i++) {
				string line = unitLines[i];*/
            foreach (string line in unitLines) {
                if (!line.StartsWith("#") && traitNames) {
                    Trait c = new Trait(line);
                    ret.Add(c);
                } else if (line.StartsWith("# Trait Names")) {
                    traitNames = true;
                } else if (line.StartsWith("# Unit Names"))
                    break;
            }
			return ret;
		}
	}
}
