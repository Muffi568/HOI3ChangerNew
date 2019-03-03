using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData {
	public abstract class Information {
		public string Shortcut { get; set; }
		public string English { get; set; }
		public string German { get; set; }
		public string French { get; set; }
		public string Spanish { get; set; }
		public override string ToString() {
			//globalInfos global = globalInfos.getInstanz();
			//Languages.languages language = global.getLanguage();
			int language = 1;
			if ((int)(language) == 0)
				return English;
			else if ((int)(language) == 1)
				return German;
			else if ((int)(language) == 2)
				return French;
			else if ((int)(language) == 3)
				return Spanish;
			else
				return "error";
		}
	}
}
