using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005F7 RID: 1527
	public class survive_battle_finish
	{
		// Token: 0x020005F8 RID: 1528
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C30 RID: 11312 RVA: 0x000B55CC File Offset: 0x000B37CC
			public request() : base(survive_battle_finish.request.max_field_count)
			{
			}

			// Token: 0x06002C31 RID: 11313 RVA: 0x000B55DC File Offset: 0x000B37DC
			public request(byte[] buffer) : base(survive_battle_finish.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000CCF RID: 3279
			// (get) Token: 0x06002C33 RID: 11315 RVA: 0x000B55F8 File Offset: 0x000B37F8
			// (set) Token: 0x06002C34 RID: 11316 RVA: 0x000B5600 File Offset: 0x000B3800
			public string info
			{
				get
				{
					return this._info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._info = value;
				}
			}

			// Token: 0x17000CD0 RID: 3280
			// (get) Token: 0x06002C35 RID: 11317 RVA: 0x000B5618 File Offset: 0x000B3818
			public bool HasInfo
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002C36 RID: 11318 RVA: 0x000B5628 File Offset: 0x000B3828
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
						this.info = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002C37 RID: 11319 RVA: 0x000B5684 File Offset: 0x000B3884
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E89 RID: 7817
			private static int max_field_count = 1;

			// Token: 0x04001E8A RID: 7818
			private string _info;
		}
	}
}
