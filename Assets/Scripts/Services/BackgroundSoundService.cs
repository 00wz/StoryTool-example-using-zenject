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
        if (audioSource.clip == clip)
        {
            return;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
