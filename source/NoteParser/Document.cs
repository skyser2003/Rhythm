using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace NoteParser
{
    public class Document
    {
        public void Parse(string content)
        {
            var file = JsonMapper.ToObject<File>(content);
        }
    }
}