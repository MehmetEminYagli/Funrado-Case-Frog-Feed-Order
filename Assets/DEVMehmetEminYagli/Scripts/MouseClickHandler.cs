using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    [SerializeField] private FrogScript frog;
    [SerializeField] private TongueController tongue;
    [SerializeField] private int frogID;
    private bool isProcessing = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isProcessing)  // Eğer işlemde değilse tıklamaya izin ver
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
            if (hit.transform.TryGetComponent<FrogScript>(out FrogScript frogScript))
            {
                frog = frogScript;
                frogID = frogScript.GetFrogID();

                isProcessing = true;

                if (hit.transform.TryGetComponent<TongueScript>(out TongueScript tonguescript))
                {
                    tongue = tonguescript.GetTongueController();
                    frogScript.SetFrogID(frogID);
                    tonguescript.ShootTongue();

                    StartCoroutine(ResetProcessing());  // Coroutine ile işlem süresi sonunda resetlenecek
                }
                else
                {
                    Debug.Log("Dil scripti bulunamadi");
                    isProcessing = false;
                }
            }
            else
            {
                Debug.Log("Tıklanan nesnede Frog script'i yok.");
                isProcessing = false;
            }
        }
    }

    private IEnumerator ResetProcessing()
    {
        yield return new WaitForSeconds(2.2f); 
        isProcessing = false;
    }
}
