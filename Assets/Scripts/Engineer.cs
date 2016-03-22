using UnityEngine;
using System.Collections;

public class Engineer : Friendly
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int RATCHET		= 0;
	private readonly int ION		 	= 1;
	private readonly int FLASHBANG		= 2;
	private readonly int SNARE	 		= 3;
	private readonly int LIGHT_WALL		= 4;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {4, 7}, {5, 8}, {6, 10}, {6, 11}, {7, 13} };
	private readonly int[] LVL_HEALTH = new int[] {19, 23, 27, 31, 35};
	private readonly int[] LVL_DODGE = new int[] {10, 15, 20, 25, 30};
	private readonly int[] LVL_SPEED = new int[] {6, 6, 7, 7, 8};
	private readonly int[] LVL_CRIT = new int[] {7, 8, 8, 9, 9};

	public Engineer () : base ()
	{
		int NewLevel = 0;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		ARMOR = 0;
		IS_STUNNED = false;

		CRIT_MODS = new int[] {3, 0, 0, 0, 0};
		DMG_MODS = new float[] {0f, -0.30f, -0.80f, -0.50f};
		ACC_MODS = new int[] {85, 85, 85, 85, 0};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 4;
		CAT = ENGINEER;
		IS_MECH = false;
		XP = 0;
	}

	public void SetStats (int NewLevel, int NewRank, int NewHealth) 
	{
		NewLevel--;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		ARMOR = 0;
		IS_STUNNED = false;

		CurrHealth = NewHealth;
		Level = NewLevel;
		Rank = NewRank;
	}

	public void Ratchet (Enemy e) 		// Stats from rampart
	{
		
	}

	public void Ion (Enemy e) 	// Stats from smite
	{

	}

	public void Flashbang (Enemy e)		// Stats from open vein
	{

	}

	public void Snare ()				// Kinda bulwark of faith?
	{
		
	}

	public void LightWall ()
	{

	}
}