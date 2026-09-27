using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000631 RID: 1585
	public class unlock_function_complete
	{
		// Token: 0x02000632 RID: 1586
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E17 RID: 11799 RVA: 0x000B93F0 File Offset: 0x000B75F0
			public request() : base(unlock_function_complete.request.max_field_count)
			{
			}

			// Token: 0x06002E18 RID: 11800 RVA: 0x000B9400 File Offset: 0x000B7600
			public request(byte[] buffer) : base(unlock_function_complete.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D91 RID: 3473
			// (get) Token: 0x06002E1A RID: 11802 RVA: 0x000B941C File Offset: 0x000B761C
			// (set) Token: 0x06002E1B RID: 11803 RVA: 0x000B9424 File Offset: 0x000B7624
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

			// Token: 0x17000D92 RID: 3474
			// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000B943C File Offset: 0x000B763C
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000D93 RID: 3475
			// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000B944C File Offset: 0x000B764C
			// (set) Token: 0x06002E1E RID: 11806 RVA: 0x000B9454 File Offset: 0x000B7654
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._state = value;
				}
			}

			// Token: 0x17000D94 RID: 3476
			// (get) Token: 0x06002E1F RID: 11807 RVA: 0x000B946C File Offset: 0x000B766C
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002E20 RID: 11808 RVA: 0x000B947C File Offset: 0x000B767C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.state = this.deserialize.read_integer();
						}
					}
					else
					{
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002E21 RID: 11809 RVA: 0x000B94F4 File Offset: 0x000B76F4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F14 RID: 7956
			private static int max_field_count = 2;

			// Token: 0x04001F15 RID: 7957
			private string _ID;

			// Token: 0x04001F16 RID: 7958
			private long _state;
		}
	}
}
