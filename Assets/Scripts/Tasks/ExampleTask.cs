using UnityEngine;

class ExampleTask : SceneTask
{
    public override void Init()
    {
        Debug.Log("Test logging");
        this.isFinished = true;
    }
}