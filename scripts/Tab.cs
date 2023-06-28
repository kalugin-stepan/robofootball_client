using Godot;
using System;

public partial class Tab : Control {
	VBoxContainer team1List;
	VBoxContainer team2List;
	readonly PackedScene tabUserScene = ResourceLoader.Load<PackedScene>("res://scenes/TabUser.tscn");
	public override void _Ready() {
		team1List = GetNode<VBoxContainer>("team1List");
		team2List = GetNode<VBoxContainer>("team2List");
	}
	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("tab") || Input.IsActionJustReleased("tab")) {
			Visible = !Visible;
		}
	}
	public void AddTabUser(long id, string username, Robot robot, Team team) {
		var tabUser = tabUserScene.Instantiate<TabUser>();

		if (team == Team.team1) {
			team1List.AddChild(tabUser);
			robot.UpdatePingEvent += ms => UpdatePlayersPing(id, ms, Team.team1);
		}
		else if (team == Team.team2) {
			team2List.AddChild(tabUser);
			robot.UpdatePingEvent += ms => UpdatePlayersPing(id, ms, Team.team2);
		}
		tabUser.Name = id.ToString();
		tabUser.username = username;
	}
	public void RemoveTabUser(long id, Team team) {
		if (team == Team.team1) {
			team1List.RemoveChild(team1List.GetNode(id.ToString()));
			return;
		}
		if (team == Team.team2) {
			team2List.RemoveChild(team2List.GetNode(id.ToString()));
			return;
		}
	}
	public void UpdatePlayersPing(long id, int ping, Team team) {
		if (team == Team.team1) {
		 	var tabUser = team1List.GetNode<TabUser>(id.ToString());
			tabUser.ping = ping;
			return;
		}
		if (team == Team.team2) {
			var tabUser = team2List.GetNode<TabUser>(id.ToString());
			tabUser.ping = ping;
			return;
		}
	}
}
