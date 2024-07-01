using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform teleportTarget;
    [SerializeField] Parallax[] parallaxBackgrounds; // Add this line

    private void Awake()
    {
        // Optionally, automatically find all Parallax components in the scene if not set in the Inspector
        if (parallaxBackgrounds == null || parallaxBackgrounds.Length == 0)
        {
            parallaxBackgrounds = FindObjectsOfType<Parallax>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = teleportTarget.position;

            // Reset the parallax effect for each background
            foreach (var background in parallaxBackgrounds)
            {
                background.ResetParallaxEffect();
            }
        }
    }
}