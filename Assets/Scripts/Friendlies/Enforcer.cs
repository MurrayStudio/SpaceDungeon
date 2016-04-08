using UnityEngine;
using System.Collections;

public class Enforcer : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	// Reference numbers
	private readonly int HEAVY_SWING 	= 0;
    private readonly int KICK           = 1;
    private readonly int SLICE 			= 2;
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
	public readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};			

	public Enforcer () : base ()
	{
		int NewLevel = 0;
		BaseHealth = LVL_HEALTH [NewLevel];
		BaseSpeed = LVL_SPEED [NewLevel];
		BaseDodge = LVL_DODGE [NewLevel];
		BaseCrit = LVL_CRIT [NewLevel];
		BaseDmg = new int[] { LVL_DMG [NewLevel, 0], LVL_DMG [NewLevel, 1] };
		BaseArmor = 0;

		CritMods = new int[] { 5, 0, 0, 0, 0 };
		DmgMods = new float[] { 0.15f, -0.15f, -0.60f, 0f, 0f };
		AccMods = new int[] { 85, 85, 85, 0, 0 };
		DebuffMods = new float[] { 0f, 0f, 0f, 0.25f, 0.25f };
		ValidRanks = new bool[][] {
			new bool [] { true, true, false, false, false, false, false },	// Heavy Swing 	1-2 both
			new bool [] { true, true, true, false, false, false, false },	// Slice		1-3 
			new bool [] { true, true, false, false, false, false, false },	// Kick			1-2
			new bool [] { false, false, false, false, true, false, false },	// Steroids		self
			new bool [] { false, false, false, false, false, true, false }	// War Cry		allies
		};
		IsMultiHit = new bool[] { true, false, false, false, true };

		CurrHealth = BaseHealth;
		Level = 1;
		Rank = ONE;
		Category = "Enforcer";
		IsMech = false;
		IsFriendly = true;
		XP = 0;
		HasPlayed = false;
	}

	public override void SetStats (int NewLevel, int NewRank, int NewHealth)
	{
		NewLevel--;
		this.BaseHealth = this.LVL_HEALTH[NewLevel];
		this.BaseSpeed = this.LVL_SPEED[NewLevel];
		this.BaseDodge = this.LVL_DODGE[NewLevel];
		this.BaseCrit = this.LVL_CRIT[NewLevel];
		this.BaseDmg = new int[] {this.LVL_DMG[NewLevel, 0], this.LVL_DMG[NewLevel, 1]};
		this.BaseArmor = 0;

		this.CurrHealth = NewHealth;
		this.Level = NewLevel;
		this.Rank = NewRank;
	}

	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target)
	{
		if (MoveID == KICK) 
		{
			this.MoveUnit (Allies, Target, 1);
		}

		if (MoveID == SLICE || MoveID == KICK || MoveID == HEAVY_SWING) 
		{
			if (!this.CheckHit(MoveID, Target)) 
			{
				return FAILURE;
			}
		}

		if (MoveID == HEAVY_SWING)
		{
			foreach (Unit U in Enemies)
			{
				if (U.GetRank() == ONE || U.GetRank() == TWO)
				{
					U.RemoveHealth (RollDamage (MoveID, this.BaseDmg, U));
				}
			}
			return SUCCESS;
		}
		else if (MoveID == SLICE)
		{
//			Debuff D1 = new Debuff (DOT_DUR, DebuffMods [SLICE], BLEED);
//			Target.AddDebuff (D1);
			Target.RemoveHealth (RollDamage (MoveID, this.BaseDmg, Target));
			return SUCCESS;
		}
		else if (MoveID == KICK)
		{
			Target.MoveUnit (Enemies, Target, -1);
			Target.RemoveHealth (RollDamage (MoveID, this.BaseDmg, Target));
			return SUCCESS;
		}
		else if (MoveID == STEROIDS)
		{
			Target.AddHealth (4); //TODO Constants for heal amounts or ranges
//			Debuff D2 = new Debuff (DEBUFF_DUR, DebuffMods [STEROIDS], SPEED);
//			Target.AddDebuff (D2);
			CheckCrit (STEROIDS, this);
			return SUCCESS;
		}
		else if (MoveID == WAR_CRY)
		{
//			Debuff D3 = new Debuff (DEBUFF_DUR, DebuffMods [WAR_CRY], DAMAGE);
//			for (int i = 0; i < Allies.Length; i++)
//			{
//				Allies [i].AddDebuff (D3);
//			}
			return SUCCESS;
		}
		return FAILURE;
	}
}