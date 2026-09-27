using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005F1 RID: 1521
	public class start_participate_dance
	{
		// Token: 0x020005F2 RID: 1522
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C11 RID: 11281 RVA: 0x000B5230 File Offset: 0x000B3430
			public request() : base(start_participate_dance.request.max_field_count)
			{
			}

			// Token: 0x06002C12 RID: 11282 RVA: 0x000B5240 File Offset: 0x000B3440
			public request(byte[] buffer) : base(start_participate_dance.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000CC7 RID: 3271
			// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000B525C File Offset: 0x000B345C
			// (set) Token: 0x06002C15 RID: 11285 RVA: 0x000B5264 File Offset: 0x000B3464
			public string curUse
			{
				get
				{
					return this._curUse;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._curUse = value;
				}
			}

			// Token: 0x17000CC8 RID: 3272
			// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000B527C File Offset: 0x000B347C
			public bool HasCurUse
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000CC9 RID: 3273
			// (get) Token: 0x06002C17 RID: 11287 RVA: 0x000B528C File Offset: 0x000B348C
			// (set) Token: 0x06002C18 RID: 11288 RVA: 0x000B5294 File Offset: 0x000B3494
			public Dictionary<string, dance_info> dance_info
			{
				get
				{
					return this._dance_info;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._dance_info = value;
				}
			}

			// Token: 0x17000CCA RID: 3274
			// (get) Token: 0x06002C19 RID: 11289 RVA: 0x000B52AC File Offset: 0x000B34AC
			public bool HasDance_info
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002C1A RID: 11290 RVA: 0x000B52BC File Offset: 0x000B34BC
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
							this.dance_info = this.deserialize.read_map<string, dance_info>((dance_info v) => v.ID);
						}
					}
					else
					{
						this.curUse = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002C1B RID: 11291 RVA: 0x000B5350 File Offset: 0x000B3550
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.curUse, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<string, dance_info>(this.dance_info, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E81 RID: 7809
			private static int max_field_count = 2;

			// Token: 0x04001E82 RID: 7810
			private string _curUse;

			// Token: 0x04001E83 RID: 7811
			private Dictionary<string, dance_info> _dance_info;
		}
	}
}
