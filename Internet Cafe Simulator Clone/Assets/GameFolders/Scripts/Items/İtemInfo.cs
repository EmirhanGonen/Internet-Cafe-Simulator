using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class İtemInfo : MonoBehaviour
{
    [SerializeField] public string Itemname;

    void OnMouseEnter()
    {
        MyCharacterController.instance.itemText.SetText(Itemname);
    }
    void OnMouseExit()
    {
        MyCharacterController.instance.itemText.SetText("");
    }
}
