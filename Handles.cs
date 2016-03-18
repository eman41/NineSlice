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
    public class Handles
    {
        public static void DrawHandle(SpriteBatch sb, Rectangle rect, Color c = default(Color), BufferSet buffers = null)
        {
            if (buffers != null)
            {
                Rectangle screen = NineSlice.Screen;
                var leftX = rect.X + buffers.Left;
                var rightX = rect.Right - buffers.Right;
                var topY = rect.Top + buffers.Top;
                var botY = rect.Bottom - buffers.Bot;

                sb.DrawLine(new Vector2(leftX, 0), new Vector2(leftX, screen.Bottom), c);
                sb.DrawLine(new Vector2(rightX, 0), new Vector2(rightX, screen.Bottom), c);
                sb.DrawLine(new Vector2(0, topY), new Vector2(screen.Right, topY), c);
                sb.DrawLine(new Vector2(0, botY), new Vector2(screen.Right, botY), c);
            }

            sb.DrawRectangle(rect, c);
        }
    }
}