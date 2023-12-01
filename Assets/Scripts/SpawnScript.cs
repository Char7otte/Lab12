using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]private GameObject[] objectsToSpawn = default;
    private Camera mainCamera = default;
    private float height;
    private float width;
    
    private void Awake() {
        mainCamera = Camera.main;
        height = 2f * mainCamera.orthographicSize;
        width = height * mainCamera.aspect;

        height /= 2;
        width /= 2;
        height -= 0.5f;
        width -= 0.5f;
    }

    private void Start() {
        InvokeRepeating("SpawnObject", 1.0f, 1.0f);
        InvokeRepeating("SpawnObject", 1.0f, 1.0f);
    }

    private void Update() {
        MouseRaycast();
        CountUpTimer();
    }

    private void SpawnObject() {
        var randomElement = Random.Range(0, objectsToSpawn.Length);
        var randomPositionX = Random.Range(width * -1, width);
        var randomPositionY = Random.Range(height * -1, height);
        var randomPosition = new Vector3(randomPositionX, randomPositionY, 0);

        var spawnedObject = Instantiate(objectsToSpawn[randomElement], randomPosition, Quaternion.identity);  
        Destroy(spawnedObject, 3);  
    }

    private void MouseRaycast() {
        if (Input.GetMouseButtonDown(0)) {
            var worldMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));
            var direction = worldMousePosition - mainCamera.transform.position;
            
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, direction, out hit, 100f)) {
                Debug.DrawLine(mainCamera.transform.position, hit.point, Color.green, 0.5f);

                var _hit = hit.collider.gameObject;
                var pointGain = 0;

                if (_hit.tag == "red") {
                    pointGain = -1;
                }
                if (_hit.tag == "green") {
                    pointGain = 1;
                }

                Destroy(_hit);

                var _scoreValue = GameManager.instance.scoreValue;
                _scoreValue += pointGain;

                GameManager.instance.scoreValue = _scoreValue;

                if (_scoreValue >= 15)  {
                    ChangeScene("GameWin");
                }

            }
            else Debug.DrawLine(mainCamera.transform.position, worldMousePosition, Color.red, 0.5f);
        }
    }

    private void CountUpTimer() {
        var _timeValue = GameManager.instance.timeValue;
        _timeValue += Time.deltaTime;

        GameManager.instance.timeValue = _timeValue;

        if (_timeValue >= 10) ChangeScene("GameLose");
    }

    private void ChangeScene(string newSceneName) {
        SceneManager.LoadScene(newSceneName);
    }
}
