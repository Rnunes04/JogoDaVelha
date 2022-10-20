using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha
{
    class JogoDaVelha
    {
        private int CountPlays;
        private string player;
        private bool endGame;
        private char turn;
        private char[] table;

        public JogoDaVelha()
        {
            CountPlays = 0;
            endGame = false;
            turn = 'X';
            table = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        }

        public void StartGame()
        {
            while (!endGame)
            {
                ReloadTable();
                UserInput();
                ReloadTable();
                EndGame();
                ChangePlayer();
            }
        }

        private string DrawTable()
        {
            return $"\n {table[0]} | {table[1]} | {table[2]} \n" +
                   $"___|___|___\n" +
                   $" {table[3]} | {table[4]} | {table[5]} \n" +
                   $"___|___|___\n" +
                   $" {table[6]} | {table[7]} | {table[8]} \n"
                   ;
        }

        private void ReloadTable()
        {
            Console.Clear();
            Console.WriteLine(DrawTable());
        }

        private void ChangePlayer()
        {
            if (turn == 'X')
            {
                player = "Player 2";
                turn = 'O';
            }
            else
            {
                player = "player 1";
                turn = 'X';
            }
        }

        private void ChangeValue(int position)
        {
            int number = position - 1;

            table[number] = turn;
            CountPlays++;
        }

        private void UserInput()
        {
            Console.WriteLine($" Vez do {turn}, select number between 1 a 9");

            bool IsValid = int.TryParse(Console.ReadLine(), out int position);

            while (!IsValid || !CheckInput(position))
            {
                Console.WriteLine("This input is not valid");
                IsValid = int.TryParse(Console.ReadLine(), out position);
            }

            ChangeValue(position);
        }

        private bool CheckInput(int position)
        {
            int number = position - 1;

            return table[number] != 'X' && table[number] != 'O';
        }

        private bool CheckHorizontalVictory()
        {

            bool Line1 = table[0] == table[1] && table[0] == table[2];
            bool Line2 = table[3] == table[4] && table[3] == table[5];
            bool Line3 = table[6] == table[7] && table[6] == table[8];

            return Line1 || Line2 || Line3;
        }

        private bool CheckVerticalVictory()
        {

            bool column1 = table[0] == table[3] && table[0] == table[6];
            bool column2 = table[1] == table[4] && table[1] == table[7];
            bool column3 = table[2] == table[5] && table[2] == table[8];

            return column1 || column2 || column3;
        }

        private bool CheckDiagonalVictory()
        {
            bool diagonal1 = table[0] == table[4] && table[0] == table[8];
            bool diagonal2 = table[2] == table[4] && table[2] == table[6];

            return diagonal1 || diagonal2;
        }

        private void EndGame()
        {
            if (CountPlays < 5)
                return;


            if (CheckHorizontalVictory() || CheckVerticalVictory() || CheckDiagonalVictory())
            {
                endGame = true;
                Console.WriteLine($"!! Victory - Winner {player} !!");
                return;
            }

            if (CountPlays is 9)
            {
                endGame = true;
                Console.WriteLine(" ---- Tie ----- ");
            }
        }
    }
}
