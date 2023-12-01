using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void ChangeScene(string newSceneName) {
        GameManager.instance.ResetVariables();
        SceneManager.LoadScene(newSceneName);
    } 
}
