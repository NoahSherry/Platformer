using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment05
{
	public class Mario : PhysicsSprite
	{
		public bool alive;
		public DateTime lastshot = DateTime.Now;
		public TimeSpan firerate = new TimeSpan(0, 0, 1);

		public Mario(int x, int y) : base(Properties.Resources.mario, x, y)
		{
			image = Properties.Resources.mario;
			X = x;
			Y = y;
			alive = true;
		}

		public override void act()
		{
			base.act();
			if ((Vx > 0 && Ax > 0) || (Vx < 0 && Ax < 0))
			{
				Vx = 0;
				Ax = 0;
			}
			if ((Vy > 0 && Ay > 0) || (Vy < 0 && Ay < 0))
			{
				Vy = 0;
				Ay = 0;
			}
		}

		public override void Kill()
		{
			base.Kill();
			alive = false;
		}

		public void shoot(int dir)
		{
			if (!alive) return;
			if (DateTime.Now - lastshot <= firerate) return;
			OtherBullet bullet = new OtherBullet((int)(X + 2 * width * Scale * 1.1f), (int)(Y + height * Scale / 2));
			bullet.X = X + 2 * width * Scale * 1.1f;
			bullet.Y = Y + height * Scale / 2;
			bullet.Vx = 50f;
			if(dir == 1)
			{
				bullet.X = X + 120;
			}
			if (dir == 3)
			{
				bullet.X = (X - width * Scale * 1.1f)+70;
				bullet.Vx *= -1;
			}
			else if (dir != 1)
			{
				bullet.X = (X + (width / 2));
				bullet.Vx = 0;
				bullet.Vy = 50;
			}
			if (dir == 2)
			{
				bullet.Y = Y + 2 * height * Scale * 1.1f;
			}
			ChaosEngine.canvas.csAdd(bullet);
			lastshot = DateTime.Now;
		}

	}
}