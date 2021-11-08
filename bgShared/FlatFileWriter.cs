using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace bMobile.Shared
{
    public class FlatFileWriter
    {
        private string _fileName = "";
        private string _fileHeader = "";
        private string _fileFooter = "";
        private string _err = "";
        private List<string> _FileLines;

        public string ffwError
        {
            get { return _err; }
        }

        public FlatFileWriter(string fileName)
        {
            _fileName = fileName;
            _FileLines = new List<string>();
            //Path.
        }

        public void AddFileHeader(string header)
        {
            _fileHeader = header;
        }

        public void AddFileFooter(string footer)
        {
            _fileFooter = footer;
        }

        public void AddFileLine(string line)
        {
            _FileLines.Add(line);
        }

        public bool WriteFile()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                //----- Add header
                if (!string.IsNullOrEmpty(_fileHeader))
                    sb.AppendLine(_fileHeader);
                //-----Add all the lines
                foreach (var line in _FileLines)
                    sb.AppendLine(line);
                //-----Add footer
                if (!string.IsNullOrEmpty(_fileHeader))
                    sb.AppendLine(_fileFooter);
                //----- Write the file now
                File.WriteAllText(_fileName, sb.ToString());
                return true;
            }
            catch (Exception ex)
            {
                _err = ex.Message;
                return false;
            }
        }
    }
}
