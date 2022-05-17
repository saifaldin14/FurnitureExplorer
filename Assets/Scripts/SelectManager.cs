using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectManager : MonoBehaviour
{
    public GameObject selectedObj;
    public TextMeshProUGUI objNameTxt;
    public GameObject selectUI;
    private BuildingManager buildingManager;
    private bool change = true;

    private MeshFilter meshFilter;
    private Mesh newMesh;
    private Mesh originalMesh;
    private Material originalMaterial;
    // Start is called before the first frame update
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        selectUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Object"))
                {
                    Select(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedObj != null)
        {
            Deselect();
        }
    }

    public void Delete()
    {
        GameObject objToDestroy = selectedObj;
        Deselect();
        Destroy(objToDestroy);
    }

    public void Move()
    {
        buildingManager.pendingObj = selectedObj;
    }

    public void Clone()
    {
        buildingManager.CloneObject(selectedObj);
    }

    public void Change()
    {
        if (change)
        {
            meshFilter = selectedObj.GetComponent<MeshFilter>();
            string name = selectedObj.name;
            int index = System.Array.IndexOf(buildingManager.names, name);
            // newMesh = buildingManager.usedObjects[index].GetComponent<MeshFilter>().sharedMesh;
            newMesh = buildingManager.usedMeshes[index];
            selectedObj.GetComponent<MeshRenderer>().material = buildingManager.usedObjects[index].GetComponent<MeshRenderer>().sharedMaterial;
            meshFilter.mesh = newMesh;
            selectedObj.GetComponent<SaveableObjectID>().id += 3;
        }
        else
        {
            selectedObj.GetComponent<MeshRenderer>().material = originalMaterial;
            meshFilter.mesh = originalMesh;
            selectedObj.GetComponent<SaveableObjectID>().id -= 3;
        }

        change = !change;
    }

    void Select(GameObject obj)
    {
        if (obj == selectedObj) return;
        Deselect();
        Outline outline = obj.GetComponent<Outline>();
        if (outline == null)
            obj.AddComponent<Outline>();
        else
            outline.enabled = true;
        objNameTxt.text = obj.name;
        selectedObj = obj;
        originalMesh = selectedObj.GetComponent<MeshFilter>().sharedMesh;
        originalMaterial = selectedObj.GetComponent<MeshRenderer>().sharedMaterial;
        selectUI.SetActive(true);
    }

    public void Deselect()
    {
        if (selectedObj != null)
        {
            selectedObj.GetComponent<Outline>().enabled = false;
            selectedObj = null;
            selectUI.SetActive(false);
        }
    }
}
