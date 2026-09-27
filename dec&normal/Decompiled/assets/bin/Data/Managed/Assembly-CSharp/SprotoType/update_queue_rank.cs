using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200064B RID: 1611
	public class update_queue_rank
	{
		// Token: 0x0200064C RID: 1612
		public class request : SprotoTypeBase
		{
			// Token: 0x06002EAB RID: 11947 RVA: 0x000BA5B0 File Offset: 0x000B87B0
			public request() : base(update_queue_rank.request.max_field_count)
			{
			}

			// Token: 0x06002EAC RID: 11948 RVA: 0x000BA5C0 File Offset: 0x000B87C0
			public request(byte[] buffer) : base(update_queue_rank.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DBD RID: 3517
			// (get) Token: 0x06002EAE RID: 11950 RVA: 0x000BA5DC File Offset: 0x000B87DC
			// (set) Token: 0x06002EAF RID: 11951 RVA: 0x000BA5E4 File Offset: 0x000B87E4
			public long rank
			{
				get
				{
					return this._rank;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._rank = value;
				}
			}

			// Token: 0x17000DBE RID: 3518
			// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x000BA5FC File Offset: 0x000B87FC
			public bool HasRank
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DBF RID: 3519
			// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x000BA60C File Offset: 0x000B880C
			// (set) Token: 0x06002EB2 RID: 11954 RVA: 0x000BA614 File Offset: 0x000B8814
			public long remain_time
			{
				get
				{
					return this._remain_time;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._remain_time = value;
				}
			}

			// Token: 0x17000DC0 RID: 3520
			// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x000BA62C File Offset: 0x000B882C
			public bool HasRemain_time
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002EB4 RID: 11956 RVA: 0x000BA63C File Offset: 0x000B883C
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
							this.remain_time = this.deserialize.read_integer();
						}
					}
					else
					{
						this.rank = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002EB5 RID: 11957 RVA: 0x000BA6B4 File Offset: 0x000B88B4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.rank, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.remain_time, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F38 RID: 7992
			private static int max_field_count = 2;

			// Token: 0x04001F39 RID: 7993
			private long _rank;

			// Token: 0x04001F3A RID: 7994
			private long _remain_time;
		}
	}
}
