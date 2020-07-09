using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the same as key, rewrite it just for practice purpose
public class Diamond : MonoBehaviour
{
    bool m_allowedToMove = false;
    Collider2D m_col;
    Vector2 m_originalPos;
    Camera m_camera;

    [SerializeField] AudioClip touchSFX;


    // Start is called before the first frame update
    void Start()
    {
        m_col = GetComponent<Collider2D>();
        m_originalPos = transform.position;
        m_camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = m_camera.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);

                if (touchedCollider == m_col)
                {
                    m_allowedToMove = true;
                    AudioSource.PlayClipAtPoint(touchSFX, m_camera.transform.position);
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (m_allowedToMove)
                {
                    transform.position = touchPos;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                m_allowedToMove = false;
            }
        }
        
    }

    public Vector2 GetOriginalPos()
    {
        return m_originalPos;
    }
}
