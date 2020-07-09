using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    bool m_moveAllowed = false;
    Collider2D m_col;
    Vector2 m_originalpos;
    Camera m_camera;

    [SerializeField] AudioClip touchKey;


    void Start()
    {
        m_col = GetComponent<Collider2D>();
        m_originalpos = transform.position;
        m_camera = FindObjectOfType<Camera>();
    }


    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = m_camera.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {

                Collider2D touchedCollier = Physics2D.OverlapPoint(touchPos);
                if (m_col == touchedCollier)
                {
                    m_moveAllowed = true;
                    AudioSource.PlayClipAtPoint(touchKey, m_camera.transform.position);
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


    public Vector2 GetOriginalPos()
    {
        return m_originalpos;
    }
}
