using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class Interaction : MonoBehaviour
{
    public abstract void Run(GameObject gameObject);
};
