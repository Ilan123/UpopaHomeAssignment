using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Action<float, float> onHealthChange = delegate { };
    public Action onZeroHealth = delegate { };
    public Action<int> onScoreChange = delegate { };
    [SerializeField] float maxHealth = 1000f;
    [SerializeField] float healthLostRate = 1f;
    [SerializeField] float amountOfHealthLost = 20f;
    [SerializeField] float healthLostAcceleration = 5f;
    private float _prevHealthLostTime;
    private int _score = 0;
    float currenthealth;
    

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        instance = this;
        currenthealth = maxHealth;
    }

    public void Start()
    {
        GameObject.Find("Spaceship").GetComponent<HealthLogic>().onGettingHitted += LoseHealth;
        _prevHealthLostTime = Time.time;
    }

    private void Update()
    {
        if(Time.time - _prevHealthLostTime > healthLostRate)
        {
            _prevHealthLostTime = Time.time;
            LoseHealth(amountOfHealthLost);
            amountOfHealthLost += healthLostAcceleration;
        }
    }

    private void LoseHealth(float damage)
    {
        currenthealth -= damage;
        onHealthChange(currenthealth, maxHealth);
        if(currenthealth <= 0)
        {
            onZeroHealth();
        }
    }

    public void AddHealth(int amount)
    {
        Debug.Log("UpdateHealth");
        currenthealth += amount;
        onHealthChange(currenthealth, maxHealth);
    }

    public void AddScore(int amount)
    {
        _score += amount;
        onScoreChange(_score);
    }
}
