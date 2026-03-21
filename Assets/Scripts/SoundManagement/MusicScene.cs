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
        songName = GetComponent<TextMeshProUGUI>();
        textAnim = GetComponent<Animator>();
        LobbyMusicAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!LobbyMusicAudioSource.isPlaying && !isPaused)
        {
            PlayRandomMusic();
        }
    }

    void PlayRandomMusic()
    {
        LobbyMusicAudioSource.clip = LobbySongsList[Random.Range(0, LobbySongsList.Length)];
        LobbyMusicAudioSource.Play();
        songName.text = "";
        textAnim.Play("ShowText");
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
    public void PlayNextTrack()
    {
        for(int i = 0; i < LobbySongsList.Length; i++)
        {
            if(LobbyMusicAudioSource.clip == LobbySongsList[i])
            {
                int nextIndex = (i + 1) % LobbySongsList.Length;
                LobbyMusicAudioSource.clip = LobbySongsList[nextIndex];
                LobbyMusicAudioSource.Play();
                songName.text = "";
                textAnim.Play("ShowText");
                break;
            }
        }
    }

    public void PlayPreviousTrack()
    {
        for(int i = 0; i<LobbySongsList.Length; i++)
        {
            if(LobbyMusicAudioSource.clip == LobbySongsList[i])
            {
                int previousIndex = (i - 1 + LobbySongsList.Length) % LobbySongsList.Length;
                LobbyMusicAudioSource.clip = LobbySongsList[previousIndex];
                LobbyMusicAudioSource.Play();
                songName.text = "";
                textAnim.Play("ShowText");
                break;
            }
        }
    }
}
