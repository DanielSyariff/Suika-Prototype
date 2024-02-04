using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private SuikaManager suikaManager;
    public TextMeshProUGUI textScore;
    public Image imageNextFruit;

    public SpriteRenderer redLine;

    [Header("Setting Menu")]
    public GameObject settingPanel;
    public Transform settingPopUp;

    public GameObject creditPanel;
    public Transform creditPopUp;

    [Header("Game Over Menu")]
    public GameObject animatedGameOver;
    public TextMeshProUGUI animatedGameOverText;
    public Color animatedWhiteBGColor;
    public Color animatedBlackBGColor;

    public TextMeshProUGUI resultScore;
    public GameObject resultPanel;
    public Transform resultPopUp;

    private void Start()
    {
        suikaManager = SuikaManager.instance;
        UpdateUIScore();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGameOverAnimation();
        }
    }

    #region Gameplay UI
    public void UpdateUIScore()
    {
        textScore.text = suikaManager.score.ToString();
    }

    public void UpdateUINextFruit(Sprite image)
    {
        imageNextFruit.sprite = image;
    }

    public void RedLineTriggered()
    {
        redLine.gameObject.SetActive(true);
        //Debug.Log("Activating Alert");
    }
    public void RedLineUntriggered()
    {
        redLine.gameObject.SetActive(false);
        //Debug.Log("Deactivating Alert");
    }
    #endregion

    #region Game Over Animation
    public void StartGameOverAnimation()
    {
        StartCoroutine(AnimateGameOver());
    }

    public IEnumerator AnimateGameOver()
    {
        animatedGameOver.SetActive(true);
        //animatedGameOver.GetComponent<Image>().color = Color.white; 
        animatedGameOver.GetComponent<Image>().DOColor(animatedBlackBGColor, 0.5f);
        animatedGameOverText.text = "";
        yield return new WaitForSeconds(0.5f);
        animatedGameOverText.transform.localScale = Vector3.zero;
        animatedGameOverText.text = "GAME OVER";
        animatedGameOverText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.5f);
        animatedGameOverText.transform.localScale = Vector3.zero;
        animatedGameOverText.text = "BREWING POTIONS";
        animatedGameOverText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.5f);
        animatedGameOverText.text = "";
        animatedGameOver.GetComponent<Image>().DOColor(animatedWhiteBGColor, 0.5f);
        yield return new WaitForSeconds(2f);
        OpenResultScreen();
    }
    #endregion

    #region Setting Menu
    public void OpenSetting()
    {
        settingPanel.SetActive(true);
        settingPopUp.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

        suikaManager.audioManager.PlaySFXOneShot("BtnClickPositif");
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        settingPopUp.DOScale(Vector3.zero, 0.5f);

        suikaManager.audioManager.PlaySFXOneShot("BtnClickNegatif");
    }

    public void OpenCredit()
    {   
        creditPanel.SetActive(true);
        creditPopUp.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

        suikaManager.audioManager.PlaySFXOneShot("BtnClickPositif");
    }

    public void CloseCredit()
    {
        creditPanel.SetActive(false);
        creditPopUp.DOScale(Vector3.zero, 0.5f);

        suikaManager.audioManager.PlaySFXOneShot("BtnClickNegatif");
    }
    #endregion

    #region Result Screen Menu
    public void OpenResultScreen()
    {
        resultScore.text = suikaManager.score.ToString();

        resultPanel.SetActive(true);
        resultPopUp.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }
    #endregion
}
