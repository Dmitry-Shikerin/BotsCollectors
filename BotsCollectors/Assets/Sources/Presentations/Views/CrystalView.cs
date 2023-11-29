using System;
using Sources.PresentationsInterfaces.Vievs;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(Rigidbody))]
public class CrystalView : MonoBehaviour, ICrystalView
{
    private NavMeshObstacle _navMeshObstacle;
    private Rigidbody _rigidbody;

    public Vector3 Position => transform.position;
    
    private void Start()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _rigidbody = GetComponent<Rigidbody>();
    }


    public void SetUnavailable()
    {
        //TODO заменить на константу
        _navMeshObstacle.enabled = false;
        _rigidbody.isKinematic = true;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent.transform;
        transform.localPosition = Vector3.zero;
    }

    public void RemoveParent()
    {
        //TODO это метод базы чтобы забрать ресурс
        transform.parent = null;
        transform.position = transform.position;
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(false);
    }

    public void Hide()
    {
        gameObject.layer = 0;
    }

    public void SetAvailable()
    {
        //TODO из вьюшки кристалла
    }
}
