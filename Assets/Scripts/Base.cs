using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private Scaner _scaner;

    private List<Resource> _resources;
    private int _resourceCount = 0;

    private void Update()
    {
        _resources = _scaner.Scan();

        foreach (var resource in _resources)
        {
            if (resource.IsReserved == false)
            {
                foreach (var unit in _units)
                {
                    if (unit.isWorking == false)
                    {
                        resource.Reserved();

                        unit.MoveToResource(resource);
                        break;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Resource>(out Resource resource))
        {
            Destroy(resource.gameObject);

            _resourceCount++;
        }
    }
}
