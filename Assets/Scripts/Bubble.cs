using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    Collider2D m_col;
    Collider2D m_touchedCollier;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Debug.Log(touchPos);

            if (touch.phase == TouchPhase.Began)
            {

                m_touchedCollier = Physics2D.OverlapPoint(touchPos);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (m_col == m_touchedCollier)
                {
                    // Explosion
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
