using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace VRM
{
    /// <summary>
    /// VRMBlendShapeProxy によるランダムに瞬きするサンプル。
    /// VRMBlendShapeProxy のある GameObject にアタッチする。
    /// </summary>
    public class Blinker : MonoBehaviour
    {
        public Animator animator;
        VRMBlendShapeProxy m_blendShapes;

        [FormerlySerializedAs("m_interVal")]
        [SerializeField]
        public float Interval = 5.0f;

        [FormerlySerializedAs("m_closingTime")]
        [SerializeField]
        public float ClosingTime = 0.06f;

        [FormerlySerializedAs("m_openingSeconds")]
        [SerializeField]
        public float OpeningSeconds = 0.03f;

        [FormerlySerializedAs("m_closeSeconds")]
        [SerializeField]
        public float CloseSeconds = 0.1f;

        Coroutine m_coroutine;
        private int hashJoyfJump = 2081823275;
        float m_nextRequest;
        bool m_request;
        public bool Request
        {
            get { return m_request; }
            set
            {
                if (Time.time < m_nextRequest)
                {
                    return;
                }
                m_request = value;
                m_nextRequest = Time.time + 1.0f;
            }
        }

        IEnumerator BlinkRoutine()
        {
            while (true)
            {
                var waitTime = Time.time + Random.value * Interval;
                while (waitTime > Time.time)
                {
                    if (Request)
                    {
                        m_request = false;
                        break;
                    }
                    yield return null;
                }

                // close
                var value = 0.0f;
                var closeSpeed = 1.0f / CloseSeconds;
                while (true)
                {
                    value += Time.deltaTime * closeSpeed;
                    if (value >= 1.0f)
                    {
                        break;
                    }

                    m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), value);
                    yield return null;
                }
                m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 1.0f);

                // wait...
                yield return new WaitForSeconds(ClosingTime);

                // open
                value = 1.0f;
                var openSpeed = 1.0f / OpeningSeconds;
                while (true)
                {
                    value -= Time.deltaTime * openSpeed;
                    if (value < 0)
                    {
                        break;
                    }

                    m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), value);
                    yield return null;
                }
                m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0);
            }
        }

        private void OnEnable()
        {
            m_blendShapes = GetComponent<VRM.VRMBlendShapeProxy>();
            m_coroutine = StartCoroutine(BlinkRoutine());
        }

        private void OnDisable()
        {
            if (m_coroutine != null)
            {
                StopCoroutine(m_coroutine);
                m_coroutine = null;
            }
        }

        private float Speed = 2f;
        public void Update(){
            //Debug.Log("animacion:" + this.animator.GetCurrentAnimatorStateInfo(0).shortNameHash + " inf " + hashJoyfJump );
             if (Input.GetKeyDown(KeyCode.Space)){
                animator.PlayInFixedTime("Joyful Jump",-1, 0.30f);
                StartCoroutine(Smile());
             }
                
               
            
        }

        IEnumerator Smile(){
            float value = 0;
            while (true)
                {
                    value += Time.deltaTime * Speed;
                    if (value >= 1.0f)
                    {
                        break;
                    }
                    m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy), value);
                    yield return null;
                }
                Debug.Log("Sonrie");
                m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy), 1.0f); 
            
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Joyful Jump"))
            {
                Debug.Log("Jumping");
            }
               while (true)
                {
                    value -= Time.deltaTime * Speed;
                    if (value < 0f)
                    {
                        break;
                    }
                    m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy), value);
                    yield return null;
                }
                m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy), 0.0f);
            
        }
    }
}
