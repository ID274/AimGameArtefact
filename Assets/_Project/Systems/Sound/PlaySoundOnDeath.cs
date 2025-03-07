using UnityEngine;

[RequireComponent(typeof(SoundPlayer))]
public class PlaySoundOnDeath : MonoBehaviour
{
    [SerializeField] private string soundName = "TEMP";

    public void PlayDestroySound()
    {
        GameObject soundObject = new GameObject(soundName);
        SoundPlayer soundPlayer = soundObject.AddComponent<SoundPlayer>();
        AudioClip soundClip = AudioLibrary.Instance.GrabSound(soundName);
        float soundLength = soundClip != null ? soundClip.length : 0;
        soundPlayer.delayTime = soundLength;
        soundPlayer.destroyAfterPlaying = true;
        soundPlayer.PlaySound(soundName);
    }
}