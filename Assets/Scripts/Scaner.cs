using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    private List<Resource> ResourceFound = new List<Resource>(); 

    public List<Resource> Scan()
    {
        ResourceFound.Clear();

        var resources = Physics.OverlapSphere(transform.position, 100);

        foreach (var resource in resources)
        {
            if (resource.TryGetComponent(out Resource newResource))
            {
                ResourceFound.Add(newResource); 
            }
        }

        return ResourceFound;
    }
}
