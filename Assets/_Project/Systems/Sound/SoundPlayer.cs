using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private bool randomizePitch = false;
    [SerializeField] private float pitchRange = 0.1f;
    private AudioSource audioSource;

    public bool destroyAfterPlaying = false;

    private bool applicationQuitting = false;

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
            StartCoroutine(DestroyAfterDelay(gameObject, audioSource.clip.length));
        }
    }

    public IEnumerator DestroyAfterDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (applicationQuitting) yield break;

        Destroy(gameObject);
    }
}
