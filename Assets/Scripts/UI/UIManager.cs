using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text textScore = null;
    [SerializeField] Slider sliderHealth = null;
    [SerializeField] GameObject panelMainMenu = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void UpdateScore(int score)
    {
        textScore.text = score.ToString();
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        Debug.Log("UpdateHealth");
        sliderHealth.value = currentHealth / maxHealth;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowEndGameScreen()
    {
        panelMainMenu.SetActive(true);
    }
}
