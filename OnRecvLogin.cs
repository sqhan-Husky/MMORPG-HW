using Common;
using Backend.Game;
using Npgsql;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvLogin(IChannel channel, Message message)
        {
            CLogin request = message as CLogin;
			SPlayerEnter response = new SPlayerEnter();
			string scene = "Level1";
			response.user = request.user;
			response.token = request.user;
			response.scene = scene;

			Console.WriteLine("connecting...");
			string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=meteor0622;Database=Game;";
			NpgsqlConnection conn = new NpgsqlConnection(connectionString);
			conn.Open();
			Console.WriteLine("connected");
			string sql = string.Format("SELECT passward FROM player WHERE pname = '{0}'", request.user);
			var cmd = new NpgsqlCommand(sql, conn);
			var reader = cmd.ExecuteReader();
			reader.Read();
			Console.WriteLine("Finish Read");
			Console.WriteLine(reader.GetString(0));
	
			if(reader.GetString(0) == null) { ClientTipInfo(channel, "No player!"); }     // no player has a little problem
			if ( Equals(reader.GetString(0),request.password))
			{
				Console.WriteLine("success");
				channel.Send(response);

			}
			else 
			{
				Console.WriteLine("failed");
				ClientTipInfo(channel, "Wrong Password!");
			}
			conn.Close();

			Player player = new Player(channel);
			player.scene = scene;
			// TODO read from database
			DEntity dentity = World.Instance.EntityData["Ellen"];
			player.FromDEntity(dentity);
			player.forClone = false;
			//ClientTipInfo(channel, "TODO: get player's attribute from database");
			// player will be added to scene when receive client's CEnterSceneDone message




        }
    }
}
