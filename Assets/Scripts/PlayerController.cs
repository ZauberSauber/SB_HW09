using UnityEngine;

/// <summary>
/// Класс, отвечающий за передвижение персонажа игроком
/// </summary>
public class PlayerController : MonoBehaviour {
    [Header("Настройки движения")]
    public ForceMode forceMode;
    public ForceMode jumpForceMode;
    public float movementSpeed = 10;
    public float speedLimit = 20;
    public float jumpForce = 100;
    public float jumpAbleDistance = 0.6f;
    public string groundLayerMask;
    

    private bool _isJumping;
    private int _groundLayerIndex;
    private Rigidbody _playerRb;
    private Transform _cameraContainer;
    
    private void Start() {
        _playerRb = GetComponent<Rigidbody>();
        _cameraContainer = GameManager.Instance.cameraContainer;
        _groundLayerIndex = LayerMask.GetMask(groundLayerMask);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _isJumping = true;
        }
    }
    private void FixedUpdate() {
        if (!GameManager.Instance.isGameWined) {
            #region управление персонажем

            if (_playerRb.velocity.magnitude < speedLimit) {
                if (Input.GetKey(KeyCode.W)) {
                    _playerRb.AddForce(Time.fixedDeltaTime * movementSpeed * _cameraContainer.forward, forceMode);
                }
        
                if (Input.GetKey(KeyCode.S)) {
                    _playerRb.AddForce(Time.fixedDeltaTime * movementSpeed * -_cameraContainer.forward, forceMode);
                }
                
                if (Input.GetKey(KeyCode.A)) {
                    _playerRb.AddForce(Time.fixedDeltaTime * movementSpeed * -_cameraContainer.right, forceMode);
                }
                
                if (Input.GetKey(KeyCode.D)) {
                    _playerRb.AddForce(Time.fixedDeltaTime * movementSpeed * _cameraContainer.right, forceMode);
                }
            }
            else {
                Debug.Log("high speed!");
            }
         
            #endregion
        
            #region прыжок
            Debug.DrawRay(_cameraContainer.position, _cameraContainer.TransformDirection(Vector3.down) * jumpAbleDistance, Color.blue);
        
            if (_isJumping && IsGrounded()) {
                _isJumping = false;
                _playerRb.AddForce(Vector3.up * jumpForce, jumpForceMode);
            }
            #endregion
        }
    }

    private bool IsGrounded() {
        if (Physics.Raycast(_cameraContainer.position, _cameraContainer.TransformDirection(Vector3.down), jumpAbleDistance, _groundLayerIndex)) {
            return true;
        }
        return false;
    }
}
