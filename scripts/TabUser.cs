using Godot;

public partial class TabUser : Control {
	static readonly Color red = new Color(255, 0, 0);
	static readonly Color yellow = new Color(255, 255, 0);
	static readonly Color green = new Color(0, 255, 0);
	public string username {
		get => usernameLabel.Text;
		set => usernameLabel.Text = value;
	}
	public int ping {
		set {
			if (value < 50) {
				pingLabel.LabelSettings.FontColor = green;
			}
			else if (value > 50) {
				pingLabel.LabelSettings.FontColor = yellow;
			}
			else if (value > 100) {
				pingLabel.LabelSettings.FontColor = red;
			}
			pingLabel.Text = value.ToString();
		}
	}
	Label usernameLabel;
	Label pingLabel;
	public override void _Ready() {
		usernameLabel = GetNode<Label>("username");
		pingLabel = GetNode<Label>("ping");
	}
}
