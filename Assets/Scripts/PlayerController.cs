using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour, CharacterInterface
{
    private CharacterManager characterManager;
    private GameManager gameManager;
    private int hearts = 3;
    private float score = 0;
    HUD hud;
    void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        hud = GameObject.FindWithTag("Hud").GetComponent<HUD>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent <GameManager>();
    }

    void Start()
    {
        UpdateHearts(hearts);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        float verticalValue = Input.GetAxisRaw("Vertical"); 
        float horizontalValue = Input.GetAxisRaw("Horizontal");

        Vector3 direction = new Vector3(horizontalValue, verticalValue, 0);
        characterManager.walk(direction);

        bool punch = Input.GetButtonDown("Punch");
        if (punch)
        {
            characterManager.Attack(CharacterState.Punch);
        } 
        else
        {
            bool kick = Input.GetButtonDown("Kick");
            if (kick)
            {
                characterManager.Attack(CharacterState.Kick);
            }
        }

        bool testHit = Input.GetButtonDown("TestHit");
        if (testHit)
        {
            UpdateHearts(hearts - 1);
        }
    }

    public void UpdateHealth(float healthPercentage)
    {
        if (healthPercentage > 0)
        {
            hud.UpdatePlayerHealthBar(healthPercentage);
        } else
        {
            UpdateHearts(hearts - 1);
        }
    }

    private void UpdateHearts(int newHearts)
    {
        hearts = newHearts;
        hud.UpdatePlayerHearts(hearts);
        if (hearts <= 0)
        {
            gameManager.GameOver();
        } else
        {
            characterManager.HealToFull();
        }
    }

    public void GetScore(float scoreGained)
    {
        score += scoreGained;
        hud.UpdateScore(score);
    }

    public void GetKill(CharacterInterface character)
    {
        GetScore(character.ScoreValue());
    }

    public float ScoreValue()
    {
        return 0;
    }
}
