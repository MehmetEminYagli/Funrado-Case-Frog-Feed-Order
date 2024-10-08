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


    public TongueController GetTongueController()
    {
        return tonguePrefab.GetComponentInChildren<TongueController>();
    }



    public void ShootTongue()
    {
        if (currentTongue == null)
        {
            tongueTarget = tongueSpawnPoint.right;
            currentTongue = Instantiate(tonguePrefab, tongueSpawnPoint.position, Quaternion.LookRotation(tongueTarget), tongueSpawnPoint);
            currentTongue.transform.DOScaleX(tonguePrefab.transform.localScale.x + maxTongueLength, tongueSpeed)
                .OnComplete(() => StartCoroutine(ReturnTongue()));
        }
        else
        {
            currentTongue.transform.localScale = new Vector3(tonguePrefab.transform.localScale.x, currentTongue.transform.localScale.y, currentTongue.transform.localScale.z);
            currentTongue.transform.DOScaleX(tonguePrefab.transform.localScale.x + maxTongueLength, tongueSpeed)
                .OnComplete(() => StartCoroutine(ReturnTongue()));
        }
    }

    public void StopAndReturnTongue()
    {
        Debug.Log("Dil durduruyor ve geri çekiliyor");
        currentTongue.transform.DOKill(); 
        StartCoroutine(ReturnTongue()); 
    }

    private IEnumerator ReturnTongue()
    {
        yield return new WaitForSeconds(tongueWaitTime);

        currentTongue.transform.DOScaleX(tonguePrefab.transform.localScale.x, tongueSpeed).OnComplete(() =>
        {
            currentTongue.transform.position = tongueSpawnPoint.position;
            currentTongue.GetComponent<TongueController>().ClearList();
        });
    }


    //public void ChangeDirection(Vector3 newDirection)
    //{
    //    transform.right = newDirection;
    //    Debug.Log("Dil yönü değiştirildi: " + newDirection);
    //}
}