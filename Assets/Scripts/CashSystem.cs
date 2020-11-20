using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashSystem : MonoBehaviour
{
    private float m_cash;
    public float initialcash = 0;
    public Text cashtext;
    public float cash
    {
        get
        {
            return m_cash;
        }
        set
        {
            m_cash = value;
            UpdateUI();
        }
    }

    void Start()
    {
        cash = initialcash;
    }

    
    void UpdateUI()
    {
        cashtext.text = "Cash" + m_cash + "$";
    }
}
