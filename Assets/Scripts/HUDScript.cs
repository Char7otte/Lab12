using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDScript : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI scoreTextDisplay = default;
    [SerializeField]private TextMeshProUGUI timeTextDisplay = default;

    private void Update() {
        var gameManager = GameManager.instance;
        scoreTextDisplay.text = "Score: " + gameManager.scoreValue;
        timeTextDisplay.text = "Time: " + gameManager.timeValue.ToString("F0");
    }
}
