using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    private TMP_Text indicationText;

    void Awake()
    {
        indicationText = GetComponent<TMP_Text>();
    }
    void Start()
    {
        gameObject.SetActive(false);
        PlayerTrigger.onTriggerEnterWithElement += enterTriggerWithElement;
        PlayerTrigger.onTriggerExitWithElement += exitTriggerrWithElement;
    }

    private void setText(string text)
    {
        gameObject.SetActive(true);
        indicationText.text = "Presione la E para " + text;
    }

    private void enterTriggerWithElement(string element)
    {
        setText(element);
    }

    private void exitTriggerrWithElement()
    {
        gameObject.SetActive(false);
    }

    private void DisplayText()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
