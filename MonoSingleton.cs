using UnityEngine;


/// <summary>
/// Unity-unique part
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //Determine if global undeleted
    public bool global = true;
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType<T>();
            }
            return instance;
        }
    }

    void Awake()
    {    
        //if it's global
        if (global)
        {
            //Determines if the current instance has a value and is the current script
            if (instance != null && instance != this.gameObject.GetComponent<T>())
            {
                //Verify that there are two identical scripts in the scenario, delete one, and jump out of the function
                Destroy(gameObject);
                return;
            }
            //If there is only one script for the scene, we will not delete it when the scene refreshes, ensuring that it exists forever from start to close
            DontDestroyOnLoad(this.gameObject);
            //assignment instruction
            instance = this.gameObject.GetComponent<T>();
        }
        //Provide the Start function for subclasses
        this.OnStart();
    }

    protected virtual void OnStart()
    {

    }
}