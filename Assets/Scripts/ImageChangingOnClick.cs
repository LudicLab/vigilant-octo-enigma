using UnityEngine;
using UnityEngine.UI;

public class ImageChangingOnClick : MonoBehaviour
{
    public Sprite PlaySprite;
    public Sprite PauseSprite;
    public Image targetImage;
    public MusicScene musicScene;

    public void Toggle()
    {
        bool paused = musicScene.isPaused;

        if (paused)
        {
            // muzyka jest zapauzowana → klik = PLAY
            musicScene.ToggleMusic(false);
            targetImage.sprite = PauseSprite;
        }
        else
        {
            // muzyka gra → klik = PAUSE
            musicScene.ToggleMusic(true);
            targetImage.sprite = PlaySprite;
        }
    }
}
