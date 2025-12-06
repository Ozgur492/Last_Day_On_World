using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageController : MonoBehaviour
{
    [Header("Panels")]
    //[SerializeField] private GameObject settingsPanel;       // <-- Ana settings panel
    [SerializeField] private GameObject languagePanel;// <-- Languages (Türkçe/English) panel
    public SettingsMenu settingsMenu; 

    public void SetEnglish()
    {
        LocalizationSettings.SelectedLocale = 
            LocalizationSettings.AvailableLocales.Locales[0];
    }

    public void SetTurkish()
    {
        LocalizationSettings.SelectedLocale = 
            LocalizationSettings.AvailableLocales.Locales[1];
    }

    public void BackFromLanguage()
    {
        languagePanel.SetActive(false);
        settingsMenu.OpenAllPanels();
    }
}