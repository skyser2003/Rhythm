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

    private void CreateSingleNote(NoteParser.Node node, NoteParser.Vec3[] coords, int madi)
    {
        foreach (var coord in coords)
        {
            float x = (float)coord.x;
            float y = (float)coord.y;
            float ratio = (float)coord.z;

            var clone = Instantiate(GameObject.Find("SampleNote")) as GameObject;
            clone.GetComponent<Transform>().position = new Vector3(x, y, (float)(madi + ratio) * GameManager.NodeLength);
            clone.rigidbody.velocity = new Vector3(0, 0, -1 * GameManager.SecPerNode * GameManager.NodeLength);
        }
    }

    private void CreateLongNote(NoteParser.Node node, NoteParser.LongNote note, int madi)
    {
        float secPerMadi = 60.0f / (float)120;
        int madiLength = 10;

        var bezier = new Bezier();
        bezier.Init(note.bezier);

        for (int i = 0; i < 100; ++i)
        {
            float f = i * 0.01f;
            var coord = bezier.GetPosition(f);

            float x = (float)coord.x;
            float y = (float)coord.y;
            float ratio = (float)coord.z;

            var clone = Instantiate(GameObject.Find("SampleNote")) as GameObject;
            clone.GetComponent<Transform>().position = new Vector3(x, y, (float)(madi + ratio) * madiLength);
            clone.rigidbody.velocity = new Vector3(0, 0, -1 * secPerMadi * madiLength);
        }
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
                CreateSingleNote(node, singleNotes, i);
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
