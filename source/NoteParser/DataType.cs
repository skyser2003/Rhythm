﻿using System;
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
        public Vec3[] singleNotes;
        public LongNote[] longNotes;
    }

    public class LongNote
    {
        public Vec3[] bezier;
    }

    public class Vec2
    {
        public double x = 0.0;
        public double y = 0.0;
    }

    public class Vec3
    {
        public double x = 0.0;
        public double y = 0.0;
        public double z = 0.0;

        public Vec3()
        {

        }

        public Vec3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vec3 operator +(Vec3 lhs, Vec3 rhs)
        {
            return new Vec3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vec3 operator -(Vec3 lhs, Vec3 rhs)
        {
            return new Vec3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Vec3 operator *(float coefficient, Vec3 rhs)
        {
            return new Vec3(coefficient * rhs.x, coefficient * rhs.y, coefficient * rhs.z);
        }

        public static Vec3 operator *(Vec3 lhs, float coefficient)
        {
            return new Vec3(coefficient * lhs.x, coefficient * lhs.y, coefficient * lhs.z);
        }

        public static Vec3 operator *(double coefficient, Vec3 rhs)
        {
            return new Vec3(coefficient * rhs.x, coefficient * rhs.y, coefficient * rhs.z);
        }

        public static Vec3 operator *(Vec3 lhs, double coefficient)
        {
            return new Vec3(coefficient * lhs.x, coefficient * lhs.y, coefficient * lhs.z);
        }
    }
}
