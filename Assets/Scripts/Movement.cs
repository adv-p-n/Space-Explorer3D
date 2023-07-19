using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrust;
    [SerializeField] float rotationSpeed;
    [SerializeField] AudioClip thrusterAudio;
    [SerializeField] ParticleSystem mainThruster;
    [SerializeField] ParticleSystem rightThruster;
    [SerializeField] ParticleSystem leftThruster;
    //[SerializeField] Button thrustButton;

    AudioSource myAudioSource;
    Rigidbody myRigidbody;
    void Start()
    {
        myRigidbody= GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }

   
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }
    public void StartThrusting()
    {
        myRigidbody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(thrusterAudio);
        }
        if (!mainThruster.isPlaying)
        {
            mainThruster.Play();
        }
    }
    void StopThrusting()
    {
        mainThruster.Stop();
        myAudioSource.Stop();
    }


    void RotateLeft()
    {
        if (!leftThruster.isPlaying) { leftThruster.Play(); }
        ApplyRotation(rotationSpeed);
    }
     void RotateRight()
    {
        if (!rightThruster.isPlaying) { rightThruster.Play(); }
        ApplyRotation(-rotationSpeed);
    }
     void StopRotating()
    {
        leftThruster.Stop();
        rightThruster.Stop();
    }

    void ApplyRotation(float rotateThisFrame)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        myRigidbody.freezeRotation =false;
    }
}
