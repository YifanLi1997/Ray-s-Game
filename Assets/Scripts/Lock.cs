using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip failureSFX;
    [SerializeField] AudioClip hintSFX;

    [SerializeField] GameObject successFlag;

    KeyLock m_keyLock;
    Collider2D m_col;
    Key m_pairedKey;

    private void Start()
    {
        m_keyLock = FindObjectOfType<KeyLock>();
        m_col = GetComponent<Collider2D>();
        m_pairedKey = m_keyLock.findPairedKey(this);
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            if (m_keyLock.pairOrNot(collision.GetComponent<Key>(), gameObject.GetComponent<Lock>()))
            {
                // success
                AudioSource.PlayClipAtPoint(successSFX, Camera.main.transform.position, 0.25f);
                successFlag.SetActive(true);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                AudioSource.PlayClipAtPoint(failureSFX, Camera.main.transform.position, 0.25f);
                collision.gameObject.transform.position = collision.gameObject.GetComponent<Key>().GetOriginalPos();
                m_pairedKey.gameObject.GetComponent<Animator>().SetBool("isShining", true);
                Invoke("ResetIsShining", 4.5f);
            }
        }
    }

    private void ResetIsShining()
    {
        m_pairedKey.gameObject.GetComponent<Animator>().SetBool("isShining", false);
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
                    AudioSource.PlayClipAtPoint(hintSFX, Camera.main.transform.position, 0.1f);

                    // TODO: the related key hint VFX

                }
            }
        }
    }
}
