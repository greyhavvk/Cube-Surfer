using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
#pragma warning disable 0649

    [Header("Variables")]
    [SerializeField] private int _coinAmount = 1;

#pragma warning restore 0649
    private Transform _uiCanvasObject;

    void Start()
    {
        _uiCanvasObject = UIController.uiController._uiCanvasObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerParent"))
        {
            //UIController.uiController.IncreaseCoinAmountText(_coinAmount);
            GameManager.gameManager.IncreaseCollectedCoinAmount();
            AudioController.audioController.PlayCoinCollectSFX();
            Destroy(gameObject);
        }
    }
}