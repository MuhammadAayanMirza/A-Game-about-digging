using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
 [Header("Audio Source")]
 [SerializeField] AudioSource MusicSource;
 [SerializeField] AudioSource SFXSource;
 [SerializeField] AudioSource JetpackSource;

 [Header("Audio Clips")]
 public AudioClip Background;
 public AudioClip Digging;
 public AudioClip PopUp;
 public AudioClip Jetpack;
 public AudioClip SellButton;
 public AudioClip UpgradeButton;


    private void Start()
    {
        MusicSource.clip = Background;
        MusicSource.Play();

    }
    

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StartJetpackSFX()
    {
        if (JetpackSource != null && !JetpackSource.isPlaying)
        {
            JetpackSource.clip = Jetpack;
            JetpackSource.loop = true;
            JetpackSource.Play();
        }
    }

    public void StopJetpackSFX()
    {
        if (JetpackSource != null && JetpackSource.isPlaying)
        {
            JetpackSource.Stop();
        }
    }

    public void sellButton()
    {
        PlaySFX(SellButton);
    }

    public void upgradeButton()
    {
        PlaySFX(UpgradeButton);
    }

}

