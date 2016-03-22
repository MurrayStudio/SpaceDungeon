using UnityEngine;
using System.Collections;

public class Security : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int CANNON 		= 0;
	private readonly int BEAM		 	= 1;
	private readonly int SELF_DESTRUCT	= 2;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {5, 10}, {6, 12}, {7, 13}, {8, 15}, {9, 16} };
	private readonly int[] LVL_HEALTH = new int[] {16, 21, 26, 31, 36};
	private readonly int[] LVL_DODGE = new int[] {0, 5, 10, 15, 20};
	private readonly int[] LVL_SPEED = new int[] {1, 2, 2, 3, 3};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};

	public Security () : base ()
	{
		int NewLevel = 0;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		BASE_SPEED = LVL_SPEED[NewLevel];
		BASE_DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BASE_ARMOR = 0;

		CRIT_MODS = new int[] {0, 0, 0};
		DMG_MODS = new float[] {0f, -0.50f, 0.5f};
		ACC_MODS = new int[] {85, 85, 85};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 1;
		CAT = SECURITY;
		IS_MECH = true;
		IS_FRIENDLY = false;
		HasPlayed = false;
	}

	public override void SetStats (int NewLevel, int NewRank, int NewHealth)
	{
		NewLevel--;
		this.MAX_HEALTH = this.LVL_HEALTH[NewLevel];
		this.BASE_SPEED = this.LVL_SPEED[NewLevel];
		this.BASE_DODGE = this.LVL_DODGE[NewLevel];
		this.BASE_CRIT = this.LVL_CRIT[NewLevel];
		this.BASE_DMG = new int[] {this.LVL_DMG[NewLevel, 0], this.LVL_DMG[NewLevel, 1]};
		this.BASE_ARMOR = 0;

		this.CurrHealth = NewHealth;
		this.Level = NewLevel;
		this.Rank = NewRank;
	}

	public bool Cannon (Unit Enemy) 		// Stats from rampart
	{
		if (!CheckHit (CANNON, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (CANNON, this)));
		return true;
	}

	public bool Beam (Unit Enemy) 	// Stats from smite
	{
		if (!CheckHit (BEAM, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (BEAM, this)));
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

