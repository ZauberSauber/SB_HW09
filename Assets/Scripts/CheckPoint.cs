using UnityEngine;

/// <summary>
/// Активация чекпоинтов
/// </summary>
public class CheckPoint : MonoBehaviour {
    public Material commonMaterial;
    public Material activeMaterial;
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (GameManager.Instance.lastCheckPoint) {
                GameManager.Instance.lastCheckPoint.GetComponent<Renderer>().material = commonMaterial;
            }
            GameManager.Instance.lastCheckPoint = gameObject;
            
            GetComponent<Renderer>().material = activeMaterial;
        }
    }
}
