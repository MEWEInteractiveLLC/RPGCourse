using UnityEngine;

namespace RPG.Combat
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float health = 100.0f;


        public void AdjustHealth(float amount)
        {
            if (Mathf.Abs(amount) > 0.0f)
            {
                health += amount;
                health = Mathf.Clamp(health, 0, 100);
                print(health);

            }
        }

    }
}