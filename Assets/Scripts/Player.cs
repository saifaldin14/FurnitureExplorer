using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.S)))
        {
            SaveableObjectID[] objectsInScene = FindObjectsOfType<SaveableObjectID>();
            SaveableObjectsInScene objectData = new SaveableObjectsInScene
            {
                SaveableObjects = new SaveableObject[objectsInScene.Length]
            };

            for (int i = 0; i < objectData.SaveableObjects.Length; i++)
            {
                objectData.SaveableObjects[i] = new SaveableObject
                {
                    id = objectsInScene[i].id,
                    position = objectsInScene[i].transform.position,
                    rotation = objectsInScene[i].transform.rotation
                };
            }

            SaveSystem.Save(objectData, "Objects");
            Debug.Log("Saving Scene");
        }

        if (Input.GetKeyDown((KeyCode.L)))
        {
            SaveSystem.Load(out SaveableObjectsInScene loadedObjectData, "Objects");
            SaveableObjectID[] objectsInScene = FindObjectsOfType<SaveableObjectID>();

            for (int i = 0; i < objectsInScene.Length; i++)
            {
                Destroy(objectsInScene[i].gameObject);
            }

            for (int i = 0; i < loadedObjectData.SaveableObjects.Length; i++)
            {
                if (loadedObjectData.SaveableObjects[i] != null)
                {
                    if (SaveableObjectLibrary.SaveableObjects[loadedObjectData.SaveableObjects[i].id] != null)
                    {
                        Instantiate
                        (
                            SaveableObjectLibrary.SaveableObjects[loadedObjectData.SaveableObjects[i].id],
                            loadedObjectData.SaveableObjects[i].position,
                            loadedObjectData.SaveableObjects[i].rotation
                        );
                    }
                }
            }

            Debug.Log("Loading Scene");
        }

    }
}
