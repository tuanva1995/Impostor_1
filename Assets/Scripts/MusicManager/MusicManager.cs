using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using UnityEngine.Events;
public class MusicManager : MonoBehaviour
{
    public enum SourceAudio { Music, Effect, Coin };
    [SerializeField]
    private AudioMixer mixer;
    public AudioSource effectSource;
    public AudioSource musicSource;
    public AudioSource coinSource;
    public float lowPitchRange = 0.95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    public float delayTime = 0.5f;
    public const string MASTER_KEY = "MASTER_KEY";
    public const string MUSIC_KEY = "MUSIC_KEY";
    public const string SOUND_KEY = "SOUND_KEY";
    public const float MIN_VALUE = -80f;
    public const float MAX_VALUE = 0f;
    
    //AUDIO CLIP SFX
    public AudioClip sfxShot;
    public AudioClip sfxAtk1;
    public AudioClip sfxAtk2;
    public AudioClip sfxAtk3;
    public AudioClip sfxPlayerDie;
    public AudioClip sfxEnemyDie;
    public AudioClip sfxClickButton;
    public AudioClip sfxGetCoin;
    public AudioClip sfxGetItem;
    public AudioClip sfxWin;
    public AudioClip sfxEatItem;
    public AudioClip sfxReload1;
    public AudioClip sfxReload2;
    public AudioClip sfxLose;
    public AudioClip sfxGo;
    public AudioClip sfxPopup;

    public AudioClip sfxStun;
    public AudioClip sfxElectric;
    public AudioClip sfxBoom;
    public AudioClip winMusic;
     public AudioClip BGMusic;
     public AudioClip GamePlayMusic;

    private AudioClip _currentMusic;
    private static MusicManager _instance;
    
    public static MusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MusicManager>();
                if (_instance != null)
                    DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public float MasterVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(MASTER_KEY, 0f);
        }
        set
        {
            SetMasterVolume(value);
        }
    }
    public float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        }
        set
        {
            SetMusicVolume(value);
        }
    }
    public float SoundVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(SOUND_KEY, 1f);
        }
        set
        {
            SetSoundVolume(value);
        }
    }

    private void Awake()
    {


        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
            {
                Destroy(gameObject);
            }
        }
    }
    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip, SourceAudio source = SourceAudio.Effect)
    {
        //if (clip == null)
        //    return;
        //switch (source)
        //{
        //    case SourceAudio.Music:
        //        if (MusicVolume == 0) return;
        //        musicSource.clip = clip;
        //        musicSource.Play();
        //        break;
        //    case SourceAudio.Effect:
        //        if (SoundVolume == 0) return;
        //        effectSource.clip = clip;
        //        effectSource.Play();
        //        break;
        //    case SourceAudio.Coin:
        //        coinSource.clip = clip;
        //        coinSource.Play();
        //        break;
        //}

    }

    public void PauseBGMusic()
    {
        musicSource.Pause();
    }

    public void ResumeBGMusic()
    {
        musicSource.UnPause();
    }

    //Used to play single sound clips.
    public void PlayOneShot(AudioClip clip, SourceAudio source = SourceAudio.Effect)
    {
        //if (clip == null)
        //    return;
        //switch (source)
        //{
        //    case SourceAudio.Music:
        //        if (MusicVolume == 0) return;
        //        musicSource.PlayOneShot(clip);
        //        break;
        //    case SourceAudio.Effect:
        //        if (SoundVolume == 0) return;
        //        effectSource.PlayOneShot(clip);
        //        break;
        //    case SourceAudio.Coin:
        //        coinSource.clip = clip;
        //        coinSource.Play();
        //        break;
        //}
    }

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        effectSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        effectSource.clip = clips[randomIndex];

        //Play the clip.
        effectSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        //if (clip == null || _currentMusic == clip)
        //    return;
        //SetMasterVolume(MasterVolume);
        //SetMusicVolume(MusicVolume);
        //SetSoundVolume(SoundVolume);
        //_currentMusic = clip;
        //StopMusic();
        //musicSource.clip = clip;
        //musicSource.PlayDelayed(delayTime);
    }
    public void RandomizeMusic(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);
        var clip = clips[randomIndex];
        if (clip == null || _currentMusic == clip)
            return;
        _currentMusic = clip;
        StopMusic();
        //Set the clip to the clip at our randomly chosen index.
        musicSource.clip = clips[randomIndex];

        //Play the clip.
        musicSource.PlayDelayed(delayTime);
    }

    public void PauseMusic()
    {
        //Play the clip.
        musicSource.Pause();
    }
    
    public void UnPauseMusic()
    {
        //Play the clip.
        musicSource.UnPause();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public bool IsMusicPlaying()
    {
        return musicSource.isPlaying;
    }


    private void SetMasterVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat(MASTER_KEY, volume);
    }
    private void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }
    private void SetSoundVolume(float volume)
    {
        mixer.SetFloat("SoundVolume", volume);
        PlayerPrefs.SetFloat(SOUND_KEY, volume);
    }

    #region === Play Sound ===
    public void PlayWinSound()
    {
        PlaySingle(winMusic, SourceAudio.Effect);
    }

    public void PlayBGMusic()
    {
        return;
        PlaySingle(BGMusic, SourceAudio.Music);
    }
    public void PlayGPMusic()
    {
        return;
        PlaySingle(GamePlayMusic, SourceAudio.Music);
    }
    public void PlaySfx(AudioClip clip)
    {
        return;
        if (SoundVolume == 0)
        {
            return;
        }
        //Debug.Log(clip.name);
        //GetComponent<SoundPool>().GetSfx(clip);
    }
    public void ToggleSound(bool isOn)
    {

        if (!isOn)
        {
            SoundVolume = 0;
            
        }
        else
        {
            SoundVolume =.5f;
        }
      
    }
    #endregion
}
