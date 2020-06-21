using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] Bubble[] bubbles;
    [SerializeField] float bubbleForceMin;
    [SerializeField] float bubbleForceMax;

    [Tooltip("In seconds")]
    [SerializeField] float spawnGap = 1f;

    private float m_spawnRange = 8f;
    private Vector3 m_spawnPos;
    private float m_count;

    void Start()
    {
        m_spawnPos = transform.position;
        m_count = 0;
    }

    void Update()
    {
        if (m_count <= 0f)
        {
            m_spawnPos.x = Random.Range(
                transform.position.x - m_spawnRange,
                transform.position.x + m_spawnRange
                );
            GameObject bubble = Instantiate(bubbles[Random.Range(0, bubbles.Length)].gameObject, m_spawnPos, Quaternion.identity) as GameObject;
            bubble.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, UnityEngine.Random.Range(bubbleForceMin, bubbleForceMax));
            m_count = spawnGap;
        }
        else
        {
            m_count -= Time.deltaTime;
        }

    }
}
