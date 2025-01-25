using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OxygenSystem : MonoBehaviour
{
    // Oxygen capacity the player will have in the sea
    public int OxygenCapacity = 100;

    // Oxygen left in the player
    private float OxygenLeft;

    // Time per breah period
    public float TimePerBreathPeriod = 4f;

    // This variable will be used if we implement mic listening feature
    // public float BrehPeriodsMax = 16f;

    // Oxygen loss per breath
    public int OxygenLoss = 5;

    // This variable will be used if we implement mic listening feature
    // public float PerdidaOxigenoMax= 3f;

    // This variable will be used to know how much time must pass between breaths
    public float timePerBreath = 3f;

    public BreahStates breathState = BreahStates.SURFACE;
    public Text uiBreathState;
    public Text uiOxygenLeft;

    private bool isBreating = false;

    private void OnEnable()
    {
        uiBreathState.text = "SURFACE";
        updateOxygen(100f);
        PlayerTrigger.onSeaEnter += StartSubmersion;
        PlayerTrigger.onSeaLeave += StopSubmersion;
        OxygenLeft = OxygenCapacity;
    }

    private void StartSubmersion()
    {
        if(!isBreating) {
            StartCoroutine("Breath");
        }
    }

    private void StopSubmersion()
    {
        updateOxygen(100f);
        changeState(BreahStates.SURFACE);
        isBreating = false;
        StopCoroutine("Breath");
    }

    private void changeState(BreahStates newState) 
    {
        breathState = newState;
        uiBreathState.text = newState.ToString();
    }

    private void updateOxygen(float oxygen) {
        OxygenLeft = oxygen;
        uiOxygenLeft.text = oxygen.ToString();
    }

    private IEnumerator Breath()
    {
        isBreating = true;
        while (PlayerTrigger.playerPosition!=PlayerPosition.BOAT)
        {
            // INHALATION
            changeState(BreahStates.INHALING);
            yield return new WaitForSeconds(TimePerBreathPeriod);

            // EXHALATION
            changeState(BreahStates.EXHALATING);
            updateOxygen(OxygenLeft - OxygenLoss);
            yield return new WaitForSeconds(TimePerBreathPeriod);

            // CHILL
            changeState(BreahStates.CHILL);
            yield return new WaitForSeconds(timePerBreath);
        }
    }
}
