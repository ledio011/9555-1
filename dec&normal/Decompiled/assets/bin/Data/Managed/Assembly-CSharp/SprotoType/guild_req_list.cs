using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003ED RID: 1005
	public class guild_req_list
	{
		// Token: 0x020003EE RID: 1006
		public class request : SprotoTypeBase
		{
			// Token: 0x06001F27 RID: 7975 RVA: 0x0009BE70 File Offset: 0x0009A070
			public request() : base(guild_req_list.request.max_field_count)
			{
			}

			// Token: 0x06001F28 RID: 7976 RVA: 0x0009BE80 File Offset: 0x0009A080
			public request(byte[] buffer) : base(guild_req_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000873 RID: 2163
			// (get) Token: 0x06001F2A RID: 7978 RVA: 0x0009BE9C File Offset: 0x0009A09C
			// (set) Token: 0x06001F2B RID: 7979 RVA: 0x0009BEA4 File Offset: 0x0009A0A4
			public long characterId
			{
				get
				{
					return this._characterId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._characterId = value;
				}
			}

			// Token: 0x17000874 RID: 2164
			// (get) Token: 0x06001F2C RID: 7980 RVA: 0x0009BEBC File Offset: 0x0009A0BC
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0009BECC File Offset: 0x0009A0CC
			// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0009BED4 File Offset: 0x0009A0D4
			public long curPage
			{
				get
				{
					return this._curPage;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._curPage = value;
				}
			}

			// Token: 0x17000876 RID: 2166
			// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0009BEEC File Offset: 0x0009A0EC
			public bool HasCurPage
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001F30 RID: 7984 RVA: 0x0009BEFC File Offset: 0x0009A0FC
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
							this.curPage = this.deserialize.read_integer();
						}
					}
					else
					{
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001F31 RID: 7985 RVA: 0x0009BF74 File Offset: 0x0009A174
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.curPage, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B1F RID: 6943
			private static int max_field_count = 2;

			// Token: 0x04001B20 RID: 6944
			private long _characterId;

			// Token: 0x04001B21 RID: 6945
			private long _curPage;
		}
	}
}
