using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip finishAudio;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] ParticleSystem finishfx;
    [SerializeField] ParticleSystem crashfx;

    Movement movement;
    AudioSource myAudiosource;

    bool isTransistioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        movement= GetComponent<Movement>();
        myAudiosource= GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessKey();
    }
    void ProcessKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        { LoadNextLevel(); }
        else if (Input.GetKeyDown(KeyCode.C))
        { collisionDisabled = !collisionDisabled;  }
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransistioning || collisionDisabled) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly Collision");
                break;
            case "Finish":
                LoadSequence();
                break;
            default:
                CrashSequence();
                break;
        }
    }
    void LoadSequence()
    {
        isTransistioning= true;
        finishfx.Play();
        myAudiosource.Stop();
        myAudiosource.PlayOneShot(finishAudio);
        movement.enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    void CrashSequence()
    {
        isTransistioning = true;
        crashfx.Play();
        myAudiosource.Stop();
        myAudiosource.PlayOneShot(crashAudio);
        movement.enabled = false;
        Invoke("ReloadLevel",delay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) { nextSceneIndex= 0; }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
