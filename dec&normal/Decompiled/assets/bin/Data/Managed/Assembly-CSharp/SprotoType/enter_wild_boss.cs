using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000399 RID: 921
	public class enter_wild_boss
	{
		// Token: 0x0200039A RID: 922
		public class request : SprotoTypeBase
		{
			// Token: 0x06001BA2 RID: 7074 RVA: 0x000948A0 File Offset: 0x00092AA0
			public request() : base(enter_wild_boss.request.max_field_count)
			{
			}

			// Token: 0x06001BA3 RID: 7075 RVA: 0x000948B0 File Offset: 0x00092AB0
			public request(byte[] buffer) : base(enter_wild_boss.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006DD RID: 1757
			// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x000948CC File Offset: 0x00092ACC
			// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x000948D4 File Offset: 0x00092AD4
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

			// Token: 0x170006DE RID: 1758
			// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x000948EC File Offset: 0x00092AEC
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001BA8 RID: 7080 RVA: 0x000948FC File Offset: 0x00092AFC
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

			// Token: 0x06001BA9 RID: 7081 RVA: 0x00094958 File Offset: 0x00092B58
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A1D RID: 6685
			private static int max_field_count = 1;

			// Token: 0x04001A1E RID: 6686
			private string _ID;
		}
	}
}
