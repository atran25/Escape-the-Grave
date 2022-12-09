using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;
    private bool dead;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;


    [SerializeField] private AudioSource hitSound;

    private Transform currentCheckpoint;
    private UIManager uiManager;

    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void TakeDamage(float _damage)
    {
        if (gameObject.tag == "Player")
        {
            hitSound.Play();
        }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            StartCoroutine(Invulnerability());
        }
        else
        {
            
            if (!dead)
            {

                foreach (Behaviour comp in components)
                {
                    comp.enabled = false;
                }

                dead = true;
                gameObject.SetActive(false);

                if (gameObject.tag == "Player")
                {
                    Respawn();
                    transform.position = currentCheckpoint.position;
                }
                else if (gameObject.tag == "Enemy")
                {

                    Invoke("Respawn", 5);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (2 * numberOfFlashes));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (2 * numberOfFlashes));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        AddHealth(startingHealth);
        dead = false;
        gameObject.SetActive(true);
        StartCoroutine(Invulnerability());
        foreach (Behaviour comp in components)
        {
            comp.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;

        }
        else if (collision.gameObject.CompareTag("end") &&  gameObject.CompareTag("Player"))
        {
            Debug.Log("Victory");
            uiManager.GameOver();
        }
    }
}

