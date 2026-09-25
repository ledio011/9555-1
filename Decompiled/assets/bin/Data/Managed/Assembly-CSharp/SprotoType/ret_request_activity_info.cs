using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000563 RID: 1379
	public class ret_request_activity_info
	{
		// Token: 0x02000564 RID: 1380
		public class request : SprotoTypeBase
		{
			// Token: 0x06002813 RID: 10259 RVA: 0x000AD21C File Offset: 0x000AB41C
			public request() : base(ret_request_activity_info.request.max_field_count)
			{
			}

			// Token: 0x06002814 RID: 10260 RVA: 0x000AD22C File Offset: 0x000AB42C
			public request(byte[] buffer) : base(ret_request_activity_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B55 RID: 2901
			// (get) Token: 0x06002816 RID: 10262 RVA: 0x000AD248 File Offset: 0x000AB448
			// (set) Token: 0x06002817 RID: 10263 RVA: 0x000AD250 File Offset: 0x000AB450
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

			// Token: 0x17000B56 RID: 2902
			// (get) Token: 0x06002818 RID: 10264 RVA: 0x000AD268 File Offset: 0x000AB468
			public bool HasActivity_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002819 RID: 10265 RVA: 0x000AD278 File Offset: 0x000AB478
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

			// Token: 0x0600281A RID: 10266 RVA: 0x000AD2F0 File Offset: 0x000AB4F0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, activity_info>(this.activity_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D67 RID: 7527
			private static int max_field_count = 1;

			// Token: 0x04001D68 RID: 7528
			private Dictionary<string, activity_info> _activity_info;
		}
	}
}
