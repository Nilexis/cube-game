using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private float score = 0.0f;
    public Text scoreText;
    private bool isDead;
    public DeathMenu deathMenu;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            score += 5 * Time.deltaTime;
            scoreText.text = "  " + ((int)score).ToString() + "  ";
        }
    }

    public void OnDeath()
    {
        isDead = true;
        deathMenu.ToggleDeathMenu(score);
    }
}
