using UnityEngine;
using UnityEngine.UI;
using Gley.MobileAds;
using System.Collections;

public class AdsManager : MonoBehaviour
{
    public int coins = 0;
    //public Text coinsText;
    public Button intersttialButton;
    public Button rewardedButton;

    /// <summary>
    /// Initialize the ads
    /// </summary>
    void Awake()
    {
        API.Initialize();
    }

    void Start()
    {
        //coinsText.text = coins.ToString();
        Invoke("ShowBanner", 3f);
    }

    /// <summary>
    /// Show banner, assigned from inspector
    /// </summary>
    public void ShowBanner()
    {
        API.ShowBanner(BannerPosition.Bottom, BannerType.Banner);
    }

    /// <summary>
    /// Hide banner assigned from inspector
    /// </summary>
    public void HideBanner()
    {
        API.HideBanner();
    }


    /// <summary>
    /// Show Interstitial, assigned from inspector
    /// </summary>
    public void ShowInterstitial()
    {
        API.ShowInterstitial();
    }

    /// <summary>
    /// Show rewarded video, assigned from inspector
    /// </summary>
    public void ShowRewardedVideo()
    {
        API.ShowRewardedVideo(CompleteMethod);
    }


    /// <summary>
    /// This is for testing purpose
    /// </summary>
    void Update()
    {
        if (API.IsInterstitialAvailable())
        {
            intersttialButton.interactable = true;
        }
        else
        {
            intersttialButton.interactable = false;
        }

        if (API.IsRewardedVideoAvailable())
        {
            rewardedButton.interactable = true;
        }
        else
        {
            rewardedButton.interactable = false;
        }
    }

    /// <summary>
    /// Complete method called every time a rewarded video is closed
    /// </summary>
    /// <param name="completed">if true, the video was watched until the end</param>
    private void CompleteMethod(bool completed)
    {
        if (completed)
        {
            coins += 100;
        }

        //coinsText.text = coins.ToString();
    }
}
