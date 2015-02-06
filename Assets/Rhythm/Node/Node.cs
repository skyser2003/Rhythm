using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class Node
{
    private int index;
    private GameObject wall;

    public void Init(int index)
    {
        this.index = index;

        wall = UnityEngine.Object.Instantiate(GameObject.Find("SampleWall")) as GameObject;
        wall.rigidbody.velocity = new Vector3(0, 0, -1 * GameManager.secPerMadi * GameManager.madiLength);
        wall.transform.localPosition = new Vector3(0, 0, index * GameManager.madiLength);
    }
}