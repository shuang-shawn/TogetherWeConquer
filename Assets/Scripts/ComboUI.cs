using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    public Image[] arrowImages;

    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;

    private KeyCode[] arrowSequence = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

    private int currentArrowIndex;

    private void Start()
    {
        foreach(Image arrow in arrowImages)
        {
            arrow.color = Color.white;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow)
           || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentArrowIndex < arrowSequence.Length)
            {
                KeyCode correctArrow = arrowSequence[currentArrowIndex];

                if (Input.GetKeyDown(correctArrow))
                {
                    arrowImages[currentArrowIndex].color = correctColor;
                }
                else
                {
                    arrowImages[currentArrowIndex].color = incorrectColor;
                }
            }

            currentArrowIndex++;

            if (currentArrowIndex > arrowSequence.Length)
            {
                foreach (Image arrow in arrowImages)
                {
                    arrow.color = Color.white;
                }

                currentArrowIndex = 0;
            }
        }
    }
}
