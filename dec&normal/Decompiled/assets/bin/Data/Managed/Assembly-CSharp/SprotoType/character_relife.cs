using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200034B RID: 843
	public class character_relife : SprotoTypeBase
	{
		// Token: 0x060018C3 RID: 6339 RVA: 0x0008E9DC File Offset: 0x0008CBDC
		public character_relife() : base(character_relife.max_field_count)
		{
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0008E9EC File Offset: 0x0008CBEC
		public character_relife(byte[] buffer) : base(character_relife.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0008EA08 File Offset: 0x0008CC08
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x0008EA10 File Offset: 0x0008CC10
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

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0008EA28 File Offset: 0x0008CC28
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0008EA38 File Offset: 0x0008CC38
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x0008EA40 File Offset: 0x0008CC40
		public attribute_other attribute_other
		{
			get
			{
				return this._attribute_other;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._attribute_other = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0008EA58 File Offset: 0x0008CC58
		public bool HasAttribute_other
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0008EA68 File Offset: 0x0008CC68
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x0008EA70 File Offset: 0x0008CC70
		public movement movement
		{
			get
			{
				return this._movement;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._movement = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0008EA88 File Offset: 0x0008CC88
		public bool HasMovement
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0008EA98 File Offset: 0x0008CC98
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
					this.attribute_other = this.deserialize.read_obj<attribute_other>();
					break;
				case 2:
					this.movement = this.deserialize.read_obj<movement>();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0008EB2C File Offset: 0x0008CD2C
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_obj(this.attribute_other, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_obj(this.movement, 2);
			}
			return this.serialize.close();
		}

		// Token: 0x04001952 RID: 6482
		private static int max_field_count = 3;

		// Token: 0x04001953 RID: 6483
		private long _id;

		// Token: 0x04001954 RID: 6484
		private attribute_other _attribute_other;

		// Token: 0x04001955 RID: 6485
		private movement _movement;
	}
}
