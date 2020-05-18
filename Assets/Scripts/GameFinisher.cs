using UnityEngine;

/// <summary>
/// Завершает игру
/// </summary>
public class GameFinisher : MonoBehaviour {
    public GameObject winTextContainer;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            GameManager.Instance.OnGameFinish();
            winTextContainer.SetActive(true);
        }
    }
}
