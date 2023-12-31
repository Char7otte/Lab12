﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int scoreValue;
    public float timeValue;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }

        else Destroy(this);
    }

    public void ResetVariables() {
        scoreValue = 0;
        timeValue = 0;
    }
}
