namespace SpaceShooter
{
    public interface IDamageProvider
    {
        /// <summary>
        /// Gets the amount of damage caused by the object.
        /// </summary>
        /// <returns>the amount of damage caused by the object</returns>
        int GetDamage();
    }
}
