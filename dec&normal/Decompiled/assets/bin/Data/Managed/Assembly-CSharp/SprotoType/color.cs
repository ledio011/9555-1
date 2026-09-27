using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000351 RID: 849
	public class color : SprotoTypeBase
	{
		// Token: 0x06001930 RID: 6448 RVA: 0x0008F8B4 File Offset: 0x0008DAB4
		public color() : base(color.max_field_count)
		{
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0008F8C4 File Offset: 0x0008DAC4
		public color(byte[] buffer) : base(color.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0008F8E0 File Offset: 0x0008DAE0
		// (set) Token: 0x06001934 RID: 6452 RVA: 0x0008F8E8 File Offset: 0x0008DAE8
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

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x0008F900 File Offset: 0x0008DB00
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x0008F910 File Offset: 0x0008DB10
		// (set) Token: 0x06001937 RID: 6455 RVA: 0x0008F918 File Offset: 0x0008DB18
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

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0008F930 File Offset: 0x0008DB30
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0008F940 File Offset: 0x0008DB40
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

		// Token: 0x0600193A RID: 6458 RVA: 0x0008F9B8 File Offset: 0x0008DBB8
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

		// Token: 0x04001973 RID: 6515
		private static int max_field_count = 2;

		// Token: 0x04001974 RID: 6516
		private string _ID;

		// Token: 0x04001975 RID: 6517
		private long _state;
	}
}
