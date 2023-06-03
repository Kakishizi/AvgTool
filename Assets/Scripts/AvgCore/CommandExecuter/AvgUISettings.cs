using System;
using AVG_module;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling.Experimental;
using UnityEngine.UI;


public struct DialogueContent
{
    public string talker_name;
    public string talker_text;
}

public class AvgUISettings : MonoBehaviour
{
    public static AvgUISettings Instance;

    public List<DialogueContent> dialogueContents = new List<DialogueContent>();

    public List<int> playerDecisions = new List<int>();

    public bool autoPlay = false;
    private int fastBtnClickCount = 0;

    //Unity UI
    public Image backgroundImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button skipButton;
    public Button fastButton;
    public List<Button> selectButtons;
    public Transform portraitPositions; //中左右
    public CanvasGroup fadePicture;

    public static void Init()
    {
        Instance = Instance == null
            ? Instantiate(Resources.Load<AvgUISettings>("AVGPanel"), GameObject.Find("Canvas").transform)
            : Instance;
        Instance.gameObject.SetActive(true);
    }

    private void Start()
    {
        SetUI();
    }

    public void End()
    {
        this.gameObject.SetActive(false);

        nameText.text = "";
        dialogueText.text = "";
        //清除选项按钮
        foreach (var VARIABLE in selectButtons)
        {
            VARIABLE.gameObject.SetActive(false);
        }

        //清除舞台上的角色
        foreach (var c in AVGModule.Instance.characters)
        {
            Destroy(c.Value);
        }

        AVGModule.Instance.characters.Clear();
    }

    private void SetUI()
    {
        //初始化跳过按钮
        this.skipButton.onClick.AddListener(() => { AVGModule.Instance.plotEnd.Invoke(); });
        fastButton.onClick.AddListener(() =>
        {
            fastBtnClickCount++;
            autoPlay = fastBtnClickCount < 4;
            if (fastBtnClickCount == 1)
            {
                AVGModule.Instance.SetTypingTimeDevision(0.05f);
            }
            else if (fastBtnClickCount == 2)
            {
                AVGModule.Instance.SetTypingTimeDevision(0.25f);
            }
            else if (fastBtnClickCount == 3)
            {
                AVGModule.Instance.SetTypingTimeDevision(0.01f);
            }
            else
            {
                fastBtnClickCount = 0;
                AVGModule.Instance.SetTypingTimeDevision(0.1f);
            }
        });
        nameText.text = "";
        dialogueText.text = "";
    }

    public void ResetUI()
    {
        //还原历史记录
        dialogueContents.Clear();

        //清除选项按钮（若有）
        playerDecisions.Clear();
    }

    public void FadeIn(float duration)
    {
        fadePicture.DOFade(1, duration);
    }

    public void FadeOut(float duration)
    {
        fadePicture.DOFade(0, duration);
    }
}