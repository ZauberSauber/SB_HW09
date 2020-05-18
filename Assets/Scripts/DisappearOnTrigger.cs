using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Класс для исчезновения платформ по входу или выходу игрока
/// </summary>
public class DisappearOnTrigger : MonoBehaviour {
    [Header("Секунд до исчезновения:")]
    public float waitSeconds = 1;
    [Header("Исчезновение после входа?")]
    public bool isTriggerOnEnter = true;
    [Header("Появиться после исчезновения?")]
    public bool isRelodable = true;
    [Header("Секунд до появления:")]
    public float reloadSeconds = 2;

    private bool _isDisappearStarted;

    private void OnEnable() {
        _isDisappearStarted = false;
    }

    private void StartDisappearing() {
        _isDisappearStarted = true;
        TriggerAction();
    }
    
    private void OnTriggerEnter(Collider other) {
        if (isTriggerOnEnter && !_isDisappearStarted) {
            StartDisappearing();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!isTriggerOnEnter && !_isDisappearStarted) {
            StartDisappearing();
        }
    }

    private void TriggerAction() {
        StartCoroutine(DisappearAfterTime());
    }
    
    IEnumerator DisappearAfterTime() {
        yield return new WaitForSeconds(waitSeconds);
        gameObject.SetActive(false);
        
        if (isRelodable) {
            GameManager.Instance.DelayAction(reloadSeconds, () => {
                if (gameObject != null) {
                    gameObject.SetActive(true);
                }
            });
        }
    }
}
