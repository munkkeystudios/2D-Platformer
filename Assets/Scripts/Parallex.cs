using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera Cam;
    [SerializeField] private float ParallaxEffectMultiplier;

    private Vector2 startPosition;
    private Vector2 startCameraPosition;
    private float startZ;

    void Start()
    {
        InitializeParallax();
    }

    void Update()
    {
        Vector2 cameraTravel = (Vector2)Cam.transform.position - startCameraPosition;
        Vector2 deltaMovement = cameraTravel * ParallaxEffectMultiplier;
        Vector3 newPosition = new Vector3(startPosition.x + deltaMovement.x, startPosition.y + deltaMovement.y, startZ);

        transform.position = newPosition;
    }

    // Call this method after the player is teleported
    public void ResetParallaxEffect()
    {
        startCameraPosition = Cam.transform.position;
    }

    private void InitializeParallax()
    {
        startPosition = transform.position;
        startCameraPosition = Cam.transform.position;
        startZ = transform.position.z;
    }
}
