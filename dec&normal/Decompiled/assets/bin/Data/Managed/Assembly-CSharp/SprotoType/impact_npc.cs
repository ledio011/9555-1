using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003F8 RID: 1016
	public class impact_npc
	{
		// Token: 0x020003F9 RID: 1017
		public class request : SprotoTypeBase
		{
			// Token: 0x06001F78 RID: 8056 RVA: 0x0009C8A0 File Offset: 0x0009AAA0
			public request() : base(impact_npc.request.max_field_count)
			{
			}

			// Token: 0x06001F79 RID: 8057 RVA: 0x0009C8B0 File Offset: 0x0009AAB0
			public request(byte[] buffer) : base(impact_npc.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700088F RID: 2191
			// (get) Token: 0x06001F7B RID: 8059 RVA: 0x0009C8CC File Offset: 0x0009AACC
			// (set) Token: 0x06001F7C RID: 8060 RVA: 0x0009C8D4 File Offset: 0x0009AAD4
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

			// Token: 0x17000890 RID: 2192
			// (get) Token: 0x06001F7D RID: 8061 RVA: 0x0009C8EC File Offset: 0x0009AAEC
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001F7E RID: 8062 RVA: 0x0009C8FC File Offset: 0x0009AAFC
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
						this.type = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001F7F RID: 8063 RVA: 0x0009C958 File Offset: 0x0009AB58
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B34 RID: 6964
			private static int max_field_count = 1;

			// Token: 0x04001B35 RID: 6965
			private long _type;
		}
	}
}
