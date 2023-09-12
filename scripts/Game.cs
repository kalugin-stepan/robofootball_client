using System.Collections.Generic;

public static class Game {
	public static string name;
	public static string server_url;
	public static Team team1 = new Team();
	public static Team team2 = new Team();
	public class Team {
		public string name;
		public List<Player> players = new List<Player>();
	}
	public class Player {
		public int id;
		public string name;
		public Player(int id, string name) {
			this.id = id;
			this.name = name;
		}
	}
}
