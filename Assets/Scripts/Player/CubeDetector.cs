using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDetector : MonoBehaviour
{
#pragma warning disable 0649

    [Header("General References")]
    [SerializeField] private Transform _playerModel;
    [SerializeField] private Transform _cubeStackParent;

    [Header("Variables")]
    [SerializeField] private float _playerModelUp;

#pragma warning restore 0649

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectedCubeBack"))
        {
            if (GameManager.gameManager.start)
            {
                if (other.transform.parent.GetComponent<CollectedCubeController>().cubeState == CollectedCubeController.CubeStates.OnHold)
                {
                    other.transform.parent.position = _playerModel.position;
                    other.transform.parent.parent = _cubeStackParent;
                    other.transform.parent.GetComponent<CollectedCubeController>().TriggerCollected();
                    _playerModel.position += Vector3.up * _playerModelUp;
                }
            }
        }
    }

    public void CheckCubeLeft()
    {
        if (_cubeStackParent.childCount <= 0)
        {
            MovementController.movementController.TriggerMovementStoppedWithDeath();
            GameManager.gameManager.LevelFail();
        }
    }

    public bool CheckSuccessFinish()
    {
        bool finish = false;

        if (_cubeStackParent.childCount == 1)
        {
            finish = true;
        }
        return finish;
    }
}