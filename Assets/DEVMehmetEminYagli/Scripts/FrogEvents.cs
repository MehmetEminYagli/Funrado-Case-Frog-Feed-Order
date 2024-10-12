using System;
using UnityEngine;

public class FrogEvents : MonoBehaviour
{
    public static event Action<GameObject> OnFrogSpawned; 
    public static event Action<GameObject> OnFrogDestroyed; 

    public static void FrogSpawned(GameObject frog)
    {
        OnFrogSpawned?.Invoke(frog);  
    }

    public static void FrogDestroyed(GameObject frog)
    {
        OnFrogDestroyed?.Invoke(frog);  
    }
}
