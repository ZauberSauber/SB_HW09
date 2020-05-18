using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Двигает платформу между 2 заданных точек
/// </summary>
public class Elevator : MonoBehaviour {
    public Transform pointA;
    public Transform pointB;
    public float speed;
    public float waitSeconds = 2;
    public Transform objectsCollector;


    private Transform _target;
    private bool _isMovingToA = true;
    private bool _canMove = true;
    private bool _isAppClosing;
    
    private void Start() {
        _target = pointA;
    }

    private void OnEnable() {
        _canMove = true;
    }
    
    private void OnDisable() {
        if (!_isAppClosing) {
            // Убирает объекты с платформы, если её отключают
            objectsCollector.DetachChildren();
        }
    }

    private void OnApplicationQuit() {
        _isAppClosing = true;
    }

    private void Update() {
        MoveTotarget();
    }

    private void OnTriggerEnter(Collider other) {
        other.gameObject.transform.SetParent(objectsCollector);
    }
    
    private void OnTriggerExit(Collider other) {
        other.gameObject.transform.SetParent(null);
    }

    /// <summary>
    /// Двигает платформу к одной из заданных точек
    /// </summary>
    private void MoveTotarget() {
        if (_canMove) {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _target.position) < 0.001f) {
                _canMove = false;
                SwapDirection();
                StartCoroutine(StopOnPoint());
            } 
        }
    }

    private void SwapDirection() {
        _isMovingToA = !_isMovingToA;

        _target = _isMovingToA ? pointA : pointB;
    }

    IEnumerator StopOnPoint() {
        yield return new WaitForSeconds(waitSeconds);

        _canMove = true;
    }
}
