using UnityEngine;
using UnityEngine.UI;

public class StopButtonMechanic:MonoBehaviour
{
    public Image oldImage;
    public Sprite newSprite;

    public void Change()
    {
        oldImage.sprite = newSprite;
    }

}
