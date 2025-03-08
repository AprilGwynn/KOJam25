using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Takes a bunch of ground objects (in z position order) and places them in a ring around this gameobject.
/// Assumes each segment has a y size of 1 unit!!! (does it...)
/// Also parents all the items to the closest ground object (by z position)
/// </summary>
public class MapRingAssembler : MonoBehaviour
{
    public GameObject objectsParent;
    public GameObject itemsParent;
    private List<GameObject> _objectList;
    private List<GameObject> _itemList;
    public float yOffset = 4;
    public bool autoUpdateYOffset = false;
    
    // Start is called before the first frame update
    void Start()
    {
        InitLists();
        
        ParentItemsToObjects();
        AssembleRing();
    }

    void InitLists()
    {
        _objectList = new List<GameObject>();
        foreach (Transform child in objectsParent.transform)
        {
            _objectList.Add(child.gameObject);
        }
        _itemList = new List<GameObject>();
        foreach (Transform child in itemsParent.transform)
        {
            _itemList.Add(child.gameObject);
        }
    }

    public void ParentItemsToObjects()
    {
        foreach (GameObject item in _itemList)
        {
            GameObject closestObj = _objectList[0];
            float closestObjDistance = Single.PositiveInfinity;
            foreach (GameObject obj in _objectList)
            {
                float itemZpos = item.transform.position.z;
                float objZpos = obj.transform.position.z;
                if (Mathf.Abs(itemZpos - objZpos) < closestObjDistance)
                {
                    closestObj = obj;
                    closestObjDistance = Mathf.Abs(itemZpos - objZpos);
                }
            }
            
            item.transform.SetParent(closestObj.transform);
        }
    }
    public void AssembleRing()
    {
        _objectList.Sort(CompareObjsByZPos);
        int index = 0;
        GameObject newEmpty;
        foreach (var obj in _objectList)
        {
            newEmpty = Instantiate(new GameObject("MapSegmentRoot" + index), objectsParent.transform);
            newEmpty.transform.localPosition = Vector3.zero;
            obj.transform.SetParent(newEmpty.transform);
            obj.transform.localPosition = new Vector3(0, -yOffset, 0);
            newEmpty.transform.rotation = Quaternion.Euler(new Vector3( -360 / ((float)_objectList.Count) * index++, 0, 0));
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (autoUpdateYOffset)
        {
            foreach (var obj in _objectList)
            {
                obj.transform.localPosition = new Vector3(0, -yOffset, 0);
            }
        }
    }

    private static int CompareObjsByZPos(GameObject first, GameObject second)
    {
        if (first.gameObject.transform.position.z > second.gameObject.transform.position.z)
        {
            return 1;
        }
        else return -1;
    }
}

