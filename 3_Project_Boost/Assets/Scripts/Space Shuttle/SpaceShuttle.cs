using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShuttle : MonoBehaviour
{
    // Movement
    bool ignition = false;
    Rigidbody rigidBody;
    enum FrameRate : int { FPS30 = 30, FPS60 = 60, FPS80 = 80 };

    // Audio
    AudioSource audioSource;
    float defaultVolume;
    const float MIN_VOLUME = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        RemoveSoundArtifacts();
    }

    // Unity has a bug where stopping as sound causes a "popping" sound
    // To remove this artifact:
    // 1. use a 2 minute thruster sound clip
    // 2. never stop the sound
    // 3. reduce sound to 0.001 instead of stopping it
    private void RemoveSoundArtifacts()
    {
        defaultVolume = audioSource.volume;
        audioSource.volume = MIN_VOLUME;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            // Disable rotation of both A and D are pressed
            print("Disable rotation");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * (int)FrameRate.FPS60);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * (int)FrameRate.FPS60);
        }

        rigidBody.freezeRotation = false; // release control to physics
    }

    private void Thrust()
    {
        if (ignition == true)
        {
            rigidBody.useGravity = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            ignition = true;
        }
        ControlThurstSound();
    }

    private void ControlThurstSound()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.volume = defaultVolume;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.volume = MIN_VOLUME;
        }
    }
}
