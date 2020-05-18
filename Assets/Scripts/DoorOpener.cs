using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpener : MonoBehaviour {
    public int itemsForOpen = 1;
    public Transform DoorLeft;
    public Transform DoorRight;
    public GameObject Lights;
    public Color openedColor;
    public Color closedColor;
    public float doorSpeed = 2;
    public GameObject doorTextContainer;
    public Text doorText;

    private bool _isOpened;
    private bool _isOpening;

    private Vector3 _leftDoorStartPoint;
    private Vector3 _leftDoorEndPoint;
    private Vector3 _rightDoorStartPoint;
    private Vector3 _rightDoorEndPoint;
    
    
    private void Start() {
        _leftDoorStartPoint = DoorLeft.position;
        _leftDoorEndPoint = _leftDoorStartPoint - DoorLeft.transform.right * 3;
        
        _rightDoorStartPoint = DoorRight.position;
        _rightDoorEndPoint = _rightDoorStartPoint + DoorRight.transform.right * 3;


    }
    
    private void Update() {
        if (GameManager.Instance.collectedItems >= itemsForOpen && !_isOpened) {
            _isOpening = true;
            OpenDoors();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (_isOpening) return;
        if (!other.gameObject.CompareTag("Player") || _isOpened) return;
        
        SetDoorAttentionText();
        doorTextContainer.SetActive(true);
        StartCoroutine(DisableText());
    }

    /// <summary>
    /// Показывает предупреждение сколько нужно собрать предметов до открытия
    /// </summary>
    private void SetDoorAttentionText() {
        doorText.text = $"Предметов не хватает, нужно ещё {itemsForOpen - GameManager.Instance.collectedItems}";
    }
    
    IEnumerator DisableText() {
        yield return new WaitForSeconds(3);
        
        doorTextContainer.SetActive(false);
    }

    private void SwitchLights(bool isOpen) {
        Color currentColor = isOpen ? openedColor : closedColor;
        
        foreach (Transform doorLight in Lights.transform) {
            doorLight.GetComponent<Light>().color = currentColor;
        }
    }
    private void OpenDoors() {
        DoorLeft.position = Vector3.MoveTowards(DoorLeft.position, _leftDoorEndPoint, doorSpeed * Time.deltaTime);
        DoorRight.position = Vector3.MoveTowards(DoorRight.position, _rightDoorEndPoint, doorSpeed * Time.deltaTime);

        if (Vector3.Distance(DoorLeft.position, _leftDoorEndPoint) < 0.2f) {
            _isOpened = true;
            SwitchLights(true);
        }
    }

    private void CloseDoors() {
        DoorLeft.position = _leftDoorStartPoint;
        DoorRight.position = _rightDoorStartPoint;

        _isOpened = false;
        SwitchLights(false);
    }
}
