using UnityEngine;
using TMPro;

public class MusicScene : MonoBehaviour
{
    public TextMeshProUGUI songName;
    public AudioSource LobbyMusicAudioSource;
    public AudioClip[] LobbySongsList;
    private Animator textAnim;

    public bool isPaused = false;

    void Start()
    {
        if (songName == null) songName = GetComponent<TextMeshProUGUI>();
        if (textAnim == null) textAnim = GetComponent<Animator>();
        if (LobbyMusicAudioSource == null) LobbyMusicAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (LobbyMusicAudioSource != null && !LobbyMusicAudioSource.isPlaying && !isPaused)
        {
            PlayRandomMusic();
        }
    }

    void PlayRandomMusic()
    {
        if (LobbySongsList == null || LobbySongsList.Length == 0) return;

        LobbyMusicAudioSource.clip = LobbySongsList[Random.Range(0, LobbySongsList.Length)];
        LobbyMusicAudioSource.Play();
        
        if (songName != null && LobbyMusicAudioSource.clip != null)
        {
            songName.text = LobbyMusicAudioSource.clip.name;
        }
        if (textAnim != null)
        {
            textAnim.Play("ShowText");
        }
    }

    public void ToggleMusic(bool pause)
    {
        isPaused = pause;

        if (pause)
            LobbyMusicAudioSource.Pause();
        else
            LobbyMusicAudioSource.Play();
    }

    public void StopMusic()
    {
        LobbyMusicAudioSource.Stop();
        isPaused = true;   
    }
        private void PlayTrack(int index)
    {
        if (LobbySongsList == null || LobbySongsList.Length == 0) return;
        if (index < 0 || index >= LobbySongsList.Length) return;

        if (LobbyMusicAudioSource != null)
        {
            LobbyMusicAudioSource.clip = LobbySongsList[index];
            LobbyMusicAudioSource.Play();
        }

        if (songName != null && LobbyMusicAudioSource != null && LobbyMusicAudioSource.clip != null)
        {
            songName.text = LobbyMusicAudioSource.clip.name;
        }

        if (textAnim != null)
        {
            textAnim.Play("ShowText");
        }
    }

    public void PlayNextTrack()
    {
        if (LobbySongsList == null || LobbySongsList.Length == 0) return;

        int currentIndex = -1;
        if (LobbyMusicAudioSource != null && LobbyMusicAudioSource.clip != null)
        {
            for (int i = 0; i < LobbySongsList.Length; i++)
            {
                if (LobbyMusicAudioSource.clip == LobbySongsList[i])
                {
                    currentIndex = i;
                    break;
                }
            }
        }

        int nextIndex = (currentIndex + 1) % LobbySongsList.Length;
        PlayTrack(nextIndex);
    }

    public void PlayPreviousTrack()
    {
        if (LobbySongsList == null || LobbySongsList.Length == 0) return;

        int currentIndex = -1;
        if (LobbyMusicAudioSource != null && LobbyMusicAudioSource.clip != null)
        {
            for (int i = 0; i < LobbySongsList.Length; i++)
            {
                if (LobbyMusicAudioSource.clip == LobbySongsList[i])
                {
                    currentIndex = i;
                    break;
                }
            }
        }

        int previousIndex = currentIndex <= 0
            ? LobbySongsList.Length - 1
            : currentIndex - 1;
        PlayTrack(previousIndex);
    }
}
