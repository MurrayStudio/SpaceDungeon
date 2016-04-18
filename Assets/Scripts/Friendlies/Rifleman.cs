using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rifleman : Unit
{
    /*==================================
			   Ability Indexes
	===================================*/
    private readonly int BAYONET        = 0;
    private readonly int NET_GUN        = 1;
    private readonly int RELOAD         = 2;
    private readonly int RIFLE 			= 3;
	private readonly int SHOTGUN		= 4;

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
		BaseHealth = LVL_HEALTH[NewLevel];
		BaseSpeed = LVL_SPEED[NewLevel];
		BaseDodge = LVL_DODGE[NewLevel];
		BaseCrit = LVL_CRIT[NewLevel];
		BaseDmg = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BaseArmor = 0;

		CritMods = new int[] {10, 0, 0, 0, 0};
		DmgMods = new float[] {0f, -0.8f, 0f, 0f, -0.4f};
		AccMods = new int[] {85, 85, 0, 85, 85};
		DebuffMods = new float[] {0f, 0f, 0f, -0.15f, 0.10f};
		HitRanks = new bool[][] {
			new bool [] { true, true, false, false, false, false, false },	// Bayonet	1-2
			new bool [] { true, true, true, true, false, false, false },	// Net Gun	1-4
			new bool [] { false, false, false, false, true, false, false },	// Reload	1-3 all
			new bool [] { false, true, true, true, false, false, false },	// Rifle	1-4
			new bool [] { true, true, true, false, false, false, false }		// Shotgun	self
		};
		IsMultiHit = new bool[] { false, false, true, false, true };
		Debuffs = new List<Debuff>();

		CurrHealth = BaseHealth;
		Level = 1;
		Rank = TWO;
		Category = "Rifleman";
		IsMech = false;
		IsFriendly = true;
		XP = 0;
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
		if (MoveID == BAYONET)
		{
			MoveUnit (Allies, this, 1);
		}

		if (MoveID == RIFLE || MoveID == BAYONET || MoveID == NET_GUN || MoveID == SHOTGUN) 
		{
			if (!this.CheckHit(MoveID, Target)) 
			{
				return false;
			}
		}

		if (MoveID == RIFLE)
		{
			Target.RemoveHealth (this.RollDamage (MoveID, this.BaseDmg, Target));
			return SUCCESS;
		}
		else if (MoveID == BAYONET)
		{
			Target.RemoveHealth (this.RollDamage (MoveID, this.BaseDmg, Target));
			return SUCCESS;
		}
		else if (MoveID == SHOTGUN)
		{
			foreach (Unit U in Enemies)
			{
				if (U.GetRank() >= ONE && U.GetRank() <= THREE)
				{
					U.RemoveHealth (this.RollDamage (MoveID, this.BaseDmg, U));
				}
			}
			return SUCCESS;
		}
		else if (MoveID == NET_GUN)
		{
			Target.RemoveHealth (this.RollDamage (MoveID, this.BaseDmg, Target));
			Debuff D1 = new Debuff (DEBUFF_DUR, DebuffMods [NET_GUN], SPEED);
			Debuff D2 = new Debuff (DEBUFF_DUR, DebuffMods [NET_GUN], DODGE);
			Target.AddDebuff (D1);
			Target.AddDebuff (D2);
			return SUCCESS;
		}
		else if (MoveID == RELOAD)
		{
			Debuff D3 = new Debuff (DEBUFF_DUR, DebuffMods [RELOAD], SPEED);
			Debuff D4 = new Debuff (DEBUFF_DUR, DebuffMods [RELOAD], DAMAGE);
			Debuff D5 = new Debuff (DEBUFF_DUR, DebuffMods [RELOAD] * -1, ARMOR);
			Target.AddDebuff (D3);
			Target.AddDebuff (D4);
			Target.AddDebuff (D5);
			Target.AddHealth(4);
			return SUCCESS;
		}

		return FAILURE;
	}
}