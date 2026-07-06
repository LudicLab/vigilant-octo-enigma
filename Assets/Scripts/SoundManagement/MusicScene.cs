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
        if (LobbySongsList == null || LobbySongsList.Length == 0) return;

        if (LobbyMusicAudioSource != null && !LobbyMusicAudioSource.isPlaying && !isPaused)
        {
            PlayRandomMusic();
        }
    }

    void PlayRandomMusic()
    {
        if (LobbyMusicAudioSource == null || LobbySongsList == null || LobbySongsList.Length == 0) return;

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

        if (LobbyMusicAudioSource == null) return;

        if (pause)
            LobbyMusicAudioSource.Pause();
        else
            LobbyMusicAudioSource.Play();
    }

    public void StopMusic()
    {
        if (LobbyMusicAudioSource != null)
        {
            LobbyMusicAudioSource.Stop();
        }
        isPaused = true;
    }

    private void PlayTrack(int index)
    {
        // early-return simplification requested in review
        if (LobbySongsList == null || LobbySongsList.Length == 0) return;
        if (index < 0 || index >= LobbySongsList.Length) return;
        if (LobbyMusicAudioSource == null) return;

        AudioClip track = LobbySongsList[index];
        if (track == null) return;

        LobbyMusicAudioSource.clip = track;
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

    // helper to extract duplicated index lookup logic (per review)
    private int GetCurrentTrackIndex()
    {
        if (LobbyMusicAudioSource == null || LobbyMusicAudioSource.clip == null || LobbySongsList == null) return -1;
        for (int i = 0; i < LobbySongsList.Length; i++)
        {
            if (LobbyMusicAudioSource.clip == LobbySongsList[i])
            {
                return i;
            }
        }
        return -1;
    }

    public void PlayNextTrack()
    {
        if (LobbySongsList == null || LobbySongsList.Length == 0) return;

        int currentIndex = GetCurrentTrackIndex();
        int nextIndex = currentIndex == -1 ? 0 : (currentIndex + 1) % LobbySongsList.Length;
        PlayTrack(nextIndex);
    }

    public void PlayPreviousTrack()
    {
        if (LobbySongsList == null || LobbySongsList.Length == 0) return;

        int currentIndex = GetCurrentTrackIndex();
        int previousIndex = currentIndex <= 0 ? LobbySongsList.Length - 1 : currentIndex - 1;
        PlayTrack(previousIndex);
    }
}
