using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour {
    [Header("UI текст кол-ва жизней")]
    public Text livesLeft;
    [Header("UI посмертный текст")]
    public GameObject deathTextContainer;
    [Header("UI game over текст")]
    public GameObject gameOverTextContainer;

    private Rigidbody _playerRb;
    private bool _isDead;
    private bool _isRespawning;
    
    
    private void Start() {
        _playerRb = GetComponent<Rigidbody>();
        SetLivesText();
    }

    
    private void FixedUpdate() {
        if (!_isDead) {
            OnFall();
        } else {
            if (!_isRespawning) {
                _isRespawning = true;
               RespawnAfterDeath(); 
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Death")) {
            _isDead = true;
        }
    }
    
    private void OnFall() {
        if (transform.position.y < -10) {
            _isDead = true;
        }
    }

    private void RespawnAfterDeath() {
        GameManager.Instance.currentLives--;
        SetLivesText();

        if (GameManager.Instance.currentLives > 0) {
            deathTextContainer.SetActive(true);
            
            Vector3 fixPosition = new Vector3(0, 1, 0);
            
            if (GameManager.Instance.lastCheckPoint) {
                transform.position = GameManager.Instance.lastCheckPoint.transform.position + fixPosition;
            } else {
                transform.position = GameManager.Instance.startPoint.transform.position + fixPosition; 
            }
        } else {
            deathTextContainer.SetActive(false);
            gameOverTextContainer.SetActive(true);
            GameManager.Instance.ResetLevelSettings();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        _isDead = false;
        _isRespawning = false;

        _playerRb.velocity = Vector3.zero;
        _playerRb.angularVelocity = Vector3.zero;

        StartCoroutine(DisableDeathText());
    }

    private void SetLivesText() {
        livesLeft.text = $"x {GameManager.Instance.currentLives}";
    }

    IEnumerator DisableDeathText() {
        yield return new WaitForSeconds(3);
        
        deathTextContainer.SetActive(false);
        gameOverTextContainer.SetActive(false);
    }
}
