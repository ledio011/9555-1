using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200040B RID: 1035
	public class login
	{
		// Token: 0x0200040C RID: 1036
		public class request : SprotoTypeBase
		{
			// Token: 0x06002009 RID: 8201 RVA: 0x0009DACC File Offset: 0x0009BCCC
			public request() : base(login.request.max_field_count)
			{
			}

			// Token: 0x0600200A RID: 8202 RVA: 0x0009DADC File Offset: 0x0009BCDC
			public request(byte[] buffer) : base(login.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170008C3 RID: 2243
			// (get) Token: 0x0600200C RID: 8204 RVA: 0x0009DAF8 File Offset: 0x0009BCF8
			// (set) Token: 0x0600200D RID: 8205 RVA: 0x0009DB00 File Offset: 0x0009BD00
			public long session
			{
				get
				{
					return this._session;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._session = value;
				}
			}

			// Token: 0x170008C4 RID: 2244
			// (get) Token: 0x0600200E RID: 8206 RVA: 0x0009DB18 File Offset: 0x0009BD18
			public bool HasSession
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170008C5 RID: 2245
			// (get) Token: 0x0600200F RID: 8207 RVA: 0x0009DB28 File Offset: 0x0009BD28
			// (set) Token: 0x06002010 RID: 8208 RVA: 0x0009DB30 File Offset: 0x0009BD30
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._id = value;
				}
			}

			// Token: 0x170008C6 RID: 2246
			// (get) Token: 0x06002011 RID: 8209 RVA: 0x0009DB48 File Offset: 0x0009BD48
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x06002012 RID: 8210 RVA: 0x0009DB58 File Offset: 0x0009BD58
			// (set) Token: 0x06002013 RID: 8211 RVA: 0x0009DB60 File Offset: 0x0009BD60
			public long logintype
			{
				get
				{
					return this._logintype;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._logintype = value;
				}
			}

			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x06002014 RID: 8212 RVA: 0x0009DB78 File Offset: 0x0009BD78
			public bool HasLogintype
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x06002015 RID: 8213 RVA: 0x0009DB88 File Offset: 0x0009BD88
			// (set) Token: 0x06002016 RID: 8214 RVA: 0x0009DB90 File Offset: 0x0009BD90
			public string version
			{
				get
				{
					return this._version;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._version = value;
				}
			}

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x06002017 RID: 8215 RVA: 0x0009DBA8 File Offset: 0x0009BDA8
			public bool HasVersion
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x06002018 RID: 8216 RVA: 0x0009DBB8 File Offset: 0x0009BDB8
			// (set) Token: 0x06002019 RID: 8217 RVA: 0x0009DBC0 File Offset: 0x0009BDC0
			public string unityVersion
			{
				get
				{
					return this._unityVersion;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._unityVersion = value;
				}
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x0600201A RID: 8218 RVA: 0x0009DBD8 File Offset: 0x0009BDD8
			public bool HasUnityVersion
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x0600201B RID: 8219 RVA: 0x0009DBE8 File Offset: 0x0009BDE8
			// (set) Token: 0x0600201C RID: 8220 RVA: 0x0009DBF0 File Offset: 0x0009BDF0
			public long serverId
			{
				get
				{
					return this._serverId;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._serverId = value;
				}
			}

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x0600201D RID: 8221 RVA: 0x0009DC08 File Offset: 0x0009BE08
			public bool HasServerId
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x0600201E RID: 8222 RVA: 0x0009DC18 File Offset: 0x0009BE18
			// (set) Token: 0x0600201F RID: 8223 RVA: 0x0009DC20 File Offset: 0x0009BE20
			public long time
			{
				get
				{
					return this._time;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._time = value;
				}
			}

			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x06002020 RID: 8224 RVA: 0x0009DC38 File Offset: 0x0009BE38
			public bool HasTime
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x06002021 RID: 8225 RVA: 0x0009DC48 File Offset: 0x0009BE48
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.session = this.deserialize.read_integer();
						break;
					case 1:
						this.id = this.deserialize.read_string();
						break;
					case 2:
						this.logintype = this.deserialize.read_integer();
						break;
					case 3:
						this.version = this.deserialize.read_string();
						break;
					case 4:
						this.unityVersion = this.deserialize.read_string();
						break;
					case 5:
						this.serverId = this.deserialize.read_integer();
						break;
					case 6:
						this.time = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002022 RID: 8226 RVA: 0x0009DD44 File Offset: 0x0009BF44
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.session, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.id, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.logintype, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_string(this.version, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_string(this.unityVersion, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.serverId, 5);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_integer(this.time, 6);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B5A RID: 7002
			private static int max_field_count = 7;

			// Token: 0x04001B5B RID: 7003
			private long _session;

			// Token: 0x04001B5C RID: 7004
			private string _id;

			// Token: 0x04001B5D RID: 7005
			private long _logintype;

			// Token: 0x04001B5E RID: 7006
			private string _version;

			// Token: 0x04001B5F RID: 7007
			private string _unityVersion;

			// Token: 0x04001B60 RID: 7008
			private long _serverId;

			// Token: 0x04001B61 RID: 7009
			private long _time;
		}

		// Token: 0x0200040D RID: 1037
		public class response : SprotoTypeBase
		{
			// Token: 0x06002023 RID: 8227 RVA: 0x0009DE60 File Offset: 0x0009C060
			public response() : base(login.response.max_field_count)
			{
			}

			// Token: 0x06002024 RID: 8228 RVA: 0x0009DE70 File Offset: 0x0009C070
			public response(byte[] buffer) : base(login.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170008D1 RID: 2257
			// (get) Token: 0x06002026 RID: 8230 RVA: 0x0009DE8C File Offset: 0x0009C08C
			// (set) Token: 0x06002027 RID: 8231 RVA: 0x0009DE94 File Offset: 0x0009C094
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._type = value;
				}
			}

			// Token: 0x170008D2 RID: 2258
			// (get) Token: 0x06002028 RID: 8232 RVA: 0x0009DEAC File Offset: 0x0009C0AC
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170008D3 RID: 2259
			// (get) Token: 0x06002029 RID: 8233 RVA: 0x0009DEBC File Offset: 0x0009C0BC
			// (set) Token: 0x0600202A RID: 8234 RVA: 0x0009DEC4 File Offset: 0x0009C0C4
			public string versionCode
			{
				get
				{
					return this._versionCode;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._versionCode = value;
				}
			}

			// Token: 0x170008D4 RID: 2260
			// (get) Token: 0x0600202B RID: 8235 RVA: 0x0009DEDC File Offset: 0x0009C0DC
			public bool HasVersionCode
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170008D5 RID: 2261
			// (get) Token: 0x0600202C RID: 8236 RVA: 0x0009DEEC File Offset: 0x0009C0EC
			// (set) Token: 0x0600202D RID: 8237 RVA: 0x0009DEF4 File Offset: 0x0009C0F4
			public string dataVersionCode
			{
				get
				{
					return this._dataVersionCode;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._dataVersionCode = value;
				}
			}

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x0600202E RID: 8238 RVA: 0x0009DF0C File Offset: 0x0009C10C
			public bool HasDataVersionCode
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170008D7 RID: 2263
			// (get) Token: 0x0600202F RID: 8239 RVA: 0x0009DF1C File Offset: 0x0009C11C
			// (set) Token: 0x06002030 RID: 8240 RVA: 0x0009DF24 File Offset: 0x0009C124
			public long serverLevel
			{
				get
				{
					return this._serverLevel;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._serverLevel = value;
				}
			}

			// Token: 0x170008D8 RID: 2264
			// (get) Token: 0x06002031 RID: 8241 RVA: 0x0009DF3C File Offset: 0x0009C13C
			public bool HasServerLevel
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06002032 RID: 8242 RVA: 0x0009DF4C File Offset: 0x0009C14C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.type = this.deserialize.read_integer();
						break;
					case 1:
						this.versionCode = this.deserialize.read_string();
						break;
					case 2:
						this.dataVersionCode = this.deserialize.read_string();
						break;
					case 3:
						this.serverLevel = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002033 RID: 8243 RVA: 0x0009DFF8 File Offset: 0x0009C1F8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.versionCode, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.dataVersionCode, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.serverLevel, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B62 RID: 7010
			private static int max_field_count = 4;

			// Token: 0x04001B63 RID: 7011
			private long _type;

			// Token: 0x04001B64 RID: 7012
			private string _versionCode;

			// Token: 0x04001B65 RID: 7013
			private string _dataVersionCode;

			// Token: 0x04001B66 RID: 7014
			private long _serverLevel;
		}
	}
}
