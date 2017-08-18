using System;

[Serializable]
public class Choice {
	public string name;
	public string info;

	public bool affectStats;
	public bool absoluteStats;

	public PlayerStats stats;
}