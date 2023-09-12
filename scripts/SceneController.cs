using Godot;
using Godot.Collections;

public partial class SceneController : Node {
	// Node root;
	// Node hud;
	// Tab tab;
	// Node choose_team;
	// Label team1ScoreLabel;
	// Label team2ScoreLabel;
	// Button joinTeam1Btn;
	// Button joinTeam2Btn;
	// public static long serverId;
	// public static Camera3D cam;
	// readonly ENetMultiplayerPeer multiplayerPeer = new ENetMultiplayerPeer();
	// Array<Dictionary<string, Variant>> team1;
	// Array<Dictionary<string, Variant>> team2;
	// static readonly PackedScene robotScene = ResourceLoader.Load<PackedScene>("res://scenes/Robot.tscn");
	// static readonly PackedScene camScene = ResourceLoader.Load<PackedScene>("res://scenes/ClippedCamera.tscn");
	// static readonly PackedScene TabUserScene = ResourceLoader.Load<PackedScene>("res://scenes/TabUser.tscn");
	// 	static readonly PackedScene blueRobotBody = ResourceLoader.Load<PackedScene>("res://models/robot_blue.glb");
	// static readonly PackedScene redRobotBody = ResourceLoader.Load<PackedScene>("res://models/robot_red.glb");
	// static readonly Vector3[] initialPositionsTeam1 = {
	// 	new Vector3(-5, 0.194f, 0),
	// 	new Vector3(-5, 0.194f, 20),
	// 	new Vector3(-5, 0.194f, -20)
	// };
	// static readonly Vector3[] initialPositionsTeam2 = {
	// 	new Vector3(5, 0.194f, 0),
	// 	new Vector3(5, 0.194f, 20),
	// 	new Vector3(5, 0.194f, -20)
	// };
	// static readonly float[] teamsInitialRotations = {
	// 	0,
	// 	Mathf.Pi
	// };
	// const string host = "127.0.0.1";
	// const int port = 9999;
	// string username;
	// public override void _Ready() {
	// 	root = GetParent();
	// 	var type_name = root.GetNode("type_name");
	// 	choose_team = root.GetNode("choose_team");
	// 	hud = root.GetNode("hud");
	// 	tab = root.GetNode<Tab>("Tab");

	// 	team1ScoreLabel = hud.GetNode<Label>("team1Score");
	// 	team2ScoreLabel = hud.GetNode<Label>("team2Score");
	// 	joinTeam1Btn = choose_team.GetNode<Button>("joinTeam1");
	// 	joinTeam2Btn = choose_team.GetNode<Button>("joinTeam2");
	// 	joinTeam1Btn.Pressed += () => JoinTeam(Team.team1);
	// 	joinTeam2Btn.Pressed += () => JoinTeam(Team.team2);
	// 	var startBtn = type_name.GetNode<Button>("start");
	// 	var usernameTextEdit = type_name.GetNode<LineEdit>("username");
		
	// 	startBtn.Pressed += () => {
	// 		if (usernameTextEdit.Text.Length < 3 || Empty(usernameTextEdit.Text))
	// 			return;
			
	// 		startBtn.Visible = false;
	// 		usernameTextEdit.Visible = false;
	// 		username = usernameTextEdit.Text;

	// 		for (int i = 0; i < type_name.GetChildCount(); i++) {
	// 			type_name.GetChild<Control>(i).Visible = false;
	// 		}
	// 		for (int i = 0; i < choose_team.GetChildCount(); i++) {
	// 			choose_team.GetChild<Control>(i).Visible = true;
	// 		}
	// 		tab.Visible = !tab.Visible;
	// 	};

	// 	multiplayerPeer.CreateClient(host, port);
	// 	Multiplayer.MultiplayerPeer = multiplayerPeer;
	// 	multiplayerPeer.PeerConnected += OnPeerConnection;
	// }
	
	// void AddRobotCharacter(long id, string username, Team team) {
	// 	var robot = robotScene.Instantiate<Robot>();
	// 	robot.SetMultiplayerAuthority((int)serverId);
	// 	robot.Name = id.ToString();
	// 	robot.username = username;
	// 	AddChild(robot);
	// 	var player = new Dictionary<string, Variant>();
	// 	player["id"] = id;
	// 	player["username"] = username;
	// 	if (team == Team.team1) {
	// 		robot.GlobalPosition = initialPositionsTeam1[team1.Count];
	// 		robot.Rotation = new Vector3(0, teamsInitialRotations[0], 0);
	// 		robot.AddChild(blueRobotBody.Instantiate());
	// 		team1.Add(player);
	// 	}
	// 	else if (team == Team.team2) {
	// 		robot.GlobalPosition = initialPositionsTeam2[team2.Count];
	// 		robot.Rotation = new Vector3(0, teamsInitialRotations[1], 0);
	// 		robot.AddChild(redRobotBody.Instantiate());
	// 		team2.Add(player);
	// 	}
	// 	if (id == multiplayerPeer.GetUniqueId()) {
	// 		robot.local = true;
	// 		var cam = camScene.Instantiate();
	// 		SceneController.cam = cam.GetNode<Camera3D>("Camera3D");
	// 		robot.AddChild(cam);
	// 		for (int i = 0; i < choose_team.GetChildCount(); i++) {
	// 			choose_team.GetChild<Control>(i).Visible = false;
	// 		}

	// 		for (int i = 0; i < hud.GetChildCount(); i++) {
	// 			hud.GetChild<Control>(i).Visible = true;
	// 		}
	// 		tab.Visible = !tab.Visible;
	// 	}
	// 	tab.AddTabUser(id, username, robot, team);
	// }
	// void JoinTeam(Team team) {
	// 	RpcId(serverId, nameof(JoinGame), username, (int)team);
	// }
	// [Rpc]
	// void SetTeamsData(Array<Dictionary<string, Variant>> team1, Array<Dictionary<string, Variant>> team2) {
	// 	this.team1 = team1;
	// 	this.team2 = team2;
	// 	int i = 0;
	// 	foreach (var player in team1) {
	// 		long id = player["id"].AsInt64();
	// 		string username = player["username"].AsString();
	// 		AddRobotCharacter(id, username, Team.team1);
	// 		i++;
	// 	}
	// 	i = 0;
	// 	foreach (var player in team2) {
	// 		long id = player["id"].AsInt64();
	// 		string username = player["username"].AsString();
	// 		AddRobotCharacter(id, username, Team.team2);
	// 		i++;
	// 	}
	// }
	// [Rpc]
	// void OnPlayerConnection(long id, string username, int team, int num) {
	// 	AddRobotCharacter(id, username, (Team)team);
	// }
	// [Rpc]
	// void OnPlayerDisconnection(long id) {
	// 	string username = null;
	// 	bool removed = false;
	// 	Team? team = null;
	// 	for (int i = 0; i < team1.Count; i++) {
	// 		if (team1[i]["id"].AsInt64() == id) {
	// 			username = team1[i]["username"].AsString();
	// 			team1.RemoveAt(i);
	// 			removed = true;
	// 			team = Team.team1;
	// 			break;
	// 		}
	// 	}
	// 	if (!removed) {
	// 		for (int i = 0; i < team2.Count; i++) {
	// 			if (team2[i]["id"].AsInt64() == id) {
	// 				username = team2[i]["username"].AsString();
	// 				team2.RemoveAt(i);
	// 				team = Team.team2;
	// 				break;
	// 			}
	// 		}
	// 	}
	// 	tab.RemoveTabUser(id, (Team)team);
	// 	RemoveChild(root.GetNode(id.ToString()));
	// }
	// [Rpc]
	// void UpdateScore(int team1Score, int team2Score) {
	// 	team1ScoreLabel.Text = team1Score.ToString();
	// 	team2ScoreLabel.Text = team2Score.ToString();
	// }
	// [Rpc]
	// void JoinGame(string username, int team) {}
	// void OnPeerConnection(long id) {
	// 	serverId = id;
	// 	var ballScene = ResourceLoader.Load("res://scenes/Ball.tscn") as PackedScene;
	// 	var ball = ballScene.Instantiate() as Ball;
	// 	ball.SetMultiplayerAuthority((int)id);
	// 	AddChild(ball);
	// }
	// bool Empty(string s) {
	// 	foreach (char c in s) {
	// 		if (c != ' ') return false;
	// 	}
	// 	return true;
	// }
}
