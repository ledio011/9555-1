using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000466 RID: 1126
	public class refresh_online_state
	{
		// Token: 0x02000467 RID: 1127
		public class request : SprotoTypeBase
		{
			// Token: 0x060022C9 RID: 8905 RVA: 0x000A3360 File Offset: 0x000A1560
			public request() : base(refresh_online_state.request.max_field_count)
			{
			}

			// Token: 0x060022CA RID: 8906 RVA: 0x000A3370 File Offset: 0x000A1570
			public request(byte[] buffer) : base(refresh_online_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009D5 RID: 2517
			// (get) Token: 0x060022CC RID: 8908 RVA: 0x000A338C File Offset: 0x000A158C
			// (set) Token: 0x060022CD RID: 8909 RVA: 0x000A3394 File Offset: 0x000A1594
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

			// Token: 0x170009D6 RID: 2518
			// (get) Token: 0x060022CE RID: 8910 RVA: 0x000A33AC File Offset: 0x000A15AC
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170009D7 RID: 2519
			// (get) Token: 0x060022CF RID: 8911 RVA: 0x000A33BC File Offset: 0x000A15BC
			// (set) Token: 0x060022D0 RID: 8912 RVA: 0x000A33C4 File Offset: 0x000A15C4
			public long id
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

			// Token: 0x170009D8 RID: 2520
			// (get) Token: 0x060022D1 RID: 8913 RVA: 0x000A33DC File Offset: 0x000A15DC
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170009D9 RID: 2521
			// (get) Token: 0x060022D2 RID: 8914 RVA: 0x000A33EC File Offset: 0x000A15EC
			// (set) Token: 0x060022D3 RID: 8915 RVA: 0x000A33F4 File Offset: 0x000A15F4
			public string mapId
			{
				get
				{
					return this._mapId;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._mapId = value;
				}
			}

			// Token: 0x170009DA RID: 2522
			// (get) Token: 0x060022D4 RID: 8916 RVA: 0x000A340C File Offset: 0x000A160C
			public bool HasMapId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060022D5 RID: 8917 RVA: 0x000A341C File Offset: 0x000A161C
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
						this.id = this.deserialize.read_integer();
						break;
					case 2:
						this.mapId = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060022D6 RID: 8918 RVA: 0x000A34B0 File Offset: 0x000A16B0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.id, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.mapId, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C17 RID: 7191
			private static int max_field_count = 3;

			// Token: 0x04001C18 RID: 7192
			private long _type;

			// Token: 0x04001C19 RID: 7193
			private long _id;

			// Token: 0x04001C1A RID: 7194
			private string _mapId;
		}
	}
}
