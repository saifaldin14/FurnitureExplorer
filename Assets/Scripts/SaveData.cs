using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveableObjectsInScene
{
    public SaveableObject[] SaveableObjects;
}

[System.Serializable]
public class SaveableObject
{
    public int id;
    public Vector3 position;
    public Quaternion rotation;
}
