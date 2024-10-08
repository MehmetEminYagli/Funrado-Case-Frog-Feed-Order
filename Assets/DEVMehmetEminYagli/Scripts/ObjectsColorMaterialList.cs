using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsColorMaterialList : MonoBehaviour
{
    [SerializeField] private List<Material> cellMaterialList;
    [SerializeField] private List<Material> frogMaterialList;
    [SerializeField] private List<Material> grapeMaterialList;

    [SerializeField] private int selectedMaterialID;

    [SerializeField] private bool grapeSpawn;
    [SerializeField] private bool frogSpawn;

    public bool GetGrapeSpawnBoolean()
    {
        return grapeSpawn;
    }

    public bool GetFrogSpawnBoolean()
    {
        return frogSpawn;
    }

    public Material SelectedCellMaterial()
    {
        if (selectedMaterialID >= 0 && selectedMaterialID < cellMaterialList.Count)
        {
            return cellMaterialList[selectedMaterialID];
        }
        else
        {
            Debug.Log("material seçilmedi");
            return null;
        }
    }


    public Material SelectedFrogMaterial()
    {
        if (selectedMaterialID >= 0 && selectedMaterialID < frogMaterialList.Count)
        {
            return frogMaterialList[selectedMaterialID];
        }
        else
        {
            Debug.Log("material seçilmedi");
            return null; 
        }
    }

    public Material SelectedGrapeMaterial()
    {
        if (selectedMaterialID >= 0 && selectedMaterialID < grapeMaterialList.Count)
        {
            return grapeMaterialList[selectedMaterialID];
        }
        else
        {
            Debug.Log("material seçilmedi");
            return null; 
        }
    }
}
