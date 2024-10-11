using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    [SerializeField] private FrogScript frog;
    [SerializeField] private TongueController tongue;
    [SerializeField] private int frogID;
    private bool isProcessing = false;  // İşlemde olup olmadığını kontrol eden değişken

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
            // Tıklanan nesnenin üzerinde Frog script'i var mı kontrol et
            if (hit.transform.TryGetComponent<FrogScript>(out FrogScript frogScript))
            {
                frog = frogScript;
                frogID = frogScript.GetFrogID();

                // İşlem başladı, isProcessing true olarak ayarlanır
                isProcessing = true;

                if (hit.transform.TryGetComponent<TongueScript>(out TongueScript tonguescript))
                {
                    tongue = tonguescript.GetTongueController();
                    frogScript.SetFrogID(frogID);
                    tonguescript.ShootTongue();

                    // TongueScript'teki işlemler tamamlandığında isProcessing'i false yapabilirsiniz.
                    StartCoroutine(ResetProcessing());  // Coroutine ile işlem süresi sonunda resetlenecek
                }
                else
                {
                    Debug.Log("Dil scripti bulunamadi");
                    isProcessing = false;  // Eğer dil scripti yoksa işlemi yeniden açıyoruz
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
        // İşlemin tamamlanması için bir süre bekleyebilirsiniz, buradaki süreyi ayarlayın
        yield return new WaitForSeconds(2.5f);  // 2 saniye bekleyelim

        isProcessing = false;  // İşlem tamamlandı
    }
}
