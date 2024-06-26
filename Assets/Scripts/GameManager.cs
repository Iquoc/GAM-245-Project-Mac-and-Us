using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //https://www.youtube.com/watch?v=rvm6C3EwDRo
    public static GameManager instance;

    private AudioSystem audioSys;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(instance);
        }

        audioSys = gameObject.GetComponent<AudioSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void playAudio(string sample)
    {
        Debug.Log(audioSys.Play(sample));
    }
}
