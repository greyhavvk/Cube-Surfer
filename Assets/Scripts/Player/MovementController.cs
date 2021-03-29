using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
#pragma warning disable 0649

    [Header("Variables")]
    [SerializeField] private bool _start = false;
    [SerializeField] private float _speed;
    [SerializeField] private float _decreasedSpeed;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _deltaThreshold;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    [Header("References")]
    [SerializeField] CubeDetector _cubeDetector;

#pragma warning restore 0649

    public static MovementController movementController;
    private Rigidbody _rigidBodyPlayer;
    private Vector2 _currentTouchPosition;
    private float _finalX;
    private Vector2 _firstPosition;
    private float _currentSpeed;
    private int currentMultiplierPlatform = 1;


    private void Awake()
    {
        movementController = this;
    }

    void Start()
    {
        AttachReferences();
        ResetValues();
    }

    void Update()
    {
        StartGame();
    }

    private void FixedUpdate()
    {
        if (_start)
        {
            EndlessRun();
        }
    }

    private void StartGame()
    {
        if (_start)
        {
            MovementWithSlide();
        }
    }

    public void TriggerGameStarted()
    {
        _start = true;
    }

    void AttachReferences()
    {
        _rigidBodyPlayer = GetComponent<Rigidbody>();
        _currentSpeed = _speed;
    }

    void ResetValues()
    {
        _rigidBodyPlayer.velocity = new Vector3(0f, _rigidBodyPlayer.velocity.y, _rigidBodyPlayer.velocity.z);
        _firstPosition = Vector2.zero;
        _finalX = 0f;
        _currentTouchPosition = Vector2.zero;
    }

    void EndlessRun()
    {
        _rigidBodyPlayer.velocity = new Vector3(_rigidBodyPlayer.velocity.x, _rigidBodyPlayer.velocity.y, _currentSpeed * Time.fixedDeltaTime);
    }

    void MovementWithSlide()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            _currentTouchPosition = Input.mousePosition;
            Vector2 touchDelta = (_currentTouchPosition - _firstPosition);

            if (_firstPosition == _currentTouchPosition)
            {
                _rigidBodyPlayer.velocity = new Vector3(0f, _rigidBodyPlayer.velocity.y, _rigidBodyPlayer.velocity.z);
            }

            _finalX = transform.position.x;

            if (Mathf.Abs(touchDelta.x) >= _deltaThreshold)
            {
                _finalX = (transform.position.x + (touchDelta.x * _sensitivity));
            }

            _rigidBodyPlayer.position = new Vector3(_finalX, transform.position.y, transform.position.z);
            _rigidBodyPlayer.position = new Vector3(Mathf.Clamp(_rigidBodyPlayer.position.x, _minX, _maxX), _rigidBodyPlayer.position.y, _rigidBodyPlayer.position.z);
            _firstPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetValues();
        }
    }

    public void CubeLost()
    {
        CheckCubesForDeath();

        if (DOTween.IsTweening("PlayerSpeedDecreaseTween"))
        {
            DOTween.Kill("PlayerSpeedDecreaseTween", true);
        }

        if (DOTween.IsTweening("PlayerSpeedIncreaseTween"))
        {
            DOTween.Kill("PlayerSpeedIncreaseTween", true);
        }

        DOTween.To(() => _currentSpeed, x => _currentSpeed = x, _decreasedSpeed, 0.1f).SetId("PlayerSpeedDecreaseTween").OnComplete(() =>
        {

            DOTween.To(() => _currentSpeed, x => _currentSpeed = x, _speed, 0.1f).SetId("PlayerSpeedIncreaseTween");
        });
    }

    void CheckCubesForDeath()
    {
        _cubeDetector.CheckCubeLeft();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishFloor"))
        {
            currentMultiplierPlatform = other.gameObject.GetComponent<FinishController>().currentMultiplierZone;
        }

        if (other.CompareTag("Stop"))
        {
            currentMultiplierPlatform = 10;

            TriggerMovementStopped();
            GameManager.gameManager.LevelSuccess(currentMultiplierPlatform);
        }
    }

    public void TriggerFinishFloor()
    {
        if (_cubeDetector.CheckSuccessFinish())
        {
            TriggerMovementStopped();
            GameManager.gameManager.LevelSuccess(currentMultiplierPlatform);
        }
        else
        {
            if (DOTween.IsTweening("PlayerFinishFloorUpperTween"))
            {
                DOTween.Kill("PlayerFinishFloorUpperTween");
            }
        }
    }

    public void TriggerMovementStoppedWithDeath()
    {
        TriggerMovementStopped();
        AudioController.audioController.PlayFailSFX();
    }
    public void TriggerMovementStopped()
    {
        _start = false;
        _rigidBodyPlayer.velocity = Vector3.zero;
    }
    public void TriggerLevelFinished()
    {
        _start = false;
    }
}