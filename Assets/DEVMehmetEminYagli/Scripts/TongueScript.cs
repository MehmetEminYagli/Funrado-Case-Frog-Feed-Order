using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TongueScript : MonoBehaviour
{

    [SerializeField] private GameObject tonguePrefab;
    [SerializeField] private Transform tongueSpawnPoint;
    [SerializeField] private GameObject currentTongue;

    [SerializeField] private float tongueSpeed = 5f;
    [SerializeField] private float maxTongueLength = 5f; 
    [SerializeField] private float tongueWaitTime = 0.5f;

    private Vector3 tongueTarget;
    private bool isReturning = false; 



    private void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    ShootTongue();
        //}

    }

    public void ShootTongue()
    {
        if(currentTongue != null)
        {
            Debug.Log("mevcuttda bir tongue prefabı zaten var");
            return;
        }

        tongueTarget = tongueSpawnPoint.right;
        currentTongue = Instantiate(tonguePrefab, tongueSpawnPoint.position, Quaternion.LookRotation(tongueTarget), tongueSpawnPoint);
        currentTongue.transform.DOScaleX(tonguePrefab.transform.localScale.x + maxTongueLength, tongueSpeed)
            .OnComplete(() => StartCoroutine(ReturnTongue(currentTongue)));
    }

    private IEnumerator ReturnTongue(GameObject tongue)
    {
        yield return new WaitForSeconds(tongueWaitTime);
        tongue.transform.DOScaleX(tonguePrefab.transform.localScale.x, tongueSpeed).OnComplete(() =>
        {
            Destroy(tongue);
        });
    }

}