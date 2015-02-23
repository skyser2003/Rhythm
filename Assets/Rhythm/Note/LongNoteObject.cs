using UnityEngine;
using System.Collections;

public class LongNoteObject : NoteObject
{
    private bool alive = true;
    public NoteParser.LongNote Data;

    LongNoteObject()
    {
        type = NOTE_TYPE.LONG_NOTE;
    }

    // Use this for initialization
    void Start()
    {
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
        if (alive == true)
        {
            alive = false;
            Debug.Log("Long note end");
        }
    }

    public void Init(NoteParser.LongNote note, int madi)
    {
        var bezier = new Bezier();
        bezier.Init(note.bezier);

        var firstPos = bezier.GetPosition(0.0f);
        NoteParser.Vec3 prevPos = null;

        if (GameConfig.LeftRightReverse == true)
        {
            firstPos.x *= -1;
        }

        for (int i = 1; i < 100; ++i)
        {
            float f = i * 0.01f;
            var coord = bezier.GetPosition(f);
            if(GameConfig.LeftRightReverse == true)
            {
                coord.x *= -1;
            }
            coord.z = (madi + coord.z) * GameConfig.NodeLength;

            var segmentObject = Instantiate(GameObject.Find("SampleLongNoteSegment")) as GameObject;
            var segment = segmentObject.AddComponent<LongNoteSegment>();

            if (i == 1)
            {
                firstPos.z = (madi + firstPos.z) * GameConfig.NodeLength;
                segment.Init(firstPos, coord);
            }
            else
            {
                segment.Init(prevPos, coord);
            }

            prevPos = coord;
        }
    }
}