using Godot;
using System;

public partial class PlayersController : Node {
	MQTT mqtt;
	public override void _Ready() {
		mqtt = GetParent().GetNode<MQTT>("MQTT");
		mqtt.Subscribe("*:pos");
		
	}
	public override void _Process(double delta) {
		
	}
	(Vector2, float) CalculatePosAndAngle(Vector2 pos0, Vector2 pos1, Vector2 pos2, Vector2 pos3) {
		Vector2 dir = (pos0 + pos1) / 2;
		Vector2 pos = (pos0 + pos2) / 2;
		float a = Mathf.Atan2(dir.Y - pos.Y, dir.X - pos.X);
		return (pos, a);
	}
}
