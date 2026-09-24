using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000443 RID: 1091
	public class play_social_dance
	{
		// Token: 0x02000444 RID: 1092
		public class request : SprotoTypeBase
		{
			// Token: 0x060021FD RID: 8701 RVA: 0x000A1B18 File Offset: 0x0009FD18
			public request() : base(play_social_dance.request.max_field_count)
			{
			}

			// Token: 0x060021FE RID: 8702 RVA: 0x000A1B28 File Offset: 0x0009FD28
			public request(byte[] buffer) : base(play_social_dance.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000997 RID: 2455
			// (get) Token: 0x06002200 RID: 8704 RVA: 0x000A1B44 File Offset: 0x0009FD44
			// (set) Token: 0x06002201 RID: 8705 RVA: 0x000A1B4C File Offset: 0x0009FD4C
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000998 RID: 2456
			// (get) Token: 0x06002202 RID: 8706 RVA: 0x000A1B64 File Offset: 0x0009FD64
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000999 RID: 2457
			// (get) Token: 0x06002203 RID: 8707 RVA: 0x000A1B74 File Offset: 0x0009FD74
			// (set) Token: 0x06002204 RID: 8708 RVA: 0x000A1B7C File Offset: 0x0009FD7C
			public string danceId
			{
				get
				{
					return this._danceId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._danceId = value;
				}
			}

			// Token: 0x1700099A RID: 2458
			// (get) Token: 0x06002205 RID: 8709 RVA: 0x000A1B94 File Offset: 0x0009FD94
			public bool HasDanceId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002206 RID: 8710 RVA: 0x000A1BA4 File Offset: 0x0009FDA4
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.danceId = this.deserialize.read_string();
						}
					}
					else
					{
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002207 RID: 8711 RVA: 0x000A1C1C File Offset: 0x0009FE1C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.danceId, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BE5 RID: 7141
			private static int max_field_count = 2;

			// Token: 0x04001BE6 RID: 7142
			private long _id;

			// Token: 0x04001BE7 RID: 7143
			private string _danceId;
		}
	}
}
