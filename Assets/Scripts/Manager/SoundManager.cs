using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource bgmSource;
	[SerializeField] AudioSource sfxSource;
	[SerializeField] AudioSource sfxMovdSource;
	[SerializeField] AudioSource sfxJumpSource;
	[SerializeField] AudioSource sfxAttackSource;

	public float BGMVolme { get { return bgmSource.volume; } set { bgmSource.volume = value; } }
    public float SFXVolme { get { return sfxSource.volume;} set { sfxSource.volume = value; sfxMovdSource.volume = value; sfxJumpSource.volume = value; sfxAttackSource.volume = value; } }

	[Header("Sound Clips")]
    [SerializeField] AudioClip titleSoundClip;
    public AudioClip TitleSoundClip { get { return titleSoundClip; } }
	[SerializeField] AudioClip kingsPassSoundClip;
	public AudioClip KingsPassSoundClip { get { return kingsPassSoundClip; } }
	[SerializeField] AudioClip dirtmouthSoundClip;
	public AudioClip DirtmouthSoundClip { get { return dirtmouthSoundClip; } }
	[SerializeField] AudioClip crossoradsSoundClip;
	public AudioClip CrossoradsSoundClip { get { return crossoradsSoundClip; } }
	[SerializeField] AudioClip bossSoundClip;
	public AudioClip BossSoundClip { get { return bossSoundClip; } }
	[SerializeField] AudioClip uiButton;
	public AudioClip UiButton { get { return uiButton; } }
	[SerializeField] AudioClip uiButtonChange;
	public AudioClip UiButtonChange { get { return uiButtonChange; } }
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
	[SerializeField] AudioClip playerDie;
	public AudioClip PlayerDie { get { return playerDie; } }
	[SerializeField] AudioClip door;
	public AudioClip Door { get { return door; } }
	[SerializeField] AudioClip gate;
	public AudioClip Gate { get { return gate; } }
	[SerializeField] AudioClip coinPop;
	public AudioClip CoinPop { get { return coinPop; } }
	[SerializeField] AudioClip coin;
	public AudioClip Coin { get { return coin; } }
	[SerializeField] AudioClip monsterTakeHit;
	public AudioClip MonsterTakeHit { get { return monsterTakeHit; } }
	[SerializeField] AudioClip elderbugFirst;
	public AudioClip ElderbugFirst { get { return elderbugFirst; } }
	[SerializeField] AudioClip[] elderbug;
	public AudioClip[] Elderbug { get { return elderbug; } }

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

    public void PlayMoveSFX(AudioClip clip)
    {
		sfxMovdSource.clip = clip;
		if(Manager.Scene.IsSceneChange)
		{
			StopMoveSFX(clip);
			return;
		}
		sfxMovdSource.Play();
    }

    public void StopMoveSFX(AudioClip clip)
    {
		sfxMovdSource.clip = clip;
		sfxMovdSource.Stop();
    }

	public void PlayJumpSFX(AudioClip clip)
	{
		if (Manager.Scene.IsSceneChange)
		{
			return;
		}
		sfxJumpSource.clip = clip;
		sfxJumpSource.PlayOneShot(clip);
	}

	public void PlayAttackSFX(AudioClip clip)
	{
		if (Manager.Scene.IsSceneChange)
		{
			return;
		}
		sfxAttackSource.clip = clip;
		sfxAttackSource.PlayOneShot(clip);
	}

	public void PlaySFX(AudioClip clip)
	{
		sfxSource.clip = clip;
		sfxSource.PlayOneShot(clip);
	}
}
