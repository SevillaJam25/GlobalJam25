using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    //Wave vars
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float length = 2f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float offset = 0f;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this); 
    }
    private void Update()
    {
        offset += Time.deltaTime * speed; 
    }

    public float GetWaveHeight(float xCoord) //Returns wave height at given height coord.
    {
        return amplitude * Mathf.Sin(xCoord/length + offset); //Wave eq
    }
}
