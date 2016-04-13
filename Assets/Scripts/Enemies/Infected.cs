using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Infected : Unit
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
		BaseHealth = LVL_HEALTH[NewLevel];
		BaseSpeed = LVL_SPEED[NewLevel];
		BaseDodge = LVL_DODGE[NewLevel];
		BaseCrit = LVL_CRIT[NewLevel];
		BaseDmg = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BaseArmor = 0;

		CritMods = new int[] {0, 0};
		DmgMods = new float[] {0f, -0.20f};
		AccMods = new int[] {85, 85};
		DebuffMods = new float[] {0f, 0f, 0f};
		HitRanks = new bool[][] {
			new bool [] { true, true, false, false, false, false, false },	// Claw				1-2
			new bool [] { false, false, true, true, false, false, false },	// Acid Spit		3-4
		};
		IsMultiHit = new bool[] { false, false, true };
		Debuffs = new List<Debuff>();

		CurrHealth = BaseHealth;
		Level = 1;
		Rank = 1;
		Category = "Infected";
		IsMech = false;
		IsFriendly = false;
		HasPlayed = false;
	}

	public override void SetStats (int NewLevel, int NewRank)
	{
		this.BaseHealth = this.LVL_HEALTH[NewLevel];
		this.BaseSpeed = this.LVL_SPEED[NewLevel];
		this.BaseDodge = this.LVL_DODGE[NewLevel];
		this.BaseCrit = this.LVL_CRIT[NewLevel];
		this.BaseDmg = new int[] {this.LVL_DMG[NewLevel, 0], this.LVL_DMG[NewLevel, 1]};
		this.BaseArmor = 0;

		this.CurrHealth = this.LVL_HEALTH[NewLevel];
		this.Level = NewLevel;
		this.Rank = NewRank;
	}

	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target) 
	{
		if (!CheckHit (MoveID, Target))
		{
			return FAILURE;
		}

		if (MoveID == CLAW)
		{
			Target.RemoveHealth (RollDamage (MoveID, this.BaseDmg, Target));
			return SUCCESS;
		}
		else if (MoveID == ACID_SPIT)
		{
			Target.RemoveHealth (RollDamage (MoveID, this.BaseDmg, Target));
			Debuff D1 = new Debuff (DOT_DUR, DebuffMods [ACID_SPIT], ACID);
			Target.AddDebuff (D1);
			return SUCCESS;
		}
		return FAILURE;
	}
}

