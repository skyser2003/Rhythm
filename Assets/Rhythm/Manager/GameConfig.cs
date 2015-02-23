using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class GameConfig
{
    static public float Speed = 0.5875f; // Node per sec
    static public float SecPerNode { get { return 1.0f / Speed;} }
    static public int NodeLength = 10;
    static public bool LeftRightReverse = true;
}