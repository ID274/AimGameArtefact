using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private bool randomizePitch = false;
    [SerializeField] private float pitchRange = 0.1f;
    private AudioSource audioSource;

    [Header("Destroy after playing")]
    public float delayTime = 1.0f;
    public bool destroyAfterPlaying = false;

    private bool applicationQuitting = false;
    private bool startingDelay = false;
    private float timeElapsed = 0;

    private void OnApplicationQuit()
    {
        applicationQuitting = true;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (randomizePitch)
        {
            audioSource.pitch = Random.Range(1 - pitchRange, 1 + pitchRange);
        }

        audioSource.clip = AudioLibrary.Instance.GrabSound(soundName);
        audioSource.Play();
        
        if (destroyAfterPlaying)
        {
            startingDelay = true;
        }
    }

    private void Update()
    {
        if (startingDelay)
        {
            timeElapsed += Time.deltaTime;
        }

        if (timeElapsed >= delayTime && !applicationQuitting)
        {
            Destroy(gameObject);
        }
    }
}
