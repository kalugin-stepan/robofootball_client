using Godot;
using System;

public partial class Ball : RigidBody3D {
	public void UpdateBallPos(Vector3 pos) {
		GlobalPosition = pos;
	}
	// [Rpc(TransferMode = MultiplayerPeer.TransferModeEnum.Unreliable)]
	// void UpdateBallData(Vector3 pos, Vector3 rotation) {
	// 	GlobalPosition = pos;
	// 	Rotation = rotation;
	// }
}
