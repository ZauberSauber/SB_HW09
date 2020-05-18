using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
    public bool isPortalToOtherLevel;
    public String levelName;
    public Transform destinationPoint;

    private void OnTriggerEnter(Collider other) {
        if (isPortalToOtherLevel) {
            SceneManager.LoadScene(levelName);
        } else {
            other.transform.position = destinationPoint.position;
        }
    }
}
