using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000352 RID: 850
	public class comb_value_up_tip
	{
		// Token: 0x02000353 RID: 851
		public class request : SprotoTypeBase
		{
			// Token: 0x0600193C RID: 6460 RVA: 0x0008FA2C File Offset: 0x0008DC2C
			public request() : base(comb_value_up_tip.request.max_field_count)
			{
			}

			// Token: 0x0600193D RID: 6461 RVA: 0x0008FA3C File Offset: 0x0008DC3C
			public request(byte[] buffer) : base(comb_value_up_tip.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x0600193F RID: 6463 RVA: 0x0008FA58 File Offset: 0x0008DC58
			// (set) Token: 0x06001940 RID: 6464 RVA: 0x0008FA60 File Offset: 0x0008DC60
			public long current
			{
				get
				{
					return this._current;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._current = value;
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x06001941 RID: 6465 RVA: 0x0008FA78 File Offset: 0x0008DC78
			public bool HasCurrent
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x06001942 RID: 6466 RVA: 0x0008FA88 File Offset: 0x0008DC88
			// (set) Token: 0x06001943 RID: 6467 RVA: 0x0008FA90 File Offset: 0x0008DC90
			public long next
			{
				get
				{
					return this._next;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._next = value;
				}
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x06001944 RID: 6468 RVA: 0x0008FAA8 File Offset: 0x0008DCA8
			public bool HasNext
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001945 RID: 6469 RVA: 0x0008FAB8 File Offset: 0x0008DCB8
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
							this.next = this.deserialize.read_integer();
						}
					}
					else
					{
						this.current = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001946 RID: 6470 RVA: 0x0008FB30 File Offset: 0x0008DD30
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.current, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.next, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001976 RID: 6518
			private static int max_field_count = 2;

			// Token: 0x04001977 RID: 6519
			private long _current;

			// Token: 0x04001978 RID: 6520
			private long _next;
		}
	}
}
