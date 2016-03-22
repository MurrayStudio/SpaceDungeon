using UnityEngine;
using System.Collections;

public class Infected : Enemy
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int CLAW 			= 0;
	private readonly int ACID_SPIT	 	= 1;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {4, 9}, {5, 11}, {6, 12}, {7, 14}, {8, 15}};
	private readonly int[] LVL_HEALTH = new int[] {15, 20, 25, 30, 35};
	private readonly int[] LVL_DODGE = new int[] {10, 15, 20, 25, 30};
	private readonly int[] LVL_SPEED = new int[] {3, 3, 4, 4, 5};
	private readonly int[] LVL_CRIT = new int[] {5, 6, 6, 7, 7};

	public Infected () : base ()
	{
		int NewLevel = 0;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		SPEED = LVL_SPEED[NewLevel];
		DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		ARMOR = 0;
		IS_STUNNED = false;

		CRIT_MODS = new int[] {0, 0};
		DMG_MODS = new float[] {0f, -0.20f};
		ACC_MODS = new int[] {85, 85};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 1;
		CAT = INFECTED;
		IS_MECH = false;
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

	public bool Claw (Unit Enemy) 		// Stats from rampart
	{
		if (!CheckHit (CLAW, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (CLAW, this)));
		return true;
	}

	public bool AcidSpit (Unit Enemy) 	// Stats from smite
	{
		if (!CheckHit (ACID_SPIT, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (ACID_SPIT, this)));
		return true;
	}
}

