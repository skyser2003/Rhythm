using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class GameManager
{
    static public int SpeedMultiplier = 1;
    static public float SecPerNode { get { return SpeedMultiplier;} }
    static public int NodeLength = 10;
}