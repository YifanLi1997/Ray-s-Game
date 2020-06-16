using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip failureSFX;

    [SerializeField] GameObject successFlag;

    KeyLock m_keyLock;

    private void Start()
    {
        m_keyLock = FindObjectOfType<KeyLock>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            if (m_keyLock.pairOrNot(collision.GetComponent<Key>(), gameObject.GetComponent<Lock>()))
            {
                // success
                AudioSource.PlayClipAtPoint(successSFX, Camera.main.transform.position);
                Instantiate(successFlag, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                // failure
                Debug.Log("not pair");
            }

          
        }
    }
}
