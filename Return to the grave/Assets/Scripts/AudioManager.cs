using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField]
    public AudioClip textSound;
    [SerializeField]
    public AudioClip swordSwingAudio;
    [SerializeField]
    public AudioClip jumpAudio;
    private float volume = 1f;
    // Start is called before the first frame update
    private void Start()
    {
       
    }
    public void TextSound()
    {
        audioSource.PlayOneShot(textSound,volume);
    }
    public void SwordSwingAudio()
    {
        audioSource.PlayOneShot(swordSwingAudio, volume);
    }
    public void JumpAudio()
    {
        audioSource.PlayOneShot(jumpAudio, volume);
    }

}
