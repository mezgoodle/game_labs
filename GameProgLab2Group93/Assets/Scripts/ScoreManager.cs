using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager: MonoBehaviour
{
    public static int scoreValue = 0;
    Text scoreObject;

    void Start()
    {
        scoreObject = GetComponent<Text>();
    }

    void Update()
    {
        scoreObject.text = "Score: " + scoreValue;
    }
}
