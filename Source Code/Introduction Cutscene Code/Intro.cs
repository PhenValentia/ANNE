using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Intro : MonoBehaviour // Entirely by NDS8
{
    public GameObject movingRoom1;
    public GameObject movingRoom2;
    public AudioSource audioSource;
    public Image FadeImg;
    public float fadeSpeed = 1.5f;
    public bool sceneStarting = true;


    void Awake()    // On Load, create a fadeable square.
    {
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }

    void Start()    // Upon Starting, fade to clear, establish all rooms
    {
        Camera.main.transform.position = new Vector3(0, -1, -10);
        audioSource.clip = (AudioClip)Resources.Load("Sounds/Intro1");
        movingRoom1.transform.position = new Vector3(-5, 0, 0);
        movingRoom2.transform.position = new Vector3(10, 0, 0);
        StartCoroutine("FadeToClear");
        StartCoroutine(playAudio1());
    }

    void Update()    // Start Moving Rooms, if they get too far, reset their position.
    {
        movingRoom1.transform.position = movingRoom1.transform.position + new Vector3(0.05f, 0, 0);
        movingRoom2.transform.position = movingRoom2.transform.position + new Vector3(0.05f, 0, 0);
        if (movingRoom2.transform.position.x > 15)
        {
            movingRoom2.transform.position = new Vector3(-15, 0, 0);
        }
        if (movingRoom1.transform.position.x > 15)
        {
            movingRoom1.transform.position = new Vector3(-15, 0, 0);
        }

    }

    private IEnumerator playAudio1()    // Play Intro 1
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length-10);
        StartCoroutine("FadeToBlack");
        yield return new WaitForSeconds(10);
        Camera.main.transform.position = new Vector3(0, -1, 0);
        audioSource.clip = (AudioClip)Resources.Load("Sounds/Intro2");
        StartCoroutine(playAudio2());
    }

    private IEnumerator playAudio2()    // Play Intro 2
    {
        StartCoroutine("FadeToClear");
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length-10);
        StartCoroutine("FadeToBlack");
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("AnnaMainMap");
    }

    private IEnumerator FadeToClear()   // Fades screen from black to clear
    {

        do
        {
            // Lerp the colour of the image between itself and transparent.
            FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
            if (FadeImg.color.a <= 0.01f)
            {
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }


    private IEnumerator FadeToBlack()   // Fades screen from clear to black
    {
        do
        {
            // Lerp the colour of the image between itself and black.
            FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
            if (FadeImg.color.a >= 0.99f)
            {
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }
}
