using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;


namespace template_test
{
    public class Layer
    {

        public Vector2 Parallax { get; set; }

        protected List<AbsObject> _objects;

        public List<AbsObject> Objects
        {
            get
            {
                return _objects;
            }
            set
            {
                _objects = value;
            }
        }

        public Layer(Camera camera)
        {
            _camera = camera;
            Parallax = Vector2.One;
            Objects =  new List<AbsObject>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetViewMatrix(Parallax));

            foreach (AbsObject temp in Objects)
            {
                if (temp.isVisible)
                {
                    temp.Draw(spriteBatch);
                }
            }
            spriteBatch.End();


        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, _camera.GetViewMatrix(Parallax));
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(_camera.GetViewMatrix(Parallax)));
        }

        private readonly Camera _camera;
    }
}