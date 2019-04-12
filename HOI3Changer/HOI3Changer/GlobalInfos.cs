using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HOI3Changer
{
    class GlobalInfos
    {
		private string _path = @"F:\Program Files (x86)\Paradox Interactive\Hearts of Iron III";
		private static GlobalInfos _this = new GlobalInfos();
		private GlobalInfos() { }
		public static GlobalInfos getInstanz() {
			return _this;
		}
		public void setPath(string path) {
			_path = path;
		}
		public string getPath(string file) {
			if (File.Exists(_path + "\\tfh\\" + file))
				return _path + "\\tfh\\" + file;
			return _path + "\\" + file;
		}
	}
}
