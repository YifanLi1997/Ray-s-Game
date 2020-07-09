using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] AudioClip bubbleBreakSFX;

    Collider2D m_col;
    AudioSource m_audioSource;

    void Start()
    {
        m_col = GetComponent<Collider2D>();
        m_audioSource = FindObjectOfType<Shredder>().GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D m_touchedCollier = Physics2D.OverlapPoint(touchPos);
                if (m_col == m_touchedCollier)
                {
                    m_audioSource.PlayOneShot(bubbleBreakSFX);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
