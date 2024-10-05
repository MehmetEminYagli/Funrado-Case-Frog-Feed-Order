using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TongueScript : MonoBehaviour
{

    [SerializeField] private GameObject tonguePrefab; // Sahnedeki dil objesi (LineRenderer ekli)
    [SerializeField] private Transform tongueSpawnPoint; // Kurbağanın ağzı (dilin başlangıç noktası)
    [SerializeField] private float tongueSpeed = 5f; // Dilin uzama hızı
    [SerializeField] private float maxTongueLength = 5f; // Dilin maksimum uzama mesafesi
    [SerializeField] private float tongueWaitTime = 0.5f;
    private Vector3 tongueTarget; // Dilin hedef yönü
    private bool isReturning = false; // Dil geri dönüyor mu
    private float currentTongueLength = 0f; // Şu anki dil uzunluğu


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ShootTongue();
        }

    }

    private void ShootTongue()
    {
        tongueTarget = tongueSpawnPoint.right;
        GameObject tongue = Instantiate(tonguePrefab, tongueSpawnPoint.position, Quaternion.LookRotation(tongueTarget), tongueSpawnPoint);
        //tongue.transform.DOScaleX(tonguePrefab.transform.localScale.x + maxTongueLength, tongueSpeed);

        tongue.transform.DOScaleX(tonguePrefab.transform.localScale.x + maxTongueLength, tongueSpeed)
            .OnComplete(() => StartCoroutine(ReturnTongue(tongue)));

    }

    private IEnumerator ReturnTongue(GameObject tongue)
    {
        yield return new WaitForSeconds(tongueWaitTime);

        // Dili geri çekme işlemi
        tongue.transform.DOScaleX(tonguePrefab.transform.localScale.x, tongueSpeed).OnComplete(() =>
        {
            Destroy(tongue);
        });
    }





    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(1) && !tongueLineRenderer.enabled)
    //    {
    //        ShootTongue();
    //    }

    //    if (tongueLineRenderer.enabled && !isReturning)
    //    {
    //        currentTongueLength += tongueSpeed * Time.deltaTime;
    //        currentTongueLength = Mathf.Min(currentTongueLength, maxTongueLength); // Maksimum uzunluğu aşmasın

    //        tongueLineRenderer.SetPosition(0, tongueSpawnPoint.position); // Dilin başlangıç noktası
    //        tongueLineRenderer.SetPosition(1, tongueSpawnPoint.position + tongueTarget * currentTongueLength); // Dilin uç noktası

    //        if (currentTongueLength >= maxTongueLength)
    //        {
    //            isReturning = true;
    //        }
    //    }

    //    else if (tongueLineRenderer.enabled && isReturning)
    //    {
    //        currentTongueLength -= tongueSpeed * Time.deltaTime;
    //        currentTongueLength = Mathf.Max(currentTongueLength, 0f); // Minimum sıfıra düşsün

    //        tongueLineRenderer.SetPosition(0, tongueSpawnPoint.position); // Dilin başlangıç noktası
    //        tongueLineRenderer.SetPosition(1, tongueSpawnPoint.position + tongueTarget * currentTongueLength); // Dilin uç noktası

    //        if (currentTongueLength <= 0f)
    //        {
    //            tongueLineRenderer.enabled = false;
    //            isReturning = false;
    //        }
    //    }
    //}

    //void ShootTongue()
    //{
    //    tongueTarget = -transform.forward;
    //    currentTongueLength = 0f;
    //    tongueLineRenderer.enabled = true;
    //}
}