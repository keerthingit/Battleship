using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Name: Keerthi Kiran
//Date: 06/04/2020 - 17/04/2020
//Assignment: BattleShip
//Purpose: gettt a 4++++! jkjk use 2D array to create a fun computer vs user game!
namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            //Intro 
            Console.Title = "BattleShip";
            Console.WriteLine("Ten Hut! It's time to battle! Welcome to Battleship!\nnote: if you would like to quit at anytime please enter 'q'\n");
            //when user gets to input and they enter 'q' the app will exit


            //Size of the Board, Enhancement 2 Part A
            int Size;
            do
            {
                Console.WriteLine("Enter Map Size 6-10");
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out Size) && Size > 5 && Size < 11)// if it is an int between 6 and 10
                {
                    break;
                }
                else
                {
                    if (Input == "q".ToLower())
                    {
                        Console.WriteLine("Thank you for playing!");
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Entry. Please Try Again.");
                    }
                }
            } while (true); // while the user enters invalid values it will repeat the question unless they enter q or the right input


            //Debug Mode makes marking easier!
            do
            {
                Console.WriteLine("Would you like to run on debug mode? (y/n)");
                string Input = Console.ReadLine();
                if (Input == "y".ToLower())
                {
                    Console.Clear();
                    battleShipDebug(Size);
                    break;
                }
                else if (Input == "n".ToLower())
                {
                    Console.Clear();
                    battleShip(Size);
                    break;
                }
                else if (Input == "q".ToLower())
                {
                    Console.WriteLine("Thank you for playing!");
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid Entry. Please Try Again.");
                }
            } while (true);
            Console.ReadKey();
        }
        public static void battleShipDebug(int BoardSize)
        {
            //2D Arrays for the user and computer's board
            string[,] UserShip = new string[BoardSize,BoardSize];
            for(int i=0;i<BoardSize;i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    UserShip[i, j] = "*";
                }
            }
            string[,] CompShip = new string[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    CompShip[i, j] = "*";
                }
            }
            //An array for the letter for ship length
            string[] ShipName = new string[6];
            ShipName[1] = "A"; //size 1 ships are A
            ShipName[2] = "B"; //size 2 ships are B
            ShipName[3] = "C"; //size 3 ships are C
            ShipName[4] = "D"; //size 4 ships are D
            ShipName[5] = "E"; //size 5 ships are E
            //3D Arrays for each Ship (first "array" is the ship number second and third "array" are the x,y coordinates of one ship)
            //The array basically creates a map with 1 ship each and where the ship is located it will == "S" of given size
            string[,,] ShipsListU = new string[11,BoardSize,BoardSize];
            for (int i =0;i<11;i++)
            {
                for(int j=0; j<BoardSize;j++)
                {
                    for (int k = 0; k < BoardSize; k++)
                    {
                        ShipsListU[i, j, k] = "*";
                    }
                }
            }
            string[,,] ShipsListC = new string[11, BoardSize, BoardSize];
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    for (int k = 0; k < BoardSize; k++)
                    {
                        ShipsListC[i,j, k] = "*";
                    }
                }
            }


            //Ship Types and Numbers Generated Randomly According to BoardSize, Enhancement 2 Part B
            Random Rand = new Random();//to randomize coordinates
            if (BoardSize < 8)//6,7 then it will have 3 types of ship with lengths 1,2, and 3 - A,B,C
            {
                int ShipNo = 0;
                //user's ship
                for(int i = 3; i>0;i--)//3 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);//generates random coordinates
                    while(randomCheck(UserShip, CorX, CorY,BoardSize, i) == false)//goes to method for checking if it is inside the board and it is not intersecting other ships
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);//randomly choose direction for ship length to add
                    if(CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if(Direc==0)//random direction is right
                        {
                            for(int k = 0;k<i;k++)//for the length of the ship 
                            {
                                UserShip[CorX, CorY + k] = ShipName[i]; //y coordinate increases from 0 to (i-1)
                                ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];//at shipno.x the 3d array is assigned a ship
                            }
                        }
                        if(Direc==1)//random direction is down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX + k, CorY] = ShipName[i];
                                ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if(CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX + k, CorY] = ShipName[i];
                            ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if(CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {
                        
                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX, CorY + k] = ShipName[i];
                            ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for(int i = BoardSize-5; i>0;i--)//random user ships, if boardsize =6, random ships no = 2 and if boardsize =7, random ships no = 3
                {
                    int ShipLen = Rand.Next(1, 4);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while(randomCheck(UserShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListU[ShipNo,RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListU[ShipNo,RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListU[ShipNo,RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListU[ShipNo,RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }
                ShipNo = 0;
                //computer's ships, same thing ^ but diff array/map
                for (int i = 3; i > 0; i--)//3 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX, CorY + k] = ShipName[i];
                                ShipsListC[ShipNo,CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX + k, CorY] = ShipName[i];
                                ShipsListC[ShipNo,CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX + k, CorY] = ShipName[i];
                            ShipsListC[ShipNo,CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX, CorY + k] = ShipName[i];
                            ShipsListC[ShipNo,CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random comp ships
                {
                    int ShipLen = Rand.Next(1, 4);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListC[ShipNo,RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListC[ShipNo,RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListC[ShipNo,RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListC[ShipNo,RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }

            }
            else if (BoardSize > 7 && BoardSize < 10)//8,9 then it will have 4 types of ships with lengths 1,2,3, and 4 - A,B,C,D
            {
                int ShipNo = 0;
                //user's ship
                for (int i = 4; i > 0; i--)//4 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(UserShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX, CorY + k] = ShipName[i];
                                ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX + k, CorY] = ShipName[i];
                                ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX + k, CorY] = ShipName[i];
                            ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX, CorY + k] = ShipName[i];
                            ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random user ships
                {
                    int ShipLen = Rand.Next(1, 5);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(UserShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListU[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListU[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListU[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListU[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }
                ShipNo = 0;
                //computer's ships
                for (int i = 4; i > 0; i--)//4 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX, CorY + k] = ShipName[i];
                                ShipsListC[ShipNo, CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX + k, CorY] = ShipName[i];
                                ShipsListC[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX + k, CorY] = ShipName[i];
                            ShipsListC[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX, CorY + k] = ShipName[i];
                            ShipsListC[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random comp ships
                {
                    int ShipLen = Rand.Next(1, 5);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListC[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListC[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListC[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListC[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }
            }
            else if (BoardSize == 10)//then it will have 5 types of ships with lengths 1,2,3,4, 5 - A,B,C,D,E
            {
                int ShipNo = 0;
                //user's ship
                for (int i = 5; i > 0; i--)//5 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(UserShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX, CorY + k] = ShipName[i];
                                ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX + k, CorY] = ShipName[i];
                                ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX + k, CorY] = ShipName[i];
                            ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX, CorY + k] = ShipName[i];
                            ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random user ships
                {
                    int ShipLen = Rand.Next(1, 5);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(UserShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListU[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListU[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListU[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListU[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }
                ShipNo = 0;
                //computer's ships
                for (int i = 5; i > 0; i--)//5 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX, CorY + k] = ShipName[i];
                                ShipsListC[ShipNo, CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX + k, CorY] = ShipName[i];
                                ShipsListC[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX + k, CorY] = ShipName[i];
                            ShipsListC[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX, CorY + k] = ShipName[i];
                            ShipsListC[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random comp ships
                {
                    int ShipLen = Rand.Next(1, 5);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListC[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListC[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListC[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListC[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }
            }


            //print board method debug mode both user's and comp's maps
            printUserMap(BoardSize, UserShip);
            Console.WriteLine();
            printEnemyMap(BoardSize, CompShip);

            //Enhancement 3 Ammo
            int UserBullets = (5 * ((BoardSize - 5) + 1));//10 or 15 or 20 or 25 or 30
            int CompBullets = (5 * ((BoardSize - 5) + 1));

            Console.WriteLine("\nYou and your Enemy have {0} bullets", UserBullets);


            //Asking for coordinates from user and randomly generating coordinates for computer
            int CorUserX;
            int CorUserY;
            int CorCompX;
            int CorCompY;
            int PointsU = 0;
            int PointsC = 0;
            int MissU = 0;
            do//game starts
            {
                //USER
                //input user's attack coordinates
                Console.WriteLine("\nYour Turn");
                //input X-coordinate
                do
                {
                    Console.WriteLine("Enter X-Coordinate (1-{0}) down: ", BoardSize);// the x coordinates are the vertical 123s
                    string InputX = Console.ReadLine();
                    if (int.TryParse(InputX, out CorUserX) && CorUserX <= BoardSize && CorUserX > 0)// after proper entry it will move on to next coordinate
                    {
                        break;
                    }
                    else if (InputX == "q".ToLower())// if q is entered it will quit
                    {
                        Console.WriteLine("Thank you for playing!");
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Entry. Please Try Again");//if not an integer or not an integer inside the board it will show invalid
                    }

                } while (true);
                //input Y-coordinate
                do
                {
                    Console.WriteLine("Enter Y-Coordinate (1-{0}) across: ", BoardSize);//the y coordinates are the horizontal 123s
                    string InputY = Console.ReadLine();
                    if (int.TryParse(InputY, out CorUserY) && CorUserY <= BoardSize && CorUserY > 0)
                    {
                        break;
                    }
                    else if (InputY == "q".ToLower())
                    {
                        Console.WriteLine("Thank you for playing!");
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Entry. Please Try Again");
                    }

                } while (true);
                Console.Clear();
                //Checking where user hit, Enhancement 1
                UserBullets--;
                if (CompShip[CorUserX - 1, CorUserY - 1] != "H" && CompShip[CorUserX - 1, CorUserY - 1] != "M" && CompShip[CorUserX - 1, CorUserY - 1] != "*")
                {
                    //if it hits a ship, it will check which ship it hit through the compshiplist array and then it will write "h" on the coordinates of ship1 in CompShip array which is going to be displayed
                    //and in the 3D array the ship1 coordinates will write "*" so that when i check if all ships are hit, no ship is "s" anymore in the 3d array
                    int Counte =0;
                    for (int i = 1; i < ShipName.Length; i++)
                    {
                        if (Counte == i)
                        {
                            break;
                        }
                        if (CompShip[CorUserX - 1, CorUserY - 1] == ShipName[i])
                        {
                            PointsU = PointsU + i;
                            for (int j = 0; j < 11; j++)
                            {
                                if (Counte == i)
                                {
                                    break;
                                }
                                if (ShipsListC[j, CorUserX - 1, CorUserY - 1] == ShipName[i])
                                {
                                    for (int k = 0; k < BoardSize; k++)
                                    {
                                        if (Counte == i)
                                        {
                                            break;
                                        }
                                        for (int l = 0; l < BoardSize; l++)
                                        {
                                            if (ShipsListC[j, k, l] == ShipName[i])
                                            {
                                                ShipsListC[j, k, l] = "*";
                                                CompShip[k, l] = "H";
                                                Counte++;
                                            }
                                            if (Counte == i)
                                            {
                                                Console.WriteLine("A Hit!");
                                                Console.WriteLine("You have {0} bullets", UserBullets);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }//a ship is hit
                else if (CompShip[CorUserX - 1, CorUserY - 1] == "H")
                {
                    MissU++;
                    CompShip[CorUserX - 1, CorUserY - 1] = "H";
                    Console.WriteLine("You hit the same coordinate, try something else next time!");
                    Console.WriteLine("You have {0} bullets", UserBullets);
                }//if it lands at h,m or * then it will be a miss and bullets will be lost no points gained
                else if(CompShip[CorUserX - 1, CorUserY - 1] == "M")
                {
                    MissU++;
                    CompShip[CorUserX - 1, CorUserY - 1] = "M";
                    Console.WriteLine("A Miss.");
                    Console.WriteLine("You hit the same coordinate, try something else next time!");
                    Console.WriteLine("You have {0} bullets", UserBullets);
                }
                else if (CompShip[CorUserX - 1, CorUserY - 1] == "*")
                {
                    MissU++;
                    CompShip[CorUserX - 1, CorUserY - 1] = "M";
                    Console.WriteLine("A Miss.");
                    Console.WriteLine("You have {0} bullets", UserBullets);
                }
                Console.WriteLine("Your Points: {0}", PointsU);
                //check if all ships in comp are sunk 
                int Count = 0;
                for (int i=0;i<11;i++)
                {
                    for(int j=0;j<BoardSize;j++)
                    {
                        for(int k=0;k<BoardSize;k++)
                        {
                            if(ShipsListC[i,j,k] != "*")
                            {
                                Count++;
                            }
                        }
                    }
                }
                if(Count ==0)
                {
                    Console.Clear();
                    Console.WriteLine("You Win!!!!");
                    Console.WriteLine("Points Earned: {0}", PointsU );
                    Console.WriteLine("Bullets Used: {0}", (5 * ((BoardSize - 5) + 1)) - UserBullets);
                    Console.WriteLine("Attacks Missed: {0}", MissU);
                    break;
                }
                
                Console.WriteLine();
                printEnemyMap(BoardSize, CompShip);



                //COMPUTER Enhancement 4
                //generate comp's attack coordinates randomly
                CorCompX = Rand.Next(0, BoardSize);
                CorCompY = Rand.Next(0, BoardSize);
                Console.WriteLine("\nEnemy's Turn");
                //Checking where computer hit, Enhancement 1
                CompBullets--;
                if (UserShip[CorCompX, CorCompY] != "H" && UserShip[CorCompX, CorCompY] != "M" && UserShip[CorCompX, CorCompY] != "*")
                {
                    int Countt = 0;
                    for (int i = 1; i < ShipName.Length; i++)
                    {
                        if (Countt == i)
                        {
                            break;
                        }
                        if (UserShip[CorCompX, CorCompY] == ShipName[i])
                        {
                            PointsC = PointsC + i;
                            for (int j = 0; j < 11; j++)
                            {
                                if (Countt == i)
                                {
                                    break;
                                }
                                if (ShipsListU[j, CorCompX, CorCompY] == ShipName[i])
                                {
                                    for (int k = 0; k < BoardSize; k++)
                                    {
                                        if (Countt == i)
                                        {
                                            break;
                                        }
                                        for (int l = 0; l < BoardSize; l++)
                                        {
                                            if (ShipsListU[j, k, l] == ShipName[i])
                                            {
                                                ShipsListU[j, k, l] = "*";
                                                UserShip[k, l] = "H";
                                                Countt++;
                                            }
                                            if (Countt == i)
                                            {
                                                Console.WriteLine("Enemy hit your ship");
                                                Console.WriteLine("Enemy has {0} bullets", CompBullets);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }//a ship is hit
                else if (UserShip[CorCompX, CorCompY] == "H")
                {
                    UserShip[CorCompX, CorCompY] = "H";
                    Console.WriteLine("A Miss.");
                    Console.WriteLine("Enemy has {0} bullets", CompBullets);
                }//if it lands at h,m or * then it will be a miss and bullets will be lost no points gained
                else if (UserShip[CorCompX, CorCompY] == "M")
                {
                    UserShip[CorCompX, CorCompY] = "M";
                    Console.WriteLine("A Miss.");
                    Console.WriteLine("Enemy has {0} bullets", CompBullets);
                }
                else if (UserShip[CorCompX, CorCompY] == "*")
                {
                    UserShip[CorCompX, CorCompY] = "M";
                    Console.WriteLine("A Miss.");
                    Console.WriteLine("Enemy has {0} bullets", CompBullets);
                }
                Console.WriteLine("Enemy's Points: {0}", PointsC);
                //check if all ships in comp are sunk 
                int Counts = 0;
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        for (int k = 0; k < BoardSize; k++)
                        {
                            if (ShipsListU[i, j, k] != "*")
                            {
                                Counts++;
                            }
                        }
                    }
                }
                if (Counts == 0)
                {
                    Console.Clear();
                    Console.WriteLine("You Lose!");
                    Console.WriteLine("Points Earned: {0}", PointsU);
                    Console.WriteLine("Bullets Used: {0}", (5 * ((BoardSize - 5) + 1)) - UserBullets);
                    Console.WriteLine("Attacks Missed: {0}", MissU);
                    break;
                }

                Console.WriteLine();
                printUserMap(BoardSize, UserShip);


            } while (CompBullets!=0 || UserBullets!=0);//keeps asking until bullets are 0
            
            if (PointsC > PointsU)
                {
                    Console.Clear();
                    Console.WriteLine("You Lose!");
                    Console.WriteLine("Points Earned: {0}", PointsU);
                    Console.WriteLine("Bullets Used: {0}", (5 * ((BoardSize - 5) + 1)) - UserBullets);
                    Console.WriteLine("Attacks Missed: {0}", MissU);
                    
                }//if comp points(ships hit, length wise) is greater than user's points
            else if (PointsC == PointsU)
                {
                    Console.Clear();
                    Console.WriteLine("A Draw!");
                    Console.WriteLine("Points Earned: {0}", PointsU);
                    Console.WriteLine("Bullets Used: {0}", (5 * ((BoardSize - 5) + 1)) - UserBullets);
                    Console.WriteLine("Attacks Missed: {0}", MissU);
                    
                }// if comp and user have equal points
            else
                {
                    Console.Clear();
                    Console.WriteLine("You Win!");
                    Console.WriteLine("Points Earned: {0}", PointsU);
                    Console.WriteLine("Bullets Used: {0}", (5 * ((BoardSize - 5) + 1)) - UserBullets);
                    Console.WriteLine("Attacks Missed: {0}", MissU);
                    
                }//if user points is greater than comp's points
            Console.ReadKey();
        }//shows user's and enemy's maps
        public static void battleShip(int BoardSize)//same thing as the debug method but this one doesn't show enemy's map, shows empty enemy's map
        {
            //2D Arrays for the user and computer's board
            string[,] UserShip = new string[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    UserShip[i, j] = "*";
                }
            }
            string[,] CompShip = new string[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    CompShip[i, j] = "*";
                }
            }
            string[,] CompEmptyShip = new string[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    CompEmptyShip[i, j] = "*";
                }
            }
            //An array for the letter for ship length
            string[] ShipName = new string[6];
            ShipName[1] = "A"; //size 1 ships are A
            ShipName[2] = "B"; //size 2 ships are B
            ShipName[3] = "C"; //size 3 ships are C
            ShipName[4] = "D"; //size 4 ships are D
            ShipName[5] = "E"; //size 5 ships are E
            //3D Arrays for each Ship (first "array" is the ship number second and third "array" are the x,y coordinates of one ship)
            //The array basically creates a map with 1 ship each and where the ship is located it will == "S" of given size
            string[,,] ShipsListU = new string[11, BoardSize, BoardSize];
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    for (int k = 0; k < BoardSize; k++)
                    {
                        ShipsListU[i, j, k] = "*";
                    }
                }
            }
            string[,,] ShipsListC = new string[11, BoardSize, BoardSize];
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    for (int k = 0; k < BoardSize; k++)
                    {
                        ShipsListC[i, j, k] = "*";
                    }
                }
            }


            //Ship Types and Numbers Generated Randomly According to BoardSize, Enhancement 2 Part B
            Random Rand = new Random();//to randomize coordinates
            if (BoardSize < 8)//6,7 then it will have 3 types of ship with lengths 1,2, and 3
            {
                int ShipNo = 0;
                //user's ship
                for (int i = 3; i > 0; i--)//3 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(UserShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX, CorY + k] = ShipName[i];
                                ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX + k, CorY] = ShipName[i];
                                ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX + k, CorY] = ShipName[i];
                            ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX, CorY + k] = ShipName[i];
                            ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random user ships
                {
                    int ShipLen = Rand.Next(1, 4);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(UserShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListU[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListU[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListU[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListU[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }
                ShipNo = 0;
                //computer's ships
                for (int i = 3; i > 0; i--)//3 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX, CorY + k] = ShipName[i];
                                ShipsListC[ShipNo, CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX + k, CorY] = ShipName[i];
                                ShipsListC[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX + k, CorY] = ShipName[i];
                            ShipsListC[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX, CorY + k] = ShipName[i];
                            ShipsListC[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random comp ships
                {
                    int ShipLen = Rand.Next(1, 4);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListC[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListC[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListC[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListC[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }

            }
            else if (BoardSize > 7 && BoardSize < 10)//8,9 then it will have 4 types of ships with lengths 1,2,3, and 4 
            {
                int ShipNo = 0;
                //user's ship
                for (int i = 4; i > 0; i--)//4 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(UserShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX, CorY + k] = ShipName[i];
                                ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX + k, CorY] = ShipName[i];
                                ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX + k, CorY] = ShipName[i];
                            ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX, CorY + k] = ShipName[i];
                            ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random user ships
                {
                    int ShipLen = Rand.Next(1, 5);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(UserShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListU[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListU[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListU[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListU[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }
                ShipNo = 0;
                //computer's ships
                for (int i = 4; i > 0; i--)//4 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX, CorY + k] = ShipName[i];
                                ShipsListC[ShipNo, CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX + k, CorY] = ShipName[i];
                                ShipsListC[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX + k, CorY] = ShipName[i];
                            ShipsListC[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX, CorY + k] = ShipName[i];
                            ShipsListC[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random comp ships
                {
                    int ShipLen = Rand.Next(1, 5);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListC[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListC[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListC[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListC[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }
            }
            else if (BoardSize == 10)//then it will have 5 types of ships with lengths 1,2,3,4, 5
            {
                int ShipNo = 0;
                //user's ship
                for (int i = 5; i > 0; i--)//5 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(UserShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX, CorY + k] = ShipName[i];
                                ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                UserShip[CorX + k, CorY] = ShipName[i];
                                ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX + k, CorY] = ShipName[i];
                            ShipsListU[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            UserShip[CorX, CorY + k] = ShipName[i];
                            ShipsListU[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random user ships
                {
                    int ShipLen = Rand.Next(1, 5);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(UserShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListU[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                UserShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListU[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListU[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            UserShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListU[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }
                ShipNo = 0;
                //computer's ships
                for (int i = 5; i > 0; i--)//5 mandatory ships
                {
                    int CorX = Rand.Next(0, BoardSize);
                    int CorY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, CorX, CorY, BoardSize, i) == false)
                    {
                        CorX = Rand.Next(0, BoardSize);
                        CorY = Rand.Next(0, BoardSize);
                    }//until the ship at the coordinates is inside the board and not intersecting with other ships new coordinates will be generated
                    int Direc = Rand.Next(0, 2);
                    if (CorX + (i - 1) < BoardSize && CorY + (i - 1) < BoardSize)//both down and right are possible
                    {
                        if (Direc == 0)//right
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX, CorY + k] = ShipName[i];
                                ShipsListC[ShipNo, CorX, CorY + k] = ShipName[i];
                            }
                        }
                        if (Direc == 1)//down
                        {
                            for (int k = 0; k < i; k++)
                            {
                                CompShip[CorX + k, CorY] = ShipName[i];
                                ShipsListC[ShipNo, CorX + k, CorY] = ShipName[i];
                            }
                        }
                    }
                    else if (CorX + (i - 1) < BoardSize && CorY + (i - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX + k, CorY] = ShipName[i];
                            ShipsListC[ShipNo, CorX + k, CorY] = ShipName[i];
                        }
                    }
                    else if (CorX + (i - 1) >= BoardSize && CorY + (i - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < i; k++)
                        {
                            CompShip[CorX, CorY + k] = ShipName[i];
                            ShipsListC[ShipNo, CorX, CorY + k] = ShipName[i];
                        }
                    }
                    ShipNo++;
                }
                for (int i = BoardSize - 5; i > 0; i--)//random comp ships
                {
                    int ShipLen = Rand.Next(1, 5);
                    int RanX = Rand.Next(0, BoardSize);
                    int RanY = Rand.Next(0, BoardSize);
                    while (randomCheck(CompShip, RanX, RanY, BoardSize, ShipLen) == false)
                    {
                        RanX = Rand.Next(0, BoardSize);
                        RanY = Rand.Next(0, BoardSize);
                    }
                    int RanDirec = Rand.Next(0, 2);
                    if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) < BoardSize)//both down and right are possible
                    {
                        if (RanDirec == 0)//right
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX, RanY + k] = ShipName[ShipLen];
                                ShipsListC[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                            }
                        }
                        if (RanDirec == 1)//down
                        {
                            for (int k = 0; k < ShipLen; k++)
                            {
                                CompShip[RanX + k, RanY] = ShipName[ShipLen];
                                ShipsListC[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                            }
                        }
                    }
                    else if (RanX + (ShipLen - 1) < BoardSize && RanY + (ShipLen - 1) >= BoardSize)//only down is possible
                    {
                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX + k, RanY] = ShipName[ShipLen];
                            ShipsListC[ShipNo, RanX + k, RanY] = ShipName[ShipLen];
                        }
                    }
                    else if (RanX + (ShipLen - 1) >= BoardSize && RanY + (ShipLen - 1) < BoardSize)//only right is possible
                    {

                        for (int k = 0; k < ShipLen; k++)
                        {
                            CompShip[RanX, RanY + k] = ShipName[ShipLen];
                            ShipsListC[ShipNo, RanX, RanY + k] = ShipName[ShipLen];
                        }
                    }
                    ShipNo++;
                }
            }

            //print board method non debug only user's map
            printUserMap(BoardSize, UserShip);
            Console.WriteLine();
            printEmptyEnemyMap(BoardSize, CompEmptyShip);

            //Enhancement 3 Ammo
            int UserBullets = (5 * ((BoardSize - 5) + 1));//10 or 15 or 20 or 25 or 30
            int CompBullets = (5 * ((BoardSize - 5) + 1));

            Console.WriteLine("\nYou and your Enemy have {0} bullets", UserBullets);


            //Asking for coordinates from user and randomly generating coordinates for computer
            int CorUserX;
            int CorUserY;
            int CorCompX;
            int CorCompY;
            int PointsU = 0;
            int PointsC = 0;
            int MissU = 0;
            do
            {
                //USER
                //input user's attack coordinates
                Console.WriteLine("\nYour Turn");
                //x-co0rdinate
                do
                {
                    Console.WriteLine("Enter X-Coordinate (1-{0}) down: ", BoardSize);
                    string InputX = Console.ReadLine();
                    if (int.TryParse(InputX, out CorUserX) && CorUserX <= BoardSize && CorUserX > 0)
                    {
                        break;
                    }
                    else if (InputX == "q".ToLower())
                    {
                        Console.WriteLine("Thank you for playing!");
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Entry. Please Try Again");
                    }

                } while (true);
                //y-coordinate
                do
                {
                    Console.WriteLine("Enter Y-Coordinate (1-{0}) across: ", BoardSize);
                    string InputY = Console.ReadLine();
                    if (int.TryParse(InputY, out CorUserY) && CorUserY <= BoardSize && CorUserY > 0)
                    {
                        break;
                    }
                    else if (InputY == "q".ToLower())
                    {
                        Console.WriteLine("Thank you for playing!");
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Entry. Please Try Again");
                    }

                } while (true);
                Console.Clear();
                //Checking where user hit, Enhancement 1
                UserBullets--;
                if (CompShip[CorUserX - 1, CorUserY - 1] != "H" && CompShip[CorUserX - 1, CorUserY - 1] != "M" && CompShip[CorUserX - 1, CorUserY - 1] != "*")
                {
                    int Counte = 0;
                    for (int i = 1; i < ShipName.Length; i++)
                    {
                        if (Counte == i)
                        {
                            break;
                        }
                        if (CompShip[CorUserX - 1, CorUserY - 1] == ShipName[i])
                        {
                            PointsU = PointsU + i;
                            for (int j = 0; j < 11; j++)
                            {
                                if (Counte == i)
                                {
                                    break;
                                }
                                if (ShipsListC[j, CorUserX - 1, CorUserY - 1] == ShipName[i])
                                {
                                    for (int k = 0; k < BoardSize; k++)
                                    {
                                        if (Counte == i)
                                        {
                                            break;
                                        }
                                        for (int l = 0; l < BoardSize; l++)
                                        {
                                            if (ShipsListC[j, k, l] == ShipName[i])
                                            {
                                                ShipsListC[j, k, l] = "*";
                                                CompShip[k, l] = "H";
                                                CompEmptyShip[k, l] = "H";
                                                Counte++;
                                            }
                                            if (Counte == i)
                                            {
                                                Console.WriteLine("A Hit!");
                                                Console.WriteLine("You have {0} bullets", UserBullets);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                else if (CompShip[CorUserX - 1, CorUserY - 1] == "H")
                {
                    MissU++;
                    CompShip[CorUserX - 1, CorUserY - 1] = "H";
                    CompEmptyShip[CorUserX - 1, CorUserY - 1] = "H";
                    Console.WriteLine("You hit the same coordinate, try something else next time!");
                    Console.WriteLine("You have {0} bullets", UserBullets);
                }
                else if (CompShip[CorUserX - 1, CorUserY - 1] == "M")
                {
                    MissU++;
                    CompShip[CorUserX - 1, CorUserY - 1] = "M";
                    CompEmptyShip[CorUserX - 1, CorUserY - 1] = "M";
                    Console.WriteLine("A Miss.");
                    Console.WriteLine("You hit the same coordinate, try something else next time!");
                    Console.WriteLine("You have {0} bullets", UserBullets);
                }
                else if (CompShip[CorUserX - 1, CorUserY - 1] == "*")
                {
                    MissU++;
                    CompShip[CorUserX - 1, CorUserY - 1] = "M";
                    CompEmptyShip[CorUserX - 1, CorUserY - 1] = "M";
                    Console.WriteLine("A Miss.");
                    Console.WriteLine("You have {0} bullets", UserBullets);
                }
                Console.WriteLine("Your Points: {0}",PointsU);
                int Count = 0;
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        for (int k = 0; k < BoardSize; k++)
                        {
                            if (ShipsListC[i, j, k] != "*")
                            {
                                Count++;
                            }
                        }
                    }
                }
                if (Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("You Win!!!!");
                    Console.WriteLine("Points Earned: {0}", PointsU);
                    Console.WriteLine("Bullets Used: {0}", (5 * ((BoardSize - 5) + 1)) - UserBullets);
                    Console.WriteLine("Attacks Missed: {0}", MissU);
                    break;
                }
                Console.WriteLine();
                printEmptyEnemyMap(BoardSize, CompEmptyShip);


                //COMPUTER Enchancement 4
                //generate comp's attack coordinates
                CorCompX = Rand.Next(0, BoardSize);
                CorCompY = Rand.Next(0, BoardSize);
                Console.WriteLine("\nEnemy's Turn");
                //Checking where computer hit, Enhancement 1
                CompBullets--;
                if (UserShip[CorCompX, CorCompY] != "H" && UserShip[CorCompX, CorCompY] != "M" && UserShip[CorCompX, CorCompY] != "*")
                {
                    int Countt = 0;
                    for (int i = 1; i < ShipName.Length; i++)
                    {
                        if (Countt == i)
                        {
                            break;
                        }
                        if (UserShip[CorCompX, CorCompY] == ShipName[i])
                        {
                            PointsC = PointsC + i;
                            for (int j = 0; j < 11; j++)
                            {
                                if (Countt == i)
                                {
                                    break;
                                }
                                if (ShipsListU[j, CorCompX, CorCompY] == ShipName[i])
                                {
                                    for (int k = 0; k < BoardSize; k++)
                                    {
                                        if (Countt == i)
                                        {
                                            break;
                                        }
                                        for (int l = 0; l < BoardSize; l++)
                                        {
                                            if (ShipsListU[j, k, l] == ShipName[i])
                                            {
                                                ShipsListU[j, k, l] = "*";
                                                UserShip[k, l] = "H";
                                                Countt++;
                                            }
                                            if (Countt == i)
                                            {
                                                Console.WriteLine("Enemy hit your ship");
                                                Console.WriteLine("Enemy has {0} bullets", CompBullets);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                else if (UserShip[CorCompX, CorCompY] == "H")
                {
                    UserShip[CorCompX, CorCompY] = "H";
                    Console.WriteLine("A Miss.");
                    Console.WriteLine("Enemy has {0} bullets", CompBullets);
                }
                else if (UserShip[CorCompX, CorCompY] == "M")
                {
                    UserShip[CorCompX, CorCompY] = "M";
                    Console.WriteLine("A Miss.");
                    Console.WriteLine("Enemy has {0} bullets", CompBullets);
                }
                else if (UserShip[CorCompX, CorCompY] == "*")
                {
                    UserShip[CorCompX, CorCompY] = "M";
                    Console.WriteLine("A Miss.");
                    Console.WriteLine("Enemy has {0} bullets", CompBullets);
                }
                Console.WriteLine("Enemy's Points: {0}", PointsC);
                int Counts = 0;
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        for (int k = 0; k < BoardSize; k++)
                        {
                            if (ShipsListU[i, j, k] != "*")
                            {
                                Counts++;
                            }
                        }
                    }
                }
                if (Counts == 0)
                {
                    Console.Clear();
                    Console.WriteLine("You Lose!");
                    Console.WriteLine("Points Earned: {0}", PointsU);
                    Console.WriteLine("Bullets Used: {0}", (5 * ((BoardSize - 5) + 1)) - UserBullets);
                    Console.WriteLine("Attacks Missed: {0}", MissU);
                    break;
                }
                Console.WriteLine();
                printUserMap(BoardSize, UserShip);

            } while (CompBullets != 0 || UserBullets != 0);//keeps asking until bullets are 0

            if (PointsC > PointsU)
            {
                Console.Clear();
                Console.WriteLine("You Lose!");
                Console.WriteLine("Points Earned: {0}", PointsU);
                Console.WriteLine("Bullets Used: {0}", (5 * ((BoardSize - 5) + 1)) - UserBullets);
                Console.WriteLine("Attacks Missed: {0}", MissU);

            }//if comp points(ships hit, length wise) is greater than user's points
            else if (PointsC == PointsU)
            {
                Console.Clear();
                Console.WriteLine("A Draw!");
                Console.WriteLine("Points Earned: {0}", PointsU);
                Console.WriteLine("Bullets Used: {0}", (5 * ((BoardSize - 5)+1)) - UserBullets);
                Console.WriteLine("Attacks Missed: {0}", MissU);

            }// if comp and user have equal points
            else
            {
                Console.Clear();
                Console.WriteLine("You Win!");
                Console.WriteLine("Points Earned: {0}", PointsU);
                Console.WriteLine("Bullets Used: {0}", (5 * ((BoardSize - 5) + 1)) - UserBullets);
                Console.WriteLine("Attacks Missed: {0}", MissU);

            }//if user points is greater than comp's points
            Console.ReadKey();
        }
        public static bool randomCheck(string[,] PlayerShip, int Corx, int Cory,int BoardSize, int Ei)
        {
            if(PlayerShip[Corx,Cory] == "*")//if the coordinates is empty meaning no ship is already there
            {
                if(BoardSize-Corx>=Ei || BoardSize - Cory >= Ei)//if the size of ship can fit either down or right of the coordinates
                {
                    if(Corx + (Ei - 1) < BoardSize && Cory + (Ei - 1) < BoardSize)//if the ship length is available down and right
                    {
                        if(Corx+1<BoardSize && Cory+1<BoardSize)
                        {
                            if (PlayerShip[Corx+1, Cory] == "*" && PlayerShip[Corx, Cory + 1] == "*")//if the right and down of the coordinates are empty
                            {
                                int Count = 0;
                                for (int i = 0; i < Ei; i++)
                                {
                                    if (PlayerShip[Corx + i, Cory] == "*" && PlayerShip[Corx, Cory + i] == "*")// if along the length of the ship is empty
                                    {
                                        Count++;
                                    }
                                }
                                if (Count == Ei)//if count == the size of the ship it will return true
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }//if the ship length is available down and right
                    else if(Corx + (Ei - 1) < BoardSize && Cory + (Ei - 1) >= BoardSize)//if the ship length is available down only
                    {
                        if(Corx+1<BoardSize && Cory + 1 < BoardSize)
                        {
                            if (PlayerShip[Corx + 1, Cory] == "*" && PlayerShip[Corx, Cory + 1] == "*")//if the right and down of the coordinates are empty
                            {
                                int Count = 0;
                                for (int i = 0; i < Ei; i++)
                                {
                                    if (PlayerShip[Corx + i, Cory] == "*")// if along the length of the ship is empty
                                    {
                                        Count++;
                                    }
                                }
                                if (Count == Ei)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if(Corx + 1 < BoardSize && Cory + 1 >= BoardSize)
                        {
                            if (PlayerShip[Corx + 1, Cory] == "*")
                            {
                                int Count = 0;
                                for (int i = 0; i < Ei; i++)
                                {
                                    if (PlayerShip[Corx + i, Cory] == "*")
                                    {
                                        Count++;
                                    }
                                }
                                if (Count == Ei)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }

                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if(Corx + (Ei - 1) >= BoardSize && Cory + (Ei - 1) < BoardSize)//if the ship length is available right only
                    {
                        if(Corx + 1 < BoardSize && Cory + 1 < BoardSize)
                        {
                            if (PlayerShip[Corx + 1, Cory] == "*" && PlayerShip[Corx, Cory + 1] == "*")
                            {
                                int Count = 0;
                                for (int i = 0; i < Ei; i++)
                                {
                                    if (PlayerShip[Corx, Cory + i] == "*")
                                    {
                                        Count++;
                                    }
                                }
                                if (Count == Ei)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if(Corx + 1 >= BoardSize && Cory + 1 < BoardSize)
                        {
                            if (PlayerShip[Corx, Cory + 1] == "*")
                            {
                                int Count = 0;
                                for (int i = 0; i < Ei; i++)
                                {
                                    if (PlayerShip[Corx, Cory + i] == "*")
                                    {
                                        Count++;
                                    }
                                }
                                if (Count == Ei)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }

                        }
                        else
                        {
                            return false;
                        }

                    }
                }
                else
                {
                    return false;
                }
            }
            return false;//if not return false
        }//checks if coordinates are inside the board and the ships are not colliding
        public static void printUserMap(int BoardSize, string[,] UsersShip)
        {
            //user's ship
            Console.WriteLine("Here is your map!");
            Console.Write("   ");
            for (int i = 1; i < BoardSize + 1; i++)
            {
                Console.Write(" " + i + " ");
            }
            for (int i = 0; i < BoardSize; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < BoardSize; j++)
                {
                    if (i == 9 && j == 0)
                    {
                        Console.Write(" " + (i + 1));
                    }
                    else if (j == 0)
                    {
                        Console.Write("  " + (i + 1));
                    }
                    Console.Write(" " + UsersShip[i, j] + " ");
                }
            }
        }//prints user's map with UserShip array
        public static void printEnemyMap(int BoardSize,  string[,] CompsShip)
        {
            //computer's ship
            Console.WriteLine("Here is your enemy's map!");
            Console.Write("   ");
            for (int i = 1; i < BoardSize + 1; i++)
            {
                Console.Write(" " + i + " ");
            }
            for (int i = 0; i < BoardSize; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < BoardSize; j++)
                {
                    if (i == 9 && j == 0)
                    {
                        Console.Write(" " + (i + 1));
                    }
                    else if (j == 0)
                    {
                        Console.Write("  " + (i + 1));
                    }
                    Console.Write(" " + CompsShip[i, j] + " ");
                }
            }
        }//prints comp's map with CompShip array
        public static void printEmptyEnemyMap(int BoardSize, string[,] CompsEmptyShip)
        {
            //computer's ship
            Console.WriteLine("Here is your enemy's map!");
            Console.Write("   ");
            for (int i = 1; i < BoardSize + 1; i++)
            {
                Console.Write(" " + i + " ");
            }
            for (int i = 0; i < BoardSize; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < BoardSize; j++)
                {
                    if (i == 9 && j == 0)
                    {
                        Console.Write(" " + (i + 1));
                    }
                    else if (j == 0)
                    {
                        Console.Write("  " + (i + 1));
                    }
                    Console.Write(" " + CompsEmptyShip[i, j] + " ");
                }
            }
        }//prints comp's map with CompShip array
    }
}   
