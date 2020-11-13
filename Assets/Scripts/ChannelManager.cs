using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChannelManager : MonoBehaviour
{
    public static string ChannelName;
    public InputField channelName;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void Join()
    {
        ChannelName = channelName.text;
        SceneManager.LoadScene("VideoCall");
    }
}
