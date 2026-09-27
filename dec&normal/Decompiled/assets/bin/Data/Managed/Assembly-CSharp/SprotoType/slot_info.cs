using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005E5 RID: 1509
	public class slot_info : SprotoTypeBase
	{
		// Token: 0x06002BA7 RID: 11175 RVA: 0x000B44DC File Offset: 0x000B26DC
		public slot_info() : base(slot_info.max_field_count)
		{
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000B44EC File Offset: 0x000B26EC
		public slot_info(byte[] buffer) : base(slot_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000B4508 File Offset: 0x000B2708
		// (set) Token: 0x06002BAB RID: 11179 RVA: 0x000B4510 File Offset: 0x000B2710
		public long curNum
		{
			get
			{
				return this._curNum;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._curNum = value;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x000B4528 File Offset: 0x000B2728
		public bool HasCurNum
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x000B4538 File Offset: 0x000B2738
		// (set) Token: 0x06002BAE RID: 11182 RVA: 0x000B4540 File Offset: 0x000B2740
		public long sumNum
		{
			get
			{
				return this._sumNum;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._sumNum = value;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000B4558 File Offset: 0x000B2758
		public bool HasSumNum
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000B4568 File Offset: 0x000B2768
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				int num2 = num;
				if (num2 != 1)
				{
					if (num2 != 2)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.sumNum = this.deserialize.read_integer();
					}
				}
				else
				{
					this.curNum = this.deserialize.read_integer();
				}
			}
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000B45E0 File Offset: 0x000B27E0
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.curNum, 1);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.sumNum, 2);
			}
			return this.serialize.close();
		}

		// Token: 0x04001E64 RID: 7780
		private static int max_field_count = 3;

		// Token: 0x04001E65 RID: 7781
		private long _curNum;

		// Token: 0x04001E66 RID: 7782
		private long _sumNum;
	}
}
