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
        isPaused = true;   // STOP = muzyka nie gra, UI pokaże PLAY
    }
}
