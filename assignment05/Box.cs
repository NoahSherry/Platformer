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
	public class Box : PhysicsSprite
	{

		public Box(int x, int y) : base(Properties.Resources.box, x, y)
		{
			setMotionModel(0);
		}

		public override void paint(Graphics g)
		{
			base.paint(g);
			//g.FillRectangle(new SolidBrush(Color.LawnGreen), 0, 0, width, height);
		}
	}
}