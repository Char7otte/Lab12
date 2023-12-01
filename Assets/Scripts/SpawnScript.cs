using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]private GameObject[] objectsToSpawn = default;
    private Camera mainCamera = default;
    private float height;
    private float width;

    private int scoreValue = default;
    [SerializeField]private TextMeshProUGUI scoreTextDisplay = default;
    
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
        InvokeRepeating("SpawnObject", 1.0f, 2.0f);
    }

    private void Update() {
        RaycastScript();
        scoreTextDisplay.text = "Score: " + scoreValue;
    }

    private void SpawnObject() {
        var randomElement = Random.Range(0, objectsToSpawn.Length);
        var randomXPosition = Random.Range(width * -1, width);
        var randomYPosition = Random.Range(height * -1, height);
        var randomPosition = new Vector3(randomXPosition, randomYPosition, 0);

        var spawnedObject = Instantiate(objectsToSpawn[randomElement], randomPosition, Quaternion.identity);  
        Destroy(spawnedObject, 3);  
    }

    private void RaycastScript() {
        if (Input.GetMouseButtonDown(0)) {
            var worldMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));
            var direction = worldMousePosition - mainCamera.transform.position;
            
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, direction, out hit, 100f)) {
                Debug.DrawLine(mainCamera.transform.position, hit.point, Color.green, 0.5f);

                if (hit.collider.gameObject.tag == "red") {
                    scoreValue--;
                }
                if (hit.collider.gameObject.tag == "green") {
                    scoreValue++;
                }
            }
            else {
                Debug.DrawLine(mainCamera.transform.position, worldMousePosition, Color.red, 0.5f);
            }
        }
    }
}
