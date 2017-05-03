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
	public class Sprite
	{
		private Sprite parent = null;
		
		public Sprite Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		//instance variable
		private float x = 0;

		public float X
		{
			get { return x; }
			set { x = value; }
		}

		private float y = 0;

		public float Y
		{
			get { return y; }
			set { y = value; }
		}

		private float scale = 1;

		public float Scale
		{
			get { return scale; }
			set { scale = value; }
		}

		private float rotation = 0;

		/// <summary>
		/// The rotation in degrees.
		/// </summary>
		public float Rotation
		{
			get { return rotation; }
			set { rotation = value; }
		}


		public List<Sprite> children = new List<Sprite>();
		public List<Sprite> toAdd = new List<Sprite>();
		public List<Sprite> toRemove = new List<Sprite>();
		public List<CollisionSprite> cchildren = new List<CollisionSprite>();
		public List<CollisionSprite> ctoAdd = new List<CollisionSprite>();
		public List<CollisionSprite> ctoRemove = new List<CollisionSprite>();

		public virtual void Kill()
		{
			parent.remove(this);
		}

		//methods
		public void render(Graphics g)
		{
			Matrix original = g.Transform.Clone();
			g.TranslateTransform(x, y);
			g.ScaleTransform(scale, scale);
			g.RotateTransform(rotation);
			paint(g);
			foreach (Sprite s in children)
			{
				s.render(g);
			}
			g.Transform = original;
		}

		public void update()
		{
			act();
			foreach (Sprite s in children)
			{
				s.update();
			}
		}

		public virtual void paint(Graphics g)
		{

		}

		public virtual void act()
		{

		}

		public void add(Sprite s)
		{
			s.parent = this;
			toAdd.Add(s);
		}

		public void cAdd(CollisionSprite s)
		{
			ctoAdd.Add(s);
		}

		public void csAdd(CollisionSprite s)
		{
			add(s);
			cAdd(s);
		}

		public void remove(Sprite s)
		{
			toRemove.Add(s);
		}

		public void cRemove(CollisionSprite s)
		{
			ctoRemove.Add(s);
		}

		public void csRemove(CollisionSprite s)
		{
			remove(s);
			cRemove(s);
		}

		public void RemoveAll()
		{
			foreach (Sprite s in children)
			{
				remove(s);
			}
			foreach (CollisionSprite s in cchildren)
			{
				cRemove(s);
			}
		}

		public void queueClear()
		{
			foreach (Sprite s in toRemove)
			{
				children.Remove(s);
			}
			toRemove = new List<Sprite>();
			foreach (Sprite s in toAdd)
			{
				children.Add(s);
			}
			toAdd = new List<Sprite>();
			foreach (CollisionSprite s in ctoRemove)
			{
				cchildren.Remove(s);
				foreach (CollisionSprite c in cchildren)
				{
					c.untrack(s);
					s.untrack(c);
				}
			}
			ctoRemove = new List<CollisionSprite>();
			foreach (CollisionSprite s in ctoAdd)
			{
				cchildren.Add(s);
				foreach (CollisionSprite c in cchildren)
				{
					c.track(s);
					s.track(c);
				}
			}
			ctoAdd = new List<CollisionSprite>();
		}

		public void updateAllTracking()
		{
			foreach (CollisionSprite c in cchildren)
			{
				c.updateTracking();
			}
		}

	}

	public class SlideSprite : Sprite
	{
		public int TargetX = 0;
		public int TargetY = 0;
		public int Velocity = 10;
		public Image Image;

		public SlideSprite(Image img)
		{
			Image = img;
			X = 0;
			Y = 0;
		}

		public SlideSprite(Image img, int X, int Y)
		{
			Image = img;
			this.X = X;
			TargetX = X;
			this.Y = Y;
			TargetY = Y;
		}

		public override void act()
		{
			if (X + Velocity < TargetX)
			{
				X += Velocity;
			}
			else if (X - Velocity > TargetX)
			{
				X -= Velocity;
			}
			else if (Math.Abs(X - TargetX) <= Velocity)
			{
				X = TargetX;
			}
			if (Y + Velocity < TargetY)
			{
				Y += Velocity;
			}
			else if (Y - Velocity > TargetY)
			{
				Y -= Velocity;
			}
			else if (Math.Abs(Y - TargetY) <= Velocity)
			{
				Y = TargetY;
			}

		}

		public override void paint(Graphics g)
		{
			//g.DrawImage(Image, X - (Image.Width / 2), Y - (Image.Height / 2));
			g.DrawImage(Image, X, Y);
		}
	}

}