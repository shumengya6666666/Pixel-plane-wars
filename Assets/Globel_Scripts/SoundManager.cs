using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip buttonClickSound;  // 按钮点击音效
    public AudioClip itemPickupSound;   // 道具捡取音效

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // 获取 AudioSource 组件
    }

    // 播放按钮点击音效
    public void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound); // 播放音效
        }
    }

    // 播放道具捡取音效
    public void PlayItemPickupSound()
    {
        if (audioSource != null && itemPickupSound != null)
        {
            audioSource.PlayOneShot(itemPickupSound); // 播放音效
        }
    }
}

