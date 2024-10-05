using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    private void Update()
    {
        // Sağ fare tuşuna basıldığında
        if (Input.GetMouseButtonDown(1)) // 1 sağ fare tuşu
        {
            HandleRightClick();
        }
    }

    private void HandleRightClick()
    {
        // Fare pozisyonunu al
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Raycast ile tıklanan nesneyi kontrol et
        if (Physics.Raycast(ray, out hit))
        {
            // Tıklanan nesnenin üzerinde Frog script'i var mı kontrol et
            if (hit.transform.TryGetComponent<FrogScript>(out FrogScript frogScript))
            {
                Debug.Log("kurbaga bulundı var");
                if (hit.transform.TryGetComponent<TongueScript>(out TongueScript tongue))
                {
                    tongue.ShootTongue(); // Örneğin, dil atma fonksiyonunu çağırabilirsiniz
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
