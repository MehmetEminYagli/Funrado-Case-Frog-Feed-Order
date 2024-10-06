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
        //aklıma suan bu geldi.
        tongueScript = GetComponentInParent<TongueScript>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<GrappgeScript>(out grappgescript))
        {
            if (grappgescript != null)
            {
                grappeid = grappgescript.GetGrappeID();
                if (frogID == grappeid)
                {
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

    }


  


    public void SetFrogID(int newFrogID)
    {
        frogID = newFrogID;
        Debug.Log("Frog ID set edildi: " + frogID);
    }

    public void ClearList()
    {
        grappeIdList.Clear();
    }

}
