using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoStreamerScript : MonoBehaviour // by JB2051
{

    public UnityEngine.UI.RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public bool startVid;
    public bool playClip;
    public bool isReady;
    public string clip;
    public bool isPrep;
    private Button playPause;
    public GameObject pp;
    void Start()
    {
        startVid = false;
        playClip = false;
        isReady = false;
        playPause = pp.GetComponent<Button>();

    }
    void Update()
    {
        if (startVid)
        {
            isPrep = videoPlayer.isPrepared;
            if (isReady && isPrep)
            {
                rawImage.texture = videoPlayer.texture;
                videoPlayer.Play();
                audioSource.Play();
                isReady = false;
            }

            if (playClip)
            {
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = clip;
                videoPlayer.Prepare();
                playClip = false;
                isReady = true;
            }
            if (!startVid)
            {
                videoPlayer.Stop();
            }

        }
        else
        {
            videoPlayer.Stop();
        }

    }

    public void GiveURL(string urllink)
    {
        this.clip = urllink;
    }

    public void PlayClip()
    {
        this.startVid = true;
        this.playClip = true;
    }

    public void StopPlay()
    {
        this.playClip = false;
        this.startVid = false;
    }

    private void TogglePause()
    {
        if (videoPlayer.isPaused)
        {
            videoPlayer.Play();
            audioSource.Play();
        }
        else
        {
            videoPlayer.Pause();
            audioSource.Pause();
        }
    }
}
