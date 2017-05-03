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
	public class TextSprite : Sprite
	{
		public String text;
		public Boolean visible;
		public int x;
		public int y;
		public int font = 24;

		public TextSprite(int x, int y, String text)
		{
			this.x = x;
			this.y = y;
			this.text = text;
			visible = false;
		}

		public void changeLocation(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public void setVisibility(Boolean visible)
		{
			this.visible = visible;
		}

		public void fontResize(int width, int height)
		{
			font = (int)(12 + (12 * (Math.Min(width, height) / 705)));
		}

		public override void paint(Graphics g)
		{
			//base.paint(g);
			if (visible) g.DrawString(text, new Font("Ubuntu", font), Brushes.Black, x, y);
		}
	}
}