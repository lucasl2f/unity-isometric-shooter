using UnityEngine;

[CreateAssetMenu(fileName = "AudioBank", menuName = "Audio/AudioBank")]
public class AudioBank : ScriptableObject
{
    [Header("BGM")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;

    [Header("SFX")]
    public AudioClip buttonClick;
}