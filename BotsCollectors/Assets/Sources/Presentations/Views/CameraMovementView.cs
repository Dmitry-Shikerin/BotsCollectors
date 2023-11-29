using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sources.Controllers.CameraMovements;
using Sources.Domain.CameraMovements;
using Sources.PresentationsInterfaces.Vievs;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovementView : MonoBehaviour, ICameraMovementView
{
    
    [SerializeField] private Transform _camera;
    private CameraMovementPresenter _presenter;
    
    private float _scrollWheel;
    private float _multiplier = 1;
    private bool _isLeftRotation;
    private bool _isRightRotation;
    private Vector2 _movementDirection;

    private void OnEnable() => 
        _presenter.Enable();

    private void OnDisable() => 
        _presenter.Disable();

    private void Update() => 
        _presenter.Update();

    public void Construct(CameraMovementPresenter presenter)
    {
        gameObject.SetActive(false);
        _presenter = presenter ?? 
                     throw new ArgumentNullException(nameof(presenter));
        gameObject.SetActive(true);
    }

    public void Move(Vector3 direction) => 
        transform.Translate(direction);

    public void Zoom(Vector3 direction) => 
        _camera.Translate(direction, Space.Self);

    public void Rotate(float angle) => 
        transform.rotation = Quaternion.Euler(0,angle,0);
}
