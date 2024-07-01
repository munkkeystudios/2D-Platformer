using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera Cam;
    [SerializeField] private Transform Player;
    [SerializeField] private float ParallaxEffectMultiplier;

    private Vector2 startPosition;
    private Vector2 startCameraPosition;
    private float startZ;

    void Start()
    {
        // Store the initial position of the background and its Z distance from the camera
        startPosition = transform.position;
        startCameraPosition = Cam.transform.position;
        startZ = transform.position.z;

        Debug.Log($"Start Position: {startPosition}");
        Debug.Log($"Start Camera Position: {startCameraPosition}");
    }

    void Update()
    {
        // Calculate the distance moved by the camera relative to its initial position
        Vector2 cameraTravel = (Vector2)Cam.transform.position - startCameraPosition;
        Vector2 deltaMovement = cameraTravel * ParallaxEffectMultiplier;
        Vector3 newPosition = new Vector3(startPosition.x + deltaMovement.x, startPosition.y + deltaMovement.y, startZ);

        // Adjust the background position based on the camera's movement and the parallax multiplier
        transform.position = newPosition;

        // Log the positions and calculations for debugging
        Debug.Log($"Camera Position: {Cam.transform.position}");
        Debug.Log($"Delta Movement: {deltaMovement}");
        Debug.Log($"New Background Position: {newPosition}");

        // If your background is looping, you would add logic here to check if the background
        // needs to be repositioned to create a seamless loop effect.
    }
}
