using UnityEngine;

/// <summary>
/// Добавляет вращение объекту
/// </summary>
public class Rotator : MonoBehaviour {
    void Update() {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
