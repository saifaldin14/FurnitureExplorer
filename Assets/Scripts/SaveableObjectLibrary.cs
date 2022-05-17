using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableObjectLibrary : MonoBehaviour
{
    public static Dictionary<int, GameObject> SaveableObjects;

    [SerializeField] private GameObject[] RegisteredObjects;
    private void Awake()
    {
        SaveableObjects = new Dictionary<int, GameObject>();

        for (int i = 0; i < RegisteredObjects.Length; i++)
        {
            int idToRegister = RegisteredObjects[i].GetComponent<SaveableObjectID>().id;
            SaveableObjects.Add(idToRegister, RegisteredObjects[i]);
        }
    }
}
