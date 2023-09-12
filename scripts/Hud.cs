using Godot;
using System;

public partial class Hud : Node {
	static Label team1ScoreLabel;
	static Label team2ScoreLabel;
	static Label team1NameLabel;
	static Label team2NameLabel;
	public static string Team1Name {
		get => team1NameLabel.Text;
		set => team1NameLabel.Text = value;
	}
	public static string Team2Name {
		get => team2NameLabel.Text;
		set => team2NameLabel.Text = value;
	}
	public static int Team1Score {
		get => int.Parse(team1ScoreLabel.Text);
		set => team1ScoreLabel.Text = value.ToString();
	}
	public static int Team2Score {
		get => int.Parse(team2ScoreLabel.Text);
		set => team2ScoreLabel.Text = value.ToString();
	}
	public override void _Ready() {
		team1ScoreLabel = GetNode<Label>("team1Score");
		team2ScoreLabel = GetNode<Label>("team2Score");
		team1NameLabel = GetNode<Label>("team1Name");
		team2NameLabel = GetNode<Label>("team2Name");
		TypeName.onGameStart += (username, gameName, serverUrl) => {
			Team1Name = Game.team1.name;
			Team2Name = Game.team2.name;
			Hud.Show();
		};
	}
	public static void Toggle() {
		team1NameLabel.Visible = !team1NameLabel.Visible;
		team2NameLabel.Visible = !team2NameLabel.Visible;
		team1ScoreLabel.Visible = !team1ScoreLabel.Visible;
		team2ScoreLabel.Visible = !team2ScoreLabel.Visible;
	}
	public static void Show() {
		team1NameLabel.Visible = true;
		team2NameLabel.Visible = true;
		team1ScoreLabel.Visible = true;
		team2ScoreLabel.Visible = true;
	}
	public static void Hide() {
		team1NameLabel.Visible = false;
		team2NameLabel.Visible = false;
		team1ScoreLabel.Visible = false;
		team2ScoreLabel.Visible = false;
	}
}
