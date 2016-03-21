using UnityEngine;
using System.Collections;
using System.Configuration;

public class Enforcer : Friendly
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int KICK = 0;
	private readonly int HEAVY_SWING = 1;
	private readonly int SLICE = 2;
	private readonly int WAR_CRY = 3;
	private readonly int STEROIDS = 4 ;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {6, 12}, {7, 14}, {8, 16}, {9, 17}, {10, 18}};
	private readonly int[] LVL_HEALTH = new int[] {33, 40, 47, 54, 61};
	private readonly int[] LVL_DODGE = new int[] {5, 10, 15, 20, 25};
	private readonly int[] LVL_SPEED = new int[] {1, 1, 2, 2, 3};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};


	public Enforcer () : base ()
	{
		int NewLevel = 0;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = LVL_DMG[NewLevel];

		CRIT_MODS= new int[] {5, 0, 0, 0};
		DMG_MODS = new float[] {-0.60f, 0.15f, -0.15f, 0f};
		ACC_MODS = new int[] {90, 85, 85, 100};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 0;
		Cat = ENFORCER;
	}

	public void SetStats (int NewLevel, int NewRank, int NewHealth)
	{
		NewLevel--;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = LVL_DMG[NewLevel];

		CRIT_MODS= new int[] {5, 0, 0, 0};
		DMG_MODS = new float[] {-0.60f, 0.15f, -0.15f, 0f};
		ACC_MODS = new int[] {90, 85, 85, 100};

		CurrHealth = NewHealth;
		Level = NewLevel;
		Rank = NewRank;
		Cat = ENFORCER;
	}

	bool Kick (Enemy e) 		// Stats from rampart
	{
		if (!CheckHit (this, e, KICK))
			return false;

		
	}

	bool HeavySwing (Enemy e) 	// Stats from smite
	{
		if (!CheckHit (this, e, KICK))
			return;

		
	}

	bool Slice (Enemy e)		// Stats from open vein
	{
		if (!CheckHit (this, e, KICK))
			return;

		
	}

	void WarCry ()				// Kinda bulwark of faith?
	{
			
	}

	void Steroids ()
	{

	}
}