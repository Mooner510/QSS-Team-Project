public interface IEntity
{
    public float GetHealth();

    public void SetHealth(float hp);

    public void Damage(float hp);

    public void Heal(float hp);
        
    public float GetSpeed();

    public void SetSpeed(float spd);

    public void AddSpeed(float spd);

    public float GetDefaultSpeed();

    public float GetDamage();

    public void SetDamage(float dmg);

    public bool IsDeath();

    public void Kill();
}