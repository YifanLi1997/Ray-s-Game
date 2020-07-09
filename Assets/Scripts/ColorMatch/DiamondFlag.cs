using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Very similar to KeyLock, this is for the last pair of objects before the win panel
public class DiamondFlag : MonoBehaviour
{

    [SerializeField] List<Diamond> diamonds;
    [SerializeField] List<Flag> flags;

    [SerializeField] BubbleSpawner bubbleSpawner;
    [SerializeField] GameObject restartPanel;

    [SerializeField] AudioClip winClip;

    private int m_count = 4;
    private GameEnvir m_gameEnvir;
    private AudioSource m_audioSource;

    // test config - backdoor
    public bool winOrNot = false;

    private void Start()
    {
        m_count = diamonds.Count;
        m_gameEnvir = FindObjectOfType<GameEnvir>();
        m_audioSource = m_gameEnvir.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckWinCondition();
    }

    public Diamond findPairedDiamond(Flag m_flag)
    {
        int flagIndex = flags.IndexOf(m_flag);
        return diamonds[flagIndex];
    }

    public Flag findPairedFlag(Diamond m_dimd)
    {
        int dimdIndex = diamonds.IndexOf(m_dimd);
        return flags[dimdIndex];
    }

    public bool pairOrNot(Diamond m_dimd, Flag m_flag)
    {

        int flagIndex = flags.IndexOf(m_flag);
        int dimdIndex = diamonds.IndexOf(m_dimd);

        if (flagIndex != dimdIndex)
        {
            return false;
        }
        else
        {
            m_count--;
            return true;
        }
    }

    private void CheckWinCondition()
    {
        if (m_count <= 0 || winOrNot) // winOrNot is a backdoor
        {
            ShowWinEffect();
        }
    }

    private void ShowWinEffect()
    {
        //SFX
        m_audioSource.clip = winClip;
        if (!m_audioSource.isPlaying)
        {
            m_audioSource.Play();
        }

        // VFX
        bubbleSpawner.gameObject.SetActive(true);
        restartPanel.SetActive(true);
    }
}
