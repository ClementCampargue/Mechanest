using UnityEngine;
using TMPro;
public class FPS_count : MonoBehaviour
{
    public TextMeshProUGUI text;

    private float polling = 3f;
    private float time;
    private int framecount;
    public int targetfps = 240;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        framecount++;
        if (time >= polling)
        {
            int frameRate = Mathf.RoundToInt(framecount / time);
            text.text = frameRate.ToString();
            time -= polling;
            framecount = 0;

            if (frameRate > 100)
            {
                text.color = Color.green;
            }

            if (frameRate <= 100 && frameRate > 60)
            {
                text.color = Color.yellow;
            }

            if (frameRate <= 60)
            {
                text.color = Color.red;
            }
        }
    }
}
