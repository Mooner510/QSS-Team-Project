namespace LivingEntity
{
    public interface IEntity
    {
        public float GetMaxHealth();

        public void SetMaxHealth(float hp);

        public void AddMaxHealth(float hp);
        
        public float GetHealth();

        public void SetHealth(float hp);

        public void Damage(float hp);

        public void Heal(float hp);

        public float GetDamage();

        public void SetDamage(float dmg);
        
        public float GetSpeed();

        public void SetSpeed(float spd);

        public void AddSpeed(float spd);
        
        public void ResetSpeed();

        public float GetDefaultSpeed();

        public bool IsDeath();

        public void Kill();
    }
}