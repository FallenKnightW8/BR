using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    [SerializeField] private PlayerHealthSystem phs;
    [SerializeField] private BoxCollider2D Collider;
    [SerializeField] private Image IconSpell;
    [SerializeField] private GameObject Shield;

    [SerializeField] private float ColldownShieldMax;
    [SerializeField] private float ColldownShield;
    [SerializeField] private float TimeShieldMax;
    [SerializeField] private float TimeShield;
    [SerializeField] private int Damage = 2;

    private void Awake()
    {
        Collider = this.GetComponent<BoxCollider2D>();
        phs = this.GetComponent<PlayerHealthSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeShield = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SpellShield();
    }

    private void SpellShield()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ColldownShield <= 0)
            {
                phs.SendMessage("GetDamage", Damage);
                Collider.enabled = false;
                TimeShield = TimeShieldMax;
                ColldownShield = ColldownShieldMax + TimeShieldMax;

                Shield.SetActive(true);
            }
        }

        if (TimeShield >= 0)
        {
            TimeShield -= Time.deltaTime;
            IconSpell.fillAmount = 0;
        }

        if (TimeShield <= 0)
        {
            Collider.enabled = true;
            Shield.SetActive(false);
        }

        if (ColldownShield > 0)
        {
            ColldownShield -= Time.deltaTime;
            IconSpell.fillAmount -= 1 / (ColldownShield * -1f)* Time.deltaTime;
        }
        if (ColldownShield <= 0)
        {
            IconSpell.fillAmount = 1;
        }
    }
}
