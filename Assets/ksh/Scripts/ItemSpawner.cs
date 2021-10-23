using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject fruitPrefab;
    private GameObject petalPrefab;
    private GameObject heartPrefab;

    private bool gameover = false;

    private Vector3 checkPos;

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CheckPosition();
        }
    }

    IEnumerator Spawn()
    {
        yield return null;
    }

    public void SpawnFruit()
    {
        // 
    }

    public void SpawnPetal()
    {
    }

    public void SpawnHeart()
    {
    }

    private void CheckPosition()
    {
    }

    // 스폰 시기가 되면
    // 카메라 바깥에서
    // 위치 계산 후 (상하좌우)
    // 생성
}