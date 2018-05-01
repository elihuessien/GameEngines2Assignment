using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*All scene managment scripts were adapted from: 
 https://unity3d.com/learn/tutorials/projects/adventure-game-tutorial/game-state?playlist=44381*/
public class SceneController : MonoBehaviour
{

    CanvasGroup faderCanvasGroup;

    [SerializeField]
    string startingSceneName = "Start";
    [SerializeField]
    float fadeDuration = 1f;

    private bool isFading = false;




    private IEnumerator Start()
    {
        //start by loading in the main scene
        faderCanvasGroup = GameObject.Find("FaderCanvas").GetComponent<CanvasGroup>();

        yield return StartCoroutine(FadeAndSwitchScenes(startingSceneName));

        StartCoroutine(Fade(0f));
    }


    public void FadeAndLoadScene(String sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }
    }

    private IEnumerator FadeAndSwitchScenes(String sceneName)
    {
        //fade to black
        yield return StartCoroutine(Fade(1f));


        //Unload current Scene and Load new scene
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));


        //fade back in
        yield return StartCoroutine(Fade(0f));
    }


    private IEnumerator LoadSceneAndSetActive(String sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }


    //fade to white while changing scenes
    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;


        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha =
                Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        faderCanvasGroup.blocksRaycasts = true;
        isFading = false;
    }
}
