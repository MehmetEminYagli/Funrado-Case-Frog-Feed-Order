using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappgeScript : MonoBehaviour
{
    [SerializeField] private int grappeID;
    [SerializeField] private List<Material> grappeColorList;

    void Start()
    {
        grappeID = 5;
    }

    public int GetGrappeID()
    {
        return grappeID;
    }


 


}
