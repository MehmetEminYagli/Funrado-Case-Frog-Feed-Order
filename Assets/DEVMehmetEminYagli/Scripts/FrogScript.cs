using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    [SerializeField] private int frogID;

    public void SetFrogID(int newFrogID)
    {
        frogID = newFrogID;
    }


    public int GetFrogID()
    {
        return frogID;
    }
}
