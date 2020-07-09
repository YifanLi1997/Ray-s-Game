using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPauser : MonoBehaviour
{
    Collider2D m_col;
    Animator m_ani;

    private void Start()
    {
        m_col = GetComponent<Collider2D>();
        if (!m_col)
        {
            Debug.LogWarning(this.name + "has no collider.");
        }
        m_ani = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchCol = Physics2D.OverlapPoint(touchPos);
                if (m_col == touchCol)
                {
                    if (m_ani.speed <= 0f)
                    {
                        m_ani.speed = 1f;
                    }
                    else
                    {
                        m_ani.speed = 0f;
                    }
                }
            }
        }
        
    }
}
