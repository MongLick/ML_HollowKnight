using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource bgmSource;
	[SerializeField] AudioSource sfxSource;
	[SerializeField] AudioSource sfxLoopSource;

    public float BGMVolme { get { return bgmSource.volume; } set { bgmSource.volume = value; } }
    public float SFXVolme { get { return sfxSource.volume;} set { sfxSource.volume = value; sfxLoopSource.volume = value; } }

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
	[SerializeField] AudioClip playerAttack1;
	public AudioClip PlayerAttack1 { get { return playerAttack1; } }
	[SerializeField] AudioClip playerAttack2;
	public AudioClip PlayerAttack2 { get { return playerAttack2; } }
	[SerializeField] AudioClip playerAttack3;
	public AudioClip PlayerAttack3 { get { return playerAttack3; } }
	[SerializeField] AudioClip playerAttack4;
	public AudioClip PlayerAttack4 { get { return playerAttack4; } }
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
		bgmSource.clip = clip;
        bgmSource.Stop();
	}

    public void PlayLoopSFX(AudioClip clip)
    {
		sfxLoopSource.clip = clip;
		sfxLoopSource.Play();
    }

    public void StopLoopSFX(AudioClip clip)
    {
		sfxLoopSource.clip = clip;
		sfxLoopSource.Stop();
    }

	public void PlaySFX(AudioClip clip)
	{
		sfxSource.clip = clip;
		sfxSource.PlayOneShot(clip);
	}

	public void StopSFX(AudioClip clip)
	{
		sfxSource.clip = clip;
		sfxSource.Stop();
	}
}
