using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StartingCutScene : MonoBehaviour
{
    public GameObject[] DeactiveObjs;
    public PlayableDirector playableDirector;

    void Start()
    {
        foreach (var obj in DeactiveObjs)
        {
            obj.SetActive(false);
        }
        playableDirector.stopped += OnPlayableDirectorStopped;

    }
    private void OnDestroy()
    {
        // Unsubscribe from the stopped event to avoid memory leaks
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }
    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        SceneManager.LoadScene("Flying_GamePlay");
    }

}
