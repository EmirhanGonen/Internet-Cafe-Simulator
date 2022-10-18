using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerParts : InteractableObjects
{
    #region Singleton
    public static ComputerParts instance;
    #endregion
    #region UI System
    [SerializeField] GameObject PlaceDropInfo;
    #endregion
    #region ItemInfoEnum

    public enum ItemInfo { Computer, Monitor, Keyboard, Mouse, Desk, Chair };
    public ItemInfo itemInfo;
    #endregion
    #region Place System
    public Vector3 OffsetsForHands { get { return offsetsForHands; } }


    bool CanPut = true;
    float yPos;
    #endregion
    #region Move Object System
    [Header("Move Object System")]

    [SerializeField] int MaterialIndex;
    [SerializeField] public Vector3 offsetsForPlace;
    #endregion

    new void Awake()
    {
        instance = this;
        base.Awake();
        mr = GetComponent<MeshRenderer>();
        MaterialHolder = GameObject.FindGameObjectWithTag("MaterialHolder").GetComponent<MeshRenderer>();
    }
    void Start()
    {
        //Masa ve sandalyeyi kolayca calamayacagi icin gerceksi olsun diye ekledim
        if (itemInfo == ItemInfo.Desk || itemInfo == ItemInfo.Chair) return;
        ListHolder.instance.PcParts.Add(this);
    }
    new void OnMouseDown()
    {
        if (MovingItem) return;
        base.OnMouseDown();
        SetActiveInfoUI(true);
    }
    new void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isNowPlace) OnPlace();
        base.Update();
    }
    void OnPlace()
    {
        if (!transform.parent.CompareTag("MainCamera")) return;
        transform.SetParent(player); // ama obje koyareken rot etsin istemiorm
        transform.localRotation = Quaternion.identity; //Bunu Yapmamýn sebebi objelerim kameraya gore rot etmesi 
        mr.material = MaterialHolder.materials[2];
        isNowPlace = true;
        StartCoroutine(MoveObject());
    }
    IEnumerator MoveObject()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + offsetsForPlace);
            transform.position = new Vector3(mousePos.x, yPos, mousePos.z);

            if (Input.GetKey(KeyCode.Q)) RotateObject(5);
            if (Input.GetKey(KeyCode.R)) RotateObject(-5);
            if (Input.GetKey(KeyCode.V)) SetDestinationObject(0.2f);
            if (Input.GetKey(KeyCode.B)) SetDestinationObject(-0.2f);
            yield return null;
        }
        if (CanPut) PutDownObjects();
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(MoveObject());
        }
    }
    public override void PutDownObjects()
    {
        base.PutDownObjects();
        mr.material = MaterialHolder.materials[MaterialIndex];
        SetActiveInfoUI(false);
    }
    void SetDestinationObject(float i) => offsetsForPlace.z += i;
    void RotateObject(int i) => transform.Rotate(new Vector3(0, transform.rotation.y + i, 0));
    void SetPosY(Collider collisionObject, float setVar)
    {
        if (!collisionObject.TryGetComponent<ComputerParts>(out var computerParts)) return;
        if (computerParts.itemInfo != ItemInfo.Desk || itemInfo == ItemInfo.Chair || itemInfo == ItemInfo.Desk) return;
        yPos = setVar;
    }
    void OnTriggerEnter(Collider other) => CheckCanPlaceObjects(other, 1.3f, 3, false);
    void OnTriggerExit(Collider other) => CheckCanPlaceObjects(other, 0f, 2, true);
    void CheckCanPlaceObjects(Collider collisionObject, float setPosy, int materialIndex, bool canPut)
    {
        SetPosY(collisionObject, setPosy);
        if (!collisionObject.TryGetComponent<ComputerParts>(out var computerParts)) return;
        if (isNowPlace && computerParts.itemInfo != ItemInfo.Desk)
        {
            mr.material = MaterialHolder.materials[materialIndex];
            CanPut = canPut;
        }
    }
    void SetActiveInfoUI(bool Active) => PlaceDropInfo.SetActive(Active);
}