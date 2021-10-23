using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject fruitPrefab;

    [SerializeField] private List<Transform> heartPoints = new List<Transform>();
    [SerializeField] private List<Transform> fruitPoints = new List<Transform>();

    private List<Transform> points = new List<Transform>();

    private GameObject[] pointsArr;

    private int maxHeartCount = 5;
    private int heartCount = 0;

    private int maxFruitCount = 3;
    private int fruitCount = 0;

    private float spawnTime = 3f;
    private float curTime = 0;

    void Start()
    {
        pointsArr = GameObject.FindGameObjectsWithTag("Heart");
        foreach(GameObject obj in pointsArr)
            heartPoints.Add(obj.transform);

        pointsArr = GameObject.FindGameObjectsWithTag("Fruit");
        foreach(GameObject obj in pointsArr)
            fruitPoints.Add(obj.transform);

        Invoke("SpawnHeart", spawnTime);
        Invoke("SpawnFruit", spawnTime);
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if(curTime > spawnTime && heartCount < maxHeartCount)
        {
            Invoke("SpawnHeart", spawnTime);
            curTime = 0;
        }
        else if(curTime > spawnTime && fruitCount < maxFruitCount)
        {
            Invoke("SpawnFruit", spawnTime);
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

    public void SpawnFruit()
    {
        int idx = Random.Range(0, fruitPoints.Count);
        Instantiate(fruitPrefab, fruitPoints[idx].position, fruitPoints[idx].rotation);
        fruitCount++;

        if(fruitCount < maxFruitCount)
            Invoke("SpawnFruit", spawnTime);
    }
}
