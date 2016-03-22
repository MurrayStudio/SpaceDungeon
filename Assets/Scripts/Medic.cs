using UnityEngine;
using System.Collections;

public class Medic : Friendly
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int PISTOL 		= 0;
	private readonly int WAVE		 	= 1;
	private readonly int BULWARK		= 2;
	private readonly int RUSH	 		= 3;
	private readonly int TASER	 		= 4;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {4, 9}, {5, 11}, {6, 13}, {6, 14}, {7, 16}};
	private readonly int[] LVL_HEALTH = new int[] {24, 29, 34, 39, 44};
	private readonly int[] LVL_DODGE = new int[] {0, 5, 10, 15, 20};
	private readonly int[] LVL_SPEED = new int[] {4, 4, 5, 5, 6};
	private readonly int[] LVL_CRIT = new int[] {2, 3, 3, 4, 4};

	public Medic () : base ()
	{
		int NewLevel = 0;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		ARMOR = 0;
		IS_STUNNED = false;

		CRIT_MODS = new int[] {5, 0, 0, 0, 0};
		DMG_MODS = new float[] {0f, 0f, 0f, 0f, -0.5f};
		ACC_MODS = new int[] {85, 0, 0, 0, 85};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 3;
		CAT = MEDIC;
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

	public void Pistol ()				// Kinda bulwark of faith?
	{
		
	}

	public void Wave (Enemy e) 		// Stats from rampart
	{
		
	}

	public void Bulwark (Enemy e)		// Stats from open vein
	{

	}

	public void Rush (Enemy e) 	// Stats from smite
	{

	}

	public void Taser ()
	{

	}
}