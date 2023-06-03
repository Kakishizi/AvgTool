using UnityEngine;


public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool global = true;
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject(typeof(T).Name);
                instance = obj.AddComponent<T>();
            }
            return instance;
        }

    }

    void Start()
    {
        if (global) DontDestroyOnLoad(this.gameObject);
        this.OnStart();
    }

    protected virtual void OnStart()
    {

    }
}