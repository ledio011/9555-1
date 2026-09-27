using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000553 RID: 1363
	public class ret_offline_chat
	{
		// Token: 0x02000554 RID: 1364
		public class request : SprotoTypeBase
		{
			// Token: 0x060027AE RID: 10158 RVA: 0x000AC5B0 File Offset: 0x000AA7B0
			public request() : base(ret_offline_chat.request.max_field_count)
			{
			}

			// Token: 0x060027AF RID: 10159 RVA: 0x000AC5C0 File Offset: 0x000AA7C0
			public request(byte[] buffer) : base(ret_offline_chat.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B33 RID: 2867
			// (get) Token: 0x060027B1 RID: 10161 RVA: 0x000AC5DC File Offset: 0x000AA7DC
			// (set) Token: 0x060027B2 RID: 10162 RVA: 0x000AC5E4 File Offset: 0x000AA7E4
			public List<chat_item> chat_list
			{
				get
				{
					return this._chat_list;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._chat_list = value;
				}
			}

			// Token: 0x17000B34 RID: 2868
			// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000AC5FC File Offset: 0x000AA7FC
			public bool HasChat_list
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060027B4 RID: 10164 RVA: 0x000AC60C File Offset: 0x000AA80C
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
						this.chat_list = this.deserialize.read_obj_list<chat_item>();
					}
				}
			}

			// Token: 0x060027B5 RID: 10165 RVA: 0x000AC668 File Offset: 0x000AA868
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<chat_item>(this.chat_list, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D4C RID: 7500
			private static int max_field_count = 1;

			// Token: 0x04001D4D RID: 7501
			private List<chat_item> _chat_list;
		}
	}
}
