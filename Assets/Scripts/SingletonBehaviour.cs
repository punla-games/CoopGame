using UnityEngine;

public abstract class SingletonBehaviour<T>:MonoBehaviour where T : SingletonBehaviour<T>
{
    private static T instance = null;

    public static T Get
    {
        get
        {
            if(instance==null)
            {
                instance=FindAnyObjectByType<T>();
            }

            return instance;
        }
    }
}
