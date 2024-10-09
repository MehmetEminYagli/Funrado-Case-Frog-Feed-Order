using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueController : MonoBehaviour
{
    [SerializeField] private GrappgeScript grappgescript;
    [SerializeField] private int grappeid;
    [SerializeField] private List<int> grappeIdList;
    private int frogID;


    private TongueScript tongueScript;

    void Start()
    {
        tongueScript = GetComponentInParent<TongueScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<FrogScript>(out FrogScript frog))
        {
            frogID = frog.GetFrogID();
        }

        if (other.TryGetComponent<GrappgeScript>(out grappgescript))
        {
            if (grappgescript != null)
            {
                grappeid = grappgescript.GetGrappeID();
                if (frogID == grappeid)
                {
                    grappgescript.TrueGrape();
                    grappeIdList.Add(grappeid);
                    Debug.Log("Üzüm ID ile Frog ID aynı, devam ediliyor.");
                }
                else
                {
                    Debug.Log("Üzüm ID farklı! Dil geri çekiliyor.");
                    grappgescript.FalseGrape();
                    tongueScript.StopAndReturnTongue();
                }
            }
        }


        if (other.TryGetComponent<CellScript>(out CellScript cellScript))
        {
            if (cellScript != null)
            {
                //dil uzamaya devam eder
            }
            else
            {
                //dil geri çekilir geri çekilirken üzümlerde kurbağa doğru gider.
            }

        }

    }


    public void ClearList()
    {
        grappeIdList.Clear();
    }

}
