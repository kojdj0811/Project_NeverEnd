using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Expand_JH
{
    public static class Expand_JH
    {

        /// <summary>
        /// ã���� �ϴ� ������Ʈ�� �ִ� Ÿ�� ������Ʈ�� ��ȯ��.
        /// </summary>
        /// <typeparam name="T">Ÿ�� ������Ʈ</typeparam>
        /// <param name="parent">�˻��� ����� �Ǵ� �θ� ������Ʈ</param>
        /// <param name="name">ã�� ������Ʈ �̸�</param>
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
        /// Ư�� �̸��� ���ӿ�����Ʈ�� ã�� ��ȯ.
        /// </summary>
        /// <param name="parent">�˻��� ����� �Ǵ� �θ� ������Ʈ</param>
        /// <param name="name">ã�� ������Ʈ �̸�</param>
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
        /// scale�� ���� �ִϸ��̼� Ŀ�긦 �������� ��ȭ��Ŵ.
        /// </summary>
        /// <param name="transform">���</param>
        /// <param name="curve">Ŀ��</param>
        /// <param name="speed">�ӵ�</param>
        /// <param name="isSetLastValue">Ŀ���� ������ ������ �������� ���� ����</param>
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
        /// ������ ���� �ִϸ��̼� Ŀ�긦 �������� ��ȭ��Ŵ.
        /// </summary>
        /// <param name="transform">���</param>
        /// <param name="curve">Ŀ��</param>
        /// <param name="speed">�ӵ�</param>
        /// <param name="isSetLastValue">Ŀ���� ������ ������ �������� ���� ����</param>
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
        /// ������ �̵�
        /// </summary>
        /// <param name="_tr">�̵���ų ������Ʈ</param>
        /// <param name="Type">0:����(~����), 1:�¿�(~����), 2: ����(~��ŭ), 3:�¿�(~��ŭ)</param>
        /// <param name="Target">��ǥ ��ǥ, Type�� 2,3�� ���� Target��ŭ �̵�</param>
        /// <param name="TimeMode">TimeMode�� True�� �� speed�� �̵� �ð�</param>
        /// <param name="speed">�̵� �ӵ�</param>
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
