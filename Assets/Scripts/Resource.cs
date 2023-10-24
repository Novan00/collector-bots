using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool InHand {  get; private set; }
    public bool IsReserved {  get; private set; } = false;

    public event Action<Resource> Deleted;

    private void OnDestroy()
    {
        Deleted?.Invoke(this);
    }

    public void Took()
    {
        InHand = true;
    }

    public void Reserved()
    {
        IsReserved = true;
    }
}
