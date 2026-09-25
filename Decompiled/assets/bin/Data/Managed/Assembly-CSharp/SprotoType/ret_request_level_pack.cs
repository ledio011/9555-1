using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000575 RID: 1397
	public class ret_request_level_pack
	{
		// Token: 0x02000576 RID: 1398
		public class request : SprotoTypeBase
		{
			// Token: 0x06002892 RID: 10386 RVA: 0x000AE200 File Offset: 0x000AC400
			public request() : base(ret_request_level_pack.request.max_field_count)
			{
			}

			// Token: 0x06002893 RID: 10387 RVA: 0x000AE210 File Offset: 0x000AC410
			public request(byte[] buffer) : base(ret_request_level_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B7F RID: 2943
			// (get) Token: 0x06002895 RID: 10389 RVA: 0x000AE22C File Offset: 0x000AC42C
			// (set) Token: 0x06002896 RID: 10390 RVA: 0x000AE234 File Offset: 0x000AC434
			public Dictionary<string, level_pack> level_pack
			{
				get
				{
					return this._level_pack;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._level_pack = value;
				}
			}

			// Token: 0x17000B80 RID: 2944
			// (get) Token: 0x06002897 RID: 10391 RVA: 0x000AE24C File Offset: 0x000AC44C
			public bool HasLevel_pack
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002898 RID: 10392 RVA: 0x000AE25C File Offset: 0x000AC45C
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
						this.level_pack = this.deserialize.read_map<string, level_pack>((level_pack v) => v.ID);
					}
				}
			}

			// Token: 0x06002899 RID: 10393 RVA: 0x000AE2D4 File Offset: 0x000AC4D4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, level_pack>(this.level_pack, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D8F RID: 7567
			private static int max_field_count = 1;

			// Token: 0x04001D90 RID: 7568
			private Dictionary<string, level_pack> _level_pack;
		}
	}
}
