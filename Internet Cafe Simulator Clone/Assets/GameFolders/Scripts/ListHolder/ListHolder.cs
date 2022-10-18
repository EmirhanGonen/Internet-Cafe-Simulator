using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListHolder : MonoBehaviour
{
    public List<DeskManager> Desks = new();
    public List<ComputerParts> PcParts = new();
    public static ListHolder instance;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        //if (Desks.Count > 0 && !Desks[0].isFull) Debug.Log(Desks.Count);
    }
}
