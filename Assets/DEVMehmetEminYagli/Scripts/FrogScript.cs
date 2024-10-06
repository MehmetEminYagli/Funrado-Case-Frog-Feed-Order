using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    [SerializeField] private int FrogID;
    [SerializeField] private List<Material> FrogColorList;
    void Start()
    {
        FrogID = Random.RandomRange(0,10);
    }

    public int GetFrogID()
    {
        return FrogID;
    }


}



//[SerializeField] private int frogID;
//[SerializeField] private TongueScript tongueScript;

//private void Awake()
//{
//    tongueScript = GetComponent<TongueScript>();
//}
//private void Start()
//{
//    frogID = Random.RandomRange(0, 10);
//}

//public int GetFrogID()
//{
//    return frogID;
//}

//public bool TryShootTongue(out TongueController tongueController)
//{
//    if (tongueScript != null)
//    {
//        tongueScript.ShootTongue();
//        tongueController = tongueScript.GetTongueController();
//        return true;
//    }
//    tongueController = null;
//    return false;
//}
