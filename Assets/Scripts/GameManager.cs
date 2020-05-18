using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    
    public GameObject player;

    public bool isGameWined;
    
    public int startLives = 3;
    public int currentLives;
    
    public GameObject startPoint;
    public GameObject lastCheckPoint;
    
    public Transform cameraContainer;
    
    public int collectedItems;
    

    IEnumerator DelayRoutine(float seconds, Action callback) {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }
    
    public void DelayAction(float seconds, Action callback) {
        Instance.StartCoroutine(Instance.DelayRoutine(seconds, callback));
    }

    public void ResetLevelSettings() {
        currentLives = startLives;
        collectedItems = 0;
    }

    public void OnGameFinish() {
        isGameWined = true;
        Rigidbody prb = player.GetComponent<Rigidbody>();
        prb.useGravity = false;
        prb.velocity = Vector3.zero;
        prb.angularVelocity = Vector3.zero;
    }
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;

        currentLives = startLives;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Instance.StopAllCoroutines();
        startPoint = GameObject.Find("StartPoint");
        player = GameObject.Find("Player");
        cameraContainer = GameObject.Find("CameraContainer").transform;
        collectedItems = 0;
        player.transform.position = startPoint.transform.position + new Vector3(0, 1, 0);
    }
}
