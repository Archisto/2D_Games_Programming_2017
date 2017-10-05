namespace SpaceShooter
{
    public interface IDamageReceiver
    {
        /// <summary>
        /// Inflicts damage to the object.
        /// </summary>
        /// <param name="amount">the amount of damage</param>
        void TakeDamage( int amount );

        /// <summary>
        /// Restores health to the object.
        /// </summary>
        /// <param name="amount">the amount of health</param>
        void RestoreHealth( int amount );
    }
}
