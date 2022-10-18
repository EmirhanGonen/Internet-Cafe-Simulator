using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemManager : MonoBehaviour
{
    /*#region PlaceSystem
    public Vector3 place, tempObjectPos;
    private RaycastHit _Hit;
    public GameObject objectToPlace, TempObject; //Instantiate
    public GameObject objectToPlacePrefab, TempObjectPrefab; // Prefab

    public string[] Tags;

    public bool placeNow;
    public bool placeItem;

    public bool tempObjectExist;
    bool tagSame;
    #endregion

    public static TakeItemManager instance;
    void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (placeNow) SendRay();
        if (placeItem) objectToPlace = objectToPlacePrefab;
        if (Input.GetKeyDown(KeyCode.E)) PlaceItem();
    }


    private void SendRay()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _Hit))
        {
            place = _Hit.point;
            Debug.DrawRay(transform.position, place, Color.red);
            CheckTag(_Hit.transform.gameObject);
            if (tagSame && objectToPlace == null)
            {
                Cube hitCube = _Hit.transform.GetComponent<Cube>();
                tempObjectPos = hitCube.spawnPos;
                objectToPlacePrefab = hitCube.PlaceToGameObject;
                TempObjectPrefab = hitCube.TempObject;
                if (TempObjectPrefab == hitCube.TempObject) Destroy(_Hit.transform.gameObject);
            }
            if (_Hit.transform.CompareTag("Desk") || _Hit.transform.CompareTag("Ground")) // Tag iþini objectoPlace den String tag alýrýz mouse hem yere hem Masaya koyuldugu icin
            {
                if (!tempObjectExist)
                {
                    TempObject = Instantiate(TempObjectPrefab, place, Quaternion.identity);
                    tempObjectExist = true;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(objectToPlace, place + tempObjectPos, TempObject.transform.rotation);

                    objectToPlacePrefab = null;
                    objectToPlace = null;
                    TempObjectPrefab = null;
                    objectToPlacePrefab = null;
                    placeNow = false;
                    placeItem = false;
                    tagSame = false;

                    Destroy(TempObject);
                    tempObjectExist = false;
                }

                if (TempObject != null)
                {
                    TempObject.transform.position = place + tempObjectPos;
                    if (Input.GetKey(KeyCode.Q)) RotateObject(5);

                    if (Input.GetKey(KeyCode.E)) RotateObject(-5);
                }

            }

            if (Input.GetMouseButton(1))
            {
                placeNow = false;
                placeItem = false;

                Destroy(TempObject);
                tempObjectExist = false;
            }
        }
    }
    public void CheckTag(GameObject checkObject)
    {
        for (int i = 0; i < Tags.Length; i++)
        {
            if (checkObject.CompareTag(Tags[i]))
            {
                tagSame = true;
            }
        }
    }
    void RotateObject(int i)
    {
        TempObject.transform.Rotate(new Vector3(0, transform.rotation.y + i, 0));
    }
    void PlaceItem()
    {
        placeNow = true;
        placeItem = true;
   }*/
}
