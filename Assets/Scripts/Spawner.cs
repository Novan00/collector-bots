using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private TerrainExtents _terrainExtents;
    [SerializeField] private Resource _resource;

    private List<Resource> _resourcesArray = new List<Resource>();
    private int _spawnCount = 5;
    private float _radius = 1;

    private void Start()
    {
        StartCoroutine(Spawn());

        StopCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (_resourcesArray.Count < _spawnCount)
        {
            var coordinateX = UnityEngine.Random.Range(_terrainExtents.terrainCoordinateX.transform.position.x, _terrainExtents.terrainCoordinateMinusX.transform.position.x);
            var coordinateZ = UnityEngine.Random.Range(_terrainExtents.terrainCoordinateZ.transform.position.z, _terrainExtents.terrainCoordinateMinusZ.transform.position.z);

            var spawnPosition = new Vector3(coordinateX, _resource.transform.localScale.y, coordinateZ);

            var freePosition = Physics.OverlapSphere(spawnPosition + Vector3.up, _radius);

            if (freePosition.Length == 0)
            {
                Resource newResource = Instantiate(_resource, spawnPosition, Quaternion.identity);

                newResource.Deleted += OnResourceDeleted;

                _resourcesArray.Add(newResource);
            }

            yield return null;
        }
    }

    private void OnResourceDeleted(Resource obj)
    {
        _resourcesArray.Remove(obj);
        obj.Deleted -= OnResourceDeleted;

        StartCoroutine(Spawn());
    }
}

[Serializable]

public struct TerrainExtents
{
    public Transform terrainCoordinateX;
    public Transform terrainCoordinateMinusX;
    public Transform terrainCoordinateZ;
    public Transform terrainCoordinateMinusZ;
}


