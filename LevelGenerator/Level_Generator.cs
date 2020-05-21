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

    //methods for initilizing level and update
    //update will handle adding and removing from a passed in layer
    //hold the avatar's position and contentManager (for making new platforms)
    class Level_Generator
    {
        ContentManager content;
        private int starting_density = 30;
        private Vector2 screen_dimensions = new Vector2();
        private Vector2 platform_size = new Vector2(198, 32);
        private Vector2 camera_position;
        private Vector2 max_jump;
        public float distance_travelled = 0;
        private int current_density;
        private AbsAvatarObject avatarObject;
        private AudioManager audio;
        private int updates = 0;

        public Level_Generator(ContentManager manager, Camera camera, AbsAvatarObject avatar, AudioManager audio)
        {
            content = manager;
            screen_dimensions.X = camera.Viewport.Width;
            screen_dimensions.Y = camera.Viewport.Height;
            camera_position = camera.Position;
            current_density = starting_density;
            avatarObject = avatar;
            this.audio = audio;


        }

        //attempt to find the max jump height of mario with some kinematic equations
        //0.03=current y acceleration of avatar 2.6=y velocity emparted on the avatar,
        //when transitioning to a jump state.  
        //Vf = Vi + a*t => (Vf-Vi)/a = t <--- to find time till peak ark (Vf=0 then)
        //d = Vi*t + 0.5*a*t^2 <---plug in values to get displacement
        //d = (Vi + Vf)/2 * t <------ for horizontal displacement
        //not sure how much i like this method so just using some magic numbers for now (11-21)
        // (does this even need to be a method?  it will only be called once.  idk just keep it all self
        // contained for now)
        //*with the current gravity mario can jump across a whole screen, I predict I will need to amend this later to find max x displacement*
        //consider looking into float vs double.  i know decimal is overkill and a possible performance hit
        private Vector2 Find_Max_Jump()
        {
            //variables for easy change to equations
            Vector2 result = new Vector2();
            float gravity = avatarObject.Acceleration.Y;
            float V_i_y = 2.6f;
            float V_i_x = 1.25f;
            float time_till_ark = V_i_y / gravity;
            //to give the player a little bit wiggle room, subtract 16
            BoundingBox avatar_hitbox = (BoundingBox)avatarObject.Hitbox;
            result.Y = (float)((V_i_y * time_till_ark) + (0.5 * -gravity * Math.Pow(time_till_ark, 2.0))) - 50 - (avatar_hitbox.Min.Y-avatar_hitbox.Max.Y);
            //Vi = Vf, ignore /2
            result.X = (float)(V_i_x * time_till_ark * 2) - 16;
            return result;
        }

        // add initial platoforms to the layer
        public void Initialize(Layer layer)
        {
            max_jump = Find_Max_Jump();
            // use this max jump to make sure there are some platforms that the avatar can reach,
            // these platforms will be refered to as the 'keystone' platforms
            // layer.Objects.Add(Valid_Keystone(layer.Objects));
            // layer.Objects.Add(Valid_Keystone(layer.Objects));
            // assume the call to initalize is before adding the avatar to the layer
            while (layer.Objects.Count < starting_density)
            {
                PlatformObject new_platform = Valid_Platform_Init(layer.Objects);
                layer.Objects.Add(new_platform);
            }
        }


        //try some weighted average selection changing by distance travelled
        private PlatformObject Select_Type(List<AbsObject>absObjects)
        {
            PlatformObject result;
            Random random_generator = new Random(DateTime.Now.Millisecond);
            //disappearing platforms have 10% chance, moving a 40% (50% culmulative), and normal 50% (total  100%)
            //get a random number, then adjust by distance travelled
            double odds = random_generator.NextDouble();
            //40,000 gives 0.025 @1,000; 0.05 @2000; 0.75 @3000 etc, by 20,000 travelled there will be no more standard platforms
            double adjust = distance_travelled / 40000.0;
            odds -= adjust;
            if(odds < 0.1)
            {
                result = new PlatformObject(new Vector2(), new Vector2(), -1, content);
            } else if(odds < 0.5)
            {
                result = new PlatformObject(new Vector2(), 300, content);
            }
            else
            {
                result = new PlatformObject(new Vector2(), -1, content);
            }
            return result;
        }



        private ItemObject Select_Item(List<AbsObject> absObjects)
        {
            ItemObject result;
            Random random_generator = new Random(DateTime.Now.Millisecond);
            int odds = random_generator.Next(0,2);
            if (odds == 0)
            {
                result = new ItemObject(Find_Location(absObjects), content, audio, "speed");
            }
            else if (odds == 1)
            {
                result = new ItemObject(Find_Location(absObjects), content, audio, "jump");
            }
            else
            {
                result = new ItemObject(Find_Location(absObjects), content, audio, "propeller");
            }

            return result;
        }



        //this code duplication :(
        //private PlatformObject Valid_Keystone(List<AbsObject> absObjects)
        //{
        //    //60 updates, hoping for ~5 seconds 
        //    PlatformObject result = new PlatformObject(Find_Keystone_Location(absObjects), 300, content);
        //    //First find some location on screen with the avatar, then check if that location is valid (does not 
        //    //cause any overlapping with other platforms)
        //    int i = 0;
        //    while (!(Check_Location(result, absObjects)) && (i < 15))
        //    {
        //        Vector2 possible_location = Find_Keystone_Location(absObjects);
        //        result.Position = possible_location;
        //        //n.b. hitbox does not currently update automatically when given a new position 
        //        result.Hitbox = new BoundingBox(new Vector3(possible_location.X, possible_location.Y, 0), new Vector3(possible_location.X + platform_size.X, possible_location.Y + platform_size.Y, 0));
        //        i++;
        //    }
        //    result.keystone = true;
        //    return result;
        //}

        //private Vector2 Find_Keystone_Location(List<AbsObject> absObjects)
        //{
        //    Vector2 result = new Vector2();
        //    Random random_generator = new Random(DateTime.Now.Millisecond);
        //    //currently preventing any platforms from having any part of them off screen
        //    result.X = random_generator.Next((int)(avatarObject.Position.X - max_jump.X + platform_size.X), (int)(avatarObject.Position.X + max_jump.X+1));
        //    result.X = MathHelper.Clamp(result.X, 0, screen_dimensions.X - platform_size.X);
        //    //going up the screen so the minimum must be the peak jump
        //    //if the avatar is jumping, their current y cord. cannot be jumped from, try to help with a random plus 50
        //    if (avatarObject.isGrounded)
        //    {
        //        result.Y = random_generator.Next((int)(avatarObject.Position.Y - max_jump.Y + 1), (int)avatarObject.Position.Y);

        //    }
        //    else
        //    {
        //        result.Y = random_generator.Next((int)(avatarObject.Position.Y - max_jump.Y + 50 + 1), (int)avatarObject.Position.Y);

        //    }
        //    return result;
        //}


        private PlatformObject Valid_Platform_Init(List<AbsObject> absObjects)
        {
            PlatformObject result = Select_Type(absObjects);
            result.Position = Find_Location(absObjects);
            //PlatformObject result = new PlatformObject(Find_Location(absObjects), -1, content);
            //First find some location on screen with the avatar, then check if that location is valid (does not 
            //cause any overlapping with other platforms)

            while (!(Check_Location(result, absObjects)))
            {
                Vector2 possible_location = Find_Location_Init(absObjects);
                result.Position = possible_location;
                //n.b. hitbox does not currently update automatically when given a new position 
                result.Hitbox = new BoundingBox(new Vector3(possible_location.X, possible_location.Y, 0), new Vector3(possible_location.X + platform_size.X, possible_location.Y + platform_size.Y, 0));
            }
            if (result.IsMoving)
            {
                Vector2 start = result.Position;
                Vector2 end = new Vector2(start.X + 250, start.Y);
                result.Change_Range(start, end);
            }
            return result;
        }

        private Vector2 Find_Location_Init(List<AbsObject> absObjects)
        {
            Vector2 result = new Vector2();
            Random random_generator = new Random(DateTime.Now.Millisecond);
            //upper bound given in next() is exclusive, add 1 to offset this
            //currently preventing any platforms from having any part of them off screen
            result.X = random_generator.Next((int)screen_dimensions.X + 1);
            result.Y = random_generator.Next((int)(camera_position.Y - 250), (int)(camera_position.Y + screen_dimensions.Y));
            if (result.Y > 3500)
            {
                Console.WriteLine("stop");
            }
            return result;
        }


        //creates (and returns) a new platform that does not intersect with any current platforms
        private PlatformObject Valid_Platform(List<AbsObject> absObjects)
        {
            PlatformObject result = Select_Type(absObjects);
            result.Position = Find_Location(absObjects);
            //PlatformObject result = new PlatformObject(Find_Location(absObjects), -1, content);
            //First find some location on screen with the avatar, then check if that location is valid (does not 
            //cause any overlapping with other platforms)

            while (!(Check_Location(result, absObjects)))
            {
                Vector2 possible_location = Find_Location(absObjects);
                result.Position = possible_location;
                //n.b. hitbox does not currently update automatically when given a new position 
                result.Hitbox = new BoundingBox(new Vector3(possible_location.X, possible_location.Y, 0), new Vector3(possible_location.X + platform_size.X, possible_location.Y + platform_size.Y, 0));
            }
            if (result.IsMoving)
            {
                Vector2 start = result.Position;
                Vector2 end = new Vector2(start.X + 250, start.Y);
                result.Change_Range(start, end);
            }
            return result;
        }

        private Vector2 Find_Location(List<AbsObject> absObjects)
        {
            Vector2 result = new Vector2();
            Random random_generator = new Random(DateTime.Now.Millisecond);
            //upper bound given in next() is exclusive, add 1 to offset this
            //currently preventing any platforms from having any part of them off screen
            result.X = random_generator.Next((int)screen_dimensions.X +1);
            result.Y = random_generator.Next((int)(camera_position.Y - screen_dimensions.Y/2), (int)(camera_position.Y + 50));
            if(result.Y > 3500)
            {
                Console.WriteLine("stop");
            }
            return result;
        }

        //check the bounding box of the platform to be added vs all the current platforms 
        private bool Check_Location(AbsObject to_be_added, List<AbsObject> absObjects)
        {
            bool result = true;
            int i = 0;
            //absObject's hitbox is nullable (i think thats right, its of type BoundingBox?, not
            //BoundingBox, hence the needed casts)
            BoundingBox checked_hitbox = (BoundingBox)to_be_added.Hitbox;
            //loop while true and i less than count
            while (result && i < absObjects.Count)
            {
                //result is made true when there is no intersect, false when there is, stoppping the loop
                result = !(checked_hitbox.Intersects((BoundingBox)absObjects[i].Hitbox));
                i += 1;
            }
            if(i > absObjects.Count)
            {
                Console.WriteLine("could not find non-colliding pllatform space");
            }

            return result;
        }

        //private bool Check_Keystone(PlatformObject platformObject)
        //{
        //    bool result = false;
        //    if((platformObject.Position.X > (avatarObject.Position.X + max_jump.X)) || platformObject.Position.X < (avatarObject.Position.X - max_jump.X) || !platformObject.isVisible)
        //    {
        //        result = true;
        //        platformObject.isVisible = false;
        //    }
        //    return result;
        //}

        private bool Remove_Platforms(List<AbsObject> absObjects)
        {
            bool result = false;
            // make use of the isVisible bool in absObject, no need to add another variable to platObject
            foreach(AbsObject platform in absObjects)
            {
                if (platform is PlatformObject)
                {

                    PlatformObject temp = (PlatformObject)platform;
                    BoundingBox temp_box = (BoundingBox)temp.Hitbox;
                    if(temp.Position.X != temp_box.Min.X && temp.Position.Y != temp_box.Min.Y)
                    {
                        //Console.WriteLine("hitbox mismatch");
                        temp.Sprite = new SpriteStatic(content.Load<Texture2D>("used_platform"), true);
                        temp.hitbox_rep = content.Load<Texture2D>("hitbox_wrong");
                    }
                    //50 is arbitrary padding
                    if (platform.Position.Y > screen_dimensions.Y + camera_position.Y + 100)
                    {
                        platform.isVisible = false;
                        if (temp.keystone)
                        {
                            result = true;
                        }
                    }
                }
            }
            absObjects.RemoveAll(platform => platform.isVisible == false);
            return result;
        }

        public void Update_Platforms(Layer layer, Camera camera)
        {
            //updte camera position, then use distance travelled to lower platform density logarithmicly
            distance_travelled += Math.Abs(camera_position.Y - camera.Position.Y);
            camera_position = camera.Position;
            // current_density = starting_density - (int)(Math.Log((distance_travelled + 1), 2) + (distance_travelled / 250));
            current_density = (int)MathHelper.Clamp(current_density, 5, (float)(-.001 * distance_travelled) + starting_density);

            // https://stackoverflow.com/questions/1582285/how-to-remove-elements-from-a-generic-list-while-iterating-over-it
            Remove_Platforms(layer.Objects);
            while (layer.Objects.Count < current_density)
            {
                PlatformObject new_platform = Valid_Platform(layer.Objects);
                layer.Objects.Add(new_platform);
            }
            updates++;
            if(updates % 1000 == 0)
            {
                AbsObject temp = Select_Item(layer.Objects);
                layer.Objects.Add(temp);
            }
        }
    }
}
