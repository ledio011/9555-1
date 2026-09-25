using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005B2 RID: 1458
	public class runtime_agent : SprotoTypeBase
	{
		// Token: 0x06002A17 RID: 10775 RVA: 0x000B1210 File Offset: 0x000AF410
		public runtime_agent() : base(runtime_agent.max_field_count)
		{
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000B1220 File Offset: 0x000AF420
		public runtime_agent(byte[] buffer) : base(runtime_agent.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x000B123C File Offset: 0x000AF43C
		// (set) Token: 0x06002A1B RID: 10779 RVA: 0x000B1244 File Offset: 0x000AF444
		public attribute attribute
		{
			get
			{
				return this._attribute;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._attribute = value;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000B125C File Offset: 0x000AF45C
		public bool HasAttribute
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000B126C File Offset: 0x000AF46C
		// (set) Token: 0x06002A1E RID: 10782 RVA: 0x000B1274 File Offset: 0x000AF474
		public attribute attribute_all
		{
			get
			{
				return this._attribute_all;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._attribute_all = value;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x000B128C File Offset: 0x000AF48C
		public bool HasAttribute_all
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000B129C File Offset: 0x000AF49C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				int num2 = num;
				if (num2 != 6)
				{
					if (num2 != 7)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.attribute_all = this.deserialize.read_obj<attribute>();
					}
				}
				else
				{
					this.attribute = this.deserialize.read_obj<attribute>();
				}
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000B1314 File Offset: 0x000AF514
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_obj(this.attribute, 6);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_obj(this.attribute_all, 7);
			}
			return this.serialize.close();
		}

		// Token: 0x04001DF9 RID: 7673
		private static int max_field_count = 3;

		// Token: 0x04001DFA RID: 7674
		private attribute _attribute;

		// Token: 0x04001DFB RID: 7675
		private attribute _attribute_all;
	}
}
