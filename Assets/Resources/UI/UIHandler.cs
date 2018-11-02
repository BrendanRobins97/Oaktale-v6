using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour {

    public GameObject options;
    public GameObject playerInventory;
    public GameObject externalInventory;
    public GameObject craftingUI;
    public HealthManager healthManager;
    public Slider healthBar;
    public ExternalInventory externInv;

    private Player player;

    private void Start() {
        player = GameManager.Get<Player>();
    }
    void Update () {
        if (!GameManager.Get<ConsoleView>().visible)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (playerInventory.activeInHierarchy || craftingUI.activeInHierarchy || (externalInventory != null && externalInventory.activeInHierarchy))
                {
                    playerInventory.SetActive(false);
                    craftingUI.SetActive(false);

                    if (externalInventory != null)
                    {

                        //externInv.CloseInventory();
                        externalInventory.SetActive(false);
                    }
                }
                else
                {
                    options.SetActive(!options.activeInHierarchy);
                }

            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                playerInventory.SetActive(!playerInventory.activeInHierarchy);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                craftingUI.SetActive(!craftingUI.activeInHierarchy);
            }
        }
        if (playerInventory.activeInHierarchy || craftingUI.activeInHierarchy) {
            player.acceptClick = false;
        } else {
            player.acceptClick = true;
        }
        healthBar.value = healthManager.HealthPercent();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
