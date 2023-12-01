using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]private GameObject[] objectsToSpawn = default;
    [SerializeField]private Camera camera = default;
    private float height;
    private float width;
    
    private void Awake() {
        height = 2f * camera.orthographicSize;
        width = height * camera.aspect;

        height /= 2;
        width /= 2;
        height -= 0.5f;
        width -= 0.5f;
    }

    private void Start() {
        InvokeRepeating("SpawnObject", 1.0f, 2.0f);
    }

    private void SpawnObject() {
        var randomElement = Random.Range(0, objectsToSpawn.Length);
        var randomXPosition = Random.Range(width * -1, width);
        var randomYPosition = Random.Range(height * -1, height);
        var randomPosition = new Vector3(randomXPosition, randomYPosition, 0);

        var spawnedObject = Instantiate(objectsToSpawn[randomElement], randomPosition, Quaternion.identity);  
        Destroy(spawnedObject, 3);  
    }
}
