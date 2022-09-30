using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public float sliderSpeed = 0.1f;

    [Space]
    public float currentDanger;
    public float currentFame;
    public float currentRage;

    [Space]
    public Gradient DangerSliderColours;
    public Gradient FameSliderColours;
    public Gradient RageSliderColours;

    [Space]
    public Slider dangerSlider;
    public Slider fameSlider;
    public Slider rageSlider;

    [Space]
    public Image dangerImage;
    public Image fameImage;
    public Image rageImage;

    [Space]
    public TextMeshProUGUI rageText;
    public TextMeshProUGUI dangerText;
    public TextMeshProUGUI fameText;

    private void FixedUpdate()
    {
        dangerSlider.value = Mathf.Lerp(dangerSlider.value, currentDanger, sliderSpeed);
        fameSlider.value = Mathf.Lerp(fameSlider.value, currentFame, sliderSpeed);
        rageSlider.value = Mathf.Lerp(rageSlider.value, currentRage, sliderSpeed);

        dangerImage.color = DangerSliderColours.Evaluate(dangerSlider.normalizedValue);
        fameImage.color = FameSliderColours.Evaluate(fameSlider.normalizedValue);
        rageImage.color = RageSliderColours.Evaluate(rageSlider.normalizedValue);

        rageText.text = (rageSlider.normalizedValue * 100).ToString("0") + "%";
        dangerText.text = (dangerSlider.normalizedValue * 100).ToString("0") + "%";
        fameText.text = (fameSlider.normalizedValue * 100).ToString("0") + "%";
    }

    public void addRage(float amount)
    {
        rageSlider.GetComponent<Animator>().Play("AddValueSliderAni");
        currentRage += amount;
    }
    public void addFame(float amount)
    {
        //add animation
        fameSlider.GetComponent<Animator>().Play("AddValueSliderAni");
        currentFame += amount;
    }
    public void addDanger(float amount)
    {
        //add animation
        dangerSlider.GetComponent<Animator>().Play("AddValueSliderAni");
        currentDanger += amount;
    }
}
