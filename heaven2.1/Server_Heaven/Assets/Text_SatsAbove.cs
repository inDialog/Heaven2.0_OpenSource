﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Text_SatsAbove : MonoBehaviour
{
    public PathFinding Path;
    Text Text_SatAbove;
    // Start is called before the first frame update
    void Start()
    {
        Text_SatAbove = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Text_SatAbove.text = "Nr of Satelites above us : " + Path.originalSatelite.Count;
    }
}