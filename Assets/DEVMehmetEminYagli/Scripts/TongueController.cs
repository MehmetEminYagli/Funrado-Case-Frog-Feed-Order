using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TongueController : MonoBehaviour
{
    [SerializeField] private GrappgeScript grappgescript;
    [SerializeField] private int grappeid;
    [SerializeField] private List<GameObject> grappeIdList;
    [SerializeField] private List<GameObject> collectedCells = new List<GameObject>();
    private int frogID;
    [SerializeField] private int tongueID;


    private TongueScript tongueScript;

    void Start()
    {
        tongueScript = GetComponentInParent<TongueScript>();
        tongueID= tongueScript.GetTongueId();
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
                    Debug.Log("Üzüm ID ile Frog ID aynı, devam ediliyor.");
                }
                else
                {
                    Debug.Log("Üzüm ID farklı! Dil geri çekiliyor.");
                    grappgescript.FalseGrape();
                    tongueScript.StopAndReturnTongue();
                }
            }
        }

        if (other.TryGetComponent<CellScript>(out CellScript cell))
        {
            if (tongueID == cell.getCellID())
            {
                collectedCells.Add(cell.gameObject);
                Debug.Log("afsf");
            }
        }


    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CellScript>(out CellScript cell))
        {
            if (cell.GetSpawnGrapePosition().childCount == 0 && cell.GetSpawnFrogPosition().childCount == 0)
            {
                Debug.Log("bura çalıştı");
                DestroyLastCollectedCell();
            }
        }
    }

    public void DestroyLastCollectedCell()
    {
        if (collectedCells.Count > 0)
        {
            GameObject lastCell = collectedCells[collectedCells.Count - 1];

            lastCell.transform.DOScale(lastCell.transform.localScale * 1.2f, 0.2f) // 1.5 kat büyüt
        .OnComplete(() =>
        {
            lastCell.transform.DOScale(Vector3.zero, 0.4f) // 0'a küçült
                .OnComplete(() =>
                {
                    Destroy(lastCell); // Yok et
                });
        });


            collectedCells.RemoveAt(collectedCells.Count - 1);
        }
    }

    public List<GameObject> GetCollectedCells()
    {
        return collectedCells;
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
