using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;

    public List<Transform> heartPoints = new List<Transform>();
    public List<Transform> points = new List<Transform>();

    private GameObject[] pointsArr;

    private int maxHeartCount = 5;
    public int heartCount = 0;
    public float spawnTime = 3f;

    private float curTime = 0;

    void Start()
    {
        pointsArr = GameObject.FindGameObjectsWithTag("Spawner");
        foreach(GameObject obj in pointsArr)
            heartPoints.Add(obj.transform);

        Invoke("SpawnHeart", spawnTime);
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if(curTime > spawnTime && heartCount < maxHeartCount)
        {
            Invoke("SpawnHeart", spawnTime);
            curTime = 0;
        }
            
    }

    public void SpawnHeart()
    {
        int idx = Random.Range(0, heartPoints.Count);
        Instantiate(heartPrefab, heartPoints[idx].position, heartPoints[idx].rotation);
        heartCount++;

        if(heartCount < maxHeartCount)
            Invoke("SpawnHeart", spawnTime);
    }
}
