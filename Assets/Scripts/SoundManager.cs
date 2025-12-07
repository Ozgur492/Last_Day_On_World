using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne değişince yok olmasın
        }
        else
        {
            Destroy(gameObject); // İkinci SoundManager varsa yok et
        }
    }

    public void SetVolume(float value)
    {
        musicSource.volume = value;
    }
}
