using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200031F RID: 799
	public class buff : SprotoTypeBase
	{
		// Token: 0x060016FD RID: 5885 RVA: 0x0008AE7C File Offset: 0x0008907C
		public buff() : base(buff.max_field_count)
		{
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0008AE8C File Offset: 0x0008908C
		public buff(byte[] buffer) : base(buff.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0008AEA8 File Offset: 0x000890A8
		// (set) Token: 0x06001701 RID: 5889 RVA: 0x0008AEB0 File Offset: 0x000890B0
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

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x0008AEC8 File Offset: 0x000890C8
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0008AED8 File Offset: 0x000890D8
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x0008AEE0 File Offset: 0x000890E0
		public string effinfoId
		{
			get
			{
				return this._effinfoId;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._effinfoId = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0008AEF8 File Offset: 0x000890F8
		public bool HasEffinfoId
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0008AF08 File Offset: 0x00089108
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
						this.effinfoId = this.deserialize.read_string();
					}
				}
				else
				{
					this.id = this.deserialize.read_integer();
				}
			}
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0008AF80 File Offset: 0x00089180
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.effinfoId, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x040018CB RID: 6347
		private static int max_field_count = 2;

		// Token: 0x040018CC RID: 6348
		private long _id;

		// Token: 0x040018CD RID: 6349
		private string _effinfoId;
	}
}
