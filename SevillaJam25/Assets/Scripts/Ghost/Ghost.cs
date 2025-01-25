using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class Ghost : MonoBehaviour
{
    [Range(1, 9)] public int activity = 1; 
    [Range(1, 3)] public int aggressivity = 1; 

    private bool isMaxActivity = false;
    private float timeAtMax = 0f;
    private float timeSinceLastAttack = 0f;

    public Text uiActivity;
    public Text uiAggressivity;
    

    void Start()
    {
        this.uiAggressivity.text = aggressivity.ToString();
        StartCoroutine(UpdateActivity());
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    private void modifyActivity(int Activity)
    {
        activity = Activity;
        this.uiActivity.text = Activity.ToString();
    }

    private void modifyAggressivity(int Aggresivity)
    {
        aggressivity = Aggresivity;
        this.uiAggressivity.text = Aggresivity.ToString();
    }

    public void RegisterAttack()
    {
        timeSinceLastAttack = 0f; 
    }

    private int getNextActivity(float probabilityUp) {
        float valGenerated = Random.value;
        float solution = probabilityUp-valGenerated;
        Debug.Log("prob: "+valGenerated+" change: "+solution);
        if(Math.Abs(solution) == 0.1) {
            return 1;
        } else if(solution < 0) {
            return 0;
        } else {
            return 2;
        }
    }

    IEnumerator UpdateActivity()
    {
        while (true)
        {
            if (isMaxActivity)
            {
                timeAtMax -= 1f;
                if (timeAtMax <= 0)
                {
                    modifyActivity(1);
                    yield return new WaitForSeconds(5f); 
                    isMaxActivity = false;
                }
            }
            else
            {
                float probabilityUp = 0.1f + (aggressivity * 0.1f) + (timeSinceLastAttack * 0.02f); 
                probabilityUp = Mathf.Clamp(probabilityUp, 0.4f, 0.6f); 
                int nextActivity = getNextActivity(probabilityUp);

                if (nextActivity==2 && activity < 10)
                    modifyActivity(activity+1);
                else if (nextActivity==0 && activity > 1)
                    modifyActivity(activity-1);

                if (activity == 10)
                {
                    isMaxActivity = true;
                    timeSinceLastAttack = 0f;
                    timeAtMax = Random.Range(10f, 20f); 
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
