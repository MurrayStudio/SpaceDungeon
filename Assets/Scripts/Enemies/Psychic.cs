using UnityEngine;
using System.Collections;

public class Psychic : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int DRAIN 			= 0;
	private readonly int MIND_WIPE	 	= 1;
	private readonly int CRIPPLE	 	= 2;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {4, 9}, {5, 11}, {6, 12}, {7, 14}, {8, 15} };
	private readonly int[] LVL_HEALTH = new int[] {15, 20, 25, 30, 35};
	private readonly int[] LVL_DODGE = new int[] {10, 15, 20, 25, 30};
	private readonly int[] LVL_SPEED = new int[] {3, 3, 4, 4, 5};
	private readonly int[] LVL_CRIT = new int[] {5, 6, 6, 7, 7};

	public Psychic () : base ()
	{
		int NewLevel = 0;
		MAX_HEALTH = LVL_HEALTH[NewLevel];
		BASE_SPEED = LVL_SPEED[NewLevel];
		BASE_DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BASE_ARMOR = 0;

		CRIT_MODS = new int[] {0, 0, 0};
		DMG_MODS = new float[] {0f, -0.50f, -0.50f};
		ACC_MODS = new int[] {85, 85, 85};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 1;
		CAT = PSYCHIC;
		IS_MECH = false;
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

	public bool Drain (Unit Enemy) 		// Stats from rampart
	{
		if (!CheckHit (DRAIN, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (DRAIN, this)));
		return true;
	}

	public bool MindWipe (Unit Enemy) 	// Stats from smite
	{
		if (!CheckHit (MIND_WIPE, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (MIND_WIPE, this)));
		return true;
	}

	public bool Cripple (Unit Enemy) 	// Stats from smite
	{
		if (!CheckHit (CRIPPLE, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (BASE_DMG[0], BASE_DMG[1], CheckCrit (CRIPPLE, this)));
		return true;
	}
}

