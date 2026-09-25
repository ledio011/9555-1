using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000316 RID: 790
	public class attribute_overview : SprotoTypeBase
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x0008A4F0 File Offset: 0x000886F0
		public attribute_overview() : base(attribute_overview.max_field_count)
		{
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0008A500 File Offset: 0x00088700
		public attribute_overview(byte[] buffer) : base(attribute_overview.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0008A51C File Offset: 0x0008871C
		// (set) Token: 0x060016B6 RID: 5814 RVA: 0x0008A524 File Offset: 0x00088724
		public long level
		{
			get
			{
				return this._level;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._level = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0008A53C File Offset: 0x0008873C
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0008A54C File Offset: 0x0008874C
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x0008A554 File Offset: 0x00088754
		public long combValue
		{
			get
			{
				return this._combValue;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._combValue = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0008A56C File Offset: 0x0008876C
		public bool HasCombValue
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0008A57C File Offset: 0x0008877C
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
						this.combValue = this.deserialize.read_integer();
					}
				}
				else
				{
					this.level = this.deserialize.read_integer();
				}
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0008A5F4 File Offset: 0x000887F4
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.level, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.combValue, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x040018B7 RID: 6327
		private static int max_field_count = 2;

		// Token: 0x040018B8 RID: 6328
		private long _level;

		// Token: 0x040018B9 RID: 6329
		private long _combValue;
	}
}
