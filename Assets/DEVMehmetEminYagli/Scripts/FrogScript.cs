using UnityEngine;
using System;

public class FrogScript : MonoBehaviour
{
    [SerializeField] private int frogID;
    private void Start()
    {
        FrogEvents.FrogSpawned(this.gameObject);
    }

    private void OnDestroy()
    {
        FrogEvents.FrogDestroyed(this.gameObject); 
    }

    public void SetFrogID(int newFrogID)
    {
        frogID = newFrogID;
    }


    public int GetFrogID()
    {
        return frogID;
    }
}
