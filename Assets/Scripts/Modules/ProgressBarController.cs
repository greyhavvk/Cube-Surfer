using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    private float firstDistance, currentDistance;

#pragma warning disable 0649

    [Header("Variables")]
    [SerializeField] private Slider _filledBar;
    [SerializeField] private Transform _finishPoint;
    [SerializeField] private Transform _playerPoint;

#pragma warning restore 0649

    private void Start()
    {
        firstDistance = Mathf.Abs(_finishPoint.position.z - _playerPoint.position.z);
    }

    void Update()
    {
        OpenProgressBar();
    }

    private void OpenProgressBar()
    {
        if (checkIsFinish())
            currentDistance = Mathf.Abs(_finishPoint.position.z - _playerPoint.position.z);
        _filledBar.value = (firstDistance - currentDistance) / firstDistance;
    }

    private bool checkIsFinish()
    {
        if (_finishPoint.position.z <= _playerPoint.position.z) { return false; }
        return true;
    }
}