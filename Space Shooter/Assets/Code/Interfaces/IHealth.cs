namespace SpaceShooter
{
	public interface IHealth
	{
		int CurrentHealth { get; }
		void IncreaseHealth( int amount );
		void DecreaseHealth( int amount );
	}
}
