using TMPro;
using UnityEngine;

public class SC_timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    private float elapsedTime;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}