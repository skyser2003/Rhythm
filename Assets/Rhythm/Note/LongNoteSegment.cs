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

        var unityVector = new Vector3((float)x, (float)y, (float)z);

        double length = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        double xzLength = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(z, 2));

        float xAngle = (float)Math.Acos(x / xzLength);// *180 / Math.PI;
        float yAngle = (float)Math.Acos(xzLength / length);// *180 / Math.PI;
        float zAngle = (float)Math.Acos(z / xzLength);// *180 / Math.PI;

        var rotation = Quaternion.LookRotation(new Vector3(0, 1, 0), unityVector);

        transform.localPosition = new Vector3((float)center.x, (float)center.y, (float)center.z);
        transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
        transform.localRotation = rotation;// new Quaternion(xAngle, yAngle, zAngle, 0.0f);// Quaternion.Euler((float)xAngle, (float)yAngle, (float)zAngle);
        rigidbody.velocity = new Vector3(0, 0, -1 * GameConfig.Speed * GameConfig.NodeLength);
    }

    public void Init(NoteParser.Vec3 begin, NoteParser.Vec3 end)
    {
        this.begin = begin;
        this.end = end;
    }
}