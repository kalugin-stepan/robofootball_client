using Godot;
using System;

public partial class Tab : Control {
	VBoxContainer team1List;
	VBoxContainer team2List;
	readonly PackedScene tabUserScene = ResourceLoader.Load<PackedScene>("res://scenes/TabUser.tscn");
	public override void _Ready() {
		team1List = GetNode<VBoxContainer>("team1List");
		team2List = GetNode<VBoxContainer>("team2List");
		TypeName.onGameStart += (username, gameName, team) => AddPlayers();
	}
	void AddPlayers() {
		var team1_players = Game.team1.players;
		var team2_players = Game.team2.players;

		foreach (var player in team1_players) {
			var tabUser = tabUserScene.Instantiate<TabUser>();
			team1List.AddChild(tabUser);
			tabUser.username = player.name;
			tabUser.ping = 0;
		}
		foreach (var player in team2_players) {
			var tabUser = tabUserScene.Instantiate<TabUser>();
			team2List.AddChild(tabUser);
			tabUser.username = player.name;
			tabUser.ping = 0;
		}
	}
	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("tab") || Input.IsActionJustReleased("tab")) {
			Visible = !Visible;
		}
	}
}
