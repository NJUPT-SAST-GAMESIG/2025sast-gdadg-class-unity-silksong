using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{


    public Camera _camera;
    
    
    [Header("玩家Transform")]
    public Transform player; 
    
    private Vector2 _playerPosition;
    private Vector2 _cameraPositionXY;
    
    private Rigidbody2D _playerRigidbody2D;
    
    
    //相机缩放值
    private float _cameraZoom = 5f;
    
    [Header("相机移动速度")]
    public float CameraMoveSpeed;
    
    [Header("相机缩放最大值")]
    public float MaxCameraZoom = 15f;

    [Header("相机缩放最小值")]
    public float MinCameraZoom = 3f;

    [Header("相机缩放步长")] public float CameraZoomSize;

    [Header("相机缩放速度")]
    public float CameraZoomSpeed;


    [Header("初始缩放值")]
    public float CameraZoomInit = 5f;    
    
    
    private void Update()
    {
        // UpdateCameraZoom();
        UpdateZoomBySpeed();
    }

    private void Start()
    {
        _camera = Camera.main;
        _playerRigidbody2D = player.gameObject.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        UpdatePositionXY();
    }
    
    
    

    private void UpdatePositionXY()
    {
        _playerPosition = player.position;
        _cameraPositionXY = Vector2.Lerp(transform.position, _playerPosition, Time.fixedDeltaTime * CameraMoveSpeed);
        transform.position = new Vector3(_cameraPositionXY.x, _cameraPositionXY.y, -10);
    }

    // private void UpdateCameraZoom()
    // {
    //     _cameraZoom -= Input.GetAxis("Mouse ScrollWheel") * CameraZoomSize;
    //     _cameraZoom = Mathf.Clamp(_cameraZoom, MinCameraZoom, MaxCameraZoom);
    //     _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _cameraZoom, Time.deltaTime * CameraZoomSpeed);
    //     
    // }

    private void UpdateZoomBySpeed()
    {
        _cameraZoom = CameraZoomInit ;
        
        _cameraZoom += _playerRigidbody2D.velocity.magnitude * CameraZoomSize;

        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _cameraZoom, Time.deltaTime * CameraZoomSpeed);




    }
}
