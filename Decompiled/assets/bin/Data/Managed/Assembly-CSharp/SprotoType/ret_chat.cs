using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000505 RID: 1285
	public class ret_chat
	{
		// Token: 0x02000506 RID: 1286
		public class request : SprotoTypeBase
		{
			// Token: 0x06002590 RID: 9616 RVA: 0x000A8208 File Offset: 0x000A6408
			public request() : base(ret_chat.request.max_field_count)
			{
			}

			// Token: 0x06002591 RID: 9617 RVA: 0x000A8218 File Offset: 0x000A6418
			public request(byte[] buffer) : base(ret_chat.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A6F RID: 2671
			// (get) Token: 0x06002593 RID: 9619 RVA: 0x000A8234 File Offset: 0x000A6434
			// (set) Token: 0x06002594 RID: 9620 RVA: 0x000A823C File Offset: 0x000A643C
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

			// Token: 0x17000A70 RID: 2672
			// (get) Token: 0x06002595 RID: 9621 RVA: 0x000A8254 File Offset: 0x000A6454
			public bool HasChat_list
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002596 RID: 9622 RVA: 0x000A8264 File Offset: 0x000A6464
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

			// Token: 0x06002597 RID: 9623 RVA: 0x000A82C0 File Offset: 0x000A64C0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<chat_item>(this.chat_list, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CB5 RID: 7349
			private static int max_field_count = 1;

			// Token: 0x04001CB6 RID: 7350
			private List<chat_item> _chat_list;
		}
	}
}
