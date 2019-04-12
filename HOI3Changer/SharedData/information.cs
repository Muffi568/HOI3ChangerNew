using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData {
	public abstract class Information: INotifyPropertyChanged {
		private string _shortcut = "";
		public string Shortcut { get { return _shortcut; }
			set {
				_shortcut = value;
				OnPropertyChanged("Shortcut");
			}
		}
		public string English { get; set; }
		public string German { get; set; }
		public string French { get; set; }
		public string Spanish { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string property) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
        public Information() { }
        public Information (string line) {
            string[] splittedLine = line.Split(';');
            if (splittedLine.Length < 6)
                return;
            Shortcut = splittedLine[0];
            English = splittedLine[1];
            French = splittedLine[2];
            German = splittedLine[3];
            Spanish = splittedLine[5];
        }
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
