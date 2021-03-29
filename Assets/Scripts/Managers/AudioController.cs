using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController audioController;

#pragma warning disable 0649

    [Header("General References")]
    [SerializeField] private AudioSource generalAudioSource;

    [Header("SFXs")]
    [SerializeField] private AudioClip _cubeCollectSFX1;
    [SerializeField] private AudioClip _cubeCollectSFX2;
    [SerializeField] private AudioClip _cubeLoseSFX1;
    [SerializeField] private AudioClip _coinCollectSFX1;
    [SerializeField] private AudioClip _finalSFX;
    [SerializeField] private AudioClip _failSFX;

#pragma warning restore 0649

    private void Awake()
    {
        audioController = this;
    }

    public void PlayCubeCollectSFX()
    {
        int tmpRnd = Random.Range(0, 2);

        if (tmpRnd == 0)
        {
            generalAudioSource.PlayOneShot(_cubeCollectSFX1);
        }
        else
        {
            generalAudioSource.PlayOneShot(_cubeCollectSFX2);
        }
    }

    public void PlayFailSFX()
    {
        generalAudioSource.PlayOneShot(_failSFX);
    }

    public void PlayCubeLoseSFX()
    {
        generalAudioSource.PlayOneShot(_cubeLoseSFX1);
    }

    public void PlayCoinCollectSFX()
    {
        generalAudioSource.PlayOneShot(_coinCollectSFX1);
    }

    public void PlayFinishSFX()
    {
        generalAudioSource.PlayOneShot(_finalSFX);
    }
}