using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000385 RID: 901
	public class enter_guild_boss_scene
	{
		// Token: 0x02000386 RID: 902
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B2A RID: 6954 RVA: 0x000939F8 File Offset: 0x00091BF8
			public request() : base(enter_guild_boss_scene.request.max_field_count)
			{
			}

			// Token: 0x06001B2B RID: 6955 RVA: 0x00093A08 File Offset: 0x00091C08
			public request(byte[] buffer) : base(enter_guild_boss_scene.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006B5 RID: 1717
			// (get) Token: 0x06001B2D RID: 6957 RVA: 0x00093A24 File Offset: 0x00091C24
			// (set) Token: 0x06001B2E RID: 6958 RVA: 0x00093A2C File Offset: 0x00091C2C
			public string id
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

			// Token: 0x170006B6 RID: 1718
			// (get) Token: 0x06001B2F RID: 6959 RVA: 0x00093A44 File Offset: 0x00091C44
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001B30 RID: 6960 RVA: 0x00093A54 File Offset: 0x00091C54
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.id = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001B31 RID: 6961 RVA: 0x00093AB0 File Offset: 0x00091CB0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040019FF RID: 6655
			private static int max_field_count = 1;

			// Token: 0x04001A00 RID: 6656
			private string _id;
		}
	}
}
