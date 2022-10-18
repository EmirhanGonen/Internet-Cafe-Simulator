using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class CustomersController : MonoBehaviour
{
    [SerializeField] List<Customer> Customers = new();
    [Header("PcSystem Variables")]
    DeskManager choosenDesk;
    ListHolder listHolder;
    bool useComputer;
    bool canUse = true;
    #region Singleton
    public static CustomersController instance;
    #endregion

    void Start()
    {
        listHolder = ListHolder.instance;
    }
    private void Update()
    {
        ChooseDesk(); //Choosen Deski 30 saniye yada dk da bi checklet
    }

    void ChooseDesk()
    {
        for (int i = 0; i < listHolder.Desks.Count; i++)
        {
            if (listHolder.Desks[i].IsOkey && !listHolder.Desks[i].isFull && MyComputer.CafeOpen)
            {
                choosenDesk = listHolder.Desks[i];
                Customer RandomCustomer = Customers[Random.Range(0, Customers.Count)];
                Debug.Log(Customers.IndexOf(RandomCustomer));
                RandomCustomer.GoPlayComputer(choosenDesk.Chair.transform, choosenDesk);
                choosenDesk = null;
                break;
                //Debug.Log("Pc Secildi Secen Customers : " + this.name);
                //canWalk = false;
                //choosenDesk = listHolder.Desks[i];
                //choosenDesk.isFull = true;
                //Agent.SetDestination(choosenDesk.Chair.transform.position);
            }
            //if (listHolder.Desks.Count <= 0 || choosenDesk != null) return;

        }
    }
}

/*#region PcSystem


{
    instance = this;
    base.Awake();
}
new void Start()
{
    listHolder = ListHolder.instance;
    base.RandomIndex();
    base.StartCoroutine(MoveDelay());
}
void Update()


#region PlayComputer && Payment System



#endregion
#region Movement
void Move(int index)
{
    /*for (int i = 0; i < listHolder.Desks.Count; i++)
    {
        if (listHolder.Desks[i].IsOkey && !listHolder.Desks[i].isFull && !useComputer && canUse && MyComputer.CafeOpen)
        {
            Vector3 chairPos = listHolder.Desks[i].Chair.transform.position;
            Agent.SetDestination(chairPos);
            listHolder.Desks[i].isFull = true;
            choosenDesk = listHolder.Desks[i];
            useComputer = true;
            canUse = false;
            MoveDesk(i);
            Debug.Log("pc secildi");
            break;
        }
        Debug.Log("sdada");
        if (!listHolder.Desks[i].IsOkey && listHolder.Desks[i].isFull)
        {
            Debug.Log("s");
            listHolder.Desks[i].isFull = false;
            useComputer = false;
            canUse = true;
            StopAllCoroutines();
            if (paymentCash > 0) { Agent.SetDestination(paymentPosition.position); return; }
        }
        if (!canUse && !useComputer) Agent.SetDestination(paymentPosition.position);    // bilgisayardan kalktýysa ama bilgisayar secemiyorsa  
        //if (!useComputer && canUse) ;   //bilgisayarda oturmuyorsa ve bilgisayar kullabiliosa
    }
}
void MoveDesk(int index)
{
    Agent.SetDestination(listHolder.Desks[index].Chair.transform.position);
}

#endregion*/
