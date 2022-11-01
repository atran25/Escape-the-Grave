using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    public void GameOver()
    {
        foreach (Behaviour comp in components)
        {
            Debug.Log("Deactivate: " + comp);
            comp.enabled = false;
        }
        body.velocity = new Vector2(0, 0);
        gameOverScreen.SetActive(true);
    }
}
