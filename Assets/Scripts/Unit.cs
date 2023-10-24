using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private NavMeshAgent _agent;

    private bool _isWorking = false;
    private bool _IsHandsFull = false;

    public bool IsHandsFull => _IsHandsFull;
    public bool isWorking => _isWorking;

    public void MoveToResource(Resource resource)
    {
        if (_isWorking)
        {
            return;
        }

        _agent.SetDestination(resource.transform.position); 

        _isWorking = true;
    }

    public void MoveToBase()
    {
        var target = transform.parent;

        _agent.SetDestination(target.transform.position);
    }

    public void HandsFull()
    {
        _IsHandsFull = true;
    }

    public void HandsFree()
    {
        _IsHandsFull = false;
    }

    public void FinishWork()
    {
        _isWorking = false;
    }
}
