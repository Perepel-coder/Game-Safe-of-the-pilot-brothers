using System.Collections.Generic;

namespace Game_Safe_of_the_pilot_brothers.Model
{

    /// <summary>
    /// Интрефейс, описывающий ячейку игрового поля
    /// </summary>
    public interface ILever
    {
        public int LeverPosition { get; set; }
        public string LeverView { get; set; }
        public void ChangePositionToOpposite();
    }
    /// <summary>
    /// Интерфейс описывающий процесс игры
    /// </summary>
    public interface IGameService
    {
        public int PlayingFieldSize { get; set; }
        public List<List<ILever>> PlayingField { get; set; }
        public void GetData(int playingFieldsize);
        public void ChangePlayingField(int idrow, int idcolumn);
        public void CreatePlayingField();
        public void StartGameAgain();
        public bool GameEnd();
    }
}
