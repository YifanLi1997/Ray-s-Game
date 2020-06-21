using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    [SerializeField] List<Key> keys;
    [SerializeField] List<Lock> locks;

    [SerializeField] BubbleSpawner bubbleSpawner;
    [SerializeField] GameObject restartPanel;

    [SerializeField] AudioClip winClip;

    private int m_count = 4;
    private GameEnvir m_gameEnvir;
    private AudioSource m_audioSource;

    // test config
    public bool winOrNot = false;

    public bool pairOrNot(Key m_key, Lock m_lock)
    {

        int lockIndex = locks.IndexOf(m_lock);
        int keyIndex = keys.IndexOf(m_key);

        if (keyIndex != lockIndex)
        {
            return false;
        }
        else
        {
            m_count--;
            return true;
        }
    }

    public Key findPairedKey(Lock m_lock)
    {
        int lockIndex = locks.IndexOf(m_lock);
        return keys[lockIndex];
    }

    public Lock findPairedLock(Key m_key)
    {
        int keyIndex = keys.IndexOf(m_key);
        return locks[keyIndex];
    }

    private void Start()
    {
        m_gameEnvir = FindObjectOfType<GameEnvir>();
        m_audioSource = m_gameEnvir.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (m_count <= 0 || winOrNot)
        {
            ShowWinEffect();
        }
    }

    private void ShowWinEffect()
    {
        //SFX
        m_audioSource.clip = winClip;
        if (! m_audioSource.isPlaying)
        {
            m_audioSource.Play();
        }

        // VFX
        bubbleSpawner.gameObject.SetActive(true);
        restartPanel.SetActive(true);
    }
    
    
}
