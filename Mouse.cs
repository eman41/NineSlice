// Mouse.cs - 18/03/2016
// Eric Policaro

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NineSlice
{
    public class Mouse
    {
        public Mouse(Texture2D cursor)
        {
            _cursor = cursor;
        }

        public Vector2 Position
        {
            get { return _position; }
        }

        public bool LeftDown
        {
            get { return _leftDown; }
        }

        public void Update(GameTime time)
        {
            var current = Microsoft.Xna.Framework.Input.Mouse.GetState();
            _position = new Vector2
            {
                X = current.Position.X,
                Y = current.Position.Y
            };
            _leftDown = current.LeftButton == ButtonState.Pressed;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_cursor, _position);
        }

        private bool _leftDown;
        private Vector2 _position;

        private readonly Texture2D _cursor;
    }
}