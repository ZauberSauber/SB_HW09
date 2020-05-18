using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Сбор предметов и отображение очков за сбор
/// </summary>
public class CollectItems : MonoBehaviour {
    public Text countedItemsText;

    private void Start() {
        SetCountText();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            GameManager.Instance.collectedItems++;
            SetCountText();
        }
    }

    private void SetCountText() {
        countedItemsText.text = $"Собрано предметов: {GameManager.Instance.collectedItems.ToString()}";
    }
}
