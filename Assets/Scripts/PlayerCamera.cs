using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс отвечает за поворот камеры вокруг игрока и зум
/// </summary>
public class PlayerCamera : MonoBehaviour {
    [Header("Контейнер поворота камеры по оси Y")] 
    public Transform cameraRotator;
    
    [Header("Контейнер поворота камеры по оси Х")]
    public Transform cameraContainer;

    [Header("Настройки зума")]
    public float minDistance = -4;
    public float maxDistance = -12;
    public float startedDistance = -6;
    public float zoomSpeed = 2;
    
    [Header("Настройки поворота")]
    public float minYAngle = -15;
    public float maxYAngle = 45;
    public float rotationSpeed = 4;


    private Vector3 _playerPosition;
    private float _mouseX, _mouseY;
    private float _cameraHeight;
    private readonly Vector3 _fixCameraY = new Vector3(0,2,0);

    private void Start() {
        _playerPosition = GameManager.Instance.player.transform.position;
    }

    private void Update() {
        _playerPosition = GameManager.Instance.player.transform.position;
        
        #region поврот
        _mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        _mouseY -= Input.GetAxis("Mouse Y");
        #endregion

        #region зум
        startedDistance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (startedDistance < maxDistance) {
            startedDistance = maxDistance;
        }
        if (startedDistance > minDistance) {
            startedDistance = minDistance;
        }
        #endregion
        
        #region перемещение камеры за игроком
        cameraContainer.position = _playerPosition;
        #endregion
    }
    
    void FixedUpdate() {
        #region поворот камеры
        _mouseY = Mathf.Clamp(_mouseY, minYAngle, maxYAngle);
        cameraContainer.rotation = Quaternion.Euler(0,_mouseX,0);
        cameraRotator.rotation = Quaternion.Euler(_mouseY,_mouseX,0);

        transform.localPosition = new Vector3 (0,1,startedDistance);
        transform.LookAt(_playerPosition + _fixCameraY);
        #endregion
    }
}
