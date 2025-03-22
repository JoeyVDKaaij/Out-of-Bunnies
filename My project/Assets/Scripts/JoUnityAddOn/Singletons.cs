using System;
using UnityEngine;

namespace JoUnityAddOn
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance { get; protected set; }
        public static T Instance => instance;

        protected virtual void Awake()
        {
            SetInstance();
        }

        protected virtual void OnEnable()
        {
            SetInstance();
        }

        protected virtual void OnDestroy()
        {
            RemoveInstance();
        }

        protected virtual void OnDisable()
        {
            RemoveInstance();
        }

        private void SetInstance()
        {
            if (instance == null)
            {
                instance = this as T;
                
                if (instance != null)
                {
                    if (transform.parent.gameObject != null) DontDestroyOnLoad(transform.parent.gameObject);
                    else DontDestroyOnLoad(gameObject);
                }
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void RemoveInstance()
        {
            instance = null;
        }
    }
}