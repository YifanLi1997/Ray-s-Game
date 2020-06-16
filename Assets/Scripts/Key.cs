using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    bool m_moveAllowed = false;
    Collider2D m_col;

    [SerializeField] AudioClip touchKey;


    void Start()
    {
        m_col = GetComponent<Collider2D>();
    }


    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {

                Collider2D touchedCollier = Physics2D.OverlapPoint(touchPos);
                if (m_col == touchedCollier)
                {
                    m_moveAllowed = true;
                    AudioSource.PlayClipAtPoint(touchKey, Camera.main.transform.position);
                }
            }

            if(touch.phase == TouchPhase.Moved)
            {

                if (m_moveAllowed)
                {

                    transform.position = touchPos;
                }

            }

            if(touch.phase == TouchPhase.Ended)
            {
                m_moveAllowed = false;
            }
        }
        
    }
}
