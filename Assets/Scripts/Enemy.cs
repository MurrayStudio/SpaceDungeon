using UnityEngine;
using System.Collections;

public class Enemy : Unit
{
	public readonly int INFECTED = 0;
	public readonly int FREIGHT = 1;
	public readonly int SECURITY = 2;
	public readonly int PSYCHIC = 3;
	public readonly int MEDIBOT = 4;

	public Enemy () : base()
	{}
}