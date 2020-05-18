using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Добавляет эффект левитации объекту
/// </summary>
public class Levitator : MonoBehaviour {
    public float amplitude = 0.5f;
    public float frequency = 1f;
    
    private Vector3 _tempPos;
    
    void Start() {
        _tempPos = transform.position;
    }
    
    void Update() {
        _tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
        transform.position = _tempPos;
    }
}
