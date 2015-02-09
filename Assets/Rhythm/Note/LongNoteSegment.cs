using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class LongNoteSegment : MonoBehaviour
{
    private NoteParser.Vec3 begin, end;

    void Start()
    {
        var center = begin + end;
        center.x /= 2;
        center.y /= 2;
        center.z /= 2;

        transform.localPosition = new Vector3((float)center.x, (float)center.y, (float)center.z);
        rigidbody.velocity = new Vector3(0, 0, -1 * GameConfig.Speed * GameConfig.NodeLength);
    }

    public void Init(NoteParser.Vec3 begin, NoteParser.Vec3 end)
    {
        this.begin = begin;
        this.end = end;
    }
}