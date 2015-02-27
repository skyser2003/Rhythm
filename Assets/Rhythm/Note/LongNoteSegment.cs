﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class LongNoteSegment : MonoBehaviour
{
    public enum STATE
    {
        Fist,
        Middle,
        Last
    }

    private Vector3 velocity;
    private STATE state;

    public STATE State { get { return state; } }

    public void Init(NoteParser.Vec3 begin, NoteParser.Vec3 end, STATE state)
    {
        this.state = state;

        // Position
        var center = begin + end;
        center.x /= 2;
        center.y /= 2;
        center.z /= 2;

        // Rotation
        var vector = end - begin;
        var x = vector.x;
        var y = vector.y;
        var z = vector.z;

        var lookVector = new Vector3((float)x, (float)y, (float)z);

        transform.localPosition = new Vector3((float)center.x, (float)center.y, (float)center.z);
        transform.localRotation = Quaternion.LookRotation(lookVector) * Quaternion.Euler(90, 0, 0);
        velocity = new Vector3(0, 0, -1 * GameConfig.Speed * GameConfig.NodeLength);
    }

    public void Run()
    {
        rigidbody.velocity = velocity;
    }

    public void Pause()
    {
        rigidbody.velocity = new Vector3(0, 0, 0);
    }
}