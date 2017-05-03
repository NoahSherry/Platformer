using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment05
{
	public class Enemy : PhysicsSprite
	{
		private Boolean left = false;
		private Random r = new Random();
		public DateTime lastshot = DateTime.Now;
		public TimeSpan firerate = new TimeSpan(0, 0, 0, 2, 50);

		public Enemy() : base(Properties.Resources.hammer)
		{
			Vx = 4f;
			Gy = 0.25f;
			ChaosEngine.enemyCount++;
		}

		public Enemy(int x, int y) : base(Properties.Resources.hammer, x, y)
		{
			Vx = 4f;
			Gy = 0.25f;
			ChaosEngine.enemyCount++;
		}

		public void Shoot()
		{
			Bullet bullet = new Bullet((int)(X + 2 * width * Scale * 1.1f), (int)(Y + height * Scale / 2));
			if (left)
			{
				bullet.X = X - width * Scale * 1.1f;
				bullet.Vx *= -1;
			}
			ChaosEngine.canvas.csAdd(bullet);
		}

		public bool isWall()
		{
			X += Vx;
			if (getCollisions().Count > 0)
			{
				X -= Vx;
				return true;
			}
			X -= Vx;
			return false;
		}

		public bool isCeiling()
		{
			Y += Vy;
			if (getCollisions().Count > 0)
			{
				Y -= Vy;
				return true;
			}
			Y -= Vy;
			return false;
		}

		public void killCharacter()
		{
			X += Vx;
			List<CollisionSprite> list = getCollisions();
			X -= Vx;
			foreach (CollisionSprite s in list)
			{
				if (s.GetType() == typeof(Mario)) s.Kill();
			}
		}

		public override void act()
		{
			base.act();
			killCharacter();
			if (r.NextDouble() < .01) Vy = -20;
			if (isWall()) Vx *= -1;
			if (isCeiling()) Vy = 0;
			if (r.NextDouble() < .01)
			{
				Shoot();
				lastshot = DateTime.Now;
			}
			if (Vx < 0) left = true;
			if (Vx > 0) left = false;
			if (DateTime.Now - lastshot >= firerate)
			{
				Shoot();
				lastshot = DateTime.Now;
			}
		}

		public override void Kill()
		{
			base.Kill();
			ChaosEngine.enemyCount--;
		}

	}
}