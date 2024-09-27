using UnityEngine;

namespace Pattern.Singleton
{
    /// <summary>
    /// This singleton is persistent across scenes by calling <see cref="UnityEngine.Object.DontDestroyOnLoad(Object)"/>
    /// </summary>
    public abstract class PersistentMonoSingleton<T> : MonoSingleton<T> where T : MonoSingleton<T>
    {
        #region Protected Methods
        protected override void OnInitializing()
        {
            base.OnInitializing();
            if (Application.isPlaying)
                DontDestroyOnLoad(gameObject);
        }
        #endregion
    }
}
