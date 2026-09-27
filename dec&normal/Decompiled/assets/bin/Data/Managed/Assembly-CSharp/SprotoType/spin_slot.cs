using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005E9 RID: 1513
	public class spin_slot
	{
		// Token: 0x020005EA RID: 1514
		public class request : SprotoTypeBase
		{
			// Token: 0x06002BF3 RID: 11251 RVA: 0x000B4F10 File Offset: 0x000B3110
			public request() : base(spin_slot.request.max_field_count)
			{
			}

			// Token: 0x06002BF4 RID: 11252 RVA: 0x000B4F20 File Offset: 0x000B3120
			public request(byte[] buffer) : base(spin_slot.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000CC3 RID: 3267
			// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x000B4F3C File Offset: 0x000B313C
			// (set) Token: 0x06002BF7 RID: 11255 RVA: 0x000B4F44 File Offset: 0x000B3144
			public string ID
			{
				get
				{
					return this._ID;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._ID = value;
				}
			}

			// Token: 0x17000CC4 RID: 3268
			// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x000B4F5C File Offset: 0x000B315C
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002BF9 RID: 11257 RVA: 0x000B4F6C File Offset: 0x000B316C
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
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002BFA RID: 11258 RVA: 0x000B4FC8 File Offset: 0x000B31C8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E7B RID: 7803
			private static int max_field_count = 1;

			// Token: 0x04001E7C RID: 7804
			private string _ID;
		}
	}
}
