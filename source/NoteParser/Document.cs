using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace NoteParser
{
    public class Document
    {
        public class File
        {
            public Header header;
            public NoteList notes;
        }

        public class Header
        {
            public string songName;
            public string author;
            public int difficulty;
        }

        public class NoteList
        {

        }

        public void Parse(string content)
        {
            var file = JsonMapper.ToObject<File>(content);
        }
    }
}