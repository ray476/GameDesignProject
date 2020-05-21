using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_test
{
    class Collision
    {
        private int _cellWidth;
        private int _cellHeight;
        private Dictionary<AbsObject, BoundingBox> _occupiedCells;
        public bool output = false;

        public enum CollisionType { TSide = 1, RSide, BSide, LSide, TRCorner, BRCorner, BLCorner, TLCorner }

        public Collision(int windowWidth, int windowHeight, int gridColumns, int gridRows)
        {
            _cellWidth = windowWidth / gridColumns;
            _cellHeight = windowHeight / gridRows;
            _occupiedCells = new Dictionary<AbsObject, BoundingBox>();
        }

        public void Update(List<AbsObject> gameObjects)
        {
            if (output)
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Update called!");
                Console.WriteLine("");
            }


            Dictionary<AbsObject, List<AbsObject>> collisionPairs;
            Single timeLeft = 1;
            List<AbsObject> objectsToCheck = FindMovingObjects(gameObjects);
            CalculateCells(gameObjects, _occupiedCells, _cellWidth, _cellHeight);
            do
            {
                collisionPairs = BroadSweep(objectsToCheck, gameObjects, _occupiedCells);
                Single minTimeToCollision = -1;
                NarrowSweep(ref collisionPairs, ref objectsToCheck, ref minTimeToCollision, timeLeft);
                if (minTimeToCollision >= 0)
                {
                    timeLeft -= minTimeToCollision;
                    PartialUpdate(gameObjects, minTimeToCollision);
                    HandleCollisions(collisionPairs);
                }
                objectsToCheck = FindMovingObjects(objectsToCheck);
                CalculateCells(objectsToCheck, _occupiedCells, _cellWidth, _cellHeight);
                if (output)
                {
                    Console.WriteLine("");
                }
            } while (collisionPairs.Count() > 0);
            PartialUpdate(gameObjects, timeLeft);
        }

        private static List<AbsObject> FindMovingObjects(List<AbsObject> gameObjects)
        {
            List<AbsObject> movingObjects = new List<AbsObject>();
            foreach (AbsObject gameObject in gameObjects)
            {
                if (!(gameObject.Velocity == Vector2.Zero))
                {
                    movingObjects.Add(gameObject);
                }
            }
            return movingObjects;
        }

        private static void CalculateCells(List<AbsObject> gameObjects, Dictionary<AbsObject, BoundingBox> occupiedCells, int cellWidth, int cellHeight)
        {
            foreach (AbsObject gameObject in gameObjects)
            {
                if (!gameObject.Hitbox.HasValue)
                {
                    continue;
                }
                BoundingBox hitbox = gameObject.Hitbox.Value;
                Vector2 velocity = gameObject.Velocity;
                Vector3 occupiedCellsMin = new Vector3(0);
                Vector3 occupiedCellsMax = new Vector3(0);
                if (velocity.X >= 0)
                {
                    occupiedCellsMin.X = (int)hitbox.Min.X / cellWidth;
                    occupiedCellsMax.X = (int)(hitbox.Max.X + velocity.X) / cellWidth;
                }
                else
                {
                    occupiedCellsMin.X = (int)(hitbox.Min.X + velocity.X) / cellWidth;
                    occupiedCellsMax.X = (int)hitbox.Max.X / cellWidth;
                }
                if (velocity.Y >= 0)
                {
                    occupiedCellsMin.Y = (int)hitbox.Min.Y / cellWidth;
                    occupiedCellsMax.Y = (int)(hitbox.Max.Y + velocity.Y) / cellHeight;
                }
                else
                {
                    occupiedCellsMin.Y = (int)(hitbox.Min.Y + velocity.Y) / cellHeight;
                    occupiedCellsMax.Y = (int)hitbox.Max.Y / cellWidth;
                }
                if (occupiedCells.ContainsKey(gameObject))
                {
                    occupiedCells[gameObject] = new BoundingBox(occupiedCellsMin, occupiedCellsMax);
                }
                else
                {
                    occupiedCells.Add(gameObject, new BoundingBox(occupiedCellsMin, occupiedCellsMax));
                }
            }
        }

        private static Dictionary<AbsObject, List<AbsObject>> BroadSweep(List<AbsObject> objectsToCheck, List<AbsObject> gameObjects, Dictionary<AbsObject, BoundingBox> occupiedCells)
        {
            bool output = false;
            if(output)
            {
                Console.WriteLine(" BroadSweep called!");
            }
        
            Dictionary<AbsObject, List<AbsObject>> possibleCollisionPairs = new Dictionary<AbsObject, List<AbsObject>>();
            foreach (AbsObject objectToCheck in objectsToCheck)
            {
                if (!objectToCheck.Hitbox.HasValue)
                {
                    continue;
                }
                gameObjects.Remove(objectToCheck);
                foreach (AbsObject gameObject in gameObjects)
                {
                    if (!gameObject.Hitbox.HasValue)
                    {
                        continue;
                    }
                    if (occupiedCells[objectToCheck].Intersects(occupiedCells[gameObject]) && !BorderlessIntersects(objectToCheck.Hitbox.Value, gameObject.Hitbox.Value) &&
                        !objectToCheck.ObjectsToNotCollide.Contains(gameObject))
                    {
                        if (output)
                        {

                            Console.WriteLine("  Possible collision pair detected!");
                            if (objectToCheck is MarioObject)
                            {
                                Console.Write("   (Mario, ");
                            }
                            else if (objectToCheck is EnemyObject)
                            {
                                Console.Write("   (Enemy, ");
                            }
                            else if (objectToCheck is ItemObject)
                            {
                                Console.Write("   (Item, ");
                            }
                            else if (objectToCheck is BlockObject)
                            {
                                Console.Write("   (Block, ");
                            }
                            if (gameObject is MarioObject)
                            {
                                Console.WriteLine("Mario)");
                            }
                            else if (gameObject is EnemyObject)
                            {
                                Console.WriteLine("Enemy)");
                            }
                            else if (gameObject is ItemObject)
                            {
                                Console.WriteLine("Item)");
                            }
                            else if (gameObject is BlockObject)
                            {
                                Console.WriteLine("Block)");
                            }
                        }

                        if (possibleCollisionPairs.ContainsKey(objectToCheck))
                        {
                            possibleCollisionPairs[objectToCheck].Add(gameObject);
                        }
                        else
                        {
                            possibleCollisionPairs.Add(objectToCheck, new List<AbsObject> { gameObject });
                        }
                    }
                }
            }
            gameObjects.AddRange(objectsToCheck);
            return possibleCollisionPairs;
        }

        private static Boolean BorderlessIntersects(BoundingBox hitbox1, BoundingBox hitbox2)
        {
            Boolean intersects = hitbox1.Intersects(hitbox2);
            if (intersects)
            {
                if (hitbox1.Min.X == hitbox2.Max.X || hitbox1.Max.X == hitbox2.Min.X || hitbox1.Min.Y == hitbox2.Max.Y || hitbox1.Max.Y == hitbox2.Min.Y)
                {
                    intersects = false;
                }
            }
            return intersects;
        }
        
        private static float? BorderlessIntersects(Ray ray, BoundingBox hitbox)
        {
            // Console.WriteLine("  BorderlessIntersects (ray) called!");

            float? intersectDist = ray.Intersects(hitbox);
            if (intersectDist != null)
            {
                if ((ray.Position.X == hitbox.Min.X && ray.Direction.X <= 0) || (ray.Position.X == hitbox.Max.X && ray.Direction.X >= 0) ||
                    (ray.Position.Y == hitbox.Min.Y && ray.Direction.Y <= 0) || (ray.Position.Y == hitbox.Max.Y && ray.Direction.Y >= 0))
                {
                    intersectDist = null;
                }
            }

            // Console.WriteLine("   Result: " + intersectDist);

            return intersectDist;
        }

        private static void NarrowSweep(ref Dictionary<AbsObject, List<AbsObject>> collisionPairs, ref List<AbsObject> objectsToCheck, ref float minTimeToCollision, Single maxTimeLeft)
        {
            bool showOutput = false;
            if (showOutput)
            {
                Console.WriteLine(" NarrowSweep called!");
            }
            String output = "";

            Dictionary<AbsObject, List<AbsObject>> simultaneousCollisions = new Dictionary<AbsObject, List<AbsObject>>();
            foreach (AbsObject obj1 in collisionPairs.Keys)
            {
                foreach (AbsObject obj2 in collisionPairs[obj1])
                {
                    float timeToCollision = CollisionTime(obj1, obj2);
                    if (timeToCollision >= 0 && timeToCollision < maxTimeLeft && (timeToCollision <= minTimeToCollision || minTimeToCollision < 0))
                    {

                        
                        output += "  Actual collision detected!\n";
                        if (obj1 is MarioObject)
                        {
                            output += "   (Mario, ";
                        }
                        else if (obj1 is EnemyObject)
                        {
                            output += "   (Enemy, ";
                        }
                        else if (obj1 is ItemObject)
                        {
                            output += "   (Item, ";
                        }
                        else if (obj1 is BlockObject)
                        {
                            output += "   (Block, ";
                        }
                        if (obj2 is MarioObject)
                        {
                            output += "Mario)\n";
                        }
                        else if (obj2 is EnemyObject)
                        {
                            output += "Enemy)\n";
                        }
                        else if (obj2 is ItemObject)
                        {
                            output += "Item)\n";
                        }
                        else if (obj2 is BlockObject)
                        {
                            output += "Block)\n";
                        }
                        

                        if (timeToCollision < minTimeToCollision || minTimeToCollision < 0)
                        {
                            minTimeToCollision = (Single)timeToCollision;
                            simultaneousCollisions.Clear();

                            output = "";

                        }
                        if (!simultaneousCollisions.ContainsKey(obj1))
                        {
                            simultaneousCollisions.Add(obj1, new List<AbsObject>());
                        }
                        if (!simultaneousCollisions.ContainsKey(obj2))
                        {
                            simultaneousCollisions.Add(obj2, new List<AbsObject>());
                        }

                        simultaneousCollisions[obj1].Add(obj2);
                        simultaneousCollisions[obj2].Add(obj1);
                    }
                }
            }
            collisionPairs = simultaneousCollisions;
            foreach (AbsObject obj in simultaneousCollisions.Keys)
            {
                if (!objectsToCheck.Contains(obj))
                    objectsToCheck.Add(obj);
            }
            if (showOutput)
            {
                Console.Write(output);
                Console.WriteLine("  Collided objects count: " + simultaneousCollisions.Count);
            }
        }

        private static float CollisionTime(AbsObject obj1, AbsObject obj2)
        {

            // Console.WriteLine("  CornerContactTime 1 called!");

            float minTime = CornerContactTime(obj1, obj2);

            // Console.WriteLine("  CornerContactTime 2 called!");

            float time = CornerContactTime(obj2, obj1);
            if (time >= 0 && time < minTime)
            {
                minTime = time;
            }
            return minTime;
        }

        private static float CornerContactTime(AbsObject primaryObj, AbsObject contactedObject)
        {

            // Console.WriteLine("  CornerContactTime called!");

            float minTime = -1;
            Vector2 velocity = primaryObj.Velocity - contactedObject.Velocity;
            Vector2 unitVelocity = Vector2.Normalize(velocity);
            Boolean immediateCollision = BorderlessIntersects(contactedObject.Hitbox.Value, new BoundingBox(new Vector3(primaryObj.Hitbox.Value.Min.X + unitVelocity.X, 
                primaryObj.Hitbox.Value.Min.Y + unitVelocity.Y, 0), new Vector3(primaryObj.Hitbox.Value.Max.X + unitVelocity.X, primaryObj.Hitbox.Value.Max.Y + unitVelocity.Y, 0)));
            if (immediateCollision)
            {
                minTime = 0;

                // Console.WriteLine("   Immediate collision detected!");

            }
            else
            {
                List<Ray> rays = GetRays(primaryObj.Hitbox.Value.GetCorners(), velocity);
                foreach (Ray ray in rays)
                {
                    float? time = BorderlessIntersects(ray, contactedObject.Hitbox.Value);
                    if (time != null && time > 0 && (time < minTime || minTime < 0))
                    {
                        minTime = (Single)time;
                    }
                }
            }

            // Console.WriteLine("   minTime = " + minTime);

            return minTime;
        }

        private static List<Ray> GetRays(Vector3[] points, Vector2 velocity)
        {
            List<Ray> rays = new List<Ray>();
            foreach (Vector3 point in points)
            {
                rays.Add(new Ray(point, new Vector3(velocity.X, velocity.Y, 0)));
            }
            return rays;
        }

        private static void PartialUpdate(List<AbsObject> gameObjects, Single time)
        {
            bool output = false;
            if (output)
            {
                Console.WriteLine(" PartialUpdate called!");
                Console.WriteLine("  Update time = " + time);
            }

            List<AbsObject> movingObjects = FindMovingObjects(gameObjects);
            foreach (AbsObject movingObject in movingObjects)
            {
                movingObject.Displace(movingObject.Velocity.X * time, movingObject.Velocity.Y * time);
                if (output)
                {
                    Console.WriteLine("  Displaced an object!");

                    if (movingObject is MarioObject)
                    {
                        Console.WriteLine("   Object type: Mario");
                    }
                    else if (movingObject is EnemyObject)
                    {
                        Console.WriteLine("   Object type: Enemy");
                    }
                    else if (movingObject is ItemObject)
                    {
                        Console.WriteLine("   Object type: Item");
                    }
                    else if (movingObject is BlockObject)
                    {
                        Console.WriteLine("   Object type: Block");
                    }

                    Console.WriteLine("   Object velocity = (" + movingObject.Velocity.X + ", " + movingObject.Velocity.Y + ")");
                    Console.WriteLine("   xDisp: " + movingObject.Velocity.X * time + " yDisp: " + movingObject.Velocity.Y * time);
                    Console.WriteLine("   xPos: " + movingObject.Position.X + " yPos: " + movingObject.Position.Y);
                }
            }
        }

        private static void HandleCollisions(Dictionary<AbsObject, List<AbsObject>> collisionPairs)
        {
            bool output = false;
            if (output)
            {
                Console.WriteLine(" HandleCollisions called!");
                Console.WriteLine("  Collisions to handle = " + collisionPairs.Keys.Count);
            }

            foreach (AbsObject primaryObj in collisionPairs.Keys)
            {
                primaryObj.Collide(collisionPairs[primaryObj]);
            }
        }

        public static Collision.CollisionType GetCollisionType(AbsObject primaryObj, AbsObject collidedObj)
        {

            // Console.WriteLine("   CollisionType called!");

            CollisionType type;
            Boolean top = primaryObj.Hitbox.Value.Min.Y - collidedObj.Hitbox.Value.Max.Y >= 0;
            Boolean bottom = primaryObj.Hitbox.Value.Max.Y - collidedObj.Hitbox.Value.Min.Y <= 0;
            Boolean right = primaryObj.Hitbox.Value.Max.X - collidedObj.Hitbox.Value.Min.X <= 0;
            Boolean left = primaryObj.Hitbox.Value.Min.X - collidedObj.Hitbox.Value.Max.X >= 0;
            if (top)
            {
                type = CollisionType.TSide;
                if (right)
                {
                    type = CollisionType.TRCorner;
                } else if (left)
                {
                    type = CollisionType.TLCorner;
                }
            } else if (bottom)
            {
                type = CollisionType.BSide;
                if (right)
                {
                    type = CollisionType.BRCorner;
                } else if (left)
                {
                    type = CollisionType.BLCorner;
                }
            } else
            {
                if (right)
                {
                    type = CollisionType.RSide;
                }
                else
                {
                    type = CollisionType.LSide;
                }
            }

            // Console.WriteLine("    Collision type = " + type);

            return type;
        }

        /*
        public static int GetCollisionType(AbsObject primaryObj, AbsObject collidedObj)
        {

            Console.WriteLine("   CollisionType called!");

            int type;
            Boolean top = primaryObj.Hitbox.Min.Y - collidedObj.Hitbox.Max.Y >= 0;
            Boolean bottom = primaryObj.Hitbox.Max.Y - collidedObj.Hitbox.Min.Y <= 0;
            Boolean right = primaryObj.Hitbox.Max.X - collidedObj.Hitbox.Min.X <= 0;
            Boolean left = primaryObj.Hitbox.Min.X - collidedObj.Hitbox.Max.X >= 0;
            if (top)
            {
                type = 1;
                if (right)
                {
                    type = 5;
                }
                else if (left)
                {
                    type = 8;
                }
            }
            else if (bottom)
            {
                type = 3;
                if (right)
                {
                    type = 6;
                }
                else if (left)
                {
                    type = 7;
                }
            }
            else
            {
                if (right)
                {
                    type = 2;
                }
                else
                {
                    type = 4;
                }
            }

            Console.WriteLine("    Collision type = " + type);

            return type;
        }
        */
    }
}
