using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Human : MonoBehaviour
{
    [SerializeField] Transform[] target;
    public bool canWalk = true;
    public NavMeshAgent Agent;
    int index;
    MeshRenderer mr;
    // Customers Controller ac tamamlanan ve bos olan masalara random bi customer secip gondersin customer codundada human soyundan gelip movement olsn;
    protected void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        mr = GetComponent < MeshRenderer>();
    }
    protected void RandomIndex()
    {
        index = Random.Range(0, target.Length);
    }
    protected IEnumerator MoveDelay()
    {
        while (Vector3.Distance(transform.position, target[index].position) > 2f && canWalk)
        {
            Agent.SetDestination(target[index].position);
            yield return null;
        }
        yield return new WaitForSeconds(Random.Range(2, 5));
        RandomIndex();
        StartCoroutine(MoveDelay());
    }
    public virtual void TakeDamage()
    {
        canWalk = false;
        mr.enabled = false;
        //transform.position = target[Random.Range(0, target.Length)].position;
        //canWalk = true;
        //mr.enabled = true;
    }
}