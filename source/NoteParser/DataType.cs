using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoteParser
{
    public class File
    {
        public Header header;
        public Node[] nodes;
    }

    public class Header
    {
        public string songName;
        public string author;
        public int difficulty;
    }

    public class Node
    {
        public double speed;
        public Notes notes;
    }

    public class Notes
    {
        public SingleNote[] singleNotes;
        public LongNote[] longNotes;
    }

    public class Note
    {
        public string type;
    }

    public class SingleNote : Note
    {
        public double time;
        public Vector2[] coords;
    }

    public class LongNote : Note
    {
        public double beginTime;
        public double endTime;
    }

    public class Vector2
    {
        public double x;
        public double y;
    }
}
