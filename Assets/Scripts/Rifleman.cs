using UnityEngine;
using System.Collections;
using System.Diagnostics.Eventing.Reader;

public class Rifleman : Friendly
{
	private readonly int KICK = 0;
	private readonly int HEAVY_SWING = 1;
	private readonly int SLICE = 2;
	private readonly int WAR_CRY = 3;

	public Rifleman () : base ()
	{
		MAX_HEALTH = 23;
		SPEED = 5;
		DODGE = 10;
		BASE_CRIT = 0.05f;
		BASE_DMG = new int[] {5, 10};
		CRIT = new float[] {0f, 0f, 0f, 0f};
		DMG_MODS = new int[] {0, 0, 0, 0};
		ACC = new int[] {0, 0, 0, 0};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 1;
		Cat = RIFLEMAN;
	}

	void Kick (Enemy e) 		// Stats from rampart
	{
		
	}

	void HeavySwing (Enemy e) 	// Stats from smite
	{

	}

	void Slice (Enemy e)		// Stats from open vein
	{

	}

	void WarCry ()				// Kinda bulwark of faith?
	{
		
	}

	void FIFTH ()
	{

	}
}

