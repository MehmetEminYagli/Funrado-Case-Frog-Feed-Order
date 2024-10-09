using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    [SerializeField] private FrogScript frog;
    [SerializeField] private TongueController tongue;
    [SerializeField] private int frogID;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();

        }
    }

    private void HandleRightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Tıklanan nesnenin üzerinde Frog script'i var mı kontrol et
            if (hit.transform.TryGetComponent<FrogScript>(out FrogScript frogScript))
            {
                frog = frogScript;
                frogID = frogScript.GetFrogID();
                if (hit.transform.TryGetComponent<TongueScript>(out TongueScript tonguescript))
                {
                    tongue = tonguescript.GetTongueController();
                    frogScript.SetFrogID(frogID);
                    tonguescript.ShootTongue();

                }
                else
                {
                    Debug.Log("dil scripti bulunamadi");
                }
            }
            else
            {
                Debug.Log("Tıklanan nesnede Frog script'i yok.");
            }
        }
    }



}
