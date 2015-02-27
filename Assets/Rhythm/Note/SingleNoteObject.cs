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

    public void Init(NoteParser.SingleNote note, int madi)
    {
        Data = note;

        var coord = note.coord;

        float x = (float)coord.x;
        float y = (float)coord.y;
        float ratio = (float)coord.z;

        if (GameConfig.LeftRightReverse == true)
        {
            x *= -1;
        }

        transform.position = new Vector3(x, y, (float)(madi + ratio) * GameConfig.NodeLength);
        rigidbody.velocity = new Vector3(0, 0, -1 * GameConfig.Speed * GameConfig.NodeLength);
    }
}
