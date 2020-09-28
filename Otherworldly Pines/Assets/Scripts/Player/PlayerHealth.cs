﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHUDConnected {

    private static float maxHealth = 100f;
    private static float minHealth = 0f;
    
    private HealthBar healthBar;
    [HideInInspector] public float currentHealth = 100f;

    private void Start() {
        RefillMaxHealth();
    }

    private void SetHealth(float health) {
        currentHealth = health;
        if (healthBar != null) healthBar.SetHealth(currentHealth);
    }

    public void RefillMaxHealth() {
        SetHealth(maxHealth);
    }

    public void TakeDamage(float damage) {
        SetHealth(currentHealth - damage);
    }

    public void ConnectToHUD(HUD hud) {
        healthBar = hud.healthBar;
    }

}