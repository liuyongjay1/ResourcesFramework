using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T mInstance = null;

    public static T Instance
    {
        get
        {
            if (mInstance == null)
            {
                LogManager.LogError("Instance is null,Singleton: " + typeof(T).Name);
                return null;
            }

            return mInstance;
        }
    }

    public static void InitMonoSingleton(Transform root)
    {
        GameObject go = new GameObject(typeof(T).Name);
        mInstance = go.AddComponent<T>();
        go.transform.parent = root;
    }

}