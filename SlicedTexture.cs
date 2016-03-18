// ----------------------------------------------------------------------------
//  Copyright © 2016 Schell Games, LLC. All Rights Reserved. 
// 
//  Author: Eric Policaro
// 
//  Date: 03/18/2016
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NineSlice
{
    public class SlicedTexture
    {
        public SlicedTexture(GraphicsDevice gfx, SpriteBatch sb, TextureInfo info)
        {
            _sb = sb;
            _gfx = gfx;
            _info = info;
            RefreshTexture(_gfx, _sb);
        }

        public Vector2 Position
        {
            get; set;
        }

        public Rectangle Bounds
        {
            get { return _info.Target; }
        }

        public BufferSet Buffer
        {
            get { return _info.Buffers; }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_final, Position, Color.White);
        }

        private void RefreshTexture(GraphicsDevice gfx, SpriteBatch batch)
        {
            var renderTarget = new RenderTarget2D(gfx, Bounds.Width, Bounds.Height);
            gfx.SetRenderTarget(renderTarget);

            DrawPieces(batch);

            gfx.SetRenderTarget(null);
            _final = renderTarget;
        }

        private void DrawPieces(SpriteBatch sb)
        {
            Rectangle temp = _info.Target;
            Rectangle r = temp;
            r.Location = Point.Zero;
            _info.Target = r;

            var sourceSlice = SliceRectangle(_info.Source.Bounds, _info.Buffers);
            var destSlices = SliceRectangle(_info.Target, _info.Buffers);

            sb.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, 
                null, null, null, null);
            for (int i = 0; i < sourceSlice.Count; i++)
            {
                sb.Draw(_info.Source, destSlices[i], sourceSlice[i], Color.White);
            }
            sb.End();

            _info.Target = temp;
        }

        private List<Rectangle> SliceRectangle(Rectangle rect, BufferSet buffers)
        {
            var result = new List<Rectangle>();

            int x = rect.X;
            int y = rect.Y;
            int bottom = rect.Bottom;
            int right = rect.Right;

            int middleWidth = rect.Width - (buffers.Left + buffers.Right);
            var s0 = new Rectangle
            {
                X = x,
                Y = y,
                Width = buffers.Left,
                Height = buffers.Top,
            };
            result.Add(s0);

            var s1 = new Rectangle
            {
                X = x + buffers.Left,
                Y = y,
                Width = middleWidth,
                Height = buffers.Top,
            };
            result.Add(s1);

            var s2 = new Rectangle
            {
                X = right - buffers.Right,
                Y = y,
                Width = buffers.Right,
                Height = buffers.Top
            };
            result.Add(s2);

            // MIDDLE ROW
            int middleHeight = rect.Height - (buffers.Top + buffers.Bot);
            var s3 = new Rectangle
            {
                X = x,
                Y = y + buffers.Top,
                Width = buffers.Left,
                Height = middleHeight,
            };
            result.Add(s3);

            var s4 = new Rectangle
            {
                X = x + buffers.Left,
                Y = y + buffers.Top,
                Width = middleWidth,
                Height = middleHeight,
            };
            result.Add(s4);

            var s5 = new Rectangle
            {
                X = right - buffers.Right,
                Y = y + buffers.Top,
                Width = buffers.Right,
                Height = middleHeight
            };
            result.Add(s5);

            // BOTTOM ROW
            var s6 = new Rectangle
            {
                X = x,
                Y = bottom - buffers.Bot,
                Width = buffers.Left,
                Height = buffers.Bot,
            };
            result.Add(s6);

            var s7 = new Rectangle
            {
                X = x + buffers.Left,
                Y = bottom - buffers.Bot,
                Width = middleWidth,
                Height = buffers.Bot,
            };
            result.Add(s7);

            var s8 = new Rectangle
            {
                X = right - buffers.Right,
                Y = bottom - buffers.Bot,
                Width = buffers.Right,
                Height = buffers.Bot
            };
            result.Add(s8);

            return result;
        }

        private Texture2D _final;

        private readonly GraphicsDevice _gfx;
        private readonly SpriteBatch _sb;
        private readonly TextureInfo _info;
    }

    public class TextureInfo
    {
        public Texture2D Source { get; set; }
        public Rectangle Target { get; set; }
        public BufferSet Buffers { get; set; }
    }

    public class BufferSet
    {
        public BufferSet() : this(0)
        {
        }
        
        public BufferSet(int size) : this(size, size, size, size)
        {
        }

        public BufferSet(int top, int bot, int left, int right)
        {
            Top = top;
            Bot = bot;
            Left = left;
            Right = right;
        }

        public int Top   { get; set; }
        public int Bot   { get; set; }
        public int Left  { get; set; }
        public int Right { get; set; }
    }
}