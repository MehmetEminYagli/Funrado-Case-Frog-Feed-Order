using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrappgeScript : MonoBehaviour
{
    [SerializeField] private int grappeID;
    [SerializeField] private List<Material> grappeColorList;
    [SerializeField] private Material FalseMaterialColor;
    private Material originalMaterial;
    void Start()
    {
        grappeID = 5;
        originalMaterial = GetComponentInChildren<Renderer>().material;
    }

    public int GetGrappeID()
    {
        return grappeID;
    }

    public void TrueGrape()
    {
        transform.DOScale(transform.localScale * 1.5f, 0.2f)
            .OnComplete(() =>
            {
                // Küçülme animasyonu
                transform.DOScale(transform.localScale / 1.5f, 0.2f);
            });
    }

    public void FalseGrape()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();

        // Büyürken rengi kırmızıya dönüyor
        transform.DOScale(transform.localScale * 1.5f, 0.2f)
            .OnStart(() =>
            {
                renderer.material = FalseMaterialColor; // Rengi kırmızıya çevir
            })
            .OnComplete(() =>
            {
                // Küçülme işlemi başlıyor ve orijinal renge geri dönüyoruz
                transform.DOScale(transform.localScale / 1.5f, 0.2f)
                    .OnComplete(() =>
                    {
                        renderer.material = originalMaterial; // Eski renge geri dön
                    });
            });
    }


}
