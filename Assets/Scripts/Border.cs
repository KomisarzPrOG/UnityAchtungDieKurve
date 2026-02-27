using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] Transform topBorder;
    [SerializeField] Transform bottomBorder;
    [SerializeField] Transform leftBorder;
    [SerializeField] Transform rightBorder;

    public bool wrappedActive { get; private set; } = false;
    private int wrappedCounter = 0;

    [SerializeField] float blinkSpeed = 2f;
    private Coroutine blinkRoutine;

    [SerializeField] List<SpriteRenderer> bordersSR;

    public float GetMinX() { return leftBorder.position.x; }
    public float GetMaxX() { return rightBorder.position.x; }
    public float GetMinY() { return bottomBorder.position.y; }
    public float GetMaxY() { return topBorder.position.y; }

    public void PlayerOffScreen(Head player)
    {
        float minX = GetMinX();
        float maxX = GetMaxX();
        float minY = GetMinY();
        float maxY = GetMaxY();

        Vector3 pos = player.transform.position;

        bool offScreen = pos.x < minX ||
                         pos.x > maxX ||
                         pos.y < minY ||
                         pos.y > maxY;

        if (!offScreen) return;

        if(wrappedActive || player.playerWrap)
        {
            WrapPlayer(player, minX, maxX, minY, maxY);
        }
        else
            player.PlayerDeath($"{player.Name} was off screen");
    }

    void WrapPlayer(Head player, float minX, float maxX, float minY, float maxY)
    {
        Vector3 pos = player.transform.position;

        if (pos.x < minX)
            pos.x = maxX;
        else if (pos.x > maxX)
            pos.x = minX;

        if (pos.y < minY)
            pos.y = maxY;
        else if (pos.y > maxY)
            pos.y = minY;

        player.transform.position = pos;

        player.tail.StartNewSegment();
    }

    public IEnumerator WrappedBorders(float duration)
    {
        wrappedCounter++;

        if(wrappedCounter == 1)
        {
            wrappedActive = true;
            StartBlink();
        }

        yield return new WaitForSeconds(duration);

        wrappedCounter--;

        if(wrappedCounter == 0)
        {
            wrappedActive = false;
            StopBlink();
        }
    }

    private void StartBlink()
    {
        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);

        blinkRoutine = StartCoroutine(WrappedBlink());
    }

    private void StopBlink()
    {
        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);

        SetAlpha(1f);
    }

    private IEnumerator WrappedBlink()
    {
        while (true)
        {
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            float alpha = Mathf.Lerp(0.2f, 1f, t);

            SetAlpha(alpha);
            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        foreach(SpriteRenderer sr in bordersSR)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }
}
