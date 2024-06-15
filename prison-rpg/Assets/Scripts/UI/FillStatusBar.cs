using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public Target target;
    public Image fillimage;
    private Slider slider;

    // Start is called before the first frame update
    void Awake()
    {   
        target = GetComponentInParent<Target>();
        slider = GetComponent<Slider>();
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            float fillValue = (float)target.currentHealth / target.maxHealth;
            slider.value = fillValue;

            if (slider.value <= slider.minValue)
            {
                fillimage.enabled = false;
            }
            else
            {
                fillimage.enabled = true;

                if (fillValue <= 1f / 3f)
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
