using System.Collections.Generic;
using Autofac;
using System;

namespace Game_Safe_of_the_pilot_brothers.Model
{
    internal class Game: IGameService
    {
        public int PlayingFieldSize { get; set; }
        public List<List<ILever>> PlayingField { get; set; }
        public List<int[]> algorithmCreatingGame;
        public Game() { PlayingField = new(); algorithmCreatingGame = new(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playingFieldSize" размер игрового поля></param>
        /// <param name="container" Autofac контейнер></param>
        public void GetData(int playingFieldSize)
        {
            this.PlayingFieldSize = playingFieldSize;
            this.PlayingField = new ();
            this.algorithmCreatingGame = new();
            for (int i = 0; i < playingFieldSize; i++)
            {
                PlayingField.Add(new());
                for (int j = 0; j < playingFieldSize; j++)
                {
                    PlayingField[i].Add(GameModule.Container.Resolve<ILever>());
                }
            }

        }

        /// <summary>
        /// Генерировать игровое поле
        /// </summary>
        public void CreatePlayingField()
        {
            this.algorithmCreatingGame.Clear();
            Random rnd = new();
            int size = PlayingFieldSize / 2;
            for (int i = 0; i < size; i++)
            {
                int idRow = rnd.Next(PlayingFieldSize);
                int idColumn = rnd.Next(PlayingFieldSize);

                this.algorithmCreatingGame.Add(new int[] { idRow, idColumn });
                this.ChangePlayingField(idRow, idColumn);
            }

        }
        /// <summary>
        /// Изменить характеристики ячеек на обратные
        /// </summary>
        /// <param name="idrow"></param>
        /// <param name="idcolumn"></param>
        public void ChangePlayingField(int idrow, int idcolumn)
        {
            for (int i = 0; i < PlayingFieldSize; i++)
            {
                this.PlayingField[idrow][i].ChangePositionToOpposite();
                if (i != idrow) { this.PlayingField[i][idcolumn].ChangePositionToOpposite(); }
            }
        }
        /// <summary>
        /// Начать заново ту же комбинацию 
        /// </summary>
        public void StartGameAgain()
        {
            this.PlayingField.Clear();
            for (int i = 0; i < this.PlayingFieldSize; i++)
            {
                PlayingField.Add(new());
                for (int j = 0; j < this.PlayingFieldSize; j++) { PlayingField[i].Add(GameModule.Container.Resolve<ILever>()); }
            }

            for (int i = 0; i < algorithmCreatingGame.Count; i++)
            {
                int idRow = algorithmCreatingGame[i][0];
                int idColumn = algorithmCreatingGame[i][1];
                this.ChangePlayingField(idRow, idColumn);
            }
        }

        /// <summary>
        /// Обрабатывает конец игры
        /// </summary>
        /// <returns></returns>
        public bool GameEnd()
        {
            var leverFlag = PlayingField[0][0];
            for (int i = 0; i < this.PlayingFieldSize; i++)
            {
                for (int j = 0; j < this.PlayingFieldSize; j++)
                {
                   if(leverFlag.LeverPosition != this.PlayingField[i][j].LeverPosition) { return false; }
                }
            }
            return true;
        }
    }
}
