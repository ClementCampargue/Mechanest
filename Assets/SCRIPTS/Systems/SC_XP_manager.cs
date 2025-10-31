using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_XP_manager : MonoBehaviour
{

    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int[] xp_each_level;
    public int current_xp;
    [SerializeField] private int current_level;
    [SerializeField] private int money_to_nextlevel;
    public int money_to_nextlevel_;
    public GameObject panel_upgrade;

    void Start()
    {
        money_to_nextlevel = xp_each_level[0];
    }

    void Update()
    {
        text.text = current_xp.ToString();

        fill.fillAmount = (float)(current_xp - money_to_nextlevel_ )/ money_to_nextlevel;

        if (fill.fillAmount ==1)
        {
            current_level++;
            money_to_nextlevel_ = current_xp;
            money_to_nextlevel = xp_each_level[current_level];
            panel_upgrade.SetActive(true);
            Time.timeScale = 0;

        }

    }
}
