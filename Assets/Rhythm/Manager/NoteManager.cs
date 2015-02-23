using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NoteParser;

public class NoteManager : MonoBehaviour
{
    static public NoteManager Instance = null;

    private List<KeyValuePair<NoteObject, NoteCriterion>> hitList = new List<KeyValuePair<NoteObject, NoteCriterion>>();
    private List<Node> nodes = new List<Node>();

    private void SingleNoteHitResult(SingleNoteObject note, NoteCriterion criterion)
    {
        Destroy(note.gameObject);
    }

    private void LongNoteHitResult(LongNoteObject note, NoteCriterion criterion)
    {
    }

    private void CreateNode(int madi)
    {
        var node = new Node();
        node.Init(madi);

        nodes.Add(node);
    }

    private void CreateSingleNote(NoteParser.Node node, SingleNote note, int madi)
    {
        var coord = note.coord;

        float x = (float)coord.x;
        float y = (float)coord.y;
        float ratio = (float)coord.z;

        if (GameConfig.LeftRightReverse == true)
        {
            x *= -1;
        }
        
        var clone = Instantiate(GameObject.Find("SampleNote")) as GameObject;
        clone.GetComponent<Transform>().position = new Vector3(x, y, (float)(madi + ratio) * GameConfig.NodeLength);
        clone.rigidbody.velocity = new Vector3(0, 0, -1 * GameConfig.Speed * GameConfig.NodeLength);

        clone.GetComponent<SingleNoteObject>().Data = note;
    }

    private void CreateLongNote(NoteParser.Node node, NoteParser.LongNote note, int madi)
    {
        var clone = Instantiate(GameObject.Find("SampleLongNote")) as GameObject;
        clone.GetComponent<LongNoteObject>().Init(note, madi);
    }

    // Use this for initialization
    void Start()
    {
        // Temp singleton
        Instance = this;

        NoteParser.Document doc = new NoteParser.Document();
        doc.Parse(Resources.Load<TextAsset>("song/sample").text);

        var header = doc.File.header;
        var nodes = doc.File.nodes;

        for (int i = 0; i < nodes.Length;++i)
        {
            var node = nodes[i];

            var singleNotes = node.notes.singleNotes;
            var longNotes = node.notes.longNotes;

            CreateNode(i);

            if (singleNotes != null)
            {
                foreach(var singleNote in singleNotes)
                {
                    CreateSingleNote(node, singleNote, i);
                }
            }

            if (longNotes != null)
            {
                foreach (var longNote in longNotes)
                {
                    CreateLongNote(node, longNote, i);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var pair in hitList)
        {
            var note = pair.Key as NoteObject;
            var criterion = pair.Value as NoteCriterion;

            switch (note.Type)
            {
                case NOTE_TYPE.SINGLE_NOTE:
                    {
                        SingleNoteHitResult((SingleNoteObject)note, criterion);
                    }
                    break;
                case NOTE_TYPE.LONG_NOTE:
                    {
                        LongNoteHitResult((LongNoteObject)note, criterion);
                    }
                    break;
            }
        }

        hitList.Clear();
    }

    public void AddHitResultSet(NoteObject note, NoteCriterion criterion)
    {
        hitList.Add(new KeyValuePair<NoteObject, NoteCriterion>(note, criterion));
    }
}
