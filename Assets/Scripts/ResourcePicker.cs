using UnityEngine;

[RequireComponent(typeof(Unit))]

public class ResourcePicker : MonoBehaviour
{
    [SerializeField] private Transform _hand;

    private Unit _unit;

    private void Start()
    {
        _unit = GetComponent<Unit>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Resource>(out Resource resource))
        {
            if (resource.InHand == true)
            {
                return;
            }

            if (_unit.IsHandsFull == true)
            {
                return;
            }

            resource.Took();

            _unit.HandsFull();

            resource.Deleted += Resource_Deleted;
            resource.gameObject.transform.SetParent(_hand);
            resource.gameObject.transform.position = _hand.transform.position;

            _unit.MoveToBase();
        }
    }

    private void Resource_Deleted(Resource obj)
    {
        _unit.FinishWork();

        _unit.HandsFree();

        obj.Deleted -= Resource_Deleted;
    }
}