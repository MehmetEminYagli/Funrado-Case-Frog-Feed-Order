using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueController : MonoBehaviour
{
    [SerializeField] private GrappgeScript grappgescript;
    [SerializeField] private int grappeid;
    [SerializeField] private List<int> grappeIdList;
    private int frogID;

    void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<GrappgeScript>(out grappgescript))
        {

            if (grappgescript != null)
            {
                grappeid = grappgescript.GetGrappeID();
                if (frogID == grappeid)
                {
                    Debug.Log(grappeid);
                    grappeIdList.Add(grappeid);
                }
                else
                {
                    Debug.Log("üzümle frog idleri aynı değil");
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
