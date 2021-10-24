using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Expand_JH;

public class Ending_Manager : MonoBehaviour
{
    public GameObject canvas;
    public static Ending_Manager instance;
    CanvasGroup cg_group;
    Text text_playTime;
    Text text_life;
    Image img_life;
    Text text_msg0;
    Text text_msg1;

     float StartTime;
     float EndTime;
     float playtime;

    bool isEnd = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEnd)
        {
            if (collision.CompareTag("Bird"))
            {
                GameFinish();
                isEnd = true;
            }
        }
    }

    private void Awake()
    {
        instance = this;
        cg_group = transform.FindGameObjectByName<CanvasGroup>("cg_group");
        text_playTime = transform.FindGameObjectByName<Text>("text_playtime");
        text_life = transform.FindGameObjectByName<Text>("text_heart");
        img_life = transform.FindGameObjectByName<Image>("img_heart");
        text_msg0 = transform.FindGameObjectByName<Text>("text_Message0");
        text_msg1 = transform.FindGameObjectByName<Text>("text_Message1");
    }
    void Start()
    {
        StartTime = Time.timeSinceLevelLoad;
        canvas.SetActive(false);
    }
    public void GameFinish()
    {
        canvas.SetActive(true);
        EndTime = Time.timeSinceLevelLoad;      
        playtime = EndTime - StartTime;
        StartCoroutine(cg_on());
    }

    IEnumerator cg_on(float speed=3f)
    {
        int m_time = (int)(playtime / 60);
        int s_time = (int)playtime - (m_time * 60);
        text_playTime.text = "PlayTime: " + m_time + "m " + s_time+"s";
        text_life.text = ": " + Character.S.Life + "";

        //Character.S.ActiveRigidbodys(false);
        Character.S.flyPower = 0f;
        Character.S.CurrentState = CharacterState.Finish;

        Color color_tmp = new Color(1, 1, 1, 0);
        text_playTime.color = color_tmp;
        text_life.color = color_tmp;
        //mg_life.color = color_tmp;
        text_msg0.color = color_tmp;
        text_msg1.color = color_tmp;


        var alpha = cg_group.alpha;
        while (alpha <= 1f)
        {
            alpha += Time.deltaTime * speed;
            cg_group.alpha = alpha;
            yield return null;
        }
        cg_group.alpha = 1f;

        StartCoroutine(text_playTime.SetAlphaText(1f,2f));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(text_life.SetAlphaText(1f, 2f));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(text_msg0.SetAlphaText(1f, 2f));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(text_msg1.SetAlphaText(1f, 2f));
    }
    public void Restart()
    {
        cg_group.alpha = 0;
        StartTime = Time.timeSinceLevelLoad;

        Character.S.transform.position = Character.S.initPosition;
        Character.S.transform.eulerAngles = Character.S.initEularAngles;
        Character.S.transform.localScale = Character.S.initLocalScale;
        Character.S.flyPower = Character.S.initFlyPower;

        if (MapSet_Manager.Instance!=null)
        {
            MapSet_Manager.Instance.ShuffleMap();
        }
        // Character.S.CurrentState = CharacterState.Sleep;
        Character.S.shield.Life = 0;
        Character.S.Life = 99;
        Character.S.CurrentState = CharacterState.Sleep;
        EndTarget.instance.isEnd = false;
        canvas.SetActive(false);
    }
    


}
