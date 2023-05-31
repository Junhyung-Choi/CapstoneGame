using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class GuideVideoManager : MonoBehaviour
{

    public static GuideVideoManager instance;
    public VideoClip walkClip, stepUpClip, climbClip, SquatClip, plankClip;

    VideoPlayer videoPlayer;
    TMPro.TMP_Text noticeText;
    

    void Start()
    {
        videoPlayer = this.transform.Find("Video Player").GetComponent<VideoPlayer>();
        noticeText = this.transform.Find("Notice").GetComponent<TMPro.TMP_Text>();

        videoPlayer.loopPointReached += EndReached;
        
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void ShowVideo(ChunkType type)
    {
        videoPlayer.transform.GetComponent<RawImage>().enabled = true;
        switch(type)
        {
            case ChunkType.WALK:
                videoPlayer.clip = walkClip;
                break;
            case ChunkType.STEPUP:
                videoPlayer.clip = stepUpClip;
                break;
            case ChunkType.CLIMB:
                videoPlayer.clip = climbClip;
                break;
            case ChunkType.SQUAT:
                videoPlayer.clip = SquatClip;
                break;
            case ChunkType.PLANK:
                videoPlayer.clip = plankClip;
                break;
        }
        videoPlayer.Play();
    }

    public void ShowNotice(string ment)
    {
        noticeText.gameObject.SetActive(true); 
        noticeText.text = ment;
    }

    public void EndReached(VideoPlayer vp)
    {
        videoPlayer.transform.GetComponent<RawImage>().enabled = false;
    }


    public void _ShowWalk()
    {
        ShowVideo(ChunkType.WALK);
    }

    public void _ShowStepUp()
    {
        ShowVideo(ChunkType.STEPUP);
    }

    public void _ShowClimb()
    {
        ShowVideo(ChunkType.CLIMB);
    }

    public void _ShowSquat()
    {
        ShowVideo(ChunkType.SQUAT);
    }

    public void _ShowPlank()
    {
        ShowVideo(ChunkType.PLANK);
    }

}
