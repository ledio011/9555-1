using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200080B RID: 2059
	public class Package : SprotoTypeBase
	{
		// Token: 0x0600316D RID: 12653 RVA: 0x000C13A4 File Offset: 0x000BF5A4
		public Package() : base(Package.max_field_count)
		{
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000C13B4 File Offset: 0x000BF5B4
		public Package(byte[] buffer) : base(Package.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000C13D0 File Offset: 0x000BF5D0
		// (set) Token: 0x06003171 RID: 12657 RVA: 0x000C13D8 File Offset: 0x000BF5D8
		public long type
		{
			get
			{
				return this._type;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._type = value;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000C13F0 File Offset: 0x000BF5F0
		public bool HasType
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000C1400 File Offset: 0x000BF600
		public void Reset()
		{
			this.has_field.set_field(0, false);
			this.has_field.set_field(1, false);
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x000C141C File Offset: 0x000BF61C
		// (set) Token: 0x06003175 RID: 12661 RVA: 0x000C1424 File Offset: 0x000BF624
		public long session
		{
			get
			{
				return this._session;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._session = value;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x000C143C File Offset: 0x000BF63C
		public bool HasSession
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000C144C File Offset: 0x000BF64C
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
						this.session = this.deserialize.read_integer();
					}
				}
				else
				{
					this.type = this.deserialize.read_integer();
				}
			}
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000C14C4 File Offset: 0x000BF6C4
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.type, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.session, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x0400212F RID: 8495
		private static int max_field_count = 2;

		// Token: 0x04002130 RID: 8496
		private long _type;

		// Token: 0x04002131 RID: 8497
		private long _session;
	}
}
