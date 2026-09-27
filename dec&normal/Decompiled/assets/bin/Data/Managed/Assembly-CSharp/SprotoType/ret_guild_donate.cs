using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200052D RID: 1325
	public class ret_guild_donate
	{
		// Token: 0x0200052E RID: 1326
		public class request : SprotoTypeBase
		{
			// Token: 0x060026A1 RID: 9889 RVA: 0x000AA408 File Offset: 0x000A8608
			public request() : base(ret_guild_donate.request.max_field_count)
			{
			}

			// Token: 0x060026A2 RID: 9890 RVA: 0x000AA418 File Offset: 0x000A8618
			public request(byte[] buffer) : base(ret_guild_donate.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AD1 RID: 2769
			// (get) Token: 0x060026A4 RID: 9892 RVA: 0x000AA434 File Offset: 0x000A8634
			// (set) Token: 0x060026A5 RID: 9893 RVA: 0x000AA43C File Offset: 0x000A863C
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x17000AD2 RID: 2770
			// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000AA454 File Offset: 0x000A8654
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000AD3 RID: 2771
			// (get) Token: 0x060026A7 RID: 9895 RVA: 0x000AA464 File Offset: 0x000A8664
			// (set) Token: 0x060026A8 RID: 9896 RVA: 0x000AA46C File Offset: 0x000A866C
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._id = value;
				}
			}

			// Token: 0x17000AD4 RID: 2772
			// (get) Token: 0x060026A9 RID: 9897 RVA: 0x000AA484 File Offset: 0x000A8684
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000AD5 RID: 2773
			// (get) Token: 0x060026AA RID: 9898 RVA: 0x000AA494 File Offset: 0x000A8694
			// (set) Token: 0x060026AB RID: 9899 RVA: 0x000AA49C File Offset: 0x000A869C
			public long contribute
			{
				get
				{
					return this._contribute;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._contribute = value;
				}
			}

			// Token: 0x17000AD6 RID: 2774
			// (get) Token: 0x060026AC RID: 9900 RVA: 0x000AA4B4 File Offset: 0x000A86B4
			public bool HasContribute
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000AD7 RID: 2775
			// (get) Token: 0x060026AD RID: 9901 RVA: 0x000AA4C4 File Offset: 0x000A86C4
			// (set) Token: 0x060026AE RID: 9902 RVA: 0x000AA4CC File Offset: 0x000A86CC
			public long all_contribute
			{
				get
				{
					return this._all_contribute;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._all_contribute = value;
				}
			}

			// Token: 0x17000AD8 RID: 2776
			// (get) Token: 0x060026AF RID: 9903 RVA: 0x000AA4E4 File Offset: 0x000A86E4
			public bool HasAll_contribute
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000AD9 RID: 2777
			// (get) Token: 0x060026B0 RID: 9904 RVA: 0x000AA4F4 File Offset: 0x000A86F4
			// (set) Token: 0x060026B1 RID: 9905 RVA: 0x000AA4FC File Offset: 0x000A86FC
			public long level
			{
				get
				{
					return this._level;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._level = value;
				}
			}

			// Token: 0x17000ADA RID: 2778
			// (get) Token: 0x060026B2 RID: 9906 RVA: 0x000AA514 File Offset: 0x000A8714
			public bool HasLevel
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000ADB RID: 2779
			// (get) Token: 0x060026B3 RID: 9907 RVA: 0x000AA524 File Offset: 0x000A8724
			// (set) Token: 0x060026B4 RID: 9908 RVA: 0x000AA52C File Offset: 0x000A872C
			public long exp
			{
				get
				{
					return this._exp;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._exp = value;
				}
			}

			// Token: 0x17000ADC RID: 2780
			// (get) Token: 0x060026B5 RID: 9909 RVA: 0x000AA544 File Offset: 0x000A8744
			public bool HasExp
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x060026B6 RID: 9910 RVA: 0x000AA554 File Offset: 0x000A8754
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.state = this.deserialize.read_integer();
						break;
					case 1:
						this.id = this.deserialize.read_string();
						break;
					case 2:
						this.contribute = this.deserialize.read_integer();
						break;
					case 3:
						this.all_contribute = this.deserialize.read_integer();
						break;
					case 4:
						this.level = this.deserialize.read_integer();
						break;
					case 5:
						this.exp = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060026B7 RID: 9911 RVA: 0x000AA634 File Offset: 0x000A8834
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.id, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.contribute, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.all_contribute, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.level, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.exp, 5);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D00 RID: 7424
			private static int max_field_count = 6;

			// Token: 0x04001D01 RID: 7425
			private long _state;

			// Token: 0x04001D02 RID: 7426
			private string _id;

			// Token: 0x04001D03 RID: 7427
			private long _contribute;

			// Token: 0x04001D04 RID: 7428
			private long _all_contribute;

			// Token: 0x04001D05 RID: 7429
			private long _level;

			// Token: 0x04001D06 RID: 7430
			private long _exp;
		}
	}
}
