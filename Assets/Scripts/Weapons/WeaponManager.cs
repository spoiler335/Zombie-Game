using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    Police9mm,
    PortableMagnum
}

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    private Weapon[] weapons = { Weapon.Police9mm,Weapon.PortableMagnum};
    private int currentweaponindex = 0;
    void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SwitchToCurrentWeapon();
    }

    void SwitchToCurrentWeapon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        GameObject weapon = transform.Find(weapons[currentweaponindex].ToString()).gameObject;
        weapon.SetActive(true);
        weapon.GetComponent<WeaponsBase>().UpdateTexts();
    }

    void Update()
    {
        CheckWeaponSwitch();
    }

    void CheckWeaponSwitch()
    {
        float mousewheel = Input.GetAxis("Mouse ScrollWheel");
        if (mousewheel>0)
        {
            SelectPreviousWeapon();
        }

        else if(mousewheel<0)
        {
            SelectNextWeapon();
        }
    }

    void SelectPreviousWeapon()
    {
        if(currentweaponindex==0)
        {
            currentweaponindex = weapons.Length - 1;
        }
        else
        {
            currentweaponindex--;
        }
        SwitchToCurrentWeapon();
    }
    void SelectNextWeapon()
    {
        if(currentweaponindex>=(weapons.Length-1))
        {
            currentweaponindex = 0;
        }
        else
        {
            currentweaponindex++;
        }
        SwitchToCurrentWeapon();
    }

}
