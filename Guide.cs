// Guide.cs - 16/03/2016
// Eric Policaro

using System.Reflection;
using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Guide
    {
        public Guide(Rectangle container)
        {
            _container = container;
            Color = Color.Magenta;
            Orientation = Orientation.Vertical;
        }

        public Color Color
        {
            get; set;
        }

        public Orientation Orientation
        {
            get; set; 
        }

        public void Update(Mouse m)
        {
            if(m.LeftDown)
                _lastMouse = m.Position;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawLine(Start, End, Color);
        }

        private Vector2 Start
        {
            get
            {
                bool isHorizontal = Orientation == Orientation.Horizontal;
                return new Vector2
                {
                    X = isHorizontal ? _container.X : Location,
                    Y = isHorizontal ? Location : _container.Y,
                };
            }
        }

        private Vector2 End
        {
            get
            {
                bool isHorizontal = Orientation == Orientation.Horizontal;
                return new Vector2
                {
                    X = isHorizontal ? _container.Right : Location,
                    Y = isHorizontal ? Location : _container.Bottom,
                };
            }
        }

        private float Location
        {
            get
            {
                bool isHorizontal = Orientation == Orientation.Horizontal;
                return isHorizontal ? _lastMouse.Y : _lastMouse.X;
            }
        }

        private Vector2 _lastMouse;
        private Rectangle _container;
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }
}