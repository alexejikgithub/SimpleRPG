namespace SimpleRPG.Data
{
	public class PlayerProgress
	{
		public WorldData WorldData;

		public PlayerProgress(string initialLevel)
		{
			WorldData = new WorldData(initialLevel);	
		}
	}
}
