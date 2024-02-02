using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealthNew;
    [SerializeField] private int MaxHealthOld;
    [SerializeField] private int Health;
    [SerializeField] private Slider mySlider;
    [SerializeField] private RectTransform NoLiveImage;

    public void GetDamage(int damage)
    {
        Health -= damage;
    }

    public void SetHealth(int heal)
    {
        Health += heal
    }

    public void SetHeart(int heart)
    {
        MaxHealthNew += heart;
    }

    private void Start()
    {
        Health = MaxHealthOld;
        mySlider.maxValue = MaxHealthOld;
        MaxHealthNew = MaxHealthOld;

        NoLiveImage.offsetMax += new Vector2(MaxHealthOld / 2 * 100, 0);
    }

    private void Update()
    {
        ChangeHealth();
    }

    private void ChangeHealth()
    {
        if (MaxHealthOld < MaxHealthNew)
        {
            NoLiveImage.offsetMax = new Vector2((MaxHealthNew / 2 * 100 + 30), -30);
            mySlider.maxValue = MaxHealthNew;
            Health += MaxHealthNew - MaxHealthOld;
            MaxHealthOld = MaxHealthNew;
        }
        if (Health > MaxHealthOld)
        {
            Health = MaxHealthOld;
        }
        mySlider.value = Health;
    }
}
