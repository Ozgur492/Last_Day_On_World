using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Previous Scene")]
    [SerializeField] private string MainMenu = "MainMenu";

    [Header("Sub Panels")]
    [SerializeField] private GameObject graphicsPanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject languagePanel;
    
    [Header("Language Panel")]
    [SerializeField] private GameObject languageOptionsPanel;
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void OpenGraphics()
    {
        CloseAllPanels();
        graphicsPanel?.SetActive(true);
    }

    public void OpenAudio()
    {
        CloseAllPanels();
        audioPanel.SetActive(true);
    }


    public void OpenControls()
    {
        CloseAllPanels();
        controlsPanel?.SetActive(true);
    }

    public void OpenGameplay()
    {
        CloseAllPanels();
        gameplayPanel?.SetActive(true);
    }

    public void OpenLanguage()
    {
        CloseAllPanels();
        languageOptionsPanel?.SetActive(true);
    }

    private void CloseAllPanels()
    {
        graphicsPanel?.SetActive(false);
        audioPanel?.SetActive(false);
        controlsPanel?.SetActive(false);
        gameplayPanel?.SetActive(false);
        languagePanel?.SetActive(false);
    }
    
    public void OpenAllPanels()
    {
        graphicsPanel?.SetActive(true);
        audioPanel?.SetActive(true);
        controlsPanel?.SetActive(true);
        gameplayPanel?.SetActive(true);
        languagePanel?.SetActive(true);
    }
}