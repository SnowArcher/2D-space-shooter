using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Static reference to the Player (can be used in other scripts).
    public static Player instance = null;
    // Player health.
    public int player_Health = 1;
    // Reference to the Shield GameObject.
    public GameObject obj_Shield;
    // Shield health.
    public int shield_Health = 1;
    // Reference private to the UI's health bar.
    private Slider _slider_hp_Player;
    // Reference private to the UI's shield bar.
    private Slider _slider_hp_Shield;

    private void Awake()
    {
        // Setting up the references.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Look for an object with tag hp_Player and take the Slider component from it
        _slider_hp_Player = GameObject.FindGameObjectWithTag("sl_HP").GetComponent<Slider>();
        // Look for an object with tag hp_Shield and take the Slider component from it
        _slider_hp_Shield = GameObject.FindGameObjectWithTag("sl_Shield").GetComponent<Slider>();
    }

    private void Start()
    {
        // Set the health bar's value to the current health.
        _slider_hp_Player.maxValue = (float)player_Health;
        _slider_hp_Player.value = (float)player_Health;
        // If the shield has life...
        if (shield_Health != 0)
        {
            //Show shield.
            obj_Shield.SetActive(true);
            // Set the shield bar's value to the current health.
            _slider_hp_Shield.maxValue = (float)shield_Health;
            _slider_hp_Shield.value = (float)shield_Health;
        }
        // If the shield has no life...
        else
        {
            // Hide shield.
            obj_Shield.SetActive(false);
            _slider_hp_Shield.value = 0;
        }
    }
    // Method of taking damage by the shield
    public void GetDamageShield(int damage)
    {
        // Reduce the shield health by the damage amount.
        shield_Health -= damage;
        // Update shield bar's value to the current shield health.
        _slider_hp_Shield.value = (float)shield_Health;

        // If the shield does not have a health...
        if (shield_Health <= 0)
        {
            // Hide shield.
            obj_Shield.SetActive(false);
        }
    }

    // Method of taking damage by the player
    public void GetDamage(int damage)
    {
        // Reduce the health by the damage amount.
        player_Health -= damage;
        // Update health bar's value to the current health.
        _slider_hp_Player.value = (float)player_Health;

        // If the player does not have a health...
        if (player_Health <= 0)
        {
            // Call the player destruction method
            Destruction();
        }
    }
    // Method destruction player.
    void Destruction()
    {
        // Destroy the current player object.
        Destroy(gameObject);
    }
}
