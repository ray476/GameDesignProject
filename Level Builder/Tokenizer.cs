using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

// this seems to work as written.  The major problem is not being able to use the mario + block factories.  They require a state, which needs can 
// only be instantiated from a non null object, which we dont have.  (chicken and the egg)  Maybe look at some way to change the factories
// or add some null sprite behavior?

namespace template_test
{
    class Tokenizer
    {
        public ContentManager content;

        private String _fileName;
        private MarioObject mario;
        public AudioManager audio;
        public String FileName
        {
            set { _fileName = value; }
        }

        public Tokenizer(String filename, ContentManager content)
        {
            _fileName = filename;
            this.content = content;
            this.audio = new AudioManager(content);
        }


        private List<AbsObject> Parse(string[] input)
        {
            //let this be a list for the floor
            List<AbsObject> sprites = new List<AbsObject>();
            switch (input[0].ToLower())
            {
                case "item":
                    sprites.Add(new ItemObject(new Vector2(float.Parse(input[1]), float.Parse(input[2])),content,audio, input[3]));
                    break;
                case "enemy":
                    sprites.Add(new EnemyObject(new Vector2(float.Parse(input[1]), float.Parse(input[2])), content,audio, input[3]));
                    break;
                case "block":
                    //sprites.Add(new BlockObject(new Vector2(float.Parse(input[1]), float.Parse(input[2])), content, input[3]));
                    ItemObject item = null;
                    Queue<ItemObject> temp = new Queue<ItemObject>();
                    int coinCount = 0;
                    if (input[4] != "null")
                    {
                        if (input[4] == "coin")
                        {
                            while (coinCount < Convert.ToInt32(input[5]))
                            {
                                item = new ItemObject(new Vector2(float.Parse(input[1]), float.Parse(input[2])), content,audio, input[4]);
                                item.inBlock = true;
                                item.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                                item.isVisible = false;
                                sprites.Add(item);
                                temp.Enqueue(item);
                                coinCount++;

                            }
                        }
                        else
                        {
                            item = new ItemObject(new Vector2(float.Parse(input[1]), float.Parse(input[2])), content,audio, input[4]);
                            item.inBlock = true;
                            item.Hitbox = new BoundingBox(new Vector3(0), new Vector3(0));
                            item.isVisible = false;
                            sprites.Add(item);
                            temp.Enqueue(item);
                        }

                    }
                    BlockObject block = new BlockObject(new Vector2(float.Parse(input[1]), float.Parse(input[2])), content,audio, input[3]);
                    if(input[4] != "null")
                        block.hasItems = true;
                    block.items = temp;
                    sprites.Add(block);


                    break;
                // not implemnted for this sprint -SR
                //case "projectile":
                    //sprites.Add(_projectileFactory.build(new Vector2(float.Parse(input[1]), float.Parse(input[2])), input[3]));
                    //break;
                case "mario":
                    mario = new MarioObject(new Vector2(float.Parse(input[1]), float.Parse(input[2])), content, audio);
                    //this would allow for initial velocity to be changed, but the state is still in right facing idle
                    //could have to have the state in the level def. as well
                    //mario._velocity = new Vector2(float.Parse(input[3]), float.Parse(input[4]));
                    sprites.Add(mario);
                    //sprites.Add(new MarioObject(new Vector2(float.Parse(input[1]), float.Parse(input[2])), content));
                    break;
                case "floor":
                    //there are a few other ways to go about this.  I chose this one
                    for(int q = 0; q < (Int32.Parse(input[2])-Int32.Parse(input[1]))/16; q++)
                    {
                        //2 rows of blocks
                        sprites.Add(new BlockObject(new Vector2((q * 16 + Int32.Parse(input[1])), 464.0f),content,audio, input[0].ToLower()));
                        sprites.Add(new BlockObject(new Vector2((q * 16 + Int32.Parse(input[1])), 448.0f), content,audio, input[0].ToLower()));

                    }
                    break;
                case "stairs_up":
                    int i = 0;
                    int first = Int32.Parse(input[1]);
                    // each run of this loop builds one column of the stairs, height input must be at least 1 or nothing will draw
                    for(i = 0; i < (Int32.Parse(input[2])); i++)
                    {
                        //base of column (when i=0 inner loop body wont run) assuming 480 to the bottom of the screen + 2 rows of floor blocksa
                        sprites.Add(new BlockObject(new Vector2((first + (16 * i)), 432.0f), content,audio, "stair"));
                        //now build the height of the column
                        for(int j = 0; j < i; j++)
                        {
                            //same assumptions about floor as above 
                            sprites.Add(new BlockObject(new Vector2((first + (16 * i)), (416.0f-(16*j))), content,audio, "stair"));

                        }
                    }
                    //this loop repeats the last column (this is why i is declared outside of the for loop, should contain the value of i
                    //that 'broke' the outer while loop)
                    for(int r = 0; r < (Int32.Parse(input[3])); r++)
                    {
                        sprites.Add(new BlockObject(new Vector2((first + (16 * i) + (16*r)), 432.0f), content,audio, "stair"));
                        //i is 1 larger than it needs to be
                        for (int j = 0; j < (i-1); j++)
                        {
                            sprites.Add(new BlockObject(new Vector2((first + (16 * i) + (16*r)), (416.0f - (16 * j))), content,audio, "stair"));

                        }
                    }
                    break;
                case "stairs_down":
                    int fir = Int32.Parse(input[1]);
                    int height = Int32.Parse(input[2]);
                    int repeats = 0;
                    // repeat first
                    for (repeats = 0; repeats < (Int32.Parse(input[3])); repeats++)
                    {
                        sprites.Add(new BlockObject(new Vector2((fir + (16 * repeats)), 432.0f), content,audio, "stair"));
                        for (int j = 0; j < (height - 1); j++)
                        {
                            sprites.Add(new BlockObject(new Vector2((fir + (16 * repeats)), (416.0f - (16 * j))), content,audio, "stair"));

                        }
                    }

                    for(int k = 0; k < height; k++)
                    {
                        //base
                        sprites.Add(new BlockObject(new Vector2((fir + (16 * k) + (16 * repeats)), 432.0f), content,audio, "stair"));
                        for (int j = 0; j < (height - 1 - k); j++)
                        {
                            sprites.Add(new BlockObject(new Vector2((fir + (16 * k) + (16 * repeats)), (416.0f - (16 * j))), content, audio, "stair"));
                        }
                    }

                    break;
                case "pipe":
                    MiscObject pipeHead = new MiscObject(new Vector2(Int32.Parse(input[1]), Int32.Parse(input[2])), content, "pipe_head");
                    if (input[4] != "null")
                    {
                        if (input[4] == "warp")
                        {
                            pipeHead.WarpDestination = new Vector2(Int32.Parse(input[5]), Int32.Parse(input[6]));
                            MiscObject destPipeHead = new MiscObject(pipeHead.WarpDestination.Value, content, "pipe_head");
                            destPipeHead.WarpDestination = new Vector2(Int32.Parse(input[1]), Int32.Parse(input[2]));
                            int destPipeSections = Int32.Parse(input[7]);
                            sprites.Add(destPipeHead);
                            for (int j = 1; j <= destPipeSections; j++)
                            {
                                sprites.Add(new MiscObject(new Vector2(Int32.Parse(input[5]), Int32.Parse(input[6]) + 16 * j), content, "pipe_section"));
                            }
                        }
                        else
                        {
                            pipeHead.Mario = mario;
                            EnemyObject pirhana = new EnemyObject(new Vector2(Int32.Parse(input[1]) + 8, Int32.Parse(input[2])), content, audio, "pirhana");
                            pirhana.Pipe = pipeHead;
                            sprites.Add(pirhana);
                        }
                    }
                    int pipeSections = Int32.Parse(input[3]);
                    for (int j = 1; j <= pipeSections; j++)
                    {
                        sprites.Add(new MiscObject(new Vector2(Int32.Parse(input[1]), Int32.Parse(input[2]) + 16 * j), content, "pipe_section"));
                    }
                    sprites.Add(pipeHead);
                    break;
                case "misc":
                    sprites.Add(new MiscObject(new Vector2(Int32.Parse(input[1]), Int32.Parse(input[2])), content, input[3]));
                    break;
                default:
                    Console.WriteLine($"No match for {input[0].ToLower()}");
                    break;
            }


            return sprites;
        }

        public List<AbsObject> GetSprites()
        {
            List<AbsObject> sprites = new List<AbsObject>();


            //https://www.c-sharpcorner.com/UploadFile/mahesh/how-to-read-a-text-file-in-C-Sharp/
            // Read file using StreamReader. Reads file line by line  
            // need to edit file in bin\Windows\x86\Debug\test_level.txt  figure out file pathing later

            
            using (StreamReader file = new StreamReader(_fileName))
            {
                int counter = 0;
                string ln;
                List<AbsObject> tempSprite;
                char[] separators = new char[] { ' ' };

                while ((ln = file.ReadLine()) != null)
                {
                    string[] splitString = ln.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if(splitString[0] != "//")
                    {
                        tempSprite = Parse(splitString);
                        sprites = sprites.Concat(tempSprite).ToList();
                    }
                    for (int i = 0; i < splitString.Length; i++)
                    {
                        Console.WriteLine(splitString[i]);
                    }
                    counter++;
                }
                
                Console.WriteLine($"File has {counter} lines.");

            }
            return sprites;
        }

    }
}
