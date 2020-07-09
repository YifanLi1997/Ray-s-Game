using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [System.Serializable]
    public struct MyClip{
        public AudioClip audioClip;
        public float volume;
    }

    [SerializeField] MyClip[] successSFXs;
    [SerializeField] AudioClip failureSFX;
    [SerializeField] AudioClip hintSFX;

    [SerializeField] GameObject nextObject;

    KeyLock m_keyLock;
    Collider2D m_col;
    Key m_pairedKey;

    private void Start()
    {
        m_keyLock = FindObjectOfType<KeyLock>();
        m_col = GetComponent<Collider2D>();
        m_pairedKey = m_keyLock.findPairedKey(this);
    }
   
    // This is terrible collision logic
    // NEVER DO THIS AGAIN
    // USER LAYER-BASED COLLISON SYSTEM
    // having some questions about whether OnTrigger events respect collison matrix,
    // may figure out sometime in the future
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            if (m_keyLock.pairOrNot(collision.GetComponent<Key>(), this))
            {
                // success
                AudioSource.PlayClipAtPoint(successSFXs[0].audioClip, Camera.main.transform.position, successSFXs[0].volume);
                AudioSource.PlayClipAtPoint(successSFXs[1].audioClip, Camera.main.transform.position, successSFXs[1].volume);
                nextObject.SetActive(true);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                // wrong
                AudioSource.PlayClipAtPoint(failureSFX, Camera.main.transform.position, 0.25f);
                collision.gameObject.transform.position = collision.gameObject.GetComponent<Key>().GetOriginalPos();
                if (m_pairedKey)
                {
                    m_pairedKey.gameObject.GetComponent<Animator>().SetBool("isShining", true);
                    Invoke("ResetIsShining", 4.5f);
                }
            }
        }
        else if (collision.CompareTag("Bubble"))
        {

        }
        else
        {
            AudioSource.PlayClipAtPoint(failureSFX, Camera.main.transform.position, 0.25f);
            collision.gameObject.transform.position = collision.gameObject.GetComponent<Diamond>().GetOriginalPos();
            if (m_pairedKey)
            {
                m_pairedKey.gameObject.GetComponent<Animator>().SetBool("isShining", true);
                Invoke("ResetIsShining", 4.5f);
            }
        }
            
    }

    private void ResetIsShining()
    {
        if (m_pairedKey)
        {
            m_pairedKey.gameObject.GetComponent<Animator>().SetBool("isShining", false);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);

                if (touchedCollider == m_col)
                {
                    if (m_pairedKey)
                    {
                        AudioSource.PlayClipAtPoint(hintSFX, Camera.main.transform.position, 0.1f);

                        m_pairedKey.gameObject.GetComponent<Animator>().SetBool("isShining", true);
                        Invoke("ResetIsShining", 4.5f);
                    }
                }
            }
        }
    }
}
