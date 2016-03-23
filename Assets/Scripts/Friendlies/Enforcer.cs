using UnityEngine;
using System.Collections;

public class Enforcer : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	// Reference numbers
	private readonly int HEAVY_SWING 	= 0;
	private readonly int SLICE 			= 1;
	private readonly int KICK			= 2;
	private readonly int STEROIDS		= 3;
	private readonly int WAR_CRY		= 4;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {6, 12}, {7, 14}, {8, 16}, {9, 17}, {10, 18} };
	private readonly int[] LVL_HEALTH = new int[] {33, 40, 47, 54, 61};
	private readonly int[] LVL_DODGE = new int[] {5, 10, 15, 20, 25};
	private readonly int[] LVL_SPEED = new int[] {1, 1, 2, 2, 3};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};


	public Enforcer () : base ()
	{
		int NewLevel = 0;
		BASE_HEALTH = LVL_HEALTH[NewLevel];
		BASE_SPEED = LVL_SPEED[NewLevel];
		BASE_DODGE = LVL_DODGE[NewLevel];
		BASE_CRIT = LVL_CRIT[NewLevel];
		BASE_DMG = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BASE_ARMOR = 0;

		CRIT_MODS= new int[] {5, 0, 0, 0, 0};
		DMG_MODS = new float[] {0.15f, -0.15f, -0.60f, 0f, 0f};
		ACC_MODS = new int[] {85, 85, 85, 0, 0};
		DEBUFF_MODS = new float[] {0f, 0f, 0f, 0.25f, 0.25f};

		CurrHealth = BASE_HEALTH;
		Level = 1;
		Rank = 0;
		CAT = ENFORCER;
		IS_MECH = false;
		IS_FRIENDLY = true;
		XP = 0;
		HasPlayed = false;
	}

	public override void SetStats (int NewLevel, int NewRank, int NewHealth)
	{
		NewLevel--;
		this.BASE_HEALTH = this.LVL_HEALTH[NewLevel];
		this.BASE_SPEED = this.LVL_SPEED[NewLevel];
		this.BASE_DODGE = this.LVL_DODGE[NewLevel];
		this.BASE_CRIT = this.LVL_CRIT[NewLevel];
		this.BASE_DMG = new int[] {this.LVL_DMG[NewLevel, 0], this.LVL_DMG[NewLevel, 1]};
		this.BASE_ARMOR = 0;

		this.CurrHealth = NewHealth;
		this.Level = NewLevel;
		this.Rank = NewRank;
	}

	public bool HeavySwing (Unit Enemy) 	
	{
		if (!CheckHit (HEAVY_SWING, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (HEAVY_SWING, BASE_DMG[0], BASE_DMG[1], Enemy));
		return true;
	}

	public bool Slice (Unit Enemy)		
	{
		if (!CheckHit (SLICE, this, Enemy)) 
		{
			return false;
		}

		Debuff D = new Debuff (DOT_DUR, DEBUFF_MODS [SLICE], BLEED);
		this.AddDebuff (D);

		Enemy.DecreaseHealth (RollDamage (SLICE, BASE_DMG[0], BASE_DMG[1], Enemy));
		return true;
	}

	public bool Kick (Unit Enemy) 		
	{
		if (!CheckHit (KICK, this, Enemy)) 
		{
			return false;
		}

		Enemy.DecreaseHealth (RollDamage (KICK, BASE_DMG[0], BASE_DMG[1], Enemy));
		return true;
	}

	public void Steroids ()
	{
		Debuff D = new Debuff (DEBUFF_DUR, DEBUFF_MODS [STEROIDS], SPEED);
		this.AddDebuff (D);
		this.AddHealth (4); //TODO Constants for heal amounts or ranges
	}

	public void WarCry (Unit[] Allies)				
	{
		Debuff D = new Debuff (DEBUFF_DUR, DEBUFF_MODS [WAR_CRY], DAMAGE);
		for (int i = 0; i < Allies.Length; i++)
		{
			Allies [i].AddDebuff (D);
		}
	}
}