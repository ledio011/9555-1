using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003B5 RID: 949
	public class game_check
	{
		// Token: 0x020003B6 RID: 950
		public class request : SprotoTypeBase
		{
			// Token: 0x06001CBF RID: 7359 RVA: 0x00096DBC File Offset: 0x00094FBC
			public request() : base(game_check.request.max_field_count)
			{
			}

			// Token: 0x06001CC0 RID: 7360 RVA: 0x00096DCC File Offset: 0x00094FCC
			public request(byte[] buffer) : base(game_check.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700075B RID: 1883
			// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00096DE8 File Offset: 0x00094FE8
			// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x00096DF0 File Offset: 0x00094FF0
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

			// Token: 0x1700075C RID: 1884
			// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00096E08 File Offset: 0x00095008
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001CC5 RID: 7365 RVA: 0x00096E18 File Offset: 0x00095018
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

			// Token: 0x06001CC6 RID: 7366 RVA: 0x00096E74 File Offset: 0x00095074
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A6D RID: 6765
			private static int max_field_count = 1;

			// Token: 0x04001A6E RID: 6766
			private long _type;
		}
	}
}
