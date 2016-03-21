using UnityEngine;
using System.Collections;
using System.Diagnostics.Eventing.Reader;

public class Enforcer : Friendly
{
	private readonly int KICK = 0;
	private readonly int HEAVY_SWING = 1;
	private readonly int SLICE = 2;
	private readonly int WAR_CRY = 3;
	private readonly int STEROIDS = 4 ;

	public Enforcer () : base ()
	{
		MAX_HEALTH = 33;
		SPEED = 1;
		DODGE = 5;
		BASE_CRIT = 0.05f;
		BASE_DMG = new int[] {6, 12};
		CRIT = new float[] {0.05f, 0f, 0f, 0f};
		DMG_MODS = new float[] {-0.60f, 0.15f, -0.15f, 0f};
		ACC = new int[] {90, 85, 85, 100};

		CurrHealth = MAX_HEALTH;
		Level = 1;
		Rank = 1;
		Cat = ENFORCER;
	}

	void Kick (Enemy e) 		// Stats from rampart
	{
		if (!CheckHit (this, e, KICK))
			return;

		
	}

	void HeavySwing (Enemy e) 	// Stats from smite
	{
		if (!CheckHit (this, e, KICK))
			return;

		
	}

	void Slice (Enemy e)		// Stats from open vein
	{
		if (!CheckHit (this, e, KICK))
			return;

		
	}

	void WarCry ()				// Kinda bulwark of faith?
	{
			
	}

	void Steroids ()
	{

	}
}

