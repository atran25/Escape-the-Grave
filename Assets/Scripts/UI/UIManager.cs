using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    public void GameOver()
    {
        foreach (Behaviour comp in components)
        {
            Debug.Log("Deactivate: " + comp);
            comp.enabled = false;
        }
        gameOverScreen.SetActive(true);
    }
}
