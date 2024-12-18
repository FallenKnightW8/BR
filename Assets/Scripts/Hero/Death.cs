using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    private HerMovement ScriptHeroMovment;
    private Dash ScriptDashes;
    private PlayerHealthSystem ScriptPlayerHealthSystem;
    private PlayerCombat ScriptPlayerCombat;
    private Rigidbody2D Rigidbody;

    private Animator Animators;

    // Start is called before the first frame update
    void Awake()
    {
        ScriptHeroMovment = GetComponent<HerMovement>();
        ScriptDashes = GetComponent<Dash>();
        ScriptPlayerHealthSystem = GetComponent<PlayerHealthSystem>();
        ScriptPlayerCombat = GetComponent<PlayerCombat>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CallDeath();
    }

    private void CallDeath()
    {
        if (ScriptPlayerHealthSystem.GetHealth() <= 0 )
        {
            ScriptHeroMovment.enabled = false;
            ScriptDashes.enabled = false;
            ScriptPlayerHealthSystem.enabled = false;
            ScriptPlayerCombat.enabled = false;

            Rigidbody.simulated = false;
            ScriptHeroMovment.SetDirection(new Vector2(0, 0));
            ScriptHeroMovment.SetActivityMoveSpeed(0);

            transform.position = transform.position;
        }
    }
}
