using Godot;
using System;

public partial class Ball : RigidBody3D {
	[Rpc(TransferMode = MultiplayerPeer.TransferModeEnum.Unreliable)]
	void UpdateBallData(Vector3 pos, Vector3 rotation) {
		GlobalPosition = pos;
		Rotation = rotation;
	}
}
