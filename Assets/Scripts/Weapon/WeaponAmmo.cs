using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [SerializeField] private int clipSize;
    [SerializeField] private int extraAmmo;

    private int currentAmmo;

    [SerializeField] private AudioClip gunShot;
    [SerializeField] private AudioClip magInSound;
    [SerializeField] private AudioClip magOutSound;
    [SerializeField] private AudioClip releaseSlideSound;
    private AudioSource audioSource;
    
    

    public int CurrentAmmo
    {
        get { return currentAmmo; }
    }

    private void Awake()
    {
        currentAmmo = clipSize;
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }

    public void PlayGunShot()
    {
        audioSource.PlayOneShot(gunShot);
    }

    public void Decrease()
    {
        if(currentAmmo > 0)
            currentAmmo -= clipSize;
    }

    public bool CanReload()
    {
        if (currentAmmo == clipSize) return false; // 총알 한발도 안쏨
        if(extraAmmo == 0) return false;

        return true;
    }

    public void Reload()
    {
        if(extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if(extraAmmo > 0)
        {
            if(extraAmmo + currentAmmo > clipSize) // clipSize : 탄창에 최대 들어갈 크기
            {
                int leftOverAmmo = extraAmmo + currentAmmo - clipSize;
                extraAmmo = leftOverAmmo;
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }


    public void MagOut()
    {
        audioSource.PlayOneShot(magOutSound);
    }
    public void MagIn()
    {
        audioSource.PlayOneShot(magInSound);
    }
    public void ReleaseSlide()
    {
        audioSource.PlayOneShot(releaseSlideSound);
    }
}
