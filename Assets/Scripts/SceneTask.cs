using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class SceneTask : MonoBehaviour
{
    public bool isFinished = false;

    //init should set isfinished when done
    public abstract void Init();
};
