using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [Header("EXP Settings")]
    public float currentExp;     // Current EXP
    public float nextLevelExp;   // EXP required for the next level

    [Header("UI Elements")]
    public Image fillImage;      // Reference to the fill image of the bar

    private void Update()
    {
        UpdateExpBar();
    }

    public void UpdateExpBar()
    {
        if (fillImage != null && nextLevelExp > 0)
        {
            float fillAmount = Mathf.Clamp01(currentExp / nextLevelExp);
            fillImage.fillAmount = fillAmount; // Update the bar fill
        }
    }

    // Call this method to increase EXP
    public void SetExp(float exp, float totalExp)
    {
        currentExp = exp;
        nextLevelExp = totalExp;
    }
    


}
