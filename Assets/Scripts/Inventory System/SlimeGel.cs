using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGel: MonoBehaviour, IPickable
{
    public static event GelPicked OnGelPicked;
    public delegate void GelPicked(Loot loot);
    [SerializeField] private Loot gelData;
    private bool isBeingPickedUp = false;
    public void Pick()
    {
        if (isBeingPickedUp) return;
        isBeingPickedUp = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        OnGelPicked?.Invoke(gelData);
        gameObject.SetActive(false);
    }

    public void SetGelData(Loot newGelData)
    {
        if (newGelData != null)
        {
            gelData = newGelData;
        }
        else
        {
            Debug.LogError("Attempted to set gelData with null");
        }
    }

    public void StartJumpEffect()
    {
        StartCoroutine(JumpEffect());

        Invoke(nameof(StartFloatingEffect), 1f);
    }

    void StartFloatingEffect()
    {
        StartCoroutine(FloatingEffect());
    }

    private IEnumerator JumpEffect()
    {
        float jumpHeight = 1f;
        float duration = 1f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + jumpHeight, startPosition.z);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {//going up
            float height = Mathf.Sin(Mathf.PI * (elapsedTime / duration));
            transform.position = Vector3.Lerp(startPosition, endPosition, height);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;
        while (elapsedTime < duration)
        {//coming down
            float height = Mathf.Sin(Mathf.PI * (elapsedTime / duration));
            transform.position = Vector3.Lerp(endPosition, startPosition, height);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FloatingEffect()
    {
        float elapsedTime = 0f;
        float duration = 2f;
        Vector3 originalPosition = transform.position;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            float phase = Mathf.Sin(elapsedTime / duration * 2 * Mathf.PI) * 0.5f;
            transform.position = originalPosition + Vector3.up * phase * 0.1f;
            yield return null;
        }
    }

}
