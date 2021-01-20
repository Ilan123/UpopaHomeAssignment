using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    public static GameInit instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.onHealthChange += UIManager.instance.UpdateHealth;
        GameManager.instance.onScoreChange += UIManager.instance.UpdateScore;
        GameManager.instance.onZeroHealth += UIManager.instance.ShowEndGameScreen;
    }

}
