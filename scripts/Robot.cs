using System;
using Godot;
using Godot.Collections;

public partial class Robot : RigidBody3D {
	public delegate void UpdatePing(int ms);
	public event UpdatePing UpdatePingEvent;
	Label3D usernameLabel;
	public string username;
	public bool local = false;
	// arrays of wheels to rotate it
	Array<Node3D> leftWheels = new Array<Node3D>();
	Array<Node3D> rightWheels = new Array<Node3D>();
	float wheelR;
	float R; // distance from center to wheel
	public override void _Ready() {
		leftWheels.Add(GetNode<Node3D>("wheel_l_mesh"));
		leftWheels.Add(GetNode<Node3D>("wheel_lb_mesh"));

		rightWheels.Add(GetNode<Node3D>("wheel_r_mesh"));
		rightWheels.Add(GetNode<Node3D>("wheel_rb_mesh"));

		wheelR = GetMeta("wheelR").AsSingle();
		R = CenterOfMass.DistanceTo(leftWheels[0].Position);

		usernameLabel = GetNode<Label3D>("username");
		usernameLabel.Text = username;
	}
	DateTime lastPingTime = DateTime.Now;
	public override void _PhysicsProcess(double delta) {
		if (local) {
			RpcId(SceneController.serverId, nameof(SetInput), GetInput());
		}
		if (SceneController.cam != null) {
			usernameLabel.LookAt(SceneController.cam.GlobalPosition);
			usernameLabel.RotateY(Mathf.Pi);
		}
	}

	Vector2 GetInput() {
		var rez = Vector2.Zero;
		if (Input.IsActionPressed("f")) {
			rez.X += 1;
			rez.Y += 1;
		}
		if (Input.IsActionPressed("b")) {
			rez.X -= 1;
			rez.Y -= 1;
		}
		if (Input.IsActionPressed("l")) {
			rez.X -= 0.5f;
			rez.Y += 0.5f;
		}
		if (Input.IsActionPressed("r")) {
			rez.X += 0.5f;
			rez.Y -= 0.5f;
		}
		return rez;
	}
	void RotateWheels(float Sl, float Sr) {
		foreach (var wheel in leftWheels) {
			wheel.RotateZ(Sl / wheelR);
		}
		foreach (var wheel in rightWheels) {
			wheel.RotateZ(Sr / wheelR);
		}
	}

	[Rpc(TransferMode = MultiplayerPeer.TransferModeEnum.Unreliable)]
	void UpdatePlayerData(Vector3 pos, float rotation) {
		float S = pos.DistanceTo(GlobalPosition);
		float rotationS = (rotation - Rotation.Y) * R;
		RotateWheels(S - rotationS, S + rotationS);
		GlobalPosition = pos;
		Rotation = new Vector3(0, rotation, 0);
	}
	[Rpc(TransferMode = MultiplayerPeer.TransferModeEnum.Unreliable)]
	void SetInput(Vector2 dir) {}
	[Rpc(TransferMode = MultiplayerPeer.TransferModeEnum.Unreliable)]
	void Ping() {
		RpcId(SceneController.serverId, nameof(Ping));
	}
	[Rpc]
	void RemoteUpdatePing(int ms) {
		UpdatePingEvent(ms);
	}
}
