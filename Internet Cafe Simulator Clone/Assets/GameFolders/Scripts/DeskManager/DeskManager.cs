using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeskManager : MonoBehaviour
{
    // Oturan musteri yap customer seklinde oturunca kendini set etsin eger masa kaldýrýlýrsa icindeki customeri SitUp computer calýssýn
    // Muþteri giderken masayý her saniye checklesinki Probelm Cýkmasn; yada bi eþya aldýgýmýz zaman baglý oldugu masayi bul o masanin customeri null deilse kaldýrsýn 
    CustomersController custom;
    ListHolder listHolder;
    #region UI's Object
    [SerializeField] Image[] CompleteIcons;
    [SerializeField] Sprite okeyIcon;
    [SerializeField] Sprite cursorIcon;
    [SerializeField] Canvas IconCanvas;

    [Space]
    #endregion
    #region PcSystem
    [Header("Pc System")]
    public GameObject Computer;
    public GameObject Monitor;
    public GameObject Keyboard;
    public GameObject Mouse;
    public GameObject Chair;

    public bool IsOkey => Computer != null && Monitor != null && Keyboard != null && Mouse != null && Chair != null;
    public bool isFull;
    #endregion
    #region Singleton
    public static DeskManager instance;
    #endregion
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        custom = CustomersController.instance;
        listHolder = ListHolder.instance;
        listHolder.Desks.Add(this);
        // Enum u prefabdan deðiþtiremiorz
    }
    void Update()
    {
        IconCanvas.transform.LookAt(Camera.main.transform.position);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<ComputerParts>(out var computerParts)) return;
        CheckComputer(computerParts);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<ComputerParts>(out var computerParts)) return;
        CheckComputer(computerParts);
    }
    void CheckComputer(ComputerParts computerParts)
    {
        if (ComputerParts.MovingItem) return;
        switch (computerParts.itemInfo)
        {
            case ComputerParts.ItemInfo.Computer:
                Computer = computerParts.gameObject;
                CompleteIcons[0].sprite = okeyIcon;
                break;
            case ComputerParts.ItemInfo.Monitor:
                Monitor = computerParts.gameObject;
                CompleteIcons[1].sprite = okeyIcon;
                break;
            case ComputerParts.ItemInfo.Keyboard:
                Keyboard = computerParts.gameObject;
                CompleteIcons[2].sprite = okeyIcon;
                break;
            case ComputerParts.ItemInfo.Mouse:
                Mouse = computerParts.gameObject;
                CompleteIcons[3].sprite = okeyIcon;
                break;
            case ComputerParts.ItemInfo.Chair:
                Chair = computerParts.gameObject;
                CompleteIcons[4].sprite = okeyIcon;
                break;
        }
        if (!IsOkey) return;
        IconCanvas.gameObject.SetActive(false);
    }
    void OnTriggerExit(Collider collision)
    {
        SetNullCollisionExitObject(collision.gameObject);
    }
    void OnCollisionExit(Collision collision)
    {
        SetNullCollisionExitObject(collision.gameObject);
    }
    void SetNullCollisionExitObject(GameObject collisionObject)
    {
        if (!collisionObject.TryGetComponent<ComputerParts>(out var computerParts)) return;
        switch (computerParts.itemInfo)
        {
            case ComputerParts.ItemInfo.Computer:
                if (Computer == null) return;
                Computer = null;
                CompleteIcons[0].sprite = cursorIcon;
                break;
            case ComputerParts.ItemInfo.Monitor:
                if (Monitor == null) return;
                CompleteIcons[1].sprite = cursorIcon;
                Monitor = null;
                break;
            case ComputerParts.ItemInfo.Keyboard:
                if (Keyboard == null) return;
                CompleteIcons[2].sprite = cursorIcon;
                Keyboard = null;
                break;
            case ComputerParts.ItemInfo.Mouse:
                if (Mouse == null) return;
                CompleteIcons[3].sprite = cursorIcon;
                Mouse = null;
                break;
            case ComputerParts.ItemInfo.Desk:
                break;
            case ComputerParts.ItemInfo.Chair:
                if (Chair == null) return;
                CompleteIcons[4].sprite = cursorIcon;
                Chair = null;
                break;
        }
        if (IsOkey) return;
        IconCanvas.gameObject.SetActive(true);
    }
}