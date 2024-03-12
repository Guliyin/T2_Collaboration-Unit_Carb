using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] Clips;

    Dictionary<string, AudioClip> ClipsDict;
    private void Awake()
    {
        object[] objs = FindObjectsOfType(typeof(AudioManager));
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        ClipsDict = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in Clips)
        {
            ClipsDict.Add(clip.name, clip);
        }
    }
    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void PlayAudio(string name)
    {
        audioSource.PlayOneShot(ClipsDict[name]);
    }
    public void PlayAudio(string[] names)
    {
        string name = names[Random.Range(0, names.Length)];
        PlayAudio(name);
    }
}
