using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class Node
{
    private int index;
    private GameObject wall;
    private Vector3 velocity;

    public void Init(int index)
    {
        this.index = index;

        wall = UnityEngine.Object.Instantiate(GameObject.Find("SampleWall")) as GameObject;
        velocity = new Vector3(0, 0, -1 * GameConfig.Speed * GameConfig.NodeLength);
        wall.transform.localPosition = new Vector3(0, 0, (index + 1) * GameConfig.NodeLength);
    }

    public void Run()
    {
        wall.rigidbody.velocity = velocity;
    }

    public void Pause()
    {
        wall.rigidbody.velocity = new Vector3(0, 0, 0);
    }
}