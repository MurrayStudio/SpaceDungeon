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

	private readonly int STEROIDS_HEAL	= 4;

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
		BASE_HEALTH = LVL_HEALTH [NewLevel];
		BASE_SPEED = LVL_SPEED [NewLevel];
		BASE_DODGE = LVL_DODGE [NewLevel];
		BASE_CRIT = LVL_CRIT [NewLevel];
		BASE_DMG = new int[] { LVL_DMG [NewLevel, 0], LVL_DMG [NewLevel, 1] };
		BASE_ARMOR = 0;

		CRIT_MODS = new int[] { 5, 0, 0, 0, 0 };
		DMG_MODS = new float[] { 0.15f, -0.15f, -0.60f, 0f, 0f };
		ACC_MODS = new int[] { 85, 85, 85, 0, 0 };
		DEBUFF_MODS = new float[] { 0f, 0f, 0f, 0.25f, 0.25f };
		VALID_RANKS = new int[][] {
			new int [] { ONE, TWO },	// Heavy Swing
			new int [] { ONE, THREE },	// Slice
			new int [] { ONE, TWO },	// Kick
			new int [] { SELF },		// Steroids
			new int [] { ALLIES }		// War Cry
		};
		IS_MULTI_HIT = new bool[] { true, false, false, false, true };

		CurrHealth = BASE_HEALTH;
		Level = 1;
		Rank = ONE;
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

	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target)
	{
		if (MoveID == KICK) {
			this.MoveUnit (Allies, Target, 1);
		}

		if (MoveID == HEAVY_SWING || MoveID == SLICE || MoveID == KICK) {
			if (!this.CheckHit(MoveID, Target)) {
				return false;
			}
		}

		if (MoveID == HEAVY_SWING) 
		{
			Target.RemoveHealth (RollDamage (MoveID, this.BASE_DMG [0], this.BASE_DMG [1], Target));
			return true;
		}
		else if (MoveID == SLICE)
		{
			Debuff D1 = new Debuff (DOT_DUR, DEBUFF_MODS [SLICE], BLEED);
			Target.AddDebuff (D1);
			Target.RemoveHealth (RollDamage (MoveID, this.BASE_DMG [0], this.BASE_DMG [1], Target));
			return true;
		}
		else if (MoveID == KICK)
		{
			Target.MoveUnit (Enemies, Target, -1);
			Target.RemoveHealth (RollDamage (MoveID, this.BASE_DMG [0], this.BASE_DMG [1], Target));
			return true;
		}
		else if (MoveID == STEROIDS)
		{
			Debuff D2 = new Debuff (DEBUFF_DUR, DEBUFF_MODS [STEROIDS], SPEED);
			this.AddDebuff (D2);
			CheckCrit (STEROIDS, this);
			this.AddHealth (4); //TODO Constants for heal amounts or ranges
			return true;
		}
		else if (MoveID == WAR_CRY)
		{
			Debuff D3 = new Debuff (DEBUFF_DUR, DEBUFF_MODS [WAR_CRY], DAMAGE);
			for (int i = 0; i < Allies.Length; i++)
			{
				Allies [i].AddDebuff (D3);
			}
			return true;
		}
		return false;
	}
}