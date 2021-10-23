using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Expand_JH;

public class Ending_Manager : MonoBehaviour
{
    public static Ending_Manager instance;
    CanvasGroup cg_group;
    Text text_playTime;
    Text text_life;

     float StartTime;
     float EndTime;
     float playtime;

    private void Awake()
    {
        instance = this;
        cg_group = transform.FindGameObjectByName<CanvasGroup>("cg_group");
        text_playTime = transform.FindGameObjectByName<Text>("text_playtime");
        text_life = transform.FindGameObjectByName<Text>("text_heart");
    }
    void Start()
    {
        StartTime = Time.timeSinceLevelLoad;
    }
    public void GameFinish()
    {
        EndTime = Time.timeSinceLevelLoad;
        
        playtime = EndTime - StartTime;
        StartCoroutine(cg_on());
    }

    IEnumerator cg_on(float speed=3f)
    {
        int m_time = (int)(playtime / 60);
        int s_time = (int)playtime - (m_time * 60);
        text_playTime.text = "플레이 시간:" + m_time + "분 " + s_time + "초";
        text_life.text = ": " + Character.S.Life + " 개";

        //Character.S.ActiveRigidbodys(false);
        Character.S.flyPower = 0f;
        Character.S.CurrentState = CharacterState.Sleep;


        var alpha = cg_group.alpha;
        while (alpha <= 1f)
        {
            alpha += Time.deltaTime * speed;
            cg_group.alpha = alpha;
            yield return null;
        }
        cg_group.alpha = 1f;
    }
    public void Restart()
    {
        Character.S.transform.position = Character.S.initPosition;
        Character.S.transform.eulerAngles = Character.S.initEularAngles;


    }
    


}
