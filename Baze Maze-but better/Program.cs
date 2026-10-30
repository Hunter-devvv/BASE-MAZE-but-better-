
//Hunter Cannon
//Base Maze
//10/21/2026
//FinalBaceMaze.cs
//a game where you must travese the maze, avoid enemys, and beat all odds to win and acheve GLORIUS VICTORY.

using System;
using System.Threading;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace BaseMazeFINAL
{


    internal class program
    {



        internal static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern bool RegisterHotKey(IntPtr windowHandle, int hotkeyId, uint modifierKeys, uint virtualKey);

            [DllImport("user32.dll")]
            public static extern bool UnregisterHotKey(IntPtr windowHandle, int hotkeyId);
        }



        static int[] wasd_pressed = new int[4] { 0, 0, 0, 0 };
        static bool isRunning;
        static int moveX, moveY;


        static int lives;
        static bool PlayerWon;

        static int FrameCount = 0; //restets to 0 once it gets biger then 16, used for timed things.


        static int[] checkpoint = new int[2] { 5, 5 };

        static int[] playerPos = new int[2] { 5, 5 };

        static int[] OLDplayerPos = new int[2] { 5, 5 };


        static int A = 11;
        static int B = 12;
        static int C = 13;
        static int D = 14;
        static int E = 15;
        static int F = 16;
        static int G = 17;
        static int H = 18;
        static int I = 19;
        static int J = 20;
        static int K = 21;
        static int L = 22;
        static int M = 23;
        static int N = 24;
        static int O = 25;
        static int P = 26;
        static int Q = 27;

        static int R = 28;
        static int S = 29;



        static int keys;

        static int masterkeys;

        static int width = 40;

        static int higth = 25;


        static int[][] E_stats =
       {

                //—---------------------------------------------------------------------------------------------------------
                //this array contains all the data for all of the enemies.
                //the starting values get calculated in the get_enemys() function
                //—----------------------------------------------------------------------------------------------------------
                //each row is a different enemy 
                //columns  0: Y-position    1: X-position   2: Direction   3: enemy-type  4: oldY  6:oldX
                //—---------------------------------------------------------------------------------------------------------
                // if i wanted to add a new enemy i would just add one on the grid
                // and then add another blank row with 4 zero’s
                //—---------------------------------------------------------------------------------------------------------

                new int[6]{0,0,0,0,0,0},
                new int[6]{0,0,0,0,0,0},
                new int[6]{0,0,0,0,0,0},
                new int[6]{0,0,0,0,0,0},
                new int[6]{0,0,0,0,0,0},

                new int[6]{0,0,0,0,0,0},
                new int[6]{0,0,0,0,0,0},
                new int[6]{0,0,0,0,0,0},
                new int[6]{0,0,0,0,0,0},
                new int[6]{0,0,0,0,0,0},


                new int[6]{0,0,0,0,0,0}




            };
        static int numb_of_enemys = 10;


        static int[][] GRID =
           {

            //--------------------------------------------------------------------------------------------------
           //
            //I HAVE NOT ADDED ALL OF THESE YET I AM JUST SETTING IT OUT SO IF I WANT TO ADD THEM I HAVE THE ID's
            //
            //--------------------------------------------------------------------------------------------------


                //0=nothing
                //1=wall

                //2=enemy1 (up down)
                //3=enemy1 (down up)
                //4=enemy1 (left right)
                //5=enemy1 (right left)

                //6=enemy2 (up down)
                //7=enemy2 (down up)
                //8=enemy2 (left right)
                //9=enemy2 (right left)

                //10=enemy3              
                  
                //11=key    A
                //12=master key     B
                //13=lock   C
                //14=master lock    D
            
                //15=spikes      E
                //16=spikes(active)        F
                //17=spikes(inactive)        G
                
                //18=Wi (win)   H
                //19=m (win)    I
                //20=win (secret)   J

                //21=portal1a   K
                //22=portal1b   L

                //23=portal2a   M
                //24=portal2b   N
                
                //25=dual movement upgrade  O
                //26=slow down portal   P
                
                //27=Checkpoint     Q

                //28=Barrier(vertical)     R
                //29=Barrier   S

               new int[40] {1,0,0,0,0,1,0,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,0,0,0,0,1,0,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,0,0,0,0,1,D,1,C,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,5,0,0,0,0,0,0,1,0,0},
               new int[40] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,3,1,0,0},
               new int[40] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,5,0,0,0,0,0,0,1,0,0},
               new int[40] {1,0,0,0,A,0,A,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,3,1,0,0},
               new int[40] {0,0,1,0,B,0,B,0,0,0,0,0,0,0,0,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,1,1,0,0,0,0,0,0,6,0,0,1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,H,I,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {0,0,0,0,0,Q,0,0,0,0,0,0,0,0,0,1,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0},
               new int[40] {1,1,0,0,0,0,0,0,0,0,0,0,0,2,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,Q,0,0,0,0,0,0,0,0,0,0,0,4,2,4,2,0},
               new int[40] {1,0,0,0,1,1,1,1,1,0,0,0,0,0,0,R,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,R,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {0,0,0,0,0,0,0,0,0,Q,0,0,0,0,0,R,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0},
               new int[40] {0,0,0,0,0,E,0,0,0,0,0,0,0,0,0,R,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,R,1,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,0,0,0,0,F,F,F,0,0,0,0,0,0,1,0,S,S,S,S,S,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
               new int[40] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0}





            };
        static void Main(string[] args)
        {



            get_enemys();
            initalazation();

            while (isRunning == true)
            {
                Input();
                Phyisics();

                if (isRunning == true)
                {
                    LOADSCREEN();
                    ENDofFrame();
                }
                else
                {
                    if (PlayerWon == true)
                    {
                        Console.Clear();
                        Console.WriteLine("YOU WIN");
                        Console.ReadKey();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("YOU LOSE");
                        Console.ReadKey();
                    }
                }


            }





            static void initalazation()
            {

                //randomly sets players positon in y axis until they are in a blank space

                bool inblank = false;

                while (inblank == false)
                {
                    Random random = new Random();
                    playerPos[0] = random.Next(0, higth);
                    playerPos[1] = 0;

                    if (GRID[playerPos[0]][playerPos[1]] == 0)
                    {
                        inblank = true;
                    }

                }



                isRunning = true;

                moveX = 0;
                moveY = 0;

                PlayerWon = false;
                lives = 10;



                keys = 0;
                masterkeys = 0;

                Console.CursorVisible = false;

                for (int Yax = 0; Yax < 25; Yax++)
                {
                    for (int Xax = 0; Xax < 40; Xax++)
                    {
                        switch (GRID[Yax][Xax])
                        {
                            case 2:





                                break;
                        }
                    }
                }


            }


            static void ENDofFrame()
            {
                Stopwatch.StartNew();

                while (Stopwatch.GetTimestamp() < 100)
                {
                    quickInput();

                }

                FrameCount++;
                if (FrameCount > 10)
                {
                    FrameCount = 0;
                }
            }

            static void Phyisics()
            {



                inBERRIER(playerPos[1], playerPos[0]);

                if (!isRunning)
                    return;

                OLDplayerPos[0] = playerPos[0];
                OLDplayerPos[1] = playerPos[1];

                int NextY = playerPos[0] + moveY;
                int NextX = playerPos[1] + moveX;

                if (NextX >= 0 && NextX <= 40 && NextY >= 0 && NextY <= 25)
                {

                    //0=nothing                                CHECK
                    //1=wall                                CHECK

                    //2=enemy1 (up down)                                  CHECK
                    //3=enemy1 (down up)                                CHECK
                    //4=enemy1 (left right)                                CHECK
                    //5=enemy1 (right left)                                CHECK

                    //6=enemy2 (up down)                                CHECK
                    //7=enemy2 (down up)                                CHECK
                    //8=enemy2 (left right)                                CHECK
                    //9=enemy2 (right left)                                CHECK

                    //10=enemy3              

                    //11=key    A                                CHECK
                    //12=master key     B                                CHECK
                    //13=lock   C                                CHECK
                    //14=master lock    D                                CHECK

                    //15=spikes      E                                CHECK
                    //16=spikes(active)        F
                    //17=spikes(inactive)        G

                    //18=Wi (win)   H                                CHECK
                    //19=m (win)    I                                CHECK
                    //20=win (secret)   J

                    //21=portal1a   K
                    //22=portal1b   L

                    //23=portal2a   M
                    //24=portal2b   N

                    //25=dual movement upgrade  O
                    //26=slow down portal   P

                    //27=Checkpoint     Q                                CHECK

                    //28=Barrier(vertical)     R                                CHECK
                    //29=Barrier   S
                    //CHECK

                    bool canmove = true;

                    if (moveX != 0 && moveY != 0)
                    {
                        if (GRID[NextY][playerPos[1]] == 1 && GRID[playerPos[0]][NextX] == 1)
                        {
                            canmove = false;

                        }

                    }
                    if (canmove == true)
                    {
                        switch (GRID[NextY][NextX])
                        {
                            case 0: playerPos[0] = NextY; playerPos[1] = NextX; break; //empty space

                            case 11: playerPos[0] = NextY; playerPos[1] = NextX; keys++; GRID[NextY][NextX] = 0; break;       //Key
                            case 12: playerPos[0] = NextY; playerPos[1] = NextX; masterkeys++; GRID[NextY][NextX] = 0; break;       //Master key
                            case 13: if (keys > 0) { keys--; GRID[NextY][NextX] = 0; playerPos[0] = NextY; playerPos[1] = NextX; }; break;       //lock
                            case 14: if (masterkeys > 0) { masterkeys--; GRID[NextY][NextX] = 0; playerPos[0] = NextY; playerPos[1] = NextX; }; break;       //master lock

                            case 15: PLAYER_DIED(); break;       //spikes
                            case 16: PLAYER_DIED(); break;       //eneabled spikes
                            case 17: playerPos[0] = NextY; playerPos[1] = NextX; break;       //dissabled spikes

                            case 18: PlayerWon = true; isRunning = false; break;       //win
                            case 19: PlayerWon = true; isRunning = false; break;       //win
                            case 27: playerPos[0] = NextY; playerPos[1] = NextX; checkpoint[0] = playerPos[0]; checkpoint[1] = playerPos[1]; break;     //checkpoint

                            case 28: playerPos[0] = NextY; playerPos[1] = NextX; break; //barriers can be entered, but inberrier() stops certain movement options
                            case 29: playerPos[0] = NextY; playerPos[1] = NextX; break; //also a barrier

                            default: break;  //if its not one of those, its probably a wall

                        }
                    }


                }

                if (!isRunning)
                    return;


                for (int enemyID = 0; enemyID < numb_of_enemys; enemyID++)
                {
                    if (!isRunning)
                        return;
                    if (E_stats[enemyID][3] == 2)
                    {
                        MoveEnemy(enemyID, E_stats[enemyID][1], E_stats[enemyID][0], E_stats[enemyID][2], 2);

                    }
                    else
                    {
                        MoveEnemy(enemyID, E_stats[enemyID][1], E_stats[enemyID][0], E_stats[enemyID][2], 1);

                    }

                }

                CheckEnemyCollision();


            }

            static void get_enemys()
            {

                //—--------------------------------------------------------------------------------------
                //this function finds all of the enemies on the grid
                //and then assigns all the necessary data for them in the E_stats array
                //—--------------------------------------------------------------------------------------





                int id = 0;

                for (int Yaxis = 0; Yaxis < 25; Yaxis++)
                {

                    for (int Xaxis = 0; Xaxis < 40; Xaxis++)
                    {

                        //—--------------------------------------------------------------------------------------
                        //this switch checks if a point on the grid contains an enemy
                        // any number 2-10 is considered an enemy
                        //different enemy numbers give the enemy different stats.
                        //—--------------------------------------------------------------------------------------

                        switch (GRID[Yaxis][Xaxis])
                        {

                            case 2:
                                E_stats[id][0] = Yaxis;
                                E_stats[id][1] = Xaxis;
                                E_stats[id][2] = 1;
                                E_stats[id][3] = 1;
                                E_stats[id][4] = Yaxis;
                                E_stats[id][5] = Xaxis;

                                GRID[Yaxis][Xaxis] = 0;
                                id++;
                                break;

                            case 3:
                                E_stats[id][0] = Yaxis;
                                E_stats[id][1] = Xaxis;
                                E_stats[id][2] = 2;
                                E_stats[id][3] = 1;
                                E_stats[id][4] = Yaxis;
                                E_stats[id][5] = Xaxis;

                                GRID[Yaxis][Xaxis] = 0;
                                id++;
                                break;

                            case 4:
                                E_stats[id][0] = Yaxis;
                                E_stats[id][1] = Xaxis;
                                E_stats[id][2] = 3;
                                E_stats[id][3] = 1;
                                E_stats[id][4] = Yaxis;
                                E_stats[id][5] = Xaxis;

                                GRID[Yaxis][Xaxis] = 0;
                                id++;
                                break;


                            case 5:
                                E_stats[id][0] = Yaxis;
                                E_stats[id][1] = Xaxis;
                                E_stats[id][2] = 4;
                                E_stats[id][3] = 1;
                                E_stats[id][4] = Yaxis;
                                E_stats[id][5] = Xaxis;

                                GRID[Yaxis][Xaxis] = 0;
                                id++;
                                break;

                            case 6:
                                E_stats[id][0] = Yaxis;
                                E_stats[id][1] = Xaxis;
                                E_stats[id][2] = 1;
                                E_stats[id][3] = 2;
                                E_stats[id][4] = Yaxis;
                                E_stats[id][5] = Xaxis;

                                GRID[Yaxis][Xaxis] = 0;
                                id++;
                                break;

                            case 7:
                                E_stats[id][0] = Yaxis;
                                E_stats[id][1] = Xaxis;
                                E_stats[id][2] = 2;
                                E_stats[id][3] = 2;
                                E_stats[id][4] = Yaxis;
                                E_stats[id][5] = Xaxis;

                                GRID[Yaxis][Xaxis] = 0;
                                id++;
                                break;

                            case 8:
                                E_stats[id][0] = Yaxis;
                                E_stats[id][1] = Xaxis;
                                E_stats[id][2] = 3;
                                E_stats[id][3] = 2;
                                E_stats[id][4] = Yaxis;
                                E_stats[id][5] = Xaxis;

                                GRID[Yaxis][Xaxis] = 0;
                                id++;
                                break;

                            case 9:
                                E_stats[id][0] = Yaxis;
                                E_stats[id][1] = Xaxis;
                                E_stats[id][2] = 4;
                                E_stats[id][3] = 2;
                                E_stats[id][4] = Yaxis;
                                E_stats[id][5] = Xaxis;

                                GRID[Yaxis][Xaxis] = 0;
                                id++;
                                break;

                            case 10:
                                E_stats[id][0] = Yaxis;
                                E_stats[id][1] = Xaxis;
                                E_stats[id][2] = 1;
                                E_stats[id][3] = 3;
                                E_stats[id][4] = Yaxis;
                                E_stats[id][5] = Xaxis;

                                GRID[Yaxis][Xaxis] = 0;
                                id++;
                                break;

                            default: break;

                        }

                    }


                }

            }


            static void MoveEnemy(int id, int X, int Y, int direction, int speed)
            {

                //-------------------------------------------------------
                //instead of 2 derections i have 4
                // 1=up 2=right 3=down 4=left
                //----------------------------------------------------------

                for (int step = 0; step < speed; step++)
                {
                    E_stats[id][4] = Y;
                    E_stats[id][5] = X;

                    int nextX = X;
                    int nextY = Y;

                    switch (direction)
                    {
                        case 1: nextY -= speed; break;
                        case 2: nextX += speed; break;
                        case 3: nextY += speed; break;
                        case 4: nextX -= speed; break;

                    }

                    if (nextX < 0 || nextX > 40 || nextY > 25 || nextY < 0 || GRID[nextY][nextX] == 1 || GRID[nextY][nextX] == 13 || GRID[nextY][nextX] == 14)
                    {

                        switch (direction)
                        {
                            case 1: direction = 3; break;
                            case 2: direction = 4; break;
                            case 3: direction = 1; ; break;
                            case 4: direction = 2; break;

                        }

                        switch (direction)
                        {
                            case 1: nextY -= (speed * 2); break;
                            case 2: nextX += (speed * 2); break;
                            case 3: nextY += (speed * 2); break;
                            case 4: nextX -= (speed * 2); break;

                        }


                    }


                    E_stats[id][0] = nextY;
                    E_stats[id][1] = nextX;
                    E_stats[id][2] = direction;



                    if (!isRunning == true)
                    {
                        return;
                    }
                }
            }

            static void CheckEnemyCollision()
            {
                for (int enemyID = 0; enemyID < numb_of_enemys; enemyID++)
                {
                    if (playerPos[0] == E_stats[enemyID][0] && playerPos[1] == E_stats[enemyID][1])
                    {
                        PLAYER_DIED();

                    }
                    else if (playerPos[0] == E_stats[enemyID][4] && playerPos[1] == E_stats[enemyID][5] && OLDplayerPos[0] == E_stats[enemyID][0] && OLDplayerPos[1] == E_stats[enemyID][1])
                    {
                        PLAYER_DIED();

                    }
                }
            }

            static void PLAYER_DIED()
            {

                //—---------------------------------------------------------------------------------------
                //checks if the player has any lives left
                //—----------------------------------------------------------------------------------------
                //if they do       they will respawn at their last checkpoint and lose 1 life
                //if they dont    they then they fail and the game ends.
                //—---------------------------------------------------------------------------------------

                if (lives > 0)
                {
                    Console.Clear();
                    Thread.Sleep(150);

                    lives--;
                    playerPos[0] = checkpoint[0];
                    playerPos[1] = checkpoint[1];
                }
                else
                {

                    PlayerWon = false;
                    isRunning = false;
                }

            }


            static void Input()
            {
                moveX = 0;
                moveY = 0;



                //if  no key is pressed the players movement resets.
                if (Console.KeyAvailable == false)
                {
                    wasd_pressed[0] = Math.Clamp(wasd_pressed[0] - 1, 0, 2);
                    wasd_pressed[1] = Math.Clamp(wasd_pressed[1] - 1, 0, 2);
                    wasd_pressed[2] = Math.Clamp(wasd_pressed[2] - 1, 0, 2);
                    wasd_pressed[3] = Math.Clamp(wasd_pressed[3] - 1, 0, 2);

                }


                while (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;



                    switch (key)
                    {

                        case ConsoleKey.W: wasd_pressed[0] = 1; break;
                        case ConsoleKey.S: wasd_pressed[1] = 1; break;
                        case ConsoleKey.A: wasd_pressed[2] = 1; break;
                        case ConsoleKey.D: wasd_pressed[3] = 1; break;
                        case ConsoleKey.Escape: isRunning = false; break;

                    }





                }

                if (wasd_pressed[0] == 1)
                {
                    moveY--;
                }
                if (wasd_pressed[1] == 1)
                {
                    moveY++;

                }
                if (wasd_pressed[2] == 1)
                {
                    moveX--;

                }
                if (wasd_pressed[3] == 1)
                {
                    moveX++;

                }

            }


            //quick input just modifies the wasd_pressed values more times to keep up with speed.
            static void quickInput()
            {



                while (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;



                    switch (key)
                    {

                        case ConsoleKey.W: wasd_pressed[0] = Math.Clamp(wasd_pressed[0] + 1, 0, 2); break;
                        case ConsoleKey.S: wasd_pressed[1] = Math.Clamp(wasd_pressed[1] + 1, 0, 2); break;
                        case ConsoleKey.A: wasd_pressed[2] = Math.Clamp(wasd_pressed[2] + 1, 0, 2); break;
                        case ConsoleKey.D: wasd_pressed[3] = Math.Clamp(wasd_pressed[3] + 1, 0, 2); break;
                        case ConsoleKey.Escape: isRunning = false; break;

                    }





                }

            }


            static void inBERRIER(int x, int y)
            {
                //checks for x berriers  (x berriers can be entered on y axes but not x axis)
                if (GRID[y][x] == 28 && moveX != 0)
                {
                    moveX = 0;
                }
                else if (GRID[y][Math.Clamp(x + 1, 0, 40)] == 28 && moveX == 1)
                {
                    moveX = 0;
                }
                else if (GRID[y][Math.Clamp(x - 1, 0, 40)] == 28 && moveX == (-1))
                {
                    moveX = 0;
                }


                //checks for Y berriers  (Y berriers can be entered on X axes but not y axis)
                if (GRID[y][x] == 29 && moveY != 0)
                {
                    moveY = 0;
                }
                else if (GRID[Math.Clamp(y + 1, 0, 25)][x] == 29 && moveY == 1)
                {
                    moveY = 0;
                }
                else if (GRID[Math.Clamp(y - 1, 0, 25)][x] == 28 && moveY == (-1))
                {
                    moveY = 0;
                }
            }

            static void LOADSCREEN()
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                Console.WriteLine($"Stock BaceMaze: By Hunter Cannon   ||    LIVES:{lives}       KEYS:{keys}      Master Keys:{masterkeys}  {wasd_pressed[0]}{wasd_pressed[1]}{wasd_pressed[2]}{wasd_pressed[3]}");
                Console.WriteLine("----------------------------------------------------------------------------------------------------\n");

                for (int Yaxis = 0; Yaxis < 25; Yaxis++)
                {


                    Console.WriteLine("");

                    for (int Xaxis = 0; Xaxis < 40; Xaxis++)
                    {


                        //0=nothing
                        //1=wall

                        //2=enemy1 (up down)
                        //3=enemy1 (down up)
                        //4=enemy1 (left right)
                        //5=enemy1 (right left)

                        //6=enemy2 (up down)
                        //7=enemy2 (down up)
                        //8=enemy2 (left right)
                        //9=enemy2 (right left)

                        //10=enemy3 

                        //11=key
                        //12=master key
                        //13=lock
                        //14=master lock

                        //15=spikes
                        //16=spikes(active)
                        //17=spikes(inactive)

                        //18=Wi (win)
                        //19=m (win)
                        //20=win (secret)

                        //21=portal1a
                        //22=portal1b

                        //23=portal2a
                        //24=portal2b

                        //25=dual movement upgrade
                        //26=slow down portal

                        //27=Checkpoint



                        //-----------------------------------------------
                        // looks for enemys in E_stats and prints them out
                        //-----------------------------------------------
                        int enemythere = 0;
                        for (int id = 0; id < numb_of_enemys; id++)
                        {
                            if (E_stats[id][0] == Yaxis && Xaxis == E_stats[id][1])
                            {
                                switch (E_stats[id][3])
                                {
                                    case 1: enemythere = 1; break;
                                    case 2: enemythere = 2; break;
                                    case 3: enemythere = 3; break;
                                    default: enemythere = 0; break;
                                }

                            }
                        }
                        Console.ForegroundColor = ConsoleColor.White;

                        if (Yaxis == playerPos[0] && Xaxis == playerPos[1])
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("P ");
                        }
                        else if (enemythere == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.Write("E1");
                        }
                        else if (enemythere == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.Write("E2");
                        }
                        else if (enemythere == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.Write("E3");
                        }
                        else
                        {

                            switch (GRID[Yaxis][Xaxis])
                            {
                                case 0:
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.Write(". ");
                                    break;

                                case 1:

                                    Console.Write("[]");


                                    break;
                                case 2:
                                    Console.Write("E1");

                                    break;
                                case 6:
                                    Console.Write("E2");

                                    break;
                                case 11:
                                    Console.ForegroundColor = ConsoleColor.Yellow;

                                    Console.Write("o~");

                                    break;
                                case 12:
                                    Console.ForegroundColor = ConsoleColor.Yellow;

                                    Console.Write("M~");

                                    break;
                                case 13:
                                    Console.ForegroundColor = ConsoleColor.Gray;

                                    Console.Write("{}");

                                    break;
                                case 14:
                                    Console.ForegroundColor = ConsoleColor.Gray;

                                    Console.Write("MM");

                                    break;

                                case 15:
                                    Console.ForegroundColor = ConsoleColor.Red;

                                    Console.Write("!!");

                                    break;
                                case 16:
                                    Console.ForegroundColor = ConsoleColor.Red;

                                    Console.Write("!!");

                                    if (FrameCount == 10)
                                    {
                                        GRID[Yaxis][Xaxis] = 17;
                                        FrameCount = 0;

                                    }

                                    break;
                                case 17:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("..");

                                    if (FrameCount == 10)
                                    {
                                        GRID[Yaxis][Xaxis] = 16;
                                        FrameCount = 0;
                                    }

                                    break;

                                case 18:
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    Console.Write("WI");

                                    break;
                                case 19:
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    Console.Write("N ");

                                    break;
                                case 27:
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    Console.Write("CP");

                                    break;
                                case 28:

                                    Console.Write("||");

                                    break;
                                case 29:
                                    Console.Write("==");
                                    break;
                                default:
                                    Console.Write("  ");

                                    break;


                            }
                        }
                    }

                }
            }

        }
    }
}
