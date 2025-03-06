using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaySoundOnDeath : MonoBehaviour
{
    private bool applicationQuitting = false;
    [SerializeField] private string soundName = "TEMP";
    private void OnApplicationQuit()
    {
        applicationQuitting = true;
    }

    private void OnDestroy()
    {
        if (applicationQuitting) return;


        AudioClip soundClip = AudioLibrary.Instance.GrabSound(soundName);
        float soundLength = soundClip != null ? soundClip.length : 0;

        GameObject soundObject = new GameObject("SoundObject");
        SoundPlayer soundPlayer = soundObject.AddComponent<SoundPlayer>();

        if (soundLength > 0)
        {
            soundPlayer.StartCoroutine(soundPlayer.DestroyAfterDelay(soundObject, soundLength));
        }
        else
        {
            Debug.LogWarning("Sound length is 0, sound will not be destroyed");
        }
        soundPlayer.PlaySound(soundName);
    }
}
