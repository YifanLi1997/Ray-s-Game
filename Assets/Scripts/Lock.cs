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

    private void Start()
    {
        m_keyLock = FindObjectOfType<KeyLock>();
        m_col = GetComponent<Collider2D>();
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            if (m_keyLock.pairOrNot(collision.GetComponent<Key>(), gameObject.GetComponent<Lock>()))
            {
                // success
                AudioSource.PlayClipAtPoint(successSFX, Camera.main.transform.position);
                successFlag.SetActive(true);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                // TODO: failure
                Debug.Log("not pair");
            }
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
                    AudioSource.PlayClipAtPoint(hintSFX, Camera.main.transform.position, 0.1f);

                    // TODO: the related key shakes VFX

                }
            }
        }
    }
}
