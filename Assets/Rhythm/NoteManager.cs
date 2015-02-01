using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NoteParser;

public class NoteManager : MonoBehaviour
{
    static public NoteManager Instance = null;

    private List<KeyValuePair<NoteObject, NoteCriterion>> hitList = new List<KeyValuePair<NoteObject, NoteCriterion>>();

    private void SingleNoteHitResult(SingleNoteObject note, NoteCriterion criterion)
    {
        Destroy(note.gameObject);
    }

    private void LongNoteHitResult(LongNoteObject note, NoteCriterion criterion)
    {
    }

    private void CreateSingleNote(NoteParser.Node node, NoteParser.SingleNote note, int madi)
    {
        float secPerMadi = 60.0f / (float)120;
        int madiLength = 10;

        foreach (var coord in note.coords)
        {
            float x = (float)coord.x;
            float y = (float)coord.y;
            var time = note.time;

            var clone = Instantiate(GameObject.Find("SampleNote")) as GameObject;
            clone.GetComponent<Transform>().position = new Vector3(x, y * 3, (float)(madi + time) * madiLength);
            clone.rigidbody.velocity = new Vector3(0, 0, -1 * secPerMadi * madiLength);
        }
    }

    private void CreateLongNote()
    {

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

            foreach (var singleNote in singleNotes)
            {
                CreateSingleNote(node, singleNote, i);
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
