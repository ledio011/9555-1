using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000587 RID: 1415
	public class ret_request_wild_boss_info
	{
		// Token: 0x02000588 RID: 1416
		public class request : SprotoTypeBase
		{
			// Token: 0x06002908 RID: 10504 RVA: 0x000AF0A4 File Offset: 0x000AD2A4
			public request() : base(ret_request_wild_boss_info.request.max_field_count)
			{
			}

			// Token: 0x06002909 RID: 10505 RVA: 0x000AF0B4 File Offset: 0x000AD2B4
			public request(byte[] buffer) : base(ret_request_wild_boss_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BA7 RID: 2983
			// (get) Token: 0x0600290B RID: 10507 RVA: 0x000AF0D0 File Offset: 0x000AD2D0
			// (set) Token: 0x0600290C RID: 10508 RVA: 0x000AF0D8 File Offset: 0x000AD2D8
			public Dictionary<string, activity_info> activity_info
			{
				get
				{
					return this._activity_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._activity_info = value;
				}
			}

			// Token: 0x17000BA8 RID: 2984
			// (get) Token: 0x0600290D RID: 10509 RVA: 0x000AF0F0 File Offset: 0x000AD2F0
			public bool HasActivity_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600290E RID: 10510 RVA: 0x000AF100 File Offset: 0x000AD300
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
						this.activity_info = this.deserialize.read_map<string, activity_info>((activity_info v) => v.ID);
					}
				}
			}

			// Token: 0x0600290F RID: 10511 RVA: 0x000AF178 File Offset: 0x000AD378
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, activity_info>(this.activity_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DB0 RID: 7600
			private static int max_field_count = 1;

			// Token: 0x04001DB1 RID: 7601
			private Dictionary<string, activity_info> _activity_info;
		}
	}
}
