using UnityEngine;
using TMPro;

public class DebugStats : MonoBehaviour
{
    public TextMeshProUGUI statsText;

    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        float fps = 1.0f / deltaTime;
        float memory = (float)System.GC.GetTotalMemory(false) / 1024f / 1024f;

        statsText.text =
            $"FPS: {fps:0.}\n" +
            $"Frame Time: {deltaTime * 1000.0f:0.0} ms\n" +
            $"Memory: {memory:0.0} MB\n";
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // void OnDisable()
    // {
    //     Debug.Log("Disabled: " + gameObject.name);
    // }
}