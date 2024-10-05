using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueController : MonoBehaviour
{
    [SerializeField] private GrappgeScript grappgescript;

    void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<GrappgeScript>(out grappgescript))
        {
            Debug.Log(grappgescript);
            if (grappgescript != null)
            {
                Debug.Log("Hedefin ID'si: " + grappgescript.GetGrappeID());
            }
        }
        else { Debug.Log("yokk"); }
    }
}
