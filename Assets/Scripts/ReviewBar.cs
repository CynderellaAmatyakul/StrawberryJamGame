using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ReviewBar : MonoBehaviour
{
    public Image reviewBar;
    public float reviewAmount;

    [SerializeField] private GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reviewAmount <= 0)
        {
            GameOver();
        }
    }

    public void TakeDamage(float damage)
    {
        reviewAmount -= damage;
        reviewBar.fillAmount = reviewAmount / 100f;
    }

    public void GameOver()
    {
        Time.timeScale = 0f; // Pause game
        gameOverPanel.SetActive(true);
        // Additional game over logic
    }
}
