using System;

namespace Enemy
{
    public class BossAttack
    {
        private readonly float _delay;
        public readonly Action Run;
        
        public BossAttack(float delay, Action run)
        {
            _delay = delay;
            Run = run;
        }

        public float GetAttackDelay() => _delay;
    }
}