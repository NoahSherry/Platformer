using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment05
{
	public class CollisionSprite : ImageSprite
	{
		protected List<CollisionSprite> sprites = new List<CollisionSprite>();
		protected List<CollisionSprite> toTrack = new List<CollisionSprite>();
		protected List<CollisionSprite> toUntrack = new List<CollisionSprite>();

		public List<CollisionSprite> TrackedSprites
		{
			get { return sprites; }
		}

		public CollisionSprite(Image image) : base(image)
		{
			toTrack.Add(this);
		}

		public void track(CollisionSprite s)
		{
			toTrack.Add(s);
		}

		public void untrack(CollisionSprite s)
		{
			toUntrack.Add(s);
		}

		public void updateTracking()
		{
			foreach (CollisionSprite s in toUntrack)
			{
				sprites.Remove(s);
			}
			toUntrack = new List<CollisionSprite>();
			foreach (CollisionSprite s in toTrack)
			{
				sprites.Add(s);
			}
			toTrack = new List<CollisionSprite>();
		}

		public override void Kill()
		{
			base.Kill();
			Parent.cRemove(this);
		}

		public List<CollisionSprite> getCollisions()
		{
			List<CollisionSprite> collisions = new List<CollisionSprite>();
			float c1x = X + (width / 2);
			float c1y = Y + (height / 2);
			foreach (CollisionSprite s in sprites)
			{
				if (s == this) continue;
				float c2x = s.X + (s.width / 2);
				float c2y = s.Y + (s.height / 2);
				if (Math.Abs(c1x - c2x) < ((width / 2) + (s.width / 2)) && Math.Abs(c1y - c2y) < ((height / 2) + (s.height / 2)))
				{
					collisions.Add(s);
				}
			}
			//Console.Out.WriteLine(collisions.Count);
			return collisions;
		}

	}
}