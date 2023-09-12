using Godot;
using Godot.Collections;
using System;
using System.Text;

public partial class TypeName : Node {
	public static Action<string, string, Teams> onGameStart;
	Button start;
	LineEdit usernameInput;
	LineEdit gameNameInput;
	string username;
	string gameName;
	Teams team = Teams.none;
	string games_server_url;
	public override void _Ready() {
		var configFile = new ConfigFile();
		configFile.Load("res://cfg/config.cfg");
		games_server_url = configFile.GetValue("config", "games_server_url").AsString();

		start = GetNode<Button>("start");
		usernameInput = GetNode<LineEdit>("username");
		gameNameInput = GetNode<LineEdit>("game_name");
		start.Pressed += OnStart;
	}
	void OnStart() {
		username = usernameInput.Text;
		gameName = gameNameInput.Text;

		var gameImfoRequest = GetParent().GetNode<HttpRequest>("GetGameInfo");
		gameImfoRequest.RequestCompleted += OnResponse;
		gameImfoRequest.Request(games_server_url + $"/get_game?name={gameName}");
	}

	void OnResponse(long res, long code, string[] headers, byte[] data) {
		var game = Json.ParseString(Encoding.UTF8.GetString(data)).AsGodotDictionary<string, Variant>();
		
		Game.name = game["name"].AsString();
		Game.server_url = game["server_url"].AsString();
		var team1 = game["team1"].AsGodotDictionary<string, Variant>();
		var team2 = game["team2"].AsGodotDictionary<string, Variant>();

		Game.team1.name = team1["name"].AsString();
		Game.team2.name = team2["name"].AsString();

		var team1Players = team1["players"].AsGodotArray<Dictionary<string, Variant>>();
		var team2Players = team2["players"].AsGodotArray<Dictionary<string, Variant>>();

		foreach (var player in team1Players) {
			var i = new Game.Player(player["id"].AsInt32(), player["name"].AsString());
			Game.team1.players.Add(i);
			if (i.name == username) team = Teams.team1;
		}
		foreach (var player in team2Players) {
			var i = new Game.Player(player["id"].AsInt32(), player["name"].AsString());
			Game.team2.players.Add(i);
			if (i.name == username) team = Teams.team2;
		}

		if (team != Teams.none) {
			start.Visible = false;
			usernameInput.Visible = false;
			gameNameInput.Visible = false;

			if (onGameStart != null) onGameStart.Invoke(username, gameName, team);
		}
 	}
}
