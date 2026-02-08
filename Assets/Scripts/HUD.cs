using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Slider playerHealthBar;
    public Slider enemyHealthBar;
    public Image heartsImage;
    public TextMeshProUGUI Score;
    public float enemyHealthTime = 1f;
    private float heartWidth = 55.8f;
    private float extraPixelsForHearts = 2f;
    
    public void UpdatePlayerHealthBar(float value)
    {
        playerHealthBar.value = Mathf.Clamp(value, 0, 1);
    }

    public void UpdatePlayerHearts(int hearts)
    {
        heartsImage.rectTransform.sizeDelta = new Vector2(hearts * heartWidth + extraPixelsForHearts, heartsImage.rectTransform.sizeDelta.y);
    }

    public void UpdateEnemyHealthBar(float value)
    {
        enemyHealthBar.value = Mathf.Clamp(value, 0, 1);
        StartCoroutine(ShowEnemyHealthBar());
    }

    private IEnumerator ShowEnemyHealthBar()
    {
        enemyHealthBar.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(enemyHealthTime);
        
        enemyHealthBar.gameObject.SetActive(false);
    }

    public void UpdateScore(float score)
    {
        Score.text = $"Score: {score}";
    }
}
