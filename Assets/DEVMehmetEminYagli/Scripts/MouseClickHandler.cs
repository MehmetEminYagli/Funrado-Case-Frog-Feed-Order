using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    private int frogID;
    [SerializeField] private GameController gameController;
    private bool isProcessing = false;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isProcessing && gameController.GetRemainingClicks() > 0) 
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
                frogID = frogScript.GetFrogID();
                isProcessing = true;

                if (hit.transform.TryGetComponent<TongueScript>(out TongueScript tonguescript))
                {

                    frogScript.SetFrogID(frogID);
                    tonguescript.ShootTongue();
                    gameController.DecreaseClickCount();
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
        yield return new WaitForSeconds(1f);
        isProcessing = false;
    }



}
