using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Expand_JH;
public class MapSet_Manager : MonoBehaviour
{
    private static MapSet_Manager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        tr_Map = new Transform[3];
        obj_Map = new GameObject[5];
        for (int i = 0; i < 3; i++)
        {
            tr_Map[i] = transform.FindGameObjectByName("Map" + (i + 1)).transform;
        }
        for (int i = 0; i < 5; i++)
        {
            obj_Map[i] = transform.FindGameObjectByName("Map" + i);
        }
    }
    public static MapSet_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    Transform[] tr_Map;
    GameObject[] obj_Map;
    int[] map_idx = { 0, 1, 2 };
    // Start is called before the first frame update
    void Start()
    {
        ShuffleMap();
    }
    public void ShuffleMap()
    {
        map_idx.ShuffleArray();
        for (int i = 0; i < 3; i++)
        {
            tr_Map[map_idx[i]].transform.position = new Vector3((i + 1) * 35, 0);
        }
        Branch_Manager.instance.Reset_branch();
        Web_Manager.instance.Reset_spider();
    }
}
