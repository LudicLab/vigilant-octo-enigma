using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Initializer : MonoBehaviour
{
    public SceneReference loadingScreenScene;
    public SceneReference mainMenuScene;

    [SerializeField] private List<SceneTask> taskList;

    bool timeFinished = false;
    bool tasksFinished = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(loadingScreenScene.name);

        StartCoroutine(WaitMinimumTime());
        StartCoroutine(RunTasks());
    }

    IEnumerator WaitMinimumTime()
    {
        yield return new WaitForSeconds(1f);
        timeFinished = true;
        TryFinish();
    }

    IEnumerator RunTasks()
    {
        foreach (var task in taskList)
            task.Init();

        bool done = false;

        while (!done)
        {
            done = true;

            foreach (var task in taskList)
            {
                if (!task.isFinished)
                {
                    done = false;
                    break;
                }
            }

            yield return null;
        }

        tasksFinished = true;
        TryFinish();
    }

    void TryFinish()
    {
        Debug.Log("trying to finish");
        if (timeFinished && tasksFinished)
            SceneManager.LoadScene(mainMenuScene.name);
    }
}