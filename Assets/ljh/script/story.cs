using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class story : MonoBehaviour
{

    public Text Text;
    private void Start()
    {
        StartCoroutine(SetAlphaText(Text, 1, 1f));
    }
    public IEnumerator SetAlphaText(Text _text, float Alpha = 1f, float speed = 2f)
    {
        Color tmp = _text.color;

        if (_text.color.a < Alpha)
        {
            while (_text.color.a + speed * Time.deltaTime < Alpha)
            {
                tmp.a += speed * Time.deltaTime;
                _text.color = tmp;
                //_text.alpha += speed * Time.deltaTime;
                yield return null;
            }
            tmp.a = Alpha;
            _text.color = tmp;
        }
        else
        {
            while (_text.color.a - speed * Time.deltaTime > Alpha)
            {
                tmp.a -= speed * Time.deltaTime;
                _text.color -= tmp;
                yield return null;
            }
            tmp.a = Alpha;
            _text.color = tmp;
        }
        yield return null;
    }

    public void SkipClick()
    {

    }
}
