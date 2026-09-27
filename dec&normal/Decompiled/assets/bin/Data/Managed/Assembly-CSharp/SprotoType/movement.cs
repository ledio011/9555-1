using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000422 RID: 1058
	public class movement : SprotoTypeBase
	{
		// Token: 0x060020CD RID: 8397 RVA: 0x0009F3F0 File Offset: 0x0009D5F0
		public movement() : base(movement.max_field_count)
		{
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0009F400 File Offset: 0x0009D600
		public movement(byte[] buffer) : base(movement.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x0009F41C File Offset: 0x0009D61C
		// (set) Token: 0x060020D1 RID: 8401 RVA: 0x0009F424 File Offset: 0x0009D624
		public position pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._pos = value;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0009F43C File Offset: 0x0009D63C
		public bool HasPos
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0009F44C File Offset: 0x0009D64C
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x0009F454 File Offset: 0x0009D654
		public position pos2
		{
			get
			{
				return this._pos2;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._pos2 = value;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x0009F46C File Offset: 0x0009D66C
		public bool HasPos2
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x0009F47C File Offset: 0x0009D67C
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
						this.pos2 = this.deserialize.read_obj<position>();
					}
				}
				else
				{
					this.pos = this.deserialize.read_obj<position>();
				}
			}
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x0009F4F4 File Offset: 0x0009D6F4
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_obj(this.pos, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_obj(this.pos2, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x04001B91 RID: 7057
		private static int max_field_count = 2;

		// Token: 0x04001B92 RID: 7058
		private position _pos;

		// Token: 0x04001B93 RID: 7059
		private position _pos2;
	}
}
