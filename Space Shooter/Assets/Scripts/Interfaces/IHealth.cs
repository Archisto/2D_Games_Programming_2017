namespace SpaceShooter
{
	public interface IHealth
	{
		int CurrentHealth { get; }
        bool IsDead { get; }
        void IncreaseHealth( int amount );
		void DecreaseHealth( int amount );
        void SetInvincible( bool isImmortal );
	}
}
