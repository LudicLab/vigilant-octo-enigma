using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] //makes variables show in the inspector
    private SoundLibrary sfxLibrary;
    [SerializeField]
    private AudioSource sfxSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void PlaySound3D(AudioClip clip, Vector3 position)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }
    public void PlaySound3D(string SoundName, Vector3 position) 
    {
        PlaySound3D(sfxLibrary.GetClipfromName(SoundName), position);
    }
    public void PlaySound2D(string SoundName)
    {
        sfxSource.PlayOneShot(sfxLibrary.GetClipfromName(SoundName));
    }
}
