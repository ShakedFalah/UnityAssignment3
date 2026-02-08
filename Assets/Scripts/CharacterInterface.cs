using UnityEngine;

public interface CharacterInterface
{
    public void UpdateHealth(float healthPercentage);

    public void GetKill(CharacterInterface character);

    public float ScoreValue();
}
