using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SharedData {
    public class TextFile {
        private string _path = "";
        private List<string> _trimedLines = new List<string>();
        public TextFile(string path) {
            _path = path;
            readFile();
        }
        private void readFile() {
            StreamReader reader = new StreamReader(_path, Encoding.UTF7);
            while (reader.Peek() > 0) {
                string line = reader.ReadLine().Trim();
                if (line != "" && line != " " && line != "\t")
                    _trimedLines.Add(line);
            }
            reader.Close();
        }
        public string Path {
            get { return _path; }
        }
        public string[] Lines {
            get { return _trimedLines.ToArray(); }
        }
    }
}
