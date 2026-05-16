using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnDebugUi : SceneTask
{
    public GameObject debugUi;

    public override void Init()
    {
        Instantiate(debugUi);
        // debugUi.SetActive(true);
        // DontDestroyOnLoad(debugUi);
        this.isFinished = true;
    }
}
