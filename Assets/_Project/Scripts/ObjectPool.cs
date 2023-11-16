using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;

    public static ObjectPool Instance
    {
        get
        {
            if(instance)
            {
                return instance;
            }
            var obj = new GameObject("Object Pool");
            instance = obj.AddComponent<ObjectPool>();
            return instance;
        }
    }

    private Dictionary<string, List<GameObject>> pool = new Dictionary<string, List<GameObject>>();

    public void SetupPoolObject(GameObject prefab, int count)
    {
        pool[prefab.name] = new List<GameObject>();

        while (count > 0)
        {
            count--;
            pool[prefab.name].Add(Instantiate(prefab));
        }

        foreach (var obj in pool[prefab.name])
        {
            obj.SetActive(false);
            obj.transform.SetParent(gameObject.transform);
        }
    }

    public GameObject GetObject(string name,GameObject prefab)
    {
        if (prefab == null)
        {
            return null;
        }

        if (!pool.ContainsKey(name))
        {
            pool[name] = new List<GameObject> { Instantiate(prefab) };
        }

        var objects = pool[name];
        foreach (var obj in objects)
        {
            if (obj && !obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        var newObj = Instantiate(prefab);
        objects.Add(newObj);
        return newObj;
    }
}