using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrappgeScript : MonoBehaviour
{
    [SerializeField] private int grappeID;
    [SerializeField] private Material FalseMaterialColor;
    private Material originalMaterial;
    void Start()
    {

        originalMaterial = GetComponentInChildren<Renderer>().material;
    }
    public int GetGrappeID()
    {
        return grappeID;
    }

    public void SetGrapeID(int newGrapeID)
    {
        grappeID = newGrapeID;
    }


    public void TrueGrape()
    {
        transform.DOScale(transform.localScale * 1.5f, 0.2f)
            .OnComplete(() =>
            {
                transform.DOScale(transform.localScale / 1.5f, 0.2f);
            });
    }

    public void FalseGrape()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();

        transform.DOScale(transform.localScale * 1.5f, 0.2f)
            .OnStart(() =>
            {
                renderer.material = FalseMaterialColor;
            })
            .OnComplete(() =>
            {
                transform.DOScale(transform.localScale / 1.5f, 0.2f)
                    .OnComplete(() =>
                    {
                        renderer.material = originalMaterial;
                    });
            });
    }
}
