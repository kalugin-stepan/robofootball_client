using Godot;
using System;

public partial class Keyboard : Node {
    double t;

    public static Action forward;
    public static Action back;
    public static Action left;
    public static Action right;

    Key f;
    Key b;
    Key l;
    Key r;

    public override void _Ready() {
        var keyboardConfig = new ConfigFile();
        keyboardConfig.Load("res://cfg/keyboard.cfg");

        f = keyboardConfig.GetValue("keyboaard", "f").As<Key>();
        b = keyboardConfig.GetValue("keyboaard", "b").As<Key>();
        l = keyboardConfig.GetValue("keyboaard", "l").As<Key>();
        r = keyboardConfig.GetValue("keyboaard", "r").As<Key>();
    }
    public override void _Process(double delta) {
        t += delta;
        if (t > 0.01) {
            t = 0;
            if (Input.IsKeyPressed(f)) {
                if (forward != null) forward.Invoke();
                return;
            }
            if (Input.IsKeyPressed(b)) {
                if (back != null) back.Invoke();
                return;
            }
            if (Input.IsKeyPressed(l)) {
                if (left != null) left.Invoke();
                return;
            }
            if (Input.IsKeyPressed(r)) {
                if (right != null) right.Invoke();
                return;
            }
        }   
    }
}
