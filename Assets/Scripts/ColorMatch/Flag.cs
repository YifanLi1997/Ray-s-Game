using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The same as Lock, rewrite just for practice purpose
public class Flag : MonoBehaviour
{
    [System.Serializable]
    public struct MyClip
    {
        public AudioClip audioClip;
        public float volume;
    }

    [SerializeField] MyClip[] successSFXs;
    [SerializeField] AudioClip failureSFX;
    [SerializeField] AudioClip hintSFX;

    DiamondFlag m_dimdFlag;
    Collider2D m_col;
    Diamond m_pairedDimd;
    Animator m_animator;

    private void OnEnable()
    {
        m_dimdFlag = FindObjectOfType<DiamondFlag>();
        m_col = GetComponent<Collider2D>();
        m_animator = GetComponent<Animator>();

        m_pairedDimd = m_dimdFlag.findPairedDiamond(this);
        m_pairedDimd.gameObject.SetActive(true);
    }

    // This is terrible collision logic
    // NEVER DO THIS AGAIN
    // USER LAYER-BASED COLLISON SYSTEM
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Diamond"))
        {
            if (m_dimdFlag.pairOrNot(collision.GetComponent<Diamond>(), this))
            {
                // success
                AudioSource.PlayClipAtPoint(successSFXs[0].audioClip, Camera.main.transform.position, successSFXs[0].volume);
                AudioSource.PlayClipAtPoint(successSFXs[1].audioClip, Camera.main.transform.position, successSFXs[1].volume);
                m_animator.SetTrigger("isEnpowered");
                Destroy(collision.gameObject);
            }
            else
            {
                // wrong
                AudioSource.PlayClipAtPoint(failureSFX, Camera.main.transform.position, 0.25f);
                collision.gameObject.transform.position = collision.gameObject.GetComponent<Diamond>().GetOriginalPos();
                if (m_pairedDimd)
                { 
                    m_pairedDimd.gameObject.GetComponent<Animator>().SetBool("isShining", true);
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
            collision.gameObject.transform.position = collision.gameObject.GetComponent<Key>().GetOriginalPos();
            if (m_pairedDimd)
            {
                m_pairedDimd.gameObject.GetComponent<Animator>().SetBool("isShining", true);
                Invoke("ResetIsShining", 4.5f);
            }
        }
    }

    private void ResetIsShining()
    {
        if (m_pairedDimd)
        {
            m_pairedDimd.gameObject.GetComponent<Animator>().SetBool("isShining", false);
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
                    if (m_pairedDimd)
                    {
                        AudioSource.PlayClipAtPoint(hintSFX, Camera.main.transform.position, 0.1f);

                        m_pairedDimd.gameObject.GetComponent<Animator>().SetBool("isShining", true);
                        Invoke("ResetIsShining", 4.5f);
                    }
                }
            }
        }
    }
}
