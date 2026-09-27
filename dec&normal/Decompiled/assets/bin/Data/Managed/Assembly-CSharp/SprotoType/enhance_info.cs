using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200037A RID: 890
	public class enhance_info : SprotoTypeBase
	{
		// Token: 0x06001AF1 RID: 6897 RVA: 0x00093368 File Offset: 0x00091568
		public enhance_info() : base(enhance_info.max_field_count)
		{
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00093378 File Offset: 0x00091578
		public enhance_info(byte[] buffer) : base(enhance_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00093394 File Offset: 0x00091594
		// (set) Token: 0x06001AF5 RID: 6901 RVA: 0x0009339C File Offset: 0x0009159C
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

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x000933B4 File Offset: 0x000915B4
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x000933C4 File Offset: 0x000915C4
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x000933CC File Offset: 0x000915CC
		public long subType
		{
			get
			{
				return this._subType;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._subType = value;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x000933E4 File Offset: 0x000915E4
		public bool HasSubType
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000933F4 File Offset: 0x000915F4
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
						this.subType = this.deserialize.read_integer();
					}
				}
				else
				{
					this.level = this.deserialize.read_integer();
				}
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0009346C File Offset: 0x0009166C
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.level, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.subType, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x040019F2 RID: 6642
		private static int max_field_count = 2;

		// Token: 0x040019F3 RID: 6643
		private long _level;

		// Token: 0x040019F4 RID: 6644
		private long _subType;
	}
}
