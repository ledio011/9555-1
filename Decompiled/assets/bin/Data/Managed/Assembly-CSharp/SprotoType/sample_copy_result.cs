using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005B5 RID: 1461
	public class sample_copy_result
	{
		// Token: 0x020005B6 RID: 1462
		public class request : SprotoTypeBase
		{
			// Token: 0x06002A32 RID: 10802 RVA: 0x000B1570 File Offset: 0x000AF770
			public request() : base(sample_copy_result.request.max_field_count)
			{
			}

			// Token: 0x06002A33 RID: 10803 RVA: 0x000B1580 File Offset: 0x000AF780
			public request(byte[] buffer) : base(sample_copy_result.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C0B RID: 3083
			// (get) Token: 0x06002A35 RID: 10805 RVA: 0x000B159C File Offset: 0x000AF79C
			// (set) Token: 0x06002A36 RID: 10806 RVA: 0x000B15A4 File Offset: 0x000AF7A4
			public bool win
			{
				get
				{
					return this._win;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._win = value;
				}
			}

			// Token: 0x17000C0C RID: 3084
			// (get) Token: 0x06002A37 RID: 10807 RVA: 0x000B15BC File Offset: 0x000AF7BC
			public bool HasWin
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C0D RID: 3085
			// (get) Token: 0x06002A38 RID: 10808 RVA: 0x000B15CC File Offset: 0x000AF7CC
			// (set) Token: 0x06002A39 RID: 10809 RVA: 0x000B15D4 File Offset: 0x000AF7D4
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type = value;
				}
			}

			// Token: 0x17000C0E RID: 3086
			// (get) Token: 0x06002A3A RID: 10810 RVA: 0x000B15EC File Offset: 0x000AF7EC
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002A3B RID: 10811 RVA: 0x000B15FC File Offset: 0x000AF7FC
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
							this.type = this.deserialize.read_integer();
						}
					}
					else
					{
						this.win = this.deserialize.read_boolean();
					}
				}
			}

			// Token: 0x06002A3C RID: 10812 RVA: 0x000B1674 File Offset: 0x000AF874
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_boolean(this.win, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E00 RID: 7680
			private static int max_field_count = 2;

			// Token: 0x04001E01 RID: 7681
			private bool _win;

			// Token: 0x04001E02 RID: 7682
			private long _type;
		}
	}
}
