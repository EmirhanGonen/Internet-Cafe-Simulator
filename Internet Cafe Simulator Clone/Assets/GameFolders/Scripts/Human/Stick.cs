using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : InteractableObjects
{
    Animator animator;
    new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    new void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0) && transform.parent.CompareTag("MainCamera"))
        {
            animator.enabled = true;
            //if (animator.GetBool("isSwing")) return;
            StartCoroutine(SetisSwingBool());
            //transform.Rotate(65f, transform.localRotation.y, transform.localRotation.z);
        }
    }
    public override void DropItem()
    {
        base.DropItem();
        animator.enabled = false;
    }

    IEnumerator SetisSwingBool()
    {
        SetisSwingBool(true);
        yield return new WaitForSeconds(0.4f);
        SetisSwingBool(false);
    }
   
    void SetisSwingBool(bool setVar) => animator.SetBool("isSwing", setVar);
    void OnTriggerEnter(Collider other)
    {     
        if (!other.TryGetComponent<Human>(out Human human)) return;
        human.TakeDamage();
    }
}
