using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Customer : Human
{
    [Header("Payment Variables")]
    [SerializeField] GameObject paymentUI;
    [SerializeField] TMP_Text paymentText;
    [SerializeField] Transform paymentPosition;
    public float paymentCash;

    DeskManager choosenDesk;
    new void Awake()
    {
        base.Awake();
        RandomIndex();
        StartCoroutine(MoveDelay());
    }
    public void GoPlayComputer(Transform ChairPosition, DeskManager ChoosenDesk)
    {
        canWalk = false;
        ChoosenDesk.isFull = true;
        choosenDesk = ChoosenDesk;
        Agent.SetDestination(ChairPosition.position);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (!collision.TryGetComponent<ComputerParts>
            (out var computerParts) && computerParts.itemInfo != ComputerParts.ItemInfo.Chair) return;
        //if (cube == null || cube.itemInfo != Cube.ItemInfo.Chair) return;
        StartCoroutine(PlayComputer(Random.Range(10, 20)));
    }
    IEnumerator PlayComputer(float duration)
    {
        //Debug.Log("bekleme suresi " + duration);
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            //Debug.Log("pc de oturuyor");
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        paymentCash = (int)elapsedTime;
        SitUpComputer();
        //Debug.Log("pc den kalktý");
        //useComputer = false;
        //canUse = false;
        //Debug.Log("Odenecek para" + paymentCash);
        //yield return new WaitForSeconds(30);
        //choosenDesk.isFull = false;
        //canUse = true;
    }
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //if (useComputer) return;
            ShopSytem.instance.currentCash += paymentCash;
            //Move(index);
            if (paymentCash <= 0) return;
            StartCoroutine(PaymentUý());
            paymentCash = 0f;
            canWalk = true;
        }
    }
    public void SitUpComputer()
    {
        Agent.SetDestination(paymentPosition.position);
        Invoke(nameof(SetDeskVariables), 10);
    }
    void SetDeskVariables()
    {
        SetChoosenDeskIsFull();
        choosenDesk = null;
    }
    bool SetChoosenDeskIsFull() => choosenDesk.isFull = false;
    IEnumerator PaymentUý()
    {
        paymentUI.SetActive(true);
        paymentText.SetText($" + ${paymentCash}");
        yield return new WaitForSeconds(1f);
        paymentUI.SetActive(false);
    }
    public override void TakeDamage()
    {
        base.TakeDamage();
    }
}
