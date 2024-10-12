using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TongueController : MonoBehaviour
{
    [SerializeField] private GrappgeScript grappgescript;
    private TongueScript tongueScript;
    [SerializeField] private List<GameObject> grappeIdList;
    [SerializeField] private List<GameObject> collectedCells = new List<GameObject>();
    [SerializeField] private int grappeid;
    private int frogID;
    private int tongueID;



    void Start()
    {
        tongueScript = GetComponentInParent<TongueScript>();
        tongueID = tongueScript.GetTongueId();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FrogScript>(out FrogScript frog))
        {
            frogID = frog.GetFrogID();
        }

        if (other.TryGetComponent<GrappgeScript>(out grappgescript))
        {
            if (grappgescript != null)
            {
                grappeid = grappgescript.GetGrappeID();
                if (frogID == grappeid)
                {
                    grappgescript.TrueGrape();
                    grappeIdList.Add(grappgescript.gameObject);
                }
                else
                {
                    grappgescript.FalseGrape();
                    tongueScript.StopAndReturnTongue();
                }
            }
        }

        if (other.TryGetComponent<CellScript>(out CellScript cell))
        {

            collectedCells.Add(cell.gameObject);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CellScript>(out CellScript cell))
        {
            if (cell.GetSpawnGrapePosition().childCount == 0 && cell.GetSpawnFrogPosition().childCount == 0)
            {
                DestroyLastCollectedCell();
            }
        }
    }

    public void DestroyLastCollectedCell()
    {
        if (collectedCells.Count > 0)
        {
            GameObject lastCell = collectedCells[collectedCells.Count - 1];

            lastCell.transform.DOScale(lastCell.transform.localScale * 1.2f, 0.2f)
        .OnComplete(() =>
        {
            lastCell.transform.DOScale(Vector3.zero, 0.4f) 
                .OnComplete(() =>
                {
                    Destroy(lastCell);
                });
        });
            collectedCells.RemoveAt(collectedCells.Count - 1);
        }
    }

    public List<GameObject> GetCollectedCells()
    {
        return collectedCells;
    }
    public void CellClearList()
    {
        for (int i = collectedCells.Count - 1; i > 0; i--)
        {
            collectedCells.RemoveAt(i);
        }
    }
    public List<GameObject> GetGrappeIdList()
    {
        return grappeIdList;
    }
    public void ClearList()
    {
        grappeIdList.Clear();
    }
}
