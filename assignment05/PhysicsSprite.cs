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
	public class PhysicsSprite : CollisionSprite
	{
		protected float gX, gY = .3f, aX, aY, vX, vY;

		enum MotionModels { Static, SetPath, Kinematic };

		MotionModels MotionModel = MotionModels.Kinematic;

		public float Gx
		{
			get { return gX; }
			set { gX = value; }
		}

		public float Gy
		{
			get { return gY; }
			set { gY = value; }
		}

		public float Ax
		{
			get { return aX; }
			set { aX = value; }
		}

		public float Ay
		{
			get { return aY; }
			set { aY = value; }
		}

		public float Vx
		{
			get { return vX; }
			set { vX = value; }
		}

		public float Vy
		{
			get { return vY; }
			set { vY = value; }
		}

		public PhysicsSprite(Image image) : base(image)
		{

		}

		public PhysicsSprite(Image image, int x, int y) : base(image)
		{
			X = x;
			Y = y;
		}

		public void setMotionModel(int type)
		{
			if (type == 0) MotionModel = MotionModels.Static;
			if (type == 1) MotionModel = MotionModels.SetPath;
			if (type == 2) MotionModel = MotionModels.Kinematic;
		}

		public override void act()
		{
			if (MotionModel != MotionModels.Static)
			{
				X += Vx;
				if (getCollisions().Count > 0)
				{
					X -= Vx;
				}
			}
			if (MotionModel != MotionModels.Static)
			{
				Y += Vy;
				if (getCollisions().Count > 0)
				{
					Y -= Vy;
				}
			}
			if (MotionModel == MotionModels.Kinematic)
			{
				Vx += Ax + Gx;
				Vy += Ay + Gy;
			}
		}

		public override void paint(Graphics g)
		{
			base.paint(g);
		}

	}
}