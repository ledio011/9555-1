using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003B7 RID: 951
	public class game_server : SprotoTypeBase
	{
		// Token: 0x06001CC7 RID: 7367 RVA: 0x00096EBC File Offset: 0x000950BC
		public game_server() : base(game_server.max_field_count)
		{
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00096ECC File Offset: 0x000950CC
		public game_server(byte[] buffer) : base(game_server.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x00096EEC File Offset: 0x000950EC
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x00096EF4 File Offset: 0x000950F4
		public long serverId
		{
			get
			{
				return this._serverId;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._serverId = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x00096F0C File Offset: 0x0009510C
		public bool HasServerId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x00096F1C File Offset: 0x0009511C
		// (set) Token: 0x06001CCE RID: 7374 RVA: 0x00096F24 File Offset: 0x00095124
		public string serverName
		{
			get
			{
				return this._serverName;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._serverName = value;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x00096F3C File Offset: 0x0009513C
		public bool HasServerName
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00096F4C File Offset: 0x0009514C
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x00096F54 File Offset: 0x00095154
		public string serverIP
		{
			get
			{
				return this._serverIP;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._serverIP = value;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00096F6C File Offset: 0x0009516C
		public bool HasServerIP
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00096F7C File Offset: 0x0009517C
		// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x00096F84 File Offset: 0x00095184
		public long serverPort
		{
			get
			{
				return this._serverPort;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._serverPort = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x00096F9C File Offset: 0x0009519C
		public bool HasServerPort
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00096FAC File Offset: 0x000951AC
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x00096FB4 File Offset: 0x000951B4
		public long serverState
		{
			get
			{
				return this._serverState;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._serverState = value;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00096FCC File Offset: 0x000951CC
		public bool HasServerState
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x00096FDC File Offset: 0x000951DC
		// (set) Token: 0x06001CDA RID: 7386 RVA: 0x00096FE4 File Offset: 0x000951E4
		public long serverPlayerState
		{
			get
			{
				return this._serverPlayerState;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._serverPlayerState = value;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x00096FFC File Offset: 0x000951FC
		public bool HasServerPlayerState
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x0009700C File Offset: 0x0009520C
		// (set) Token: 0x06001CDD RID: 7389 RVA: 0x00097014 File Offset: 0x00095214
		public long serverArea
		{
			get
			{
				return this._serverArea;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._serverArea = value;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x0009702C File Offset: 0x0009522C
		public bool HasServerArea
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0009703C File Offset: 0x0009523C
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x00097044 File Offset: 0x00095244
		public long serverRank
		{
			get
			{
				return this._serverRank;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._serverRank = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x0009705C File Offset: 0x0009525C
		public bool HasServerRank
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0009706C File Offset: 0x0009526C
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x00097074 File Offset: 0x00095274
		public long serverTimeZone
		{
			get
			{
				return this._serverTimeZone;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._serverTimeZone = value;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0009708C File Offset: 0x0009528C
		public bool HasServerTimeZone
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x0009709C File Offset: 0x0009529C
		// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x000970A4 File Offset: 0x000952A4
		public long serverWeight
		{
			get
			{
				return this._serverWeight;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._serverWeight = value;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x000970BC File Offset: 0x000952BC
		public bool HasServerWeight
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x000970CC File Offset: 0x000952CC
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x000970D4 File Offset: 0x000952D4
		public long newServer
		{
			get
			{
				return this._newServer;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._newServer = value;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x000970EC File Offset: 0x000952EC
		public bool HasNewServer
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000970FC File Offset: 0x000952FC
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.serverId = this.deserialize.read_integer();
					break;
				case 1:
					this.serverName = this.deserialize.read_string();
					break;
				case 2:
					this.serverIP = this.deserialize.read_string();
					break;
				case 3:
					this.serverPort = this.deserialize.read_integer();
					break;
				case 4:
					this.serverState = this.deserialize.read_integer();
					break;
				case 5:
					this.serverPlayerState = this.deserialize.read_integer();
					break;
				case 6:
					this.serverArea = this.deserialize.read_integer();
					break;
				case 7:
					this.serverRank = this.deserialize.read_integer();
					break;
				case 8:
					this.serverTimeZone = this.deserialize.read_integer();
					break;
				case 9:
					this.serverWeight = this.deserialize.read_integer();
					break;
				case 10:
					this.newServer = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x00097260 File Offset: 0x00095460
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.serverId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.serverName, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_string(this.serverIP, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.serverPort, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.serverState, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.serverPlayerState, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.serverArea, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.serverRank, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.serverTimeZone, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.serverWeight, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.newServer, 10);
			}
			return this.serialize.close();
		}

		// Token: 0x04001A6F RID: 6767
		private static int max_field_count = 11;

		// Token: 0x04001A70 RID: 6768
		private long _serverId;

		// Token: 0x04001A71 RID: 6769
		private string _serverName;

		// Token: 0x04001A72 RID: 6770
		private string _serverIP;

		// Token: 0x04001A73 RID: 6771
		private long _serverPort;

		// Token: 0x04001A74 RID: 6772
		private long _serverState;

		// Token: 0x04001A75 RID: 6773
		private long _serverPlayerState;

		// Token: 0x04001A76 RID: 6774
		private long _serverArea;

		// Token: 0x04001A77 RID: 6775
		private long _serverRank;

		// Token: 0x04001A78 RID: 6776
		private long _serverTimeZone;

		// Token: 0x04001A79 RID: 6777
		private long _serverWeight;

		// Token: 0x04001A7A RID: 6778
		private long _newServer;
	}
}
