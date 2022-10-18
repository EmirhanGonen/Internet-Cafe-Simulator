using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Human
{
    ListHolder listholder;
    [SerializeField] float TimeDuration;
    float elapsedTime;
    ComputerParts selectedStealObject;


    void Start()
    {
        listholder = ListHolder.instance;
        RandomIndex();
        StartCoroutine(MoveDelay());
    }
    void Update()
    {
        CheckTimeDuration();
    }
    void CheckTimeDuration()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime < TimeDuration) return;
        MoveToObject();
        if (CanStealSelectedObjectDistance()) StealObject();
    }
    void MoveToObject()
    {
        if (selectedStealObject) return;
        canWalk = false;
        selectedStealObject = listholder.PcParts[Random.Range(0, listholder.PcParts.Count)];
        Agent.SetDestination(selectedStealObject.transform.position);
    }
    void StealObject()
    {
        selectedStealObject.CarryItem(transform);
        canWalk = true;
    }
    public override void TakeDamage() //Human Soyuna ata
    {
        base.TakeDamage();
        // Setactive False Random bi movement bolgesinde true olsn
        elapsedTime = 0f;
        if (!selectedStealObject) return;
        selectedStealObject.PutDownObjects();
        InteractableObjects.MovingItem = true; //çünkü elimizde sopa varken vurabilirz anca ve sopa elimizdeyken itme almamamz lazm.
        selectedStealObject = null;
        
    }
    bool CanStealSelectedObjectDistance() => Vector3.Distance(transform.position, selectedStealObject.transform.position) < 2f;
}