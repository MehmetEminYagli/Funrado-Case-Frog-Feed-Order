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

    [SerializeField] private int tongueID;
    private Vector3 tongueTarget;

    public float GetMaxTongueLength()
    {
        return maxTongueLength;
    }

    public int GetTongueId()
    {
        return tongueID;
    }

    public TongueController GetTongueController()
    {
        return tonguePrefab.GetComponentInChildren<TongueController>();
    }

    public void ShootTongue()
    {
        if (currentTongue == null)
        {
            tongueID = GetComponent<FrogScript>().GetFrogID();
            tongueTarget = tongueSpawnPoint.right;
            currentTongue = Instantiate(tonguePrefab, tongueSpawnPoint.position, Quaternion.LookRotation(tongueTarget), tongueSpawnPoint);
            currentTongue.transform.DOScaleX(tonguePrefab.transform.localScale.x + GetMaxTongueLength(), tongueSpeed)
                .OnComplete(() => StartCoroutine(ReturnTongue()));
        }
        else
        {
            currentTongue.transform.localScale = new Vector3(tonguePrefab.transform.localScale.x, currentTongue.transform.localScale.y, currentTongue.transform.localScale.z);
            currentTongue.transform.DOScaleX(tonguePrefab.transform.localScale.x + GetMaxTongueLength(), tongueSpeed)
                .OnComplete(() => StartCoroutine(ReturnTongue()));
        }
    }

    public void StopAndReturnTongue()
    {
        Debug.Log("Dil durduruyor ve geri çekiliyor");
        currentTongue.transform.DOKill();
        StartCoroutine(ReturnTongue());
    }


    [SerializeField] private int trueGrapeCount;

    private IEnumerator ReturnTongue()
    {
        yield return new WaitForSeconds(tongueWaitTime);

        trueGrapeCount = currentTongue.GetComponentInChildren<TongueController>().GetGrappeIdList().Count;
        Vector3 targetPosition = tongueSpawnPoint.position;

        if (trueGrapeCount < 4)
        {
            ResetTonguePosition(targetPosition);
        }
        else
        {
            List<GameObject> touchedObjects = currentTongue.GetComponentInChildren<TongueController>().GetGrappeIdList();

            foreach (GameObject obj in touchedObjects)
            {
                StartCoroutine(MoveAndShrinkObject(obj, targetPosition, tongueSpeed / 1.5f));
            }
            ResetTonguePosition(targetPosition);
        }
    }

    private void ResetTonguePosition(Vector3 targetPosition)
    {
        currentTongue.transform.DOScaleX(tonguePrefab.transform.localScale.x, tongueSpeed).OnComplete(() =>
        {
            currentTongue.transform.position = targetPosition;
            currentTongue.GetComponentInChildren<TongueController>().ClearList();
        });
    }

    private IEnumerator MoveAndShrinkObject(GameObject obj, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = obj.transform.position;
        Vector3 startingScale = obj.transform.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            obj.transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            obj.transform.SetParent(null);
            // Calculate scale based on distance to target
            float distanceToTarget = Vector3.Distance(obj.transform.position, targetPosition);
            float scaleFactor = Mathf.Clamp01(distanceToTarget / 0.4f);

            obj.transform.localScale = startingScale * scaleFactor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object is exactly at the target position and scaled to a small size at the end
        obj.transform.position = targetPosition;
        obj.transform.localScale = startingScale * 0.1f; // Adjust final size as needed

        Destroy(obj);

        TongueController tongueController = currentTongue.GetComponentInChildren<TongueController>();
        if (tongueController != null)
        {
            tongueController.DestroyLastCollectedCell();
        }

        Destroy(gameObject);

    }



}
