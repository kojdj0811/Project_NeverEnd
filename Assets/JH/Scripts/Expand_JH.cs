using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Expand_JH
{
    public static class Expand_JH
    {

        /// <summary>
        /// 찾고자 하는 오브젝트에 있는 타겟 컴포넌트를 반환함.
        /// </summary>
        /// <typeparam name="T">타겟 컴포넌트</typeparam>
        /// <param name="parent">검색의 대상이 되는 부모 오브젝트</param>
        /// <param name="name">찾을 오브젝트 이름</param>
        /// <returns></returns>
        public static T FindGameObjectByName<T>(this Transform parent, string name) where T : Component
        {
            GameObject result = null;
            var childList = new List<T>();
            parent.GetComponentsInChildren<T>(true, childList);

            foreach (var child in childList)
            {
                if (child.gameObject.name == name)
                {
                    result = child.gameObject;
                    break;
                }
            }

            if (result != null)
                return result.GetComponent<T>();
            else
                return null;
        }
        /// <summary>
        /// 특정 이름의 게임오브젝트를 찾아 반환.
        /// </summary>
        /// <param name="parent">검색의 대상이 되는 부모 오브젝트</param>
        /// <param name="name">찾을 오브젝트 이름</param>
        /// <returns></returns>
        public static GameObject FindGameObjectByName(this Transform parent, string name)
        {
            GameObject result = null;

            var childList = new List<Transform>();

            parent.GetComponentsInChildren<Transform>(true, childList);

            foreach (var child in childList)
            {
                if (child.gameObject.name == name)
                {
                    result = child.gameObject;
                    break;
                }
            }

            return result;

        }

        /// <summary>
        /// scale의 값을 애니메이션 커브를 기준으로 변화시킴.
        /// </summary>
        /// <param name="transform">대상</param>
        /// <param name="curve">커브</param>
        /// <param name="speed">속도</param>
        /// <param name="isSetLastValue">커브의 마지막 값으로 정할지에 대한 여부</param>
        /// <returns></returns>
        public static IEnumerator SetScaleByAnimationCurve(this Transform transform, AnimationCurve curve, float speed = 1, bool isSetLastValue = false)
        {
            float targetTime = curve.GetPlayTime();
            float curTime = 0;
            Vector3 originScale = transform.localScale;

            while (curTime <= targetTime)
            {
                float scalar = curve.Evaluate(curTime);
                var scale = Vector3.one * scalar;

                transform.localScale = scale;

                yield return null;

                curTime += Time.deltaTime * speed;
            }

            transform.localScale = isSetLastValue ? Vector3.one * curve.GetLastValue() : originScale;
        }

        /// <summary>
        /// 포지션 값을 애니메이션 커브를 기준으로 변화시킴.
        /// </summary>
        /// <param name="transform">대상</param>
        /// <param name="curve">커브</param>
        /// <param name="speed">속도</param>
        /// <param name="isSetLastValue">커브의 마지막 값으로 정할지에 대한 여부</param>
        /// <returns></returns>
        public static IEnumerator MoveByAnimationCurve(this Transform transform,Transform tr_rot, AnimationCurve curve, float speed = 1, bool isSetLastValue = true)
        {
            float targetTime = curve.GetPlayTime();
            float curTime = 0;
            float angle;
            float degree;
            Vector3 originPos = transform.position;
            Vector3 vec_Target;

            while (curTime <= targetTime)
            {
                float pos = curve.Evaluate(curTime);
                
                vec_Target = new Vector3(originPos.x-curTime, originPos.y + pos);
                angle = Mathf.Atan2((vec_Target.x - transform.position.x) , (vec_Target.y- transform.position.y));
                degree = angle * Mathf.Rad2Deg;
                transform.position = vec_Target;               
                //tr_rot.rotation = Quaternion.Euler(0,0,degree+90);
                yield return null;
                curTime += Time.deltaTime* speed;
            }
            transform.position = new Vector3(originPos.x- targetTime, originPos.y + curve.GetLastValue());
        }
        public static float GetPlayTime(this AnimationCurve curve)
        {
            if (curve.length > 0)
                return curve.keys[curve.length - 1].time;
            else
                return 0;
        }
        public static float GetLastValue(this AnimationCurve curve)
        {
            if (curve.length > 0)
                return curve.Evaluate(curve.GetPlayTime());
            else
                return 0;

        }

        /// <summary>
        /// 배열을 셔플 시켜주는 함수
        /// </summary>
        /// <param name="array">셔플할 배열</param>
        public static void ShuffleArray(this int[] array)
        {
            int random0;
            int random1;
            int tmp;
            for (int i = 0; i < array.Length; i++)
            {
                random0 = Random.Range(0, array.Length);
                random1 = Random.Range(0, array.Length);

                tmp = array[random0];
                array[random0] = array[random1];
                array[random1] = tmp;
            }
        }
        public static IEnumerator SetAlphaText(this Text _text, float Alpha = 1f, float speed = 2f)
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

        /// <summary>
        /// 포지션 이동
        /// </summary>
        /// <param name="_tr">이동시킬 오브젝트</param>
        /// <param name="Type">0:상하(~까지), 1:좌우(~까지), 2: 상하(~만큼), 3:좌우(~만큼)</param>
        /// <param name="Target">목표 좌표, Type이 2,3일 때는 Target만큼 이동</param>
        /// <param name="TimeMode">TimeMode가 True일 때 speed는 이동 시간</param>
        /// <param name="speed">이동 속도</param>
        /// <returns></returns>
        public static IEnumerator RectMove(this Transform _tr, int Type, float Target, float speed = 100f, bool TimeMode = false)
        {
            if (TimeMode)
            {
                float curTime = 0;
                switch (Type)
                {
                    case 0:
                    case 2:
                        if (Type == 2)
                        {
                            Target = _tr.position.y + Target;
                        }
                        float YSpeed = Target - _tr.position.y;

                        while (curTime + Time.deltaTime < speed)
                        {
                            _tr.position += YSpeed * Vector3.up * Time.deltaTime / speed;
                            yield return null; 
                            curTime += Time.deltaTime;
                            
                        }
                        _tr.position = new Vector3(_tr.position.x, Target);
                        break;
                    case 1:
                    case 3:
                        if (Type == 3)
                        {
                            Target = _tr.position.x + Target;
                        }
                        float XSpeed = Target - _tr.position.x;
                        while (curTime + Time.deltaTime < speed)
                        {
                            _tr.position += XSpeed * Vector3.right * Time.deltaTime / speed;
                            yield return null;
                            curTime += Time.deltaTime;
                        }
                        _tr.position = new Vector3(Target, _tr.position.y);
                        break;
                }
            }
            else
            {
                switch (Type)
                {
                    case 0:
                        if (_tr.position.y < Target)
                        {
                            while (_tr.position.y + speed * Time.deltaTime < Target)
                            {
                                
                                _tr.position += Vector3.up * speed * Time.deltaTime;
                                yield return null;
                            }
                        }
                        else
                        {
                            while (_tr.position.y - speed * Time.deltaTime > Target)
                            {
                                _tr.position += Vector3.down * speed * Time.deltaTime;
                                yield return null;
                            }
                        }
                        _tr.position = new Vector3(_tr.position.x, Target);
                        break;
                    case 1:
                        if (_tr.position.x < Target)
                        {
                            while (_tr.position.x + speed * Time.deltaTime < Target)
                            {
                                _tr.position += Vector3.right * speed * Time.deltaTime;
                                yield return null;
                            }
                        }
                        else
                        {
                            while (_tr.position.x - speed * Time.deltaTime > Target)
                            {
                                _tr.position += Vector3.left * speed * Time.deltaTime;
                                yield return null;
                            }
                        }
                        _tr.position = new Vector3(Target, _tr.position.y);
                        break;
                    case 2:
                        float RealTargetY = _tr.position.y + Target;
                        if (_tr.position.y < RealTargetY)
                        {
                            while (_tr.position.y + speed * Time.deltaTime < RealTargetY)
                            {
                                _tr.position += Vector3.up * speed * Time.deltaTime;
                                yield return null;
                            }
                        }
                        else
                        {
                            while (_tr.position.y - speed * Time.deltaTime > RealTargetY)
                            {
                                _tr.position += Vector3.down * speed * Time.deltaTime;
                                yield return null;
                            }
                        }
                        _tr.position = new Vector3(_tr.position.x, RealTargetY);
                        break;
                    case 3:
                        float RealTargetX = _tr.position.x + Target;
                        if (_tr.position.y < RealTargetX)
                        {
                            while (_tr.position.y + speed * Time.deltaTime < RealTargetX)
                            {
                                _tr.position += Vector3.up * speed * Time.deltaTime;
                                yield return null;
                            }
                        }
                        else
                        {
                            while (_tr.position.y - speed * Time.deltaTime > RealTargetX)
                            {
                                _tr.position += Vector3.down * speed * Time.deltaTime;
                                yield return null;
                            }
                        }
                        _tr.position = new Vector3(RealTargetX, _tr.position.y);
                        break;
                }
            }

            yield break;
        }
    }
}
