using UnityEngine;

public class SoundController
{
    private readonly AudioSource _bgmSource;
    private readonly AudioSource _sfxSource;
    private readonly AudioBank _audioBank;

    public SoundController(AudioSource bgmSource, AudioSource sfxSource, AudioBank audioBank)
    {
        _bgmSource = bgmSource;
        _sfxSource = sfxSource;
        _audioBank = audioBank;
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (_bgmSource.clip == clip) return;

        _bgmSource.clip = clip;
        _bgmSource.loop = loop;
        _bgmSource.Play();
    }

    public void StopBGM()
    {
        _bgmSource.clip = null;
        _bgmSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    public void PlayMainMenuMusic() => PlayBGM(_audioBank.mainMenuMusic);
    public void PlayGameplayMusic() => PlayBGM(_audioBank.gameplayMusic);
    public void PlayButtonClick() => PlaySFX(_audioBank.buttonClick);
}