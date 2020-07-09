using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    [SerializeField] List<Key> keys;
    [SerializeField] List<Lock> locks;

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
}
