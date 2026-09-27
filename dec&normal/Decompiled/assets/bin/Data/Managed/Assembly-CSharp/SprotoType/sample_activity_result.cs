using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005B3 RID: 1459
	public class sample_activity_result
	{
		// Token: 0x020005B4 RID: 1460
		public class request : SprotoTypeBase
		{
			// Token: 0x06002A23 RID: 10787 RVA: 0x000B1388 File Offset: 0x000AF588
			public request() : base(sample_activity_result.request.max_field_count)
			{
			}

			// Token: 0x06002A24 RID: 10788 RVA: 0x000B1398 File Offset: 0x000AF598
			public request(byte[] buffer) : base(sample_activity_result.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C05 RID: 3077
			// (get) Token: 0x06002A26 RID: 10790 RVA: 0x000B13B4 File Offset: 0x000AF5B4
			// (set) Token: 0x06002A27 RID: 10791 RVA: 0x000B13BC File Offset: 0x000AF5BC
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

			// Token: 0x17000C06 RID: 3078
			// (get) Token: 0x06002A28 RID: 10792 RVA: 0x000B13D4 File Offset: 0x000AF5D4
			public bool HasWin
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C07 RID: 3079
			// (get) Token: 0x06002A29 RID: 10793 RVA: 0x000B13E4 File Offset: 0x000AF5E4
			// (set) Token: 0x06002A2A RID: 10794 RVA: 0x000B13EC File Offset: 0x000AF5EC
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

			// Token: 0x17000C08 RID: 3080
			// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000B1404 File Offset: 0x000AF604
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000C09 RID: 3081
			// (get) Token: 0x06002A2C RID: 10796 RVA: 0x000B1414 File Offset: 0x000AF614
			// (set) Token: 0x06002A2D RID: 10797 RVA: 0x000B141C File Offset: 0x000AF61C
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._id = value;
				}
			}

			// Token: 0x17000C0A RID: 3082
			// (get) Token: 0x06002A2E RID: 10798 RVA: 0x000B1434 File Offset: 0x000AF634
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002A2F RID: 10799 RVA: 0x000B1444 File Offset: 0x000AF644
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.win = this.deserialize.read_boolean();
						break;
					case 1:
						this.type = this.deserialize.read_integer();
						break;
					case 2:
						this.id = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002A30 RID: 10800 RVA: 0x000B14D8 File Offset: 0x000AF6D8
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
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.id, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DFC RID: 7676
			private static int max_field_count = 3;

			// Token: 0x04001DFD RID: 7677
			private bool _win;

			// Token: 0x04001DFE RID: 7678
			private long _type;

			// Token: 0x04001DFF RID: 7679
			private string _id;
		}
	}
}
