using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private Slider BGM_slider = null;
    [SerializeField] private Slider SFX_slider = null;
    [SerializeField] private float defaultVolume_ = 1.0f;

    [SerializeField] private TMP_Dropdown micDropdown; 
    void Start()
    {
        micDropdown.ClearOptions();

        List<string> options = new List<string>();

        foreach (var device in Microphone.devices)
        {
            options.Add(device); 
        }
        micDropdown.AddOptions(options);
        micDropdown.RefreshShownValue();

    }
    public void SetBGM(float volume)
    {
        //AudioListener.volume = volume;
        //PlayerPrefs.SetFloat("MasterVolume", AudioListener.volume);
        

    }
    public void SetSFX(float volume)
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
