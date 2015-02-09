using UnityEngine;
using System.Collections;

public class SingleNoteObject : NoteObject
{
    public NoteParser.SingleNote Data;

    SingleNoteObject()
    {
        type = NOTE_TYPE.SINGLE_NOTE;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.rigidbody.velocity = new Vector3(0, 0, -10);
    }
}
