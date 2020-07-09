using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TitleAudioPlayer : MonoBehaviour
{
    [System.Serializable]
    public struct TitleAudios
    {
        public string title;
        public AudioClip clip;
    }

    public List<TitleAudios> titleAudios;
    private Dictionary<string, AudioClip> titleAudioDict = new Dictionary<string, AudioClip>();

    private AudioSource m_audioSource;

    private void Awake()
    {
        foreach (var audio in titleAudios)
        {
            titleAudioDict.Add(audio.title, audio.clip);
        }
    }

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void PlayTitleAudio(string title)
    {
        m_audioSource.clip = titleAudioDict[title];
        m_audioSource.Play();
    }
}
