using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CameraControl : MonoBehaviour
{
    private Camera _mainCamera;
    

    private Vector2 _playerPosition;
    private Rigidbody2D _playerRigidbody2D;
    private Vector2 _cameraPositionXY;
    private float _cameraZoom;
    
    
    [SerializeField]
    private float _playerVelocity;
    
    
    [Header("玩家Transform")]
    public Transform player;
    
    [Header("相机移动速度")]
    public float CameraMoveSpeed;
    
    [Header("相机缩放速度")]
    public float CameraZoomSpeed;
    
    [Header("相机缩放步长")] 
    public float CameraZoomSize;
    
    [Header("相机缩放最小值")] 
    public float CameraZoomMin;
    
    [Header("相机缩放最大值")] 
    public float CameraZoomMax;

    [Header("初始缩放值")]
    public float InitZoom = 5.0f;
    



    
    private void Start()
    {
        _mainCamera = GetComponent<Camera>();
        _playerRigidbody2D = player.gameObject.GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        UpdatePositionXY();
    }
    
    private void Update()
    {
        // UpdateZoom();
        UpdateZoomBySpeed();
    
    }
    
    
    private void UpdatePositionXY()
    {
        _playerPosition = player.position;
        _cameraPositionXY = Vector2.Lerp(transform.position, _playerPosition, Time.fixedDeltaTime * CameraMoveSpeed);
        transform.position = new Vector3(_cameraPositionXY.x, _cameraPositionXY.y, -10);
    }
    
    private void UpdateZoom()
    {
        _cameraZoom -= Input.GetAxis("Mouse ScrollWheel") * CameraZoomSize;
        _cameraZoom = Mathf.Clamp(_cameraZoom, CameraZoomMin, CameraZoomMax);
        _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, _cameraZoom, Time.fixedDeltaTime * CameraZoomSpeed);
    }
    
    //根据速度控制缩放
    private void UpdateZoomBySpeed()
    {
        //速度模长
        _playerVelocity = _playerRigidbody2D.velocity.magnitude;
        _cameraZoom = InitZoom + _playerVelocity * CameraZoomSize;
        _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, _cameraZoom, Time.fixedDeltaTime * CameraZoomSpeed);
    }
    
}
