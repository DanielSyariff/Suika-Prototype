using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource SFX;

    public GameObject xBGM;
    public GameObject xSFX;

    public AudioClip buttonClickedPositif;
    public AudioClip buttonClickedNegatif;

    private void Start()
    {
        //SetSfxVolume();
        //SetBgmVolume();
    }

    // Metode untuk mengatur volume SFX
    public void SetSfxVolume()
    {
        if (SFX.mute)
        {
            SFX.mute = false;
            xSFX.SetActive(false);

            PlaySFXOneShot(buttonClickedPositif);
        }
        else
        {
            SFX.mute = true;
            xSFX.SetActive(true);

            PlaySFXOneShot(buttonClickedNegatif);
        }
    }

    // Metode untuk mengatur volume BGM
    public void SetBgmVolume()
    {
        if (BGM.mute)
        {
            BGM.mute = false;
            xBGM.SetActive(false);

            PlaySFXOneShot(buttonClickedPositif);
        }
        else
        {
            BGM.mute = true;
            xBGM.SetActive(true);

            PlaySFXOneShot(buttonClickedNegatif);
        }
    }

    public void ChangeBGM()
    {

    }

    public void PlaySFXOneShot(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
}
