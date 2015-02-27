using UnityEngine;
using System.Collections;

public class SingleNoteObject : NoteObject
{
    private NoteParser.SingleNote data;
    private Vector3 velocity;

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
    }

    void OnDestroy()
    {
        NoteManager.Instance.Remove(this);
    }

    public void Init(NoteParser.SingleNote note, int madi)
    {
        data = note;

        var coord = note.coord;

        float x = (float)coord.x;
        float y = (float)coord.y;
        float ratio = (float)coord.z;

        if (GameConfig.LeftRightReverse == true)
        {
            x *= -1;
        }

        transform.position = new Vector3(x, y, (float)(madi + ratio) * GameConfig.NodeLength);
        velocity = new Vector3(0, 0, -1 * GameConfig.Speed * GameConfig.NodeLength);
    }

    override public void Run()
    {
        rigidbody.velocity = velocity;
    }

    override public void Pause()
    {
        rigidbody.velocity = new Vector3(0, 0, 0);
    }
}
