using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	[SerializeField] private GameObject pooledObject;
    [SerializeField] private int poolCount;
    [SerializeField] private bool willGrow;

    [SerializeField] private List<GameObject> objects;

    void Start() {
        objects = new List<GameObject>();
        for(int i = 0; i < poolCount; i++)
            Create();
    }

    void Create(){
        GameObject obj = (GameObject) Instantiate(pooledObject, transform);
        obj.SetActive(false);
        objects.Add(obj);
    }
    
    public GameObject GetPooledObject(){
        for(int i = 0; i < objects.Count; i++){
            if(!objects[i].activeSelf){
                return objects[i];
            }
        }

        if(willGrow){
            GameObject obj = (GameObject) Instantiate(pooledObject, transform);
            obj.SetActive(false);
            objects.Add(obj);
            poolCount++;
            return obj;
        }
        return null;
    }
}
