using UnityEngine;

public class BackgroundSoundService : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    void Awake()
    {
        audioSource.loop = true;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
