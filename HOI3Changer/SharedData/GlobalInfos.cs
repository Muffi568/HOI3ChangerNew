using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharedData
{
    class GlobalInfos
    {
        //private string _path = @"F:\Program Files (x86)\Paradox Interactive\Hearts of Iron III";
        private string _path = @"D:\Program Files (x86)\GOG Galaxy\Games\Hearts of Iron III";

        private static GlobalInfos _this = new GlobalInfos();
        private List<TextFile> _readedTextFiles = new List<TextFile>();
		private GlobalInfos() { }
		public static GlobalInfos getInstance() {
			return _this;
		}
		public void setPath(string path) {
			_path = path;
		}
		public TextFile getPath(string filename) {
            string filePath = _path;
            if (File.Exists(filePath + "\\tfh\\" + filename))
                filePath += "\\tfh\\" + filename;
            else if (File.Exists(filePath + "\\" + filename))
                filePath += "\\" + filename;
            else
                return null;
            TextFile file = null;
            if (_readedTextFiles.Count != 0 && _readedTextFiles.Any(x => x.Path == filePath))
                file = _readedTextFiles.First(x => x.Path == filePath);
            if (file == null) {
                file = new TextFile(filePath);
                _readedTextFiles.Add(file);
            }
            return file;
		}
	}
}
