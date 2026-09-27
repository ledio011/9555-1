using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000387 RID: 903
	public class enter_guild_city_scene
	{
		// Token: 0x02000388 RID: 904
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B33 RID: 6963 RVA: 0x00093B00 File Offset: 0x00091D00
			public request() : base(enter_guild_city_scene.request.max_field_count)
			{
			}

			// Token: 0x06001B34 RID: 6964 RVA: 0x00093B10 File Offset: 0x00091D10
			public request(byte[] buffer) : base(enter_guild_city_scene.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006B7 RID: 1719
			// (get) Token: 0x06001B36 RID: 6966 RVA: 0x00093B2C File Offset: 0x00091D2C
			// (set) Token: 0x06001B37 RID: 6967 RVA: 0x00093B34 File Offset: 0x00091D34
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

			// Token: 0x170006B8 RID: 1720
			// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00093B4C File Offset: 0x00091D4C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001B39 RID: 6969 RVA: 0x00093B5C File Offset: 0x00091D5C
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

			// Token: 0x06001B3A RID: 6970 RVA: 0x00093BB8 File Offset: 0x00091DB8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A01 RID: 6657
			private static int max_field_count = 1;

			// Token: 0x04001A02 RID: 6658
			private string _id;
		}
	}
}
