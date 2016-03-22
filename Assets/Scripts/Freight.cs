using UnityEngine;
using System.Collections;

public class Freight : Enemy
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int SLAM 			= 0;
	private readonly int CHARGE		 	= 1;
	private readonly int SELF_DESTRUCT	= 2;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {5, 10}, {6, 12}, {7, 13}, {8, 15}, {9, 16} };
	private readonly int[] LVL_HEALTH = new int[] {22, 27, 32, 37, 42};
	private readonly int[] LVL_DODGE = new int[] {0, 5, 10, 15, 20};
	private readonly int[] LVL_SPEED = new int[] {1, 2, 2, 3, 3};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};

	public Freight () : base ()
	{
		int NewLevel = 0;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		ARMOR = 0;
		IS_STUNNED = false;

		CRIT_MODS = new int[] {0, 0, 0};
		DMG_MODS = new float[] {0f, -0.20f, 0.5f};
		ACC_MODS = new int[] {75, 85, 85};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 1;
		CAT = FREIGHT;
		IS_MECH = true;
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

	public bool Slam (Unit Enemy) 		// Stats from rampart
	{
		if (!CheckHit (SLAM, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (SLAM, this)));
		return true;
	}

	public bool Charge (Unit Enemy) 	// Stats from smite
	{
		if (!CheckHit (CHARGE, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (CHARGE, this)));
		return true;
	}

	public bool SelfDestruct (Unit Enemy) 	// Stats from smite
	{
		if (!CheckHit (SELF_DESTRUCT, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (SELF_DESTRUCT, this)));
		return true;
	}
}

