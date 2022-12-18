using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected enum GameMenu {
        Pause,
        Animal,
        Weapon,
        Enhancement,
        None
    }

    public static GameManager instance;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject animalMenu;
    [SerializeField] private GameObject enhancementMenu;
    [SerializeField] private GameObject weaponsMenu;

    private bool gamePaused = false;
    private GameMenu activeMenu;

    private void Awake()
    {
        instance = this;
        activeMenu = GameMenu.None;
    }

    public void PauseGame()
    {
        PauseHelper(pauseMenu);
    }

    public void OpenAnimalFriendPowerMenu()
    {
        PauseHelper(animalMenu);
    }

    public void OpenGuardianEnhancementMenu()
    {
        PauseHelper(enhancementMenu);
    }

    public void OpenWeaponSelectionMenu()
    {
        PauseHelper(weaponsMenu);
    }

    private void PauseHelper(GameObject menu)
    {
        if (PlayerIsUnpausing(menu))
        {
            gamePaused = false;
            PauseCheck(menu);
            activeMenu = GameMenu.None;
        }
        else
        {
            pauseMenu.SetActive(false);
            animalMenu.SetActive(false);
            weaponsMenu.SetActive(false);
            enhancementMenu.SetActive(false);

            gamePaused = true;
            PauseCheck(menu);
            activeMenu = GetCurrentlyActiveMenu(menu);
        }
    }

    private bool PlayerIsUnpausing(GameObject menu)
    {
        switch (activeMenu)
        {
            case GameMenu.None: 
                return false;
            case GameMenu.Pause: 
                return menu == pauseMenu;
            case GameMenu.Animal: 
                return menu == animalMenu;
            case GameMenu.Enhancement: 
                return menu == enhancementMenu;
            case GameMenu.Weapon:
                return menu == weaponsMenu;
            default: 
                return false;
        }
    }

    private void PauseCheck(GameObject menu)
    {
        if (gamePaused)
        {
            Time.timeScale = 0f;
            menu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            menu.SetActive(false);
        }  
    }

    private GameMenu GetCurrentlyActiveMenu(GameObject menu)
    {
        if (menu == pauseMenu)
            return GameMenu.Pause;
        else if (menu == animalMenu)
            return GameMenu.Animal;
        else if (menu == weaponsMenu)
            return GameMenu.Weapon;
        else if (menu == enhancementMenu)
            return GameMenu.Enhancement;
        else
            return GameMenu.None;
    }
}
