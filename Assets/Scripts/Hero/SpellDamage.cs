using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellDamage : MonoBehaviour
{
    [SerializeField] private PlayerHealthSystem phs;
    [SerializeField] private PlayerCombat pc;

    [SerializeField] private Image IconSpell;
    [SerializeField] private GameObject Sword;

    [SerializeField] private float ColldownBaffMax;
    [SerializeField] private float ColldownBaff;
    [SerializeField] private float TimeBaffMax;
    [SerializeField] private float TimeBaff;
    [SerializeField] private int Damage = 2;


    [SerializeField] private int OldDamageAttack;
    [SerializeField] private int BaffDamageAttack;

    private void Awake()
    {
        phs = this.GetComponent<PlayerHealthSystem>();
        pc = this.GetComponent<PlayerCombat>();
        OldDamageAttack = pc.getDamage();
        BaffDamageAttack = pc.getDamage();
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeBaff = 0;
    }

    // Update is called once per frame
    void Update()
    {
        BaffDamage();
    }

    private void BaffDamage()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (ColldownBaff <= 0)
            {
                phs.SendMessage("GetDamage", Damage);
                TimeBaff = TimeBaffMax;
                ColldownBaff = ColldownBaffMax + TimeBaffMax;

                pc.setDamage(BaffDamageAttack);

                Sword.SetActive(true);
            }
        }

        if (TimeBaff >= 0)
        {
            TimeBaff -= Time.deltaTime;
            IconSpell.fillAmount = 0;
        }

        if (TimeBaff <= 0)
        {
            Sword.SetActive(false);
            pc.setDamage(OldDamageAttack);
        }

        if (ColldownBaff > 0)
        {
            ColldownBaff -= Time.deltaTime;
            IconSpell.fillAmount -= 1 / (ColldownBaff * -1f) * Time.deltaTime;
        }
        if (ColldownBaff <= 0)
        {
            IconSpell.fillAmount = 1;
        }
    }
}
