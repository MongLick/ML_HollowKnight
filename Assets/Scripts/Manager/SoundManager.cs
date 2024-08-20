using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    public float BGMVolme { get { return bgmSource.volume; } set { bgmSource.volume = value; } }
    public float SFXVolme { get { return sfxSource.volume; } set { sfxSource.volume = value; } }

	[Header("Sound Clips")]
    [SerializeField] AudioClip titleSoundClip;
    public AudioClip TitleSoundClip { get { return titleSoundClip; } }
	[SerializeField] AudioClip uiButton;
	public AudioClip UiButton { get { return uiButton; } }
    [SerializeField] AudioClip playerMove;
	public AudioClip PlayerMove { get { return playerMove; } }
	[SerializeField] AudioClip playerJump;
	public AudioClip PlayerJump { get { return playerJump; } }
	[SerializeField] AudioClip playerLand;
	public AudioClip PlayerLand { get { return playerLand; } }
	[SerializeField] AudioClip playerAttack;
	public AudioClip PlayerAttack { get { return playerAttack; } }
	[SerializeField] AudioClip playerTakeHit;
	public AudioClip PlayerTakeHit { get { return playerTakeHit; } }
	[SerializeField] AudioClip playerDash;
	public AudioClip PlayerDash { get { return playerDash; } }
	[SerializeField] AudioClip playerHeal;
	public AudioClip PlayerHeal { get { return playerHeal; } }
	[SerializeField] AudioClip coin;
	public AudioClip Coin { get { return coin; } }
	[SerializeField] AudioClip monsterTakeHit;
	public AudioClip MonsterTakeHit { get { return monsterTakeHit; } }



	public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM(AudioClip clip)
    {
        if (bgmSource.isPlaying == false)
            return;

		bgmSource.clip = clip;
        bgmSource.Stop();
	}

    public void PlaySFX(AudioClip clip)
    {
		sfxSource.clip = clip;
		sfxSource.Play();
    }

    public void StopSFX(AudioClip clip)
    {
        if (sfxSource.isPlaying == false)
            return;

		sfxSource.clip = clip;
		sfxSource.Stop();
    }
}
