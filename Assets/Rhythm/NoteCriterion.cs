using UnityEngine;
using System.Collections;

public class NoteCriterion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        var note = collider.gameObject.GetComponent<Note>();
        if (note != null)
        {
            NoteManager.Instance.AddHitResultSet(note, this);
        }
    }
}
