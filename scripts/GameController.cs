using Godot;
using Godot.Collections;
using System.Text;


public partial class GameController : Node {
	readonly PackedScene robotScene = ResourceLoader.Load<PackedScene>("res://scenes/Robot.tscn");
	readonly PackedScene blueRobotBody = ResourceLoader.Load<PackedScene>("res://models/robot_blue.glb");
	readonly PackedScene redRobotBody = ResourceLoader.Load<PackedScene>("res://models/robot_red.glb");
	Ball ball;
	string username;
	string gameName;
	Teams team;
	MQTT mqtt;
	Array<Robot> players = new Array<Robot>();
	readonly Vector3[] initialPositionsTeam1 = {
		new Vector3(-5, 0.194f, 0),
		new Vector3(-5, 0.194f, 20),
		new Vector3(-5, 0.194f, -20)
	};
	readonly Vector3[] initialPositionsTeam2 = {
		new Vector3(5, 0.194f, 0),
		new Vector3(5, 0.194f, 20),
		new Vector3(5, 0.194f, -20)
	};
	readonly float[] teamsInitialRotations = {
		0,
		Mathf.Pi
	};

	string f_cmd;
	string b_cmd;
	string l_cmd;
	string r_cmd;

	public override void _Ready() {
		mqtt = GetParent().GetNode<MQTT>("MQTT");
		TypeName.onGameStart += GameStart;
		mqtt.BrokerConnected += () => {
			mqtt.Subscribe(gameName);
			mqtt.Subscribe(gameName + "/ball");
		};
		mqtt.ReceivedMessage += OnMessage;
		ball = ResourceLoader.Load<PackedScene>("res://scenes/Ball.tscn").Instantiate<Ball>();
	}
	void GameStart(string username, string gameName, Teams team) {
		this.username = username;
		this.gameName = gameName;
		this.team = team;
		mqtt.ConnectToBroker(Game.server_url);
		foreach (var player in Game.team1.players) {
			AddRobot(player, Teams.team1);
		}
		foreach (var player in Game.team2.players) {
			AddRobot(player, Teams.team2);
		}
		GetParent().AddChild(ball);
		SetupCommands();
		AddBindings();
	}
	void OnMessage(string topic, byte[] data) {
		var msg = Json.ParseString(Encoding.UTF8.GetString(data)).AsGodotDictionary<string, Variant>();
		if (topic == gameName) {
			var markers = msg["markers"].AsGodotArray<Dictionary<string, Variant>>();
			foreach (var marker in markers) {
				int id = marker["marker-id"].AsInt32();
				var corners = marker["corners"].AsGodotDictionary<string, Dictionary<string, float>>();
				Vector2 pos0 = new Vector2(corners["1"]["x"], corners["1"]["y"]);
				Vector2 pos1 = new Vector2(corners["2"]["x"], corners["2"]["y"]);
				Vector2 pos2 = new Vector2(corners["3"]["x"], corners["3"]["y"]);
				Vector2 pos3 = new Vector2(corners["4"]["x"], corners["4"]["y"]);
				var (pos, a) = CalculatePosAndAngle(pos0, pos1, pos2, pos3);
				foreach (var player in players) {
					if (player.id == id) {
						player.UpdatePos(new Vector3(pos.X, 0.194f, pos.Y), a);
						break;
					}
				}
			}
			return;
		}
		if (msg["ball"].VariantType == Variant.Type.String) return;
		var ball_pos = msg["ball"].AsGodotArray<Dictionary<string, float>>();
		ball.UpdateBallPos(new Vector3(ball_pos[0]["x"], 0.25f, ball_pos[0]["y"]));
	}
	void AddRobot(Game.Player player, Teams team) {
		var robot = robotScene.Instantiate<Robot>();
		robot.id = player.id;
		robot.name = player.name;
		robot.team = team;
		GetParent().AddChild(robot);

		if (robot.team == Teams.team1) {
			robot.AddChild(blueRobotBody.Instantiate());
			robot.UpdatePos(initialPositionsTeam1[0], teamsInitialRotations[0]);
		}
		else if (robot.team == Teams.team2) {
			robot.AddChild(redRobotBody.Instantiate());
			robot.UpdatePos(initialPositionsTeam2[0], teamsInitialRotations[1]);
		}

		if (robot.name == username) {
			var camScene = ResourceLoader.Load<PackedScene>("res://scenes/ClippedCamera.tscn");
			var cam = camScene.Instantiate();
			robot.AddChild(cam);
		}

		players.Add(robot);
	}
	void SetupCommands() {
		var commandsConfig = new ConfigFile();
		commandsConfig.Load("res://cfg/commands.cfg");

		f_cmd = commandsConfig.GetValue("commands", "f").AsString();
		b_cmd = commandsConfig.GetValue("commands", "b").AsString();
		l_cmd = commandsConfig.GetValue("commands", "l").AsString();
		r_cmd = commandsConfig.GetValue("commands", "r").AsString();
	}
	void AddBindings() {
		Keyboard.forward += () => mqtt.Publish($"{gameName}/{username}", f_cmd);
		Keyboard.back += () => mqtt.Publish($"{gameName}/{username}", b_cmd);
		Keyboard.left += () => mqtt.Publish($"{gameName}/{username}", l_cmd);
		Keyboard.right += () => mqtt.Publish($"{gameName}/{username}", r_cmd);
	}
	(Vector2, float) CalculatePosAndAngle(Vector2 pos0, Vector2 pos1, Vector2 pos2, Vector2 pos3) {
		Vector2 dir = (pos0 + pos1) / 2;
		Vector2 pos = (pos0 + pos1 + pos2 + pos3) / 4;
		float a = Mathf.Atan2(dir.Y - pos.Y, dir.X - pos.X) - Mathf.Pi/2;
		return (pos, a);
	}
}
