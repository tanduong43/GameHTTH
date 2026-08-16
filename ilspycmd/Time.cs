using System;

public class Time
{
	public static float timeScale = 1f;
	public static float time = 0f;

	private static long lastTick = 0;

	static Time()
	{
		lastTick = DateTime.Now.Ticks;
	}

	public static void Update()
	{
		long currentTick = DateTime.Now.Ticks;
		float deltaSeconds = (float)((currentTick - lastTick) / 10000) / 1000f;
		lastTick = currentTick;
		time += deltaSeconds * timeScale;
	}
}
