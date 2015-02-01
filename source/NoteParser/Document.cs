using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace NoteParser
{
    public class Document
    {
        private File file;
        public File File { get { return file; } }

        public void Parse(string content)
        {
            file = JsonMapper.ToObject<File>(content);
        }
    }
}