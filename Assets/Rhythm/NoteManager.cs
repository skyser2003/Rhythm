using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour
{
    static public NoteManager Instance = null;

    private List<KeyValuePair<Note, NoteCriterion>> hitList = new List<KeyValuePair<Note, NoteCriterion>>();

    private void SingleNoteHitResult(SingleNote note, NoteCriterion criterion)
    {
        Destroy(note.gameObject);
    }

    private void LongNoteHitResult(LongNote note, NoteCriterion criterion)
    {
        Debug.Log("Long Note");
    }

    // Use this for initialization
    void Start()
    {
        // Temp singleton
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var pair in hitList)
        {
            var note = pair.Key as Note;
            var criterion = pair.Value as NoteCriterion;

            switch (note.Type)
            {
                case NOTE_TYPE.SINGLE_NOTE:
                    {
                        SingleNoteHitResult((SingleNote)note, criterion);
                    }
                    break;
                case NOTE_TYPE.LONG_NOTE:
                    {
                        LongNoteHitResult((LongNote)note, criterion);
                    }
                    break;
            }
        }

        hitList.Clear();
    }

    public void AddHitResultSet(Note note, NoteCriterion criterion)
    {
        hitList.Add(new KeyValuePair<Note, NoteCriterion>(note, criterion));
    }
}
