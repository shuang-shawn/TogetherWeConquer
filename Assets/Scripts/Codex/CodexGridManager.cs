using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;

public class CodexGridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject codexGrid;
    [SerializeField]
    private GameObject comboButtonPrefab;
    private ComboList comboList;

    [SerializeField]
    private TextMeshProUGUI comboHeader;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private GameObject videoPlayer;

    [SerializeField]
    private Image comboIcon;

    [SerializeField]
    private GameObject comboSequenceUI;

    [SerializeField]
    private GameObject[] arrowImages;
    // Start is called before the first frame update
    private void Start()
    {
        comboList = GetComponent<ComboList>();
        videoPlayer.GetComponent<VideoPlayer>().clip = null;
        videoPlayer.GetComponent<VideoPlayer>().targetTexture.Release();
        PopulateGrid();
    }



    void PopulateGrid()
    {
        foreach (Combo combo in comboList.soloComboList)
        {
            GameObject itemButton = Instantiate(comboButtonPrefab, codexGrid.transform);

            // Set up the button with the combo data and callback
            CodexButton comboButton = itemButton.GetComponent<CodexButton>();
            comboButton.SetupButton(combo, OnComboButtonClick);
        }
        foreach (Combo combo in comboList.duoComboList)
        {
            GameObject itemButton = Instantiate(comboButtonPrefab, codexGrid.transform);

            // Set up the button with the combo data and callback
            CodexButton comboButton = itemButton.GetComponent<CodexButton>();
            comboButton.SetupButton(combo, OnComboButtonClick);
        }
    }

    private void OnComboButtonClick(Combo selectedCombo)
    {
        comboIcon.gameObject.SetActive(true);
        comboSequenceUI.SetActive(true); 
        string comboSkill = selectedCombo.GetComboSkill();
        if (!string.IsNullOrEmpty(comboSkill))
        {
            comboSkill = char.ToUpper(comboSkill[0]) + comboSkill.Substring(1);
        }
        comboHeader.text = comboSkill;
        description.text = selectedCombo.GetDescription();
        comboIcon.sprite = selectedCombo.GetComboIcon();

        VideoPlayer videoPlayerComponent = videoPlayer.GetComponent<VideoPlayer>();
        string videoClipName = selectedCombo.GetComboSkill();
        VideoClip videoClip = Resources.Load<VideoClip>($"Skill Gifs/{videoClipName}");

        foreach (Transform child in comboSequenceUI.transform)
        {
            Destroy(child.gameObject);
        }
        List<KeyCode> comboSequence = selectedCombo.GetComboSequence();
        foreach (KeyCode key in comboSequence)
        {
            switch (key)
            {
                case KeyCode.UpArrow:
                    Instantiate(arrowImages[0], comboSequenceUI.transform);
                    break;
                case KeyCode.DownArrow:
                     Instantiate(arrowImages[1], comboSequenceUI.transform);
                    break;
                case KeyCode.LeftArrow:
                    Instantiate(arrowImages[2], comboSequenceUI.transform);
                    break;
                case KeyCode.RightArrow:
                    Instantiate(arrowImages[3], comboSequenceUI.transform);
                    break;
                default:
                    break;
            }
        }
        if (videoClip != null)
        {
            videoPlayerComponent.clip = videoClip;
            videoPlayerComponent.Play(); // Start the video playback
        } else
        {
            videoPlayerComponent.clip = null;
            videoPlayer.GetComponent<VideoPlayer>().targetTexture.Release();
        }
    }
}