using UnityEngine;
using System.Collections;

public class Rifleman : Friendly
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int RIFLE 			= 0;
	private readonly int BAYONET	 	= 1;
	private readonly int SHOTGUN		= 2;
	private readonly int NET_GUN 		= 3;
	private readonly int RELOAD 		= 4;

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
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		ARMOR = 0;
		IS_STUNNED = false;

		CRIT_MODS = new int[] {10, 0, 0, 0, 0};
		DMG_MODS = new float[] {0f, 0f, -0.4f, -0.8f, 0f};
		ACC_MODS = new int[] {85, 85, 85, 85, 0};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 1;
		CAT = RIFLEMAN;
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

	public bool Rifle (Unit Enemy) 		// Stats from rampart
	{
		if (!CheckHit (RIFLE, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (RIFLE, this)));
		return true;
	}

	public bool Bayonet (Unit Enemy) 	// Stats from smite
	{
		if (!CheckHit (BAYONET, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (BAYONET, this)));
		return true;
	}

	public bool Shotgun (Unit Enemy)		// Stats from open vein
	{
		if (!CheckHit (SHOTGUN, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (SHOTGUN, this)));
		return true;
	}

	public bool NetGun (Unit Enemy)
	{
		if (!CheckHit (SHOTGUN, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (SHOTGUN, this)));
		return true;
	}

	public void Reload ()				// Kinda bulwark of faith?
	{
			
	}
}