using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace template_test
{
    class BlockObject : AbsObject
    {
        public Queue<ItemObject> items = new Queue<ItemObject>();
        
        public IBlockState blockState;
        public IBlockState blockMoveState;
        public ContentManager content;
        public Vector2 sPosition;
        public bool hasItems = false;
        public AudioManager audio;


        public BlockObject(Vector2 startingPosition, ContentManager cont,AudioManager audio, string startingState) 
        {
            _opacity = 1.0f;
            _objectsToNotCollide = new List<AbsObject>();
            _objectsToAdd = new List<AbsObject>();
            _hitbox = new BoundingBox(new Vector3(startingPosition.X, startingPosition.Y, 0), new Vector3(startingPosition.X + 16, startingPosition.Y + 16, 0));
            isVisible = true;
            deleteThis = false;
            _position = startingPosition;
            sPosition = startingPosition;
            content = cont;
            _acceleration = new Vector2(0);
            blockMoveState = new BrickDownState(this);
            this.audio = audio;
            switch (startingState)
            {
                case "brick":
                    blockState = new BrickState(this);
                    break;
                case "question":
                    blockState = new QuestionState(this);
                    break;
                case "floor":
                    blockState = new FloorState(this);
                    break;
                case "stair":
                    blockState = new StairState(this);
                    break;
                case "hidden":
                    blockState = new HiddenState(this);
                    break;
                case "used":
                    blockState = new UsedState(this);
                    break;
                default:
                    Console.WriteLine("startingState not found: brickState applied");
                    blockState = new BrickState(this);
                    break;
            }

        }

        

        public override void Update(GameTime gameTime)
        {
            if ((_position.Y < sPosition.Y - 8))
                _velocity = -_velocity;
            if (blockMoveState is BrickBumpedState && _position.Y > sPosition.Y)
            {
                this.Teleport((int)sPosition.X, (int)sPosition.Y);
                blockMoveState = new BrickDownState(this);
            }
            _sprite.Update(gameTime);
        }


        public void Use()
        {
            blockState.Use();
        }

        public void Bump()
        {
            blockState.Bump();
        }

        public void Reveal()
        {
            blockState.Reveal();
        }

        

        

       

        public override void Collide(List<AbsObject> collidedObjects)
        {
            _objectsToNotCollide.Clear();
            foreach (AbsObject obj in collidedObjects)
            {
                int collisionType = (int)Collision.GetCollisionType(this, obj);
                if (obj is MarioObject && collisionType == 3)
                {
                    if (items.Count != 0)
                    {

                        ItemObject item = items.Dequeue();
                        item.Bump();
                        item.isVisible = true;
                        item.speedD(obj.Position);
                        
                        blockState.Bump();
                        

                    }
                    else if (items.Count == 0 &&  this.blockState is BrickState && !(((MarioObject)obj).powerUpState is SmallState))
                    {
                        audio.PlaySound("breakBlock");
                        this.isVisible = false;
                        this.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                        AbsObject part1 = new ItemObject(this._position, content,audio, "brick_piece");
                        part1.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                        part1._velocity = new Vector2(-0.2f,-0.75f);
                        AbsObject part2 = new ItemObject(new Vector2(this._position.X + 8, this._position.Y), content,audio, "brick_piece");
                        part2.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                        part2._velocity = new Vector2(0.2f,-0.75f);
                        AbsObject part3 = new ItemObject(new Vector2(this._position.X, this._position.Y + 8), content,audio, "brick_piece");
                        part3.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                        part3._velocity = new Vector2(-0.1f,-1f);
                        AbsObject part4 = new ItemObject(new Vector2(this._position.X + 8, this._position.Y + 8), content,audio, "brick_piece");
                        part4.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                        part3._velocity = new Vector2(0.1f,-1f);
                        this._objectsToAdd.Add(part1);
                        this._objectsToAdd.Add(part2);
                        this._objectsToAdd.Add(part3);
                        this._objectsToAdd.Add(part4);
                        this.deleteThis = true;
                    }
                    else
                    {
                        audio.PlaySound("bump");
                        blockState.Bump();
                    }
                    
                }
            }
        }
    }
}
