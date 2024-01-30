using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealthNew;
    [SerializeField] private int MaxHealthOld; // MaxValue in slider
    [SerializeField] private int Health; // Value in slider
    [SerializeField] private Slider mySlider;
    [SerializeField] private RectTransform NoLiveImage;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealthOld;
        mySlider.maxValue = MaxHealthOld;

        NoLiveImage.offsetMax += new Vector2(MaxHealthOld / 2 * 100, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHealth();
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
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
