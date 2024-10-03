using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    public Image[] arrowImages;

    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;

    private List<KeyCode> arrowSequence;

    private int currentArrowIndex;

    private void Start()
    {
        arrowSequence = new List<KeyCode> { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

        foreach (Image arrow in arrowImages)
        {
            arrow.color = Color.white;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow)
           || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentArrowIndex < arrowSequence.Count)
            {
                Vector3 up = new Vector3(100.0f, 0.0f, 0.0f);
                arrowImages[currentArrowIndex].transform.Translate(up);

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

            if (currentArrowIndex > arrowSequence.Count)
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
