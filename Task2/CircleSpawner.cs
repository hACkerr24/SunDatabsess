using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public int minCircles = 5;
    public int maxCircles = 10;

    private List<GameObject> circles = new List<GameObject>();

    private void Start()
    {
        SpawnCircles();
    }

    public void SpawnCircles()
    {
        ClearCircles();

        int numCircles = Random.Range(minCircles, maxCircles + 1);

        for (int i = 0; i < numCircles; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(-4.5f, 4.5f), 0f);
            GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
            circles.Add(circle);
        }
    }

    public void ClearCircles()
    {
        foreach (GameObject circle in circles)
        {
            Destroy(circle);
        }
        circles.Clear();
    }
}
