using UnityEngine;
using System.Collections;

public enum NOTE_TYPE
{
    SINGLE_NOTE,
    LONG_NOTE
}

abstract public class NoteObject : MonoBehaviour
{
    public NOTE_TYPE Type { get { return type; } }
    protected NOTE_TYPE type;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    abstract public void Run();
    abstract public void Pause();
}
