using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Transform parent;
    AudioSource audioMusa;
    public AudioClip patrol;
    public AudioClip chase;
    public AudioClip cave;
    private void Start()
    {
        audioMusa = GetComponent<AudioSource>();
        audioMusa.clip = patrol;
        audioMusa.Play();
    }

    void Update () {
        transform.position = new Vector3(parent.position.x, parent.position.y, transform.position.z);
	}

    public void SetAudioClip(AudioClip coso)
    {
        audioMusa.clip = coso;
        audioMusa.Play();
    }

    public AudioSource GetAudioSource()
    {
        return audioMusa;
    }
}
