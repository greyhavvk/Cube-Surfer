using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

#pragma warning disable 0649

    [Header("General Variables")]
    public bool start = false;
    [SerializeField] private bool _finish = false;
    [SerializeField] private int _coinAmount = 0;
    [SerializeField] private int _collectedCoinAmount = 0;

#pragma warning restore 0649

    private void Awake()
    {
        gameManager = this;
    }

    private void Start()
    {
        InitValues();

    }

    void InitValues()
    {
        DOTween.Clear(true);

        if (PlayerPrefs.HasKey("CoinAmount"))
        {
            _coinAmount = PlayerPrefs.GetInt("CoinAmount");
        }
    }

    void GameStart()
    {
        MovementController.movementController.TriggerGameStarted();
    }

    void SetPlayerPrefSettings()
    {
        if (_finish)
        {
            int tmpCurr = PlayerPrefs.GetInt("CurrentLevel");
            tmpCurr++;
            PlayerPrefs.SetInt("CurrentLevel", tmpCurr);
            PlayerPrefs.SetInt("CoinAmount", _coinAmount);
        }
    }

    public void LevelFail()
    {
        //_finish = true;
        UIController.uiController.TriggerLevelFailed();
        SetPlayerPrefSettings();
    }

    public void LevelSuccess(int _multiplier)
    {
        _finish = true;
        TriggerLevelEndCoinCalculation(_multiplier);
        SetPlayerPrefSettings();
    }

    public void IncreaseCollectedCoinAmount()
    {
        _collectedCoinAmount++;
        UIController.uiController.IncreaseCoinAmountText(_collectedCoinAmount);
    }

    public void TriggerLevelEndCoinCalculation(int _multiplier)
    {
        int gemSum = _collectedCoinAmount * _multiplier;
        _coinAmount += gemSum;
        UIController.uiController.IncreaseCoinAmountText(gemSum);
        UIController.uiController.TriggerLevelEndCanvas(_collectedCoinAmount, _multiplier, gemSum);
    }

    public void TryAgainButtonPressed()
    {
        SceneManager.LoadScene("Surfer");
    }

    public void NextLevelButtonPressed()
    {
        SceneManager.LoadScene("Surfer");
    }

    public void StartButtonPressed()
    {
        start = true;
        GameStart();
    }
}
