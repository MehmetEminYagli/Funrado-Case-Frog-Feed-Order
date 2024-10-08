using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private Transform SpawnPosition;

    private Renderer cellRenderer;

    void Start()
    {
        cellRenderer = GetComponentInChildren<Renderer>();
        materialList = GetComponent<ObjectsColorMaterialList>();

        Material cellMaterial = materialList.SelectedCellMaterial();
        if (cellMaterial != null)
        {
            cellRenderer.material = cellMaterial;
        }

        if(materialList.GetFrogSpawnBoolean() && materialList.GetGrapeSpawnBoolean())
        {
            Debug.Log("ayni anda iki farkli nesne spawn edilemez");
            return;
        }
        if (!materialList.GetFrogSpawnBoolean() && !materialList.GetGrapeSpawnBoolean())
        {
            Debug.Log("spawn edilecek nesne secilmedi");
            return;
        }

        if (materialList.GetFrogSpawnBoolean())
        {
            //frogtrue ise yapılacaklar
            SpawnFrog(SpawnPosition.position);
        }


        if (materialList.GetGrapeSpawnBoolean())
        {
            //grape true  ise yapılacaklar
            SpawnGrape(SpawnPosition.position);
        }
    }

    // Kurbağa spawn etme fonksiyonu
    public void SpawnFrog(Vector3 spawnPosition)
    {
        GameObject frog = Instantiate(frogPrefab, spawnPosition, Quaternion.identity);
        FrogScript frogScript = frog.GetComponent<FrogScript>();

        // Kurbağa için materyali al ve uygula
        Material frogMaterial = materialList.SelectedFrogMaterial();
        if (frogMaterial != null)
        {
            SkinnedMeshRenderer frogRenderer = frog.GetComponentInChildren<SkinnedMeshRenderer>();
            frogRenderer.material = frogMaterial;
        }
    }

    // Üzüm spawn etme fonksiyonu
    public void SpawnGrape(Vector3 spawnPosition)
    {
        GameObject grape = Instantiate(grapePrefab, spawnPosition, Quaternion.identity);
        GrappgeScript grapeScript = grape.GetComponent<GrappgeScript>();

        // Üzüm için materyali al ve uygula
        Material grapeMaterial = materialList.SelectedGrapeMaterial();
        if (grapeMaterial != null)
        {
            Renderer grapeRenderer = grape.GetComponentInChildren<Renderer>();
            grapeRenderer.material = grapeMaterial;
        }
    }

}
