using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005B7 RID: 1463
	public class score_info : SprotoTypeBase
	{
		// Token: 0x06002A3D RID: 10813 RVA: 0x000B16E0 File Offset: 0x000AF8E0
		public score_info() : base(score_info.max_field_count)
		{
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000B16F0 File Offset: 0x000AF8F0
		public score_info(byte[] buffer) : base(score_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x000B170C File Offset: 0x000AF90C
		// (set) Token: 0x06002A41 RID: 10817 RVA: 0x000B1714 File Offset: 0x000AF914
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._id = value;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x000B172C File Offset: 0x000AF92C
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x000B173C File Offset: 0x000AF93C
		// (set) Token: 0x06002A44 RID: 10820 RVA: 0x000B1744 File Offset: 0x000AF944
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._name = value;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x000B175C File Offset: 0x000AF95C
		public bool HasName
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x000B176C File Offset: 0x000AF96C
		// (set) Token: 0x06002A47 RID: 10823 RVA: 0x000B1774 File Offset: 0x000AF974
		public long value
		{
			get
			{
				return this._value;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._value = value;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x000B178C File Offset: 0x000AF98C
		public bool HasValue
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000B179C File Offset: 0x000AF99C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_integer();
					break;
				case 1:
					this.name = this.deserialize.read_string();
					break;
				case 2:
					this.value = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000B1830 File Offset: 0x000AFA30
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.name, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.value, 2);
			}
			return this.serialize.close();
		}

		// Token: 0x04001E03 RID: 7683
		private static int max_field_count = 3;

		// Token: 0x04001E04 RID: 7684
		private long _id;

		// Token: 0x04001E05 RID: 7685
		private string _name;

		// Token: 0x04001E06 RID: 7686
		private long _value;
	}
}
