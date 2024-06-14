using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public Health playerHealth;
    public Image fillimage;
    private Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth != null)
        {
            float fillValue = (float)playerHealth.currentHealth / playerHealth.maxHealth; // Ensure float division
            slider.value = fillValue;

            if (slider.value <= slider.minValue)
            {
                fillimage.enabled = false;
            }
            else
            {
                fillimage.enabled = true;

                if (fillValue <= 1f / 3f) // Changed to ensure correct float comparison
                {
                    fillimage.color = Color.red;
                }
                else
                {
                    fillimage.color = Color.green;
                }
            }
        }
    }
}
