using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class JoinChannel : MonoBehaviour
{
    IRtcEngine rtcEngine;
    private string appId = "aa412416ace84d3597dd59d13c3b23af";
    public GameObject remoteVideo,selfVideo,Camera,End,Mic;
    private bool audio,video;
    public Sprite[] audioImages, videoImages;
    void Start()
    {
        audio = false;
        video = false;
        rtcEngine = IRtcEngine.GetEngine(appId);
        print(rtcEngine);
        rtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccess;
        rtcEngine.OnUserJoined = OnUserJoined;
        rtcEngine.OnUserOffline = OnUserOffline;
        rtcEngine.OnLeaveChannel = OnLeaveChannel;
        rtcEngine.EnableVideo();
        rtcEngine.EnableVideoObserver();
        rtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR | LOG_FILTER.CRITICAL);
        print(ChannelManager.ChannelName);
        print(rtcEngine.JoinChannel(ChannelManager.ChannelName, null, 0));
    }

    private void OnLeaveChannel(RtcStats stats)
    {
        SceneManager.LoadScene("JoinCall");
    }

    private void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {
        GameObject go = GameObject.Find(uid.ToString());
        if (!ReferenceEquals(go, null))
        {
            Destroy(go);
        }
    }

    private void OnUserJoined(uint uid, int elapsed)
    {
        print("user joined");
        remoteVideo.name = uid.ToString();
        VideoSurface videoSurface = makeImageSurface(uid.ToString());
        if (!ReferenceEquals(videoSurface, null))
        {
            videoSurface.SetForUser(uid);
            videoSurface.SetEnable(true);
            videoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
            videoSurface.SetGameFps(30);
        }
    }

    private void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        print("join succes");
        selfVideo.AddComponent<VideoSurface>();
    }

    public VideoSurface makeImageSurface(string goName)
    {
        
        VideoSurface videoSurface = remoteVideo.AddComponent<VideoSurface>();

        return videoSurface;
    }
    public void Leave()
    {
        rtcEngine.LeaveChannel();
        rtcEngine.DisableVideoObserver();
    }
    public void Audio()
    {
        audio = !audio;
        rtcEngine.MuteLocalAudioStream(audio);
        if (audio)
        {
            Mic.GetComponent<Button>().image.sprite =  audioImages[0];
        }
        else
        {
            Mic.GetComponent<Button>().image.sprite = audioImages[1];
        }
    }
    public void Video()
    {
        video = !video;
        rtcEngine.MuteLocalVideoStream(video);
        if (video)
        {
            //rtcEngine.DisableVideo();
            Camera.GetComponent<Button>().image.sprite = videoImages[0];
        }
        else
        {
            //rtcEngine.EnableVideo();
            Camera.GetComponent<Button>().image.sprite = videoImages[1];
        }
    }
}
