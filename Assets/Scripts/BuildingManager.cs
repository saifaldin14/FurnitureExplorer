using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public string[] names;
    public GameObject[] usedObjects;
    public Mesh[] usedMeshes;
    public GameObject pendingObj;
    public float gridSize;
    public float rotateAmount;
    public float negativeRotate;
    public bool gridOn = true;
    public bool canPlace = true;
    private Vector3 pos;
    private RaycastHit hit;
    private Material originalMaterial;
    private SelectManager selectManager;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Toggle gridToggle;
    [SerializeField] private Material[] materials;

    void Start()
    {
        selectManager = GameObject.Find("SelectManager").GetComponent<SelectManager>();

    }
    // Update is called once per frame
    void Update()
    {
        if (pendingObj != null)
        {
            if (gridOn)
            {
                pendingObj.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x),
                    RoundToNearestGrid(pos.y),
                    RoundToNearestGrid(pos.z)
                );
            }
            else
            {
                pendingObj.transform.position = pos;
            }

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }

            if (Input.GetKeyDown((KeyCode.R)))
            {
                RotateObject(rotateAmount);
            }

            if (Input.GetKeyDown((KeyCode.T)))
            {
                RotateObject(negativeRotate);
            }

            UpdateMaterials();
        }
    }

    void PlaceObject()
    {
        pendingObj.GetComponent<MeshRenderer>().sharedMaterial = originalMaterial;
        pendingObj = null;
    }

    void UpdateMaterials()
    {
        if (canPlace && pendingObj != null)
        {
            pendingObj.GetComponent<MeshRenderer>().material = materials[0];
        }
        else if (!canPlace && pendingObj != null)
        {
            pendingObj.GetComponent<MeshRenderer>().material = materials[1];
        }
    }

    public void SelectObject(int index)
    {
        selectManager.Deselect();
        pendingObj = Instantiate(objects[index], pos, objects[index].transform.rotation);
        pendingObj.name = objects[index].name;
        originalMaterial = objects[index].GetComponent<MeshRenderer>().sharedMaterial;
    }

    public void CloneObject(GameObject obj)
    {
        selectManager.Deselect();
        pendingObj = Instantiate(obj, pos, obj.transform.rotation);
        pendingObj.name = obj.name;
        originalMaterial = obj.GetComponent<MeshRenderer>().sharedMaterial;
    }

    public void RotateObject(float amount)
    {
        pendingObj.transform.Rotate(Vector3.forward, amount);
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }
    }

    float RoundToNearestGrid(float pos)
    {
        // Changeable grid system
        float xDiff = pos % gridSize;
        pos -= xDiff;

        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }
}
