using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public enum Firemode
{
    Semiauto,
    Fullauto
}
public class WeaponsBase : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource audioSource;
    protected FirstPersonController controller;
    private bool FireLock = false;
    private bool canshoot = false;
    protected CashSystem cashSystem;
    protected bool IsReloading = false;
    [Header("Object References")]
    public ParticleSystem muzzelflash;
    public GameObject sparkPrefab;
    public GameObject ReloadText;

    [Header("UI References")]
    public Text weaponnametext;
    public Text ammotext;

    public Transform shootpoint;

    [Header("Sound References")]
    public AudioClip fireSound;
    public AudioClip dryfire;
    public AudioClip magoutsound;
    public AudioClip maginsound;
    public AudioClip boltsound;
    public AudioClip drawsound;

    [Header("Weapon Attributes")]
    public Firemode firemode = Firemode.Fullauto;

    public float damage = 20f;
    public float firerate = 1f;
    public int bulletsinclip;
    public int clipsize=12;
    public int bulletsleft;
    public int maxammo=100;
    public float spread = 0.7f;
    public float recoil=0.5f;
    
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        controller = player.GetComponent<FirstPersonController>();
        animator = GetComponent<Animator>();
        cashSystem =player.GetComponent<CashSystem>();
        audioSource = GetComponent<AudioSource>();
        bulletsinclip = clipsize;
        bulletsleft = maxammo;
        UpdateTexts();
    }

    void EnableWeapon()
    {
        canshoot = true;
    }
    
    void Update()
    {
        if (firemode == Firemode.Fullauto && Input.GetButton("Fire1"))
        {
            checkfire();
        }
        else if(firemode==Firemode.Semiauto && Input.GetButtonDown("Fire1"))
        {
            checkfire();
        }
        if(Input.GetButtonDown("Reload"))
        {
            checkReload();
        }
    }

    void checkfire()
    {
        if (canshoot) return;
        if (IsReloading) return;
        if (FireLock) return;
        if (bulletsinclip > 0)
        {
            fire();
        }
        else
        {
            ReloadText.SetActive(true);
            DryFire();
        }
    }

    void fire()
    {
        audioSource.PlayOneShot(fireSound);
        FireLock = true;
        DetectHit();
        Recoil();
        muzzelflash.Stop();
        muzzelflash.Play();
        PlayFireAnimation();
        bulletsinclip--;
        UpdateTexts();
        StartCoroutine(CoResetFireLock());
    }

    void Recoil()
    {

    }

    void DetectHit()
    {
        RaycastHit hit;

        if(Physics.Raycast(shootpoint.position,CalculateSpread(spread,shootpoint),out hit))
        {
            if(hit.transform.CompareTag("Enemy"))
            {
                Health targethealth = hit.transform.GetComponent<Health>();

                if(targethealth==null)
                {
                    throw new System.Exception("cannot find health Component");
                }
                else
                {
                    targethealth.TakeDamage(damage);
                    if (targethealth.value <= 0)
                    {
                        KillReward killReward = hit.transform.GetComponent<KillReward>();
                        if(killReward==null)
                        {
                            throw new System.Exception("cannot find cash Component");
                        }
                        cashSystem.cash += killReward.amount;
                    }
                }
            }
            else
            {
                GameObject spark = Instantiate(sparkPrefab, hit.point, hit.transform.rotation);
                Destroy(spark, 1f);
            }
            
        }
    }


    public virtual void PlayFireAnimation()
    {
        animator.CrossFadeInFixedTime("Fire", 0.1f);
    }

    void DryFire()
    {
        audioSource.PlayOneShot(dryfire);
        FireLock = true;
        
        StartCoroutine(CoResetFireLock());
    }

    IEnumerator CoResetFireLock()
    {
        yield return new WaitForSeconds(firerate);
        FireLock = false;
    }

    void checkReload()
    {
        if(bulletsleft>0 && bulletsinclip<clipsize)
        {
            Reload();
        }
    }

    void Reload()
    {
        if (IsReloading) return;
        IsReloading = true;
        animator.CrossFadeInFixedTime("Reload", 0.1f);
        ReloadText.SetActive(false);
    }

    void ReloadAmmo()
    {
        int BulletsToload = clipsize - bulletsinclip;
        int BulletsToSub = (bulletsleft >= BulletsToload) ? BulletsToload : bulletsleft;
        bulletsleft -= BulletsToSub;
        bulletsinclip += BulletsToload;
        UpdateTexts();
    }

    public void OnDraw()
    {
        audioSource.PlayOneShot(drawsound);
    }

    public void OnMagOut()
    {
        audioSource.PlayOneShot(magoutsound);
    }

    public void OnMagIn()
    {
        ReloadAmmo();
        audioSource.PlayOneShot(maginsound);
    }

    public void OnBoltForwarded()
    {
        audioSource.PlayOneShot(boltsound);
        Invoke("ResetIsReloading", 1f);
    }

    void ResetIsReloading()
    {
        IsReloading = false;
    }

    public void UpdateTexts()
    {
        weaponnametext.text = GetWeaponName();
        ammotext.text = "Ammo: " + bulletsinclip + "/ " + bulletsleft;
    }

    string GetWeaponName()
    {
        string Weaponname = "";
        if(this is Police9mm)
        { Weaponname = "Police 9mm"; }
        else if(this is PortableMagnum)
        { Weaponname = "Poratble Magnum"; }
        else
        { throw new System.Exception("Unknown Weapon"); }
        return Weaponname;
    }

    Vector3 CalculateSpread(float spread,Transform shootpoint)
    {
        return Vector3.Lerp(shootpoint.TransformDirection(Vector3.forward * 100), Random.onUnitSphere, spread);
    }
}
