using System;
using UnityEngine;

namespace Pattern.Singleton
{
    /// <summary>
    /// This is the basic monobehavior singleton implementation.
    /// This singleton is destroyed after scene changes
    /// Use <see cref="PersistentMonoSingleton{T}"/> if you want a persistent and global singleton instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        #region Fields
        private static T _instance;

        private SingletonInitializationStatus initializationStatus = SingletonInitializationStatus.None;
        #endregion

        #region Properties
        /// <summary>
        /// Get Instance
        /// </summary>
        
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null) //Create new Instance object if it doesn't exist
                    {
                        GameObject obj = new GameObject();
                        obj.name = "[Singleton] " + typeof(T).Name;
                        _instance = obj.AddComponent<T>();
                        _instance.OnMonoSingletonCreated();
                    }
                }
                return _instance;
            }
        }

        // Gets whether the singleton's instance is initialzied
        public virtual bool IsInitialized => this.initializationStatus == SingletonInitializationStatus.Initialized;
        #endregion

        #region Unity Messages
        // Use this for initialization
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;

                // Initialize existing instance
                InitializeSingleton();
            }
            else
            {
                // Destroy the duplicates
                if (Application.isPlaying) Destroy(gameObject);
                else DestroyImmediate(gameObject);
            }
        }

        #endregion

        #region Protected Methods
        // This gets called once the singleton's instance is created
        protected virtual void OnMonoSingletonCreated() { }
        protected virtual void OnInitializing() { }
        protected virtual void OnInitialized() { }
        #endregion

        #region Public Methods
        public virtual void InitializeSingleton()
        {
            if (this.initializationStatus != SingletonInitializationStatus.None) return;

            this.initializationStatus = SingletonInitializationStatus.Initializing;
            OnInitializing();
            this.initializationStatus = SingletonInitializationStatus.Initialized;
            OnInitialized();
        }

        public virtual void ClearSingleton() { }
        public static void CreateInstance()
        {
            DestroyInstance();
            _instance = Instance;
        }

        private static void DestroyInstance()
        {
            if (_instance == null) return;

            _instance.ClearSingleton();
            _instance = default(T);
        }
        #endregion
    }
}
