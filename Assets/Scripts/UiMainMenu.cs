using System;
using UnityEngine;
using UnityEngine.UI;

public class UiMainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnExit;

    [SerializeField] private GameObject panelCredits;
    [SerializeField] private Button btnCreditsBack;

    [SerializeField] private GameObject panelOptions;
    [SerializeField] private Movement playerMovement;
    [SerializeField] private Slider sliderPlayer;
    [SerializeField] private Button btnOptionsBack;

    private bool isPause = true;

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlayClicked);
        btnOptions.onClick.AddListener(OnOptionsClicked);
        btnCredits.onClick.AddListener(OnCreditsClicked);
        btnExit.onClick.AddListener(OnExitClicked);
        btnCreditsBack.onClick.AddListener(OnCreditsBackClicked);

        sliderPlayer.onValueChanged.AddListener(Speed);
        btnOptionsBack.onClick.AddListener(OnOptionsBackClicked);
    }

    private void Speed(float newValue)
    {
        playerMovement.speed = newValue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnOptions.onClick.RemoveAllListeners();
        btnCredits.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
    }

    public void TogglePause()
    {
        isPause = !isPause;
        panelPause.SetActive(isPause);
        if (isPause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void OnPlayClicked()
    {
        TogglePause();
    }

    private void OnOptionsClicked()
    {
        panelOptions.SetActive(true);
    }

    private void OnCreditsClicked()
    {
        panelCredits.SetActive(true);
    }

    private void OnExitClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false; // Para el editor
    }

    private void OnOptionsBackClicked()
    {
        panelOptions.SetActive(false);
    }

    private void OnCreditsBackClicked()
    {
        panelCredits.SetActive(false);
    }
}