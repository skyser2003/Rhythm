using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour
{
    static public NoteManager Instance = null;

    private List<KeyValuePair<Note, NoteCriterion>> hitList = new List<KeyValuePair<Note, NoteCriterion>>();
    private int bpm;

    private void SingleNoteHitResult(SingleNote note, NoteCriterion criterion)
    {
        Destroy(note.gameObject);
    }

    private void LongNoteHitResult(LongNote note, NoteCriterion criterion)
    {
    }

    private void Parse(string contents)
    {
        int temp;
        string[] lines = contents.Split('\n');
        foreach (string line in lines)
        {
            if (line.Length > 4 &&
               line.Substring(0, 5).Equals("#BPM "))
            {
                bpm = int.Parse(line.Substring(5));
            }
            if (line.Length > 6 &&
               line.Substring(0, 1).Equals("#") &&
               line.Substring(6, 1).Equals(":") &&
               int.TryParse(line.Substring(1, 5), out temp))
            {
                int madi = int.Parse(line.Substring(1, 3));
                int lane = int.Parse(line.Substring(4, 2));
                string field = line.Substring(7, line.Length - 8);

                if(lane <= 10)
                {
                    continue;
                }

                lane -= 10;

                for (int tick = 0; tick < field.Length; tick += 2)
                {
                    string value = field.Substring(tick, 2);
                    if (!value.Equals("00"))
                    {
                        CreateNote(lane, madi, tick, field.Length / 2);
                    }
                }
            }
        }
    }

    private void CreateNote(int lane, int madi, int tick, int length)
    {
        float x = 0;
        float y = 0;

        switch(lane)
        {
            case 1:
            case 2:
            case 3:
                {
                    x = lane - 1;
                    y = -1;
                }
                break;
            case 4:
            case 5:
            case 6:
                {
                    x = lane - 4;
                    // Default
                    y = 0;
                }
                break;
            case 7:
            case 8:
            case 9:
                {
                    x = lane - 7;
                    y = 1;
                }
                break;
        }

        x = x - 1.5f;
        x *= 3;

        float secPerMadi = 60.0f / (float)bpm;
        int madiLength = 10;
        
        var clone = Instantiate(GameObject.Find("SampleNote")) as GameObject;
        clone.GetComponent<Transform>().position = new Vector3(x, y * 3, madi * madiLength + ((float)tick / (float)length) * madiLength);
        clone.rigidbody.velocity = new Vector3(0, 0, -1 * secPerMadi * madiLength);
    }

    // Use this for initialization
    void Start()
    {
        // Temp singleton
        Instance = this;

        // Temp data
        var path = "C:\\Projects\\Rhythm\\Assets\\BMS_Data\\002. Ehne&Sing88 - Abyss\\Abbys5b.bms";
        var contents = System.IO.File.ReadAllText(path);
        Parse(contents);
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
