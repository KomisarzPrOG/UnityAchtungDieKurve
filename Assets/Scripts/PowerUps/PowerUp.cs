using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public List<PowerUpTargetType> possibleTargets = new List<PowerUpTargetType>();
    public PowerUpTargetType targetType { get; private set; }
    
    public SpriteRenderer Border;

    protected List<Head> playerList
    {
        get { return GameManager.Instance.GetAllPlayers(); }
    }

    protected GameSettings settings
    {
        get { return GameSettings.Instance; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Head player = collision.gameObject.GetComponent<Head>();
        
        if (player == null)
        {
            Debug.LogWarning("PowerUp: Player is null");
            return;
        }

        Activate(player);
    }

    public void Activate(Head collector)
    {
        switch (targetType)
        {
            case PowerUpTargetType.Self:
                ApplySelf(collector);
                break;
            case PowerUpTargetType.Others:
                ApplyToOthers(collector);
                break;
            case PowerUpTargetType.Global:
                ApplyGlobal(collector);
                break;
        }
    }

    protected abstract void ApplySelf(Head player);
    protected abstract void ApplyToOthers(Head collector);
    protected abstract void ApplyGlobal(Head collector);

    public void RandomizeTargetType()
    {
        if(possibleTargets == null || possibleTargets.Count == 0)
            return;

        int index = Random.Range(0, possibleTargets.Count);
        targetType = possibleTargets[index];

        SetPowerUp();
    }

    public void SetPowerUp()
    {
        switch(targetType)
        {
            case PowerUpTargetType.Self:
                Border.color = Color.green;
                break;
            case PowerUpTargetType.Others:
                Border.color = Color.red;
                break;
            case PowerUpTargetType.Global:
                Border.color = Color.blue;
                break;
        }
    }
}
