using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObjects : MonoBehaviour
{
    #region PickUp System
    #region Variables
    public static bool MovingItem;
    public bool isNowPlace;
    protected Transform player;
    [SerializeField] protected Vector3 offsetsForHands;
    [SerializeField] protected Vector3 anglesForHands;
    #endregion
    #region Compenents
    protected Rigidbody rb;
    protected MeshRenderer mr;
    protected MeshRenderer MaterialHolder;
    [SerializeField] protected BoxCollider Boxcol;
    #endregion
    #endregion
    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Body").transform;
        MovingItem = false;
    }
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isNowPlace) DropItem();
    }
    protected void OnMouseDown()
    {
        CarryItem(Camera.main.transform);
    }
    public void CarryItem(Transform setParent)
    {
        if (setParent.CompareTag("MainCamera")) MovingItem = true;
        Debug.Log(MovingItem);
        transform.SetParent(setParent);
        rb.isKinematic = true;
        transform.localPosition = offsetsForHands;
        if (Boxcol != null) Boxcol.isTrigger = true;
        transform.localRotation = Quaternion.Euler(anglesForHands);
    }
    public virtual void PutDownObjects()
    {
        rb.isKinematic = false;
        if (Boxcol != null) Boxcol.isTrigger = false;
        isNowPlace = false;
        MovingItem = false;
        transform.SetParent(GameObject.Find("PcParts").transform);
    }
    public virtual void DropItem()
    {
        if (!transform.parent.CompareTag("MainCamera")) return;
        PutDownObjects();
        transform.eulerAngles = Vector3.zero;
        rb.AddForce(400 * player.forward);
    }
}