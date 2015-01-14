using UnityEngine;
using System.Collections;

public class NoteCriterion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        var note = collision.gameObject.GetComponent<Note>();
        if (note != null)
        {
            NoteManager.Instance.AddHitGroup(note, this);
        }
    }
}
