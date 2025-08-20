using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmThanh : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource MusicSource;
    public AudioSource VfxSource;

    public AudioClip MusicClip;
    public AudioClip VfxClip;

    public AudioClip WaterClip;  // Âm thanh nước
    public AudioClip GroundClip; // Âm thanh đất
    public AudioClip SandClip;   // Âm thanh cát
    public AudioClip BrickClip;  // Âm thanh gạch

    public AudioClip JumpClip;   // Âm thanh nhảy
    public AudioClip AttackClip; // Âm thanh tấn công
    public AudioClip HurtClip; // Âm thanh khi bị thương
    public AudioClip HealClip; // Âm thanh khi ăn máu
    public AudioClip DeathClip; // Âm thanh khi chết
    public AudioClip XuongClip; // Âm thanh khi ăn xương
    public AudioClip KimCuongClip; // Âm thanh khi ăn kim cương
    private string lastPlayedSound = ""; // Âm thanh đã phát gần nhất

    void Start()
    {
        MusicSource.clip = MusicClip;
        MusicSource.loop = true;
        MusicSource.Play();
    }

    void Update()
    {
        
    }
    // Hàm phát âm thanh dựa trên layer
    public void PlaySoundByLayer(int layer)
    {
        AudioClip clipToPlay = null;

        switch (LayerMask.LayerToName(layer))
        {
            case "Water_1":
                clipToPlay = WaterClip;
                break;
            case "Groud":
                clipToPlay = GroundClip;
                break;
            case "Sand":
                clipToPlay = SandClip;
                break;
            case "Brick":
                clipToPlay = BrickClip;
                break;
        }

        if (clipToPlay != null && clipToPlay.name != lastPlayedSound)
        {
            VfxSource.clip = clipToPlay;
            VfxSource.time = 0;      // Bắt đầu phát lại từ đầu
            VfxSource.loop = true;   // Bật chế độ lặp
            VfxSource.Play();
            lastPlayedSound = clipToPlay.name;
        }
    }

    // Hàm phát âm thanh nhảy
    public void PlayJumpSound()
    {
        if (JumpClip != null && VfxSource != null)
        {
            VfxSource.PlayOneShot(JumpClip);
            Debug.Log("Jump sound triggered");
        }
    }

    // Hàm phát âm thanh tấn công
    public void PlayAttackSound()
    {
        if (AttackClip != null && VfxSource != null)
        {
            VfxSource.PlayOneShot(AttackClip);
            Debug.Log("Attack sound triggered");
        }
    }
    // Hàm phát âm thanh bị thương
    public void PlayHurtSound()
    {
        if (HurtClip != null && VfxSource != null)
        {
            VfxSource.PlayOneShot(HurtClip);
            Debug.Log("Hurt sound triggered");
        }
    }

    // Hàm phát âm thanh ăn máu
    public void PlayHealSound()
    {
        if (HealClip != null && VfxSource != null)
        {
            VfxSource.PlayOneShot(HealClip);
            Debug.Log("Heal sound triggered");
        }
    }

    // Hàm phát âm thanh chết
    public void PlayDeathSound()
    {
        if (DeathClip != null && VfxSource != null)
        {
            VfxSource.PlayOneShot(DeathClip);
            Debug.Log("Death sound triggered");
        }
    }
    public void PlayXuongSound()
    {
        if (XuongClip != null && VfxSource != null)
        {
            VfxSource.PlayOneShot(XuongClip);
            Debug.Log("Xuong sound triggered");
        }
    }
    public void PlayKimCuongSound()
    {
        if (KimCuongClip != null && VfxSource != null)
        {
            VfxSource.PlayOneShot(KimCuongClip);
            Debug.Log("KimCuong sound triggered");
        }
    }
    public void StopSound()
    {
        // Dừng âm thanh nếu nó đang phát
        if (VfxSource.isPlaying)
        {
            VfxSource.Stop();
            VfxSource.loop = false; // Tắt chế độ lặp để sẵn sàng phát âm thanh mới
            lastPlayedSound = "";   // Reset âm thanh đã phát
        }
    }
}
