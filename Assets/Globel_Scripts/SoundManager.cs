using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip buttonClickSound;  // ��ť�����Ч
    public AudioClip itemPickupSound;   // ���߼�ȡ��Ч

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // ��ȡ AudioSource ���
    }

    // ���Ű�ť�����Ч
    public void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound); // ������Ч
        }
    }

    // ���ŵ��߼�ȡ��Ч
    public void PlayItemPickupSound()
    {
        if (audioSource != null && itemPickupSound != null)
        {
            audioSource.PlayOneShot(itemPickupSound); // ������Ч
        }
    }
}

