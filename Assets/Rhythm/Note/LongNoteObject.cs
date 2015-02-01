using UnityEngine;
using System.Collections;

public class LongNoteObject : NoteObject
{
    private Vector3 begin;
    private Vector3 end;
    private bool alive = true;

    public Vector3 Begin { get { return begin; } }
    public Vector3 End { get { return end; } }

    LongNoteObject()
    {
        type = NOTE_TYPE.LONG_NOTE;
    }

    // Use this for initialization
    void Start()
    {
        var transform = gameObject.GetComponent<Transform>();
        begin = new Vector3(-4, 0, 10);
        //        end = new Vector3(-10, 0, 20);
        end = begin * 2;
        var diff = begin - end;

        var x = Mathf.Acos(diff.x / diff.magnitude) * 180 / Mathf.PI;
        x = x - 90;

        transform.Rotate(0, x, 0, Space.World);

        var newScale = transform.localScale;
        newScale.y = diff.magnitude / 2;
        transform.localScale = newScale;

        var force = gameObject.AddComponent<ConstantForce>();
        force.force = diff.normalized;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (alive == false)
        {
            return;
        }

        Debug.Log("Long note begin");
    }

    void OnTriggerExit(Collider collider)
    {
        if(alive == true)
        {
            alive = false;
            Debug.Log("Long note end");
        }
    }
}