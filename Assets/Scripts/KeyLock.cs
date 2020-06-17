using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    [SerializeField] List<Key> keys;
    [SerializeField] List<Lock> locks;

    [SerializeField] BubbleSpawner bubbleSpawner;
    [SerializeField] GameObject restartButton;

    private int m_count = 4;

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

    private void Update()
    {
        if (m_count <= 0)
        {
            bubbleSpawner.gameObject.SetActive(true);
            //restartButton.SetActive(true);
        }
    }

}
