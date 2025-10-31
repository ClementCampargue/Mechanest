using UnityEngine;

public class SC_upgrade_panel : MonoBehaviour
{
    public SC_game_master master_;

    void Start()
    {
        PlayerPrefs.SetFloat("Damage_boost", 1);

        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void quit()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void unit_add()
    {

        quit();

    }

    public void unit_max()
    {

        quit();

    }

    public void unit_speed()
    {

        quit();

    }

    public void unit_damage()
    {
        PlayerPrefs.SetFloat("Damage_boost", PlayerPrefs.GetFloat("Damage_boost")*1.5f);
        quit();

    }
}
