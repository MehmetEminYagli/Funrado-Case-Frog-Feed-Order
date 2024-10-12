using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CellScript : MonoBehaviour
{
    [SerializeField] private ObjectsColorMaterialList materialList;
    [SerializeField] private GameObject frogPrefab;
    [SerializeField] private GameObject grapePrefab;

    [SerializeField] private Transform spawnFrogPosition;
    [SerializeField] private Transform spawnGrapePosition;
    [SerializeField] private TongueController tongueScript;
    [SerializeField] private int cellID;

    private Renderer cellRenderer;

    [SerializeField] private RotationOptions selectedRotation;

    private bool canSpawn = false;

    public ObjectsColorMaterialList GetMaterialList()
    {
        return materialList;
    }

    public Transform GetSpawnFrogPosition()
    {
        return spawnFrogPosition;
    }

    public Transform GetSpawnGrapePosition()
    {
        return spawnGrapePosition;
    }

    public void SetCellID(int newCellID)
    {
        cellID = newCellID;
    }

    public int getCellID()
    {
        return cellID;
    }
    void Start()
    {
        GameStartBooleanControl();
        SetRotationDescription(selectedRotation);
        Quaternion rotation = GetRotation(selectedRotation);
        StartCoroutine(CheckAboveAndSpawn(rotation));
        SetCellID(materialList.SelectedMaterialID());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TongueController>(out TongueController tongue))
        {
            tongueScript = tongue;
        }
     
    }

    public TongueController GetTongueController()
    {
        return tongueScript;
    }


    private float detectionRadius = 0.1f;  // Üstteki cell ile aradaki minimum mesafe
    private bool IsCellScriptAbove()
    {
        // Üstümüzdeki cell'i tespit etmek için bir sphere (küre) kullanarak tarama yapıyoruz
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + Vector3.up * 0.5f, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != null && hitCollider.gameObject != this.gameObject)
            {
                // Üstümüzde başka bir cell var demektir
                return true;
            }
        }
        // Üstte başka bir cell yok
        return false;
;
    }

    private void AdjustColliderSizeBasedOnPosition()
    {
        if (IsCellScriptAbove())
        {
            // Üstte başka bir CellScript varsa, bu cell'in collider boyutunu küçült
            GetComponent<BoxCollider>().size = new Vector3(1, 0.1f, 1);
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            // Üstte başka bir cell yoksa collider boyutunu normale döndür
            GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            GetComponent<BoxCollider>().enabled = true;
        }
    }


    IEnumerator CheckAboveAndSpawn(Quaternion rotation)
    {
        while (true)
        {
            AdjustColliderSizeBasedOnPosition();
            if (!IsCellScriptAbove())
            {
                canSpawn = true;
                SpawnIfPossible(rotation);
                yield break;
            }
            else
            {
                canSpawn = false;
            }
            yield return new WaitForSeconds(.2f);
        }
    }
    private void SpawnIfPossible(Quaternion rotation)
    {
        if (!canSpawn) return;

        if (materialList.GetFrogSpawnBoolean())
        {
            // Frog spawn işlemi
            SpawnFrog(spawnFrogPosition, rotation);
        }

        if (materialList.GetGrapeSpawnBoolean())
        {
            // Grape spawn işlemi
            SpawnGrape(spawnGrapePosition, GetGrapeRotation(rotation));
        }
    }


    private void GameStartBooleanControl()
    {
        cellRenderer = GetComponentInChildren<Renderer>();
        materialList = GetComponent<ObjectsColorMaterialList>();

        Material cellMaterial = materialList.SelectedCellMaterial();
        if (cellMaterial != null)
        {
            cellRenderer.material = cellMaterial;
        }

        if (materialList.GetFrogSpawnBoolean() && materialList.GetGrapeSpawnBoolean())
        {
            Debug.Log("ayni anda iki farkli nesne spawn edilemez");
            return;
        }
        if (!materialList.GetFrogSpawnBoolean() && !materialList.GetGrapeSpawnBoolean())
        {
            Debug.Log("spawn edilecek nesne secilmedi");
            return;
        }
    }

    // Kurbağa spawn etme fonksiyonu
    public void SpawnFrog(Transform spawnPosition, Quaternion spawnRotation)
    {
        GameObject frog = Instantiate(frogPrefab, spawnPosition.position, spawnRotation, spawnPosition);
        FrogScript frogScript = frog.GetComponent<FrogScript>();
        frogScript.SetFrogID(materialList.SelectedMaterialID());
        frog.transform.localScale = Vector3.zero;
        frog.transform.DOScale(Vector3.one, .2f);


        Material frogMaterial = materialList.SelectedFrogMaterial();
        if (frogMaterial != null)
        {
            SkinnedMeshRenderer frogRenderer = frog.GetComponentInChildren<SkinnedMeshRenderer>();
            frogRenderer.material = frogMaterial;
        }
    }

    // Üzüm spawn etme fonksiyonu
    public void SpawnGrape(Transform spawnPosition, Quaternion spawnRotation)
    {
        GameObject grape = Instantiate(grapePrefab, spawnPosition.position, spawnRotation, spawnPosition);
        GrappgeScript grapescript = grape.GetComponent<GrappgeScript>();
        grapescript.SetGrapeID(materialList.SelectedMaterialID());
        grape.transform.localScale = Vector3.zero;
        grape.transform.DOScale(Vector3.one, .2f);

        Material grapeMaterial = materialList.SelectedGrapeMaterial();
        if (grapeMaterial != null)
        {
            Renderer grapeRenderer = grape.GetComponentInChildren<Renderer>();
            grapeRenderer.material = grapeMaterial;
        }
    }


    private Quaternion GetGrapeRotation(Quaternion frogRotation)
    {
        //kurbaganın baktıgı yon right up iken uzumun yonunu rigt up yapmak zorunda kalıyoruz bu sorunu cozuyor;
        return frogRotation * Quaternion.Euler(0, 45, 45);
    }


    public enum RotationOptions
    {
        Right,RightUp,Up,LeftUp,Left,LeftDown,Down,RightDown
    }


    private void SetRotationDescription(RotationOptions rotationOption)
    {
        string description = rotationOption switch
        {
            RotationOptions.Right => "Sağa doğru bakar.",
            RotationOptions.RightUp => "Sağ üst köşeye doğru bakar.",
            RotationOptions.Up => "Yukarıya doğru bakar.",
            RotationOptions.LeftUp => "Sol üst köşeye doğru bakar.",
            RotationOptions.Left => "Sola doğru bakar.",
            RotationOptions.LeftDown => "Sol alt köşeye doğru bakar.",
            RotationOptions.Down => "Aşağıya doğru bakar.",
            RotationOptions.RightDown => "Sağ alt köşeye doğru bakar.",
            _ => "Geçersiz açı."
        };

    }

    private Quaternion GetRotation(RotationOptions rotationOption)
    {
        return rotationOption switch
        {
            RotationOptions.Right => Quaternion.Euler(0, 0, 0),
            RotationOptions.RightDown => Quaternion.Euler(0, 45, 0),
            RotationOptions.Down => Quaternion.Euler(0, 90, 0),
            RotationOptions.LeftDown => Quaternion.Euler(0, 135, 0),
            RotationOptions.Left => Quaternion.Euler(0, 180, 0),
            RotationOptions.LeftUp => Quaternion.Euler(0, 225, 0),
            RotationOptions.Up => Quaternion.Euler(0, 270, 0),
            RotationOptions.RightUp => Quaternion.Euler(0, 315, 0),
            _ => Quaternion.identity // Geçersiz durum
        };
    }
}
