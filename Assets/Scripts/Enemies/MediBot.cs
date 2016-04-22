using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MediBot : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int RESTORE 		= 0;
	private readonly int STIMS		 	= 1;
	private readonly int SELF_DESTRUCT	= 2;

	private readonly int RESTORE_HEAL	= 2;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {5, 10}, {6, 12}, {7, 13}, {8, 15}, {9, 16} };
	private readonly int[] LVL_HEALTH = new int[] {16, 21, 26, 31, 36};
	private readonly int[] LVL_DODGE = new int[] {0, 5, 10, 15, 20};
	private readonly int[] LVL_SPEED = new int[] {1, 2, 2, 3, 3};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};

	public MediBot () : base ()
	{
		int NewLevel = 0;
		BaseHealth = LVL_HEALTH[NewLevel];
		BaseSpeed = LVL_SPEED[NewLevel];
		BaseDodge = LVL_DODGE[NewLevel];
		BaseCrit = LVL_CRIT[NewLevel];
		BaseDmg = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BaseArmor = 0;

		CritMods = new int[] {0, 0, 0};
		DmgMods = new float[] {0f, 0f, 0.5f};
		AccMods = new int[] {0, 0, 85};
		DebuffMods = new float[] {0f, 0.15f, 0f};
		HitRanks = new bool[][] {
			new bool [] { false, false, false, false, false, true, false },	// Restore			1-4
			new bool [] { true, true, false, false, false, false, false },	// Stims			1-2
			new bool [] { false, false, false, false, false, false, true },	// Self Destruct	all
		};
		IsMultiHit = new bool[] { false, false, true };
		Debuffs = new List<Debuff>();

		CurrHealth = BaseHealth;
		Level = 1;
		Rank = 1;
		Category = "MediBot";
		IsMech = true;
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
        int move = Random.Range(0, 2);
        if ((float)this.GetHealth() / (float)this.GetBaseHealth() < 0.10f) move = SELF_DESTRUCT;

        if (move == RESTORE)
		{
            foreach (Unit U in Allies)
            {
                if (!U.GetIsDead()) { 
                    U.AddHealth(RESTORE_HEAL);
                }
			}
			return SUCCESS;
		}
		else if (move == STIMS)
		{
			Debuff D1 = new Debuff(DEBUFF_DUR, DebuffMods[STIMS], SPEED);
			foreach (Unit U in Allies)
			{
				if (!U.GetIsDead())
                {
                    U.AddDebuff(D1);
                }
            }
			return SUCCESS;
		}
		else if (move == SELF_DESTRUCT)
		{
			Target.RemoveHealth (RollDamage (move, this.BaseDmg, Target));
			this.RemoveHealth (50);
			return SUCCESS;
		}

		return FAILURE;
	}
}

