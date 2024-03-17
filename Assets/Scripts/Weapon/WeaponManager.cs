using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] bool semiAuto;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrelPos;
    [SerializeField] private float bulletVelocitiy;
    [SerializeField] private int bulletPerShot;

    private AimStateManager aim;
    private WeaponAmmo  ammo;


    private float fireRateTimer;

    private void Awake()
    {
        fireRateTimer = fireRate;
        aim = GetComponentInParent<AimStateManager>();
        ammo = GetComponent<WeaponAmmo>();

    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private bool ReadyToFire()
    {
        fireRateTimer += Time.deltaTime; // 계속 증가를 시켜줌
        if(fireRateTimer < fireRate) return false; // 발사중지
        if (ammo.CurrentAmmo == 0) return false;
        if(semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if(!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;

        return false;
    }

    private void Fire() 
    {
        if (!ReadyToFire()) // 체크
            return;

        fireRateTimer = 0f;
        barrelPos.LookAt(aim.AimPos); // 어디서 총알 생성 하냐
        // barrelPos.localEulerAngles
        ammo.PlayGunShot();
        ammo.Decrease();
        for(int i = 0; i < bulletPerShot; i++)
        {
            // 총알 생성 후 발사
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward*bulletVelocitiy, ForceMode.Impulse);
        }

    }    

}
