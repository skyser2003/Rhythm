﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LongNoteObject : NoteObject
{
    private bool alive = true;
    private List<LongNoteSegment> segments = new List<LongNoteSegment>();

    private NoteParser.LongNote data;

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

    void OnDestroy()
    {
        NoteManager.Instance.Remove(this);
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
        data = note;

        var bezier = new Bezier();
        bezier.Init(note.bezier);

        var firstPos = bezier.GetPosition(0.0f);
        NoteParser.Vec3 prevPos = null;

        if (GameConfig.LeftRightReverse == true)
        {
            firstPos.x *= -1;
        }
        firstPos.z = (madi + firstPos.z) * GameConfig.NodeLength;

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
            segments.Add(segment);

            if (i == 1)
            {
                segment.Init(firstPos, coord, LongNoteSegment.STATE.Fist);
            }
            else if(i == 100 -1)
            {
                segment.Init(prevPos, coord, LongNoteSegment.STATE.Last);
            }
            else
            {
                segment.Init(prevPos, coord, LongNoteSegment.STATE.Middle);
            }

            prevPos = coord;
        }
    }

    override public void Run()
    {
        foreach(var segment in segments)
        {
            segment.Run();
        }
    }

    override public void Pause()
    {
        foreach (var segment in segments)
        {
            segment.Pause();
        }
    }
}