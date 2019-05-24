using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Generic singleton instance to create manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonInstance<T> : MonoBehaviour where T : MonoBehaviour 
    {
        private static T _instance;

        /// <summary>
        /// Gets a generic instance
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
        }
    }
}
