using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    private PlayerHUD player1;
    private PlayerHUD player2;

    private Image utilityP1;
    //private BossBehavivour boss;

    // Use this for initialization
    void Start()
    {
        player1 = InitPlayerHUD(GameObject.FindGameObjectWithTag("Player"), transform.Find("Player1"));
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerInfo(player1);
        UpdatePlayerInfo(player2);
    }

    public void ToggleHUD()
    {

    }

    private void UpdatePlayerInfo(PlayerHUD playerHUD)
    {
        if (playerHUD == null)
        {
            return;
        }
        playerHUD.healthSlider.value = playerHUD.player.hp;
        playerHUD.armorSlider.value = playerHUD.player.armor.Value;
    }

    private PlayerHUD InitPlayerHUD(GameObject player, Transform playerHUDHolder)
    {
        if (player == null)
        {
            return null;
        }
        PlayerHUD hud = new PlayerHUD();
        hud.player = player.GetComponent<PlayerBehaviour>();
        hud.healthSlider = playerHUDHolder.Find("Health").GetComponent<Slider>();
        hud.armorSlider = playerHUDHolder.Find("Armor").GetComponent<Slider>();
        hud.weapon = playerHUDHolder.Find("Weapon").GetComponent<Image>();
        hud.utility = playerHUDHolder.Find("Utility").GetComponent<Image>();

        hud.healthSlider.maxValue = Mathf.Min(100, hud.player.hp);
        hud.armorSlider.maxValue = 100;
        return hud;
    }

    private class PlayerHUD
    {
        public PlayerBehaviour player;
        public Slider healthSlider;
        public Slider armorSlider;
        public Image weapon;
        public Image utility;
    }
}
