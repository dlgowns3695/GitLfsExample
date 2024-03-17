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
        fireRateTimer += Time.deltaTime; // ��� ������ ������
        if(fireRateTimer < fireRate) return false; // �߻�����
        if (ammo.CurrentAmmo == 0) return false;
        if(semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if(!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;

        return false;
    }

    private void Fire() 
    {
        if (!ReadyToFire()) // üũ
            return;

        fireRateTimer = 0f;
        barrelPos.LookAt(aim.AimPos); // ��� �Ѿ� ���� �ϳ�
        // barrelPos.localEulerAngles
        ammo.PlayGunShot();
        ammo.Decrease();
        for(int i = 0; i < bulletPerShot; i++)
        {
            // �Ѿ� ���� �� �߻�
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward*bulletVelocitiy, ForceMode.Impulse);
        }

    }    

}
