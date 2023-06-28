using Godot;

public partial class ClippedCamera : Node3D {
	Camera3D cam;
	RayCast3D col;
	public override void _Ready() {
		cam = GetNode<Camera3D>("Camera3D");
		col = GetNode<RayCast3D>("RayCast3D");
	}
	public override void _PhysicsProcess(double delta) {
		if (col.IsColliding()) {
			cam.GlobalPosition = col.GetCollisionPoint();
		}
		else {
			cam.GlobalPosition = GlobalPosition;
		}
	}
}
