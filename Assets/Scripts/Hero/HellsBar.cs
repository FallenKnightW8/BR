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
            if (i < Health && Health % 2 == 0)
                MaxOfGameHP[i].sprite = Live;
            else if (i < Health)
                MaxOfGameHP[i].sprite = HalfHeart;
            else
                MaxOfGameHP[i].sprite = NoLive;


            if(i< MaxHealth)
                MaxOfGameHP[i].enabled = true;
            else
                MaxOfGameHP[i].enabled = false;
        }
    }
}
