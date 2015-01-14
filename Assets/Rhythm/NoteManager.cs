using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour
{
    static public NoteManager Instance = null;

    private List<KeyValuePair<Note, NoteCriterion>> hitList = new List<KeyValuePair<Note, NoteCriterion>>();

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

            Destroy(note.gameObject);
        }

        hitList.Clear();
    }

    public void AddHitGroup(Note note, NoteCriterion criterion)
    {
        hitList.Add(new KeyValuePair<Note, NoteCriterion>(note, criterion));
    }
}
