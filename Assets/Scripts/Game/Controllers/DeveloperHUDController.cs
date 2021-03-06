﻿using UnityEngine;
using UnityEngine.UI;

public class DeveloperHUDController : MonoBehaviour
{
    private Text timerText;

    private DeveloperPlayerHUD hudP1;
    private DeveloperPlayerHUD hudP2;

    void Start()
    {
        //timerText = transform.Find("Timer").GetComponent<Text>();
        hudP1 = InitPlayerHUD(GameObject.FindGameObjectWithTag("Player"), transform.Find("Player1"));
        hudP2 = InitPlayerHUD(GameObject.FindGameObjectWithTag("Player"), transform.Find("Player1"));
    }

    void Update()
    {
        UpdateHealth(hudP1);
        UpdateArmor(hudP1);

        UpdateHealth(hudP2);
        UpdateArmor(hudP2);
    }

    private void UpdateTimer(float timeLeft)
    {
        float minutes = Mathf.Floor(timeLeft / 60);
        float seconds = (timeLeft % 60);
        //timerText.text = string.Format("{0}:{1}", minutes.ToString("0"), seconds.ToString("00"));
    }

    private void UpdateHealth(DeveloperPlayerHUD hud)
    {
        hud.health.text = hud != null ? hud.player.hp.ToString() : "0";
    }

    private void UpdateArmor(DeveloperPlayerHUD hud)
    {
        bool hasArmor = hud != null && hud.player.armor != null;
        hud.armor.text= hasArmor ? hud.player.armor.Value.ToString() : "0";
    }

    private DeveloperPlayerHUD InitPlayerHUD(GameObject player, Transform playerHUDHolder)
    {
        if (player == null)
        {
            return null;
        }
        DeveloperPlayerHUD hud = new DeveloperPlayerHUD();
        hud.player = player.GetComponent<PlayerBehaviour>();
        hud.health = playerHUDHolder.Find("Health").GetComponent<Text>();
        hud.armor = playerHUDHolder.Find("Armor").GetComponent<Text>();
        return hud;
    }

    private class DeveloperPlayerHUD
    {
        public PlayerBehaviour player;
        public Text health;
        public Text armor;
    }
}
