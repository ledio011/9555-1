using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004F7 RID: 1271
	public class ret_ask_confirm_multi_copy_scene
	{
		// Token: 0x020004F8 RID: 1272
		public class request : SprotoTypeBase
		{
			// Token: 0x06002529 RID: 9513 RVA: 0x000A7514 File Offset: 0x000A5714
			public request() : base(ret_ask_confirm_multi_copy_scene.request.max_field_count)
			{
			}

			// Token: 0x0600252A RID: 9514 RVA: 0x000A7524 File Offset: 0x000A5724
			public request(byte[] buffer) : base(ret_ask_confirm_multi_copy_scene.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A47 RID: 2631
			// (get) Token: 0x0600252C RID: 9516 RVA: 0x000A7540 File Offset: 0x000A5740
			// (set) Token: 0x0600252D RID: 9517 RVA: 0x000A7548 File Offset: 0x000A5748
			public long session
			{
				get
				{
					return this._session;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._session = value;
				}
			}

			// Token: 0x17000A48 RID: 2632
			// (get) Token: 0x0600252E RID: 9518 RVA: 0x000A7560 File Offset: 0x000A5760
			public bool HasSession
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A49 RID: 2633
			// (get) Token: 0x0600252F RID: 9519 RVA: 0x000A7570 File Offset: 0x000A5770
			// (set) Token: 0x06002530 RID: 9520 RVA: 0x000A7578 File Offset: 0x000A5778
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

			// Token: 0x17000A4A RID: 2634
			// (get) Token: 0x06002531 RID: 9521 RVA: 0x000A7590 File Offset: 0x000A5790
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002532 RID: 9522 RVA: 0x000A75A0 File Offset: 0x000A57A0
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
						this.session = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002533 RID: 9523 RVA: 0x000A7618 File Offset: 0x000A5818
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.session, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C99 RID: 7321
			private static int max_field_count = 2;

			// Token: 0x04001C9A RID: 7322
			private long _session;

			// Token: 0x04001C9B RID: 7323
			private long _state;
		}
	}
}
