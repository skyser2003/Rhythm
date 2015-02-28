using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class GameManager : MonoBehaviour
{
    static public GameManager Instance = null;

    public bool IsRunning { get { return isRunning; } }

    private bool isRunning = false;
    private AudioSource music = null;
    private GameObject leftCriterion = null, rightCriterion = null;

    void Start()
    {
        Instance = this;

        music = GameObject.Find("Music").GetComponent<AudioSource>();
        music.time = 0.9f;

        leftCriterion = GameObject.Find("Left Criterion");
        rightCriterion = GameObject.Find("Right Criterion");

        Pause();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") == true)
        {
            if(isRunning == true)
            {
                Pause();
            }
            else
            {
                Run();
            }
        }
        if(Input.GetKeyDown("c") == true)
        {
            var leftHand = leftCriterion.GetComponent<HandPosition>();
            var rightHand = rightCriterion.GetComponent<HandPosition>();

            var tempMyo = leftHand.myo;
            leftHand.myo = rightHand.myo;
            rightHand.myo = tempMyo;
        }
    }

    private void Run()
    {
        isRunning = true;
        music.Play();

        if (NoteManager.Instance != null)
        {
            NoteManager.Instance.Run();
        }
    }

    private void Pause()
    {
        isRunning = false;
        music.Pause();

        if(NoteManager.Instance != null)
        {
            NoteManager.Instance.Pause();
        }
    }
}