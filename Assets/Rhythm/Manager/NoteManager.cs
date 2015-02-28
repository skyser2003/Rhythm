using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NoteParser;

public class NoteManager : MonoBehaviour
{
    static public NoteManager Instance = null;

    private List<KeyValuePair<NoteObject, NoteCriterion>> hitList = new List<KeyValuePair<NoteObject, NoteCriterion>>();
    private List<Node> nodes = new List<Node>();

    private List<SingleNoteObject> singleNotes = new List<SingleNoteObject>();
    private List<LongNoteObject> longNotes = new List<LongNoteObject>();

    NoteParser.Node[] nodeData = null;
    private int currentNode = -1;

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
        var clone = Instantiate(GameObject.Find("SampleNote")) as GameObject;
        var singleNote = clone.GetComponent<SingleNoteObject>();
        singleNote.Init(note, madi);

        singleNotes.Add(clone.GetComponent<SingleNoteObject>());
    }

    private void CreateLongNote(NoteParser.Node node, NoteParser.LongNote note, int madi)
    {
        var clone = Instantiate(GameObject.Find("SampleLongNote")) as GameObject;
        var longNote = clone.GetComponent<LongNoteObject>();
        longNote.Init(note, madi);

        longNotes.Add(longNote);
    }

    // Use this for initialization
    void Start()
    {
        // Temp singleton
        Instance = this;

        NoteParser.Document doc = new NoteParser.Document();
        doc.Parse(Resources.Load<TextAsset>("song/sample").text);

        var header = doc.File.header;
        nodeData = doc.File.nodes;

        for (int i = 0; i < nodeData.Length; ++i)
        {
            GenerateNode(i);
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

    private void GenerateNode(int nodeIndex)
    {
        if (nodeData.Length <= nodeIndex)
        {
            return;
        }

        var node = nodeData[nodeIndex];

        var singleNotes = node.notes.singleNotes;
        var longNotes = node.notes.longNotes;

        CreateNode(nodeIndex);

        if (singleNotes != null)
        {
            foreach (var singleNote in singleNotes)
            {
                CreateSingleNote(node, singleNote, nodeIndex);
            }
        }

        if (longNotes != null)
        {
            foreach (var longNote in longNotes)
            {
                CreateLongNote(node, longNote, nodeIndex);
            }
        }   
    }

    public void AddHitResultSet(NoteObject note, NoteCriterion criterion)
    {
        hitList.Add(new KeyValuePair<NoteObject, NoteCriterion>(note, criterion));
    }

    public void Run()
    {
        foreach (var note in singleNotes)
        {
            note.Run();
        }
        foreach (var note in longNotes)
        {
            note.Run();
        }
        foreach(var node in nodes)
        {
            node.Run();
        }
    }

    public void Pause()
    {
        foreach (var note in singleNotes)
        {
            note.Pause();
        }
        foreach (var note in longNotes)
        {
            note.Pause();
        }
        foreach (var node in nodes)
        {
            node.Pause();
        }
     }

    public void Remove(SingleNoteObject singleNote)
    {
        foreach(var note in singleNotes)
        {
            if(note == singleNote)
            {
                singleNotes.Remove(note);
                break;
            }
        }
    }

    public void Remove(LongNoteObject longNote)
    {
        foreach (var note in longNotes)
        {
            if (note == longNote)
            {
                longNotes.Remove(note);
                break;
            }
        }
    }
}
