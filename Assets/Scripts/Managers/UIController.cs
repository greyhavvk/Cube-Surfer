using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController uiController;

#pragma warning disable 0649

    [Header("References")]
    public Transform _uiCanvasObject;

    [Header("UI Elements")]
    [SerializeField] private GameObject _levelEndCanvas;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _tryAgainButton;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    [SerializeField] private TextMeshProUGUI _coinMultiplierText;
    [SerializeField] private TextMeshProUGUI _collectedCoinAmountText;
    [SerializeField] private TextMeshProUGUI _collectedCoinSumAmountText;

#pragma warning restore 0649

    private void Awake()
    {
        uiController = this;
    }

    private void Start()
    {
        InitValues();
    }

    void InitValues()
    {
        if (PlayerPrefs.HasKey("CoinAmount"))
        {
            _coinAmountText.text = PlayerPrefs.GetInt("CoinAmount").ToString();
        }

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevelText.text = (PlayerPrefs.GetInt("CurrentLevel") + 1).ToString();
        }
        else
        {
            currentLevelText.text = "1";
        }
    }

    public void IncreaseCoinAmountText(int _amount)
    {
        int tmp = int.Parse(_coinAmountText.text);
        tmp = _amount;
        _coinAmountText.text = tmp.ToString();
        PlayerPrefs.SetInt("CoinAmount", tmp);
    }


    public void TriggerLevelEndCanvas(int _collectedGemAmount, int _multiplier, int _collectedSumGemAmount)
    {
        AudioController.audioController.PlayFinishSFX();
        _levelEndCanvas.SetActive(true);
        _collectedCoinAmountText.text = _collectedGemAmount.ToString();
        _collectedCoinSumAmountText.text = _collectedSumGemAmount.ToString();
        _coinMultiplierText.text = "x" + _multiplier.ToString();
    }

    public void TryAgainButtonPressed()
    {
        GameManager.gameManager.TryAgainButtonPressed();
    }

    public void NextLevelButtonPressed()
    {
        GameManager.gameManager.NextLevelButtonPressed();
    }

    public void TriggerLevelFailed()
    {
        _tryAgainButton.SetActive(true);
    }

    public void StartButtonPressed()
    {
        GameManager.gameManager.StartButtonPressed();
        _startButton.SetActive(false);
    }
}
