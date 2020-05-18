using System;
using UnityEngine;

/// <summary>
/// Даёт объекту возможность пользоваться левитационным лифтом
/// </summary>
public class LevitateByElevator : MonoBehaviour {
    public float speed = 2.5f;
    public float maxFlyingSpeed = 4;
    public float normalizeRatio = 1;
    public String levitateTag = "LevitateElevator";
    
    private Rigidbody _rb;
    private bool _isLevitating;


    private void Start() {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update() {
        if (_isLevitating) {
            if (_rb.velocity.magnitude > maxFlyingSpeed) {
                _rb.velocity = _rb.velocity.normalized * normalizeRatio;
            }

            _rb.AddForce(Vector3.up * speed);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(levitateTag)) {
            _isLevitating = true;
            _rb.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag(levitateTag)) {
            _isLevitating = false;
            _rb.useGravity = true;
        }
    }
}
