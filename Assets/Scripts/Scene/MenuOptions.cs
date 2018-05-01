using UnityEngine;
using System.Collections;

public class MenuOptions : MonoBehaviour
{
    [SerializeField]
    int menuChangeInterval= 2;

    GameObject menu;
    int counter;
    float interval;

    bool sceneChanging = false;
    bool isFading = false;


    // Use this for initialization
    void Start()
    {
        menu = GameObject.Find("Menu");
        counter = 1;


        //set the first menu option active
        menu.transform.GetChild(counter).GetComponent<CanvasGroup>().alpha = 1;
        interval = menuChangeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        interval -= Time.deltaTime;

        //if we have reached the end load next scene
        if(counter + 1 == menu.transform.childCount 
            && interval < 0 
            && !sceneChanging)
        {
            sceneChanging = true;
            StartCoroutine(Fade(0));
            SceneController sc = GameObject.Find("SceneManager").GetComponent<SceneController>();
            sc.FadeAndLoadScene("SpaceChaseScene");
        }



        if (!isFading && !sceneChanging)
        {
            if (interval < 0)
            {
                //fade Out
                StartCoroutine(Fade(0));
            }

            //if our current screen is blank, load next screen
            if (menu.transform.GetChild(counter).GetComponent<CanvasGroup>().alpha <= 0.01)
            {
                counter++;
                StartCoroutine(Fade(1));
                interval = menuChangeInterval;
            }
        }
    }


    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        CanvasGroup c = menu.transform.GetChild(counter).GetComponent<CanvasGroup>();
        c.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(c.alpha - finalAlpha) / 1;


        while (!Mathf.Approximately(c.alpha, finalAlpha))
        {
            c.alpha =
                Mathf.MoveTowards(c.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        
        isFading = false;
    }
}
