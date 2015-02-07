using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class GameManager
{
    static public int Speed = 1; // Node per sec
    static public float SecPerNode { get { return 1.0f / (float)Speed;} }
    static public int NodeLength = 10;
}