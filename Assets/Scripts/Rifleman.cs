using UnityEngine;
using System.Collections;

public class Rifleman : Friendly
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int KICK = 0;
	private readonly int HEAVY_SWING = 1;
	private readonly int SLICE = 2;
	private readonly int WAR_CRY = 3;
	private readonly int FIFTH = 4;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {5, 10}, {6, 12}, {7, 13}, {8, 15}, {9, 16}};
	private readonly int[] LVL_HEALTH = new int[] {23, 28, 33, 38, 43};
	private readonly int[] LVL_DODGE = new int[] {10, 15, 20, 25, 30};
	private readonly int[] LVL_SPEED = new int[] {5, 5, 6, 6, 7};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};

	public Rifleman () : base ()
	{
		int NewLevel = 0;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = LVL_DMG[NewLevel];

		CRIT_MODS = new int[] {0, 0, 0, 0};
		DMG_MODS = new float[] {0f, 0f, 0f, 0f};
		ACC_MODS = new int[] {0, 0, 0, 0};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 1;
		Cat = RIFLEMAN;
	}

	public void SetStats (int NewLevel, int NewRank, int NewHealth) 
	{
		NewLevel--;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = LVL_DMG[NewLevel];

		CurrHealth = NewHealth;
		Level = NewLevel;
		Rank = NewRank;
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

	void FIFTHABIL ()
	{

	}
}