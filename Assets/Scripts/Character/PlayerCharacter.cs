using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour, IDamageable
{
    public event UnityAction OnDeathEvent;
    public event UnityAction<float> OnDamageTakenEvent;

    [SerializeField]
    private float maxHP = 100.0f;
    public float MaxHP
    {
        get { return maxHP; }
    }

    private float currentHP = 0.0f;
    public float CurrentHP
    {
        get { return currentHP; }
        private set
        {
            currentHP = value;
            if (currentHP < 0.0f)
            {
                currentHP = 0.0f;
                if (OnDeathEvent != null)
                    OnDeathEvent();
            }
        }
    }

    protected void OnEnable()
    {
        CurrentHP = MaxHP;
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;
        if (OnDamageTakenEvent != null)
            OnDamageTakenEvent(CurrentHP);
    }
}
