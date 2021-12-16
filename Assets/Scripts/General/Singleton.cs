 using UnityEngine;
 
 /// <summary>
 /// Singleton only let one instace of Type T per scene.
 /// <para>Note: Singleton instance can be destroyed.</para>
 /// </summary>
 /// <typeparam name="T">T inherits from MonoBehavior</typeparam>
 public class Singleton<T> : MonoBehaviour where T : Singleton<T>
 {
    #region Singleton
    static T s_instance;
    public static T Instance
    {
        get
        {
            if (s_instance == null)
            {
                GameObject singletonObj = new GameObject();
                s_instance = singletonObj.AddComponent<T>();
                singletonObj.name = "Singleton - " + typeof(T).ToString();
            }
            return s_instance;
        }
        private set { }
    }

    public static bool HasInstance()
    {
        return s_instance != null;
    }
    protected void Awake()
    {
        if (HasInstance() && s_instance.GetInstanceID() != this.GetInstanceID())
        {
            Destroy(this);
        }
        else
        {
            s_instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
    protected void OnDestroy()
    {
        if (s_instance == this)
        {
            s_instance = null;
        }
    }
    #endregion

}