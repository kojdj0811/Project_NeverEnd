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

    // ���� �ñⰡ �Ǹ�
    // ī�޶� �ٱ�����
    // ��ġ ��� �� (�����¿�)
    // ����
}