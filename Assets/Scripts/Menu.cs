using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    private bool isMenuOpened;

    public void ToggleMenu()
    {
        isMenuOpened = !isMenuOpened;
        anim.SetBool("MenuOpened", isMenuOpened);
    }

    private void OnGUI()
    {
        currencyUI.text = LevelManager.instance.currency.ToString() + " ß";
    }
}
