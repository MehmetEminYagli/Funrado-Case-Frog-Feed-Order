using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CellScript : MonoBehaviour
{
    // Start is called before the first frame update
    /*
     üzerinde üzüm mü yoksa kurbağamı var onu kontrol edicek.
    kendi rengi ile uyumlu kurbağa ve üzümü spawn edicek
    eğer üstünde başka bir cell scripti var ise üzüm spawn etmiyecek
    üstü boş ise rengini kontrol edip o renkte üzüm spawn edicek.
    eğer kurbağa doğru renkteki üzümleri alırsa boolean bir değişken göndericek ve kendini destroy ettiğinde
    cell üstünde kurbağa scripti yoksa diğer cell scriptleride 
    kendi üstünde grape scripti varmı yok mu onu kontrol edicek. //yada sürekli bunu kontrol edebiliriz ama performans sorunu olur.
    üstünde grape scripti bulamayanlar kendilerini destroy edicek.
    !!! OK ve kurbağa yönünü kontrol edicek ve önünde cell scripti olan yöne bakıcak şekilde spawn olucak..
     
     */

    [SerializeField] private ObjectsColorMaterialList materialList;
    [SerializeField] private GameObject frogPrefab;
    [SerializeField] private GameObject grapePrefab;

    [SerializeField] private Transform spawnFrogPosition;
    [SerializeField] private Transform spawnGrapePosition;

    private Renderer cellRenderer;

    [SerializeField] private RotationOptions selectedRotation;

    private bool canSpawn = false;

    void Start()
    {
        GameStartBooleanControl();
        SetRotationDescription(selectedRotation);
        Quaternion rotation = GetRotation(selectedRotation);
        StartCoroutine(CheckAboveAndSpawn(rotation));
    }

    private bool IsCellScriptAbove()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponent<CellScript>() != null)
            {
                return true;
            }
        }
        return false;
    }


    IEnumerator CheckAboveAndSpawn(Quaternion rotation)
    {
        while (true)
        {
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

        //FrogScript frogScript = frog.GetComponent<FrogScript>();


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
    
        //GrappgeScript grapeScript = grape.GetComponent<GrappgeScript>();

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
        Right,     // 0° - Sağa doğru bakar
        RightUp,   // 45° - Sağ üst köşeye doğru bakar
        Up,        // 90° - Yukarıya doğru bakar
        LeftUp,    // 135° - Sol üst köşeye doğru bakar
        Left,      // 180° - Sola doğru bakar
        LeftDown,  // 225° - Sol alt köşeye doğru bakar
        Down,      // 270° - Aşağıya doğru bakar
        RightDown  // 315° - Sağ alt köşeye doğru bakar
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
