# Project Structure

The purpose of this document is to layout the project's structure.

## Table of Contents

- [Scenes](#scenes)
  - [Main Menu](#main-menu)
  - [Game](#game)
  - [Options](#options)
- [Managers](#managers)
- [Prefabs](#prefabs)

## Scenes

### Main Menu

- **Main Menu:** UI Canvas containing the App Title, Play Game, Options, and Exit Game button. Buttons use the [Level Manager](#level-manager) to transfer from scene to scene.
  
  - Play Game Button: Opens the [Loading Screen](#loading-screen) and loads the [Game Scene](#game)
  - Options Button: Loads the [Options Scene](#options)
  - Exit Game Button: Closes the app

The Main Menu script plays the BGM at the start via the [Sound Manager](#sound-manager):

```csharp
public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SoundManager _soundManager;
    [Header("Components")] 
    [SerializeField] AudioClip _bgm;

    private void Start()
    {
        _soundManager.PlayBGM(_bgm);
    }
}
```

- **Loading Screen:** The [Loading Screen](#loading-screen) is a UI canvas prefab used to communicate the loading progress of the next scene.

### Game

- **UI:** Simple canvas containing the Finish Game button.

  - Finish Game Button: Emulates finishing a game. Opens the Post Game Screen

- **Post Game Screen:** Displays the player's performance measurements at the end of a level. Allows the player to either restart the round of exit to the Main Menu.
  
  - Overlay: Adds a black transluscent background to highlight the post game screen
  - Score: (Placeholder) The score achieved by the player
  - Time: (Placeholder) The time spent in the level
  - Lives Used: (Placeholder) The amount of lives spent by the player in the level
  - Play Again Button: Reloads the [Game Scene](#game)
  - Main Menu Button: Loads the [Main Menu Scene](#main-menu)

### Options

## Managers

### Level manager

Scriptable Object that handles all loading from scene to scene. Also handles the closing of the application.

- **Functions**
  
  - Load Level: Loads a scene based on the scene index given.
  - Load Next Level: Loads the next scene in the build order by taking the current scene's index and adding 1
  - Restart Level: Reloads the current scene
  - Load Level, Next Level, and Restart Level Async: Similar to the previous functions but loaded asynchronously. This allows a loading screen to display the load progress to be displayed via the [Loading Screen](#loading-screen). A [Level Loader](#level-loader) needs to be instantiated in the scene as only Monobehaviours can use the StartCoroutine function.

Script:

```csharp
[CreateAssetMenu(fileName = "Level Manager", menuName = "Components/Level Manager")]
public class LevelManager : ScriptableObject
{
    [Header("Events")]
    [SerializeField] FloatEvent _levelLoading;
    [Header("Components")]
    [SerializeField] GameObject _levelLoader;

    public void LoadLevel(int p_sceneIndex)
    {
        SceneManager.LoadScene(p_sceneIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevelAsync(int p_sceneIndex)
    {
        LevelLoader levelLoader = Instantiate(_levelLoader).GetComponent<LevelLoader>();
        levelLoader.StartCoroutine(LoadLevelAsyncTask(p_sceneIndex, levelLoader));
    }

    public void LoadNextLevelAsync()
    {
        LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevelAsync(int p_sceneIndex)
    {
        LoadLevelAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    IEnumerator LoadLevelAsyncTask(int p_sceneIndex, MonoBehaviour p_levelLoader)
    {
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(p_sceneIndex);

        while (!loadSceneAsync.isDone)
        {
            float progressValue = Mathf.Clamp01(loadSceneAsync.progress / 0.9f);
            _levelLoading.Raise(progressValue);
            yield return null;
        }

        Destroy(p_levelLoader);
    }
}
```

### Sound manager

The Sound Manager handles the playing of all sounds for the project.

- **Functions**
  
  - Play SFX: Plays Sound Effects as one shots.
  - Play BGM: Plays background music. Can only have one track at a time. Loops.
  - Play VO: Plays a voiceover. Can only have one track at a time.
  - Play Button Click SFX: Plays a standardized button press sound effect.
  - Update BGM Source Volume: Allows BGM audio source volume to be adjusted while BGM is playing.
  - Update VO Source Volume: Allows VO audio source volume to be adjusted while VO is playing.
  - Stop BGM: Stops the currently playing BGM
  - Stop VO: Stops the currently playing VO

Script:

```csharp
[CreateAssetMenu(fileName = "Sound Manager", menuName = "Components/Sound Manager")]
public class SoundManager : ScriptableObject
{
    [Header("Audio Clips")]
    [SerializeField] AudioClip _buttonClickSFX;
    [Header("Variables")]
    [SerializeField] FloatVariable _masterVolume;
    [SerializeField] FloatVariable _sfxVolume;
    [SerializeField] FloatVariable _bgmVolume;
    [SerializeField] FloatVariable _voVolume;
    [SerializeField] BoolVariable _masterToggle;
    [SerializeField] BoolVariable _bgmToggle;
    [SerializeField] BoolVariable _sfxToggle;
    [SerializeField] BoolVariable _voToggle;
    [Header("Components")]
    [SerializeField] GameObject _audioSource;
    AudioSource _sfxSource, _bgmSource, _voSource;

    public void PlaySFX(AudioClip p_audioClip)
    {
        float volume;
        if (_sfxSource == null) _sfxSource = CreateAudioSource("SFX Source");

        if (!_masterToggle.value || !_sfxToggle.value)
        {
            volume = 0f;
        } else
        {
            volume = _masterVolume.value * _sfxVolume.value;
        }

        _sfxSource.PlayOneShot(p_audioClip, volume);
    }

    public void PlayBGM(AudioClip p_audioClip, bool p_fresh = false)
    {
        if (_bgmSource == null) _bgmSource = CreateAudioSource("BGM Source");

        if (!p_fresh)
        {
            if (_bgmSource.clip == p_audioClip) return;
        }

        float volume;
        if (!_masterToggle.value || !_bgmToggle.value)
        {
            volume = 0f;
        }
        else
        {
            volume = _masterVolume.value * _bgmVolume.value;
        }

        _bgmSource.clip = p_audioClip;
        _bgmSource.volume = volume;
        _bgmSource.loop = true;
        _bgmSource.Play();
    }

    public void PlayVO(AudioClip p_audioClip)
    {
        float volume;
        if (_voSource == null) _voSource = CreateAudioSource("VO Source");

        if (!_masterToggle.value || !_voToggle.value)
        {
            volume = 0f;
        }
        else
        {
            volume = _masterVolume.value * _voVolume.value;
        }

        _voSource.clip = p_audioClip;
        _voSource.volume = volume;
        _voSource.Play();
    }

    public void PlayButtonClickSFX()
    {
        PlaySFX(_buttonClickSFX);
    }

    public void UpdateBGMSourceVolume()
    {
        if (_bgmSource == null) return;

        float volume;
        if (_bgmSource == null) _bgmSource = CreateAudioSource("SFX Source");

        if (!_masterToggle.value || !_bgmToggle.value)
        {
            volume = 0f;
        }
        else
        {
            volume = _masterVolume.value * _bgmVolume.value;
        }

        _bgmSource.volume = volume;
    }

    public void UpdateVOSourceVolume()
    {
        if (_voSource == null) return;

        float volume;
        if (!_masterToggle.value || !_voToggle.value)
        {
            volume = 0f;
        }
        else
        {
            volume = _masterVolume.value * _voVolume.value;
        }

        _voSource.volume = volume;
    }

    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void StopVO()
    {
        _voSource.Stop();
    }

    private AudioSource CreateAudioSource(string p_name)
    {
        GameObject audioSource = Instantiate(_audioSource);
        DontDestroyOnLoad(audioSource);
        audioSource.name = p_name;
        return audioSource.GetComponent<AudioSource>();
    }
}
```

## Prefabs

### Audio Source

A game object with an audio source component instantiated by the Sound manager to play sounds in the scene.

### Level Loader

A monobehavior instantiated by the Level Manager to load levels asynchronously.

### Loading Screen

A screen used to display the loading progress of a scene in Unity.
