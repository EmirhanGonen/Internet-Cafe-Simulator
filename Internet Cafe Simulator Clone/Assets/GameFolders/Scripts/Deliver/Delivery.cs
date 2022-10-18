using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] ParticleSystem smokeEffect;
    public GameObject Object;
    ÝtemInfo itemInfo;
    void Awake()
    {
        itemInfo = GetComponent<ÝtemInfo>();
    }
    void Start()
    {
        itemInfo.Itemname = $"Delivery({Object.name})";
    }
    void OnMouseDown()
    {
        GameObject smoke = Instantiate(smokeEffect.gameObject, transform.position, transform.rotation);
        GameObject purschedItem = Instantiate(Object, transform.position, Quaternion.identity);
        Destroy(this.gameObject,0.1f);
    }
}
