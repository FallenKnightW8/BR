using UnityEngine;
using UnityEngine.UI;
public class HellsBar : MonoBehaviour
{
    [SerializeField] private int MaxHealth; // максимальное возможное хп на определённом уровне прокачки
    [SerializeField] private int Health; // нынешнее количество хп у игрока
    [SerializeField] private Image[] MaxOfGameHP; // максимальное количество хп за всю игру
    [SerializeField] private Sprite Live; // здесь рисунок серца или чего нибудь ещё
    [SerializeField] private Sprite HalfHeart; // рисунок половины сердца
    [SerializeField] private Sprite NoLive; // тут пустая версия


    void Start()
    {
        Health = MaxHealth;
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHealth();
    }

    private void ChangeHealth()
    {
        if (Health > MaxHealth) Health = MaxHealth;
        for (int i = 0; i < MaxOfGameHP.Length; i++)
        {
            if (Health>i && Health % 2 ==0)
                MaxOfGameHP[i].sprite = Live;
            else if (Health/2 +1 > i   && Health % 2 !=0)
            {
                MaxOfGameHP[i].sprite = Live;
                if ((i * 2) == Health * 2)
                {
                    MaxOfGameHP[i].sprite = HalfHeart;
                }

            }
            else if (Health < i)
                MaxOfGameHP[i].sprite = NoLive;

            if(i < MaxHealth)
                MaxOfGameHP[i].enabled = true;
            else
                MaxOfGameHP[i].enabled = false;
        }
    }
}
