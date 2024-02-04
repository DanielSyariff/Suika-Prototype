using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class AudioBank
{
    public string audioName;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource SFX;

    public List<AudioBank> sfxBank;

    // Variable untuk menyimpan instance AudioManager
    private static AudioManager instance;
    private void Start()
    {
        // Cek apakah instance sudah ada
        if (instance == null)
        {
            // Jika tidak, inisialisasi instance dengan objek saat ini
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // Jika sudah ada, hancurkan objek saat ini
            Destroy(this.gameObject);
        }
    }

    // Metode untuk mengatur volume SFX
    public void SetSfxVolume(GameObject indicator)
    {
        if (SFX.mute)
        {
            SFX.mute = false;
            indicator.SetActive(false);

            PlaySFXOneShot("BtnClickPositif");
        }
        else
        {
            SFX.mute = true;
            indicator.SetActive(true);

            PlaySFXOneShot("BtnClickNegatif");
        }
    }

    // Metode untuk mengatur volume BGM
    public void SetBgmVolume(GameObject indicator)
    {
        if (BGM.mute)
        {
            BGM.mute = false;
            indicator.SetActive(false);

            PlaySFXOneShot("BtnClickPositif");
        }
        else
        {
            BGM.mute = true;
            indicator.SetActive(true);

            PlaySFXOneShot("BtnClickNegatif");
        }
    }

    public void ChangeBGM()
    {

    }

    public void PlaySFXOneShot(string clipname)
    {
        AudioBank foundAudio = sfxBank.Find(audio => audio.audioName == clipname);
        SFX.PlayOneShot(foundAudio.clip);
    }
}
