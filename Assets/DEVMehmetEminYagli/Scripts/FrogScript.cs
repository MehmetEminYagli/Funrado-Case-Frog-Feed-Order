using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    [SerializeField] private int FrogID;
    [SerializeField] private SkinnedMeshRenderer frogRenderer;
    void Start()
    {
        frogRenderer = this.GetComponentInChildren<SkinnedMeshRenderer>();
        
    }

    public int GetFrogID()
    {
        return FrogID;
    }
}
