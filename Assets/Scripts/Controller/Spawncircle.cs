using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawncircle : MonoBehaviour
{
    public GameObject objectPrefab;
    public float spawnInterval = 30f;
    public float destroyDelay = 15f;

    private float screenWidth;
    private float screenHeight;
    private float timer;

    private void Start()
    {
        // Get the screen dimensions
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Start the timer
        timer = spawnInterval;
    }

    private void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            // Reset the timer
            timer = spawnInterval;

            // Spawn object at a random position
            Vector3 randomPosition = new Vector3(Random.Range(0, screenWidth), Random.Range(0, screenHeight), +34);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(randomPosition);
            GameObject spawnedObject = Instantiate(objectPrefab, worldPosition, Quaternion.identity);

            // Start the destroy coroutine
            StartCoroutine(DestroyAfterDelay(spawnedObject));
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(obj);
    }
}
