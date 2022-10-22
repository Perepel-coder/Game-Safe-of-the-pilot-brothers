
namespace Game_Safe_of_the_pilot_brothers.Model
{
    public enum LEVERPOSITION { OFF, ON};
    internal class Lever: ILever
    {
        public int LeverPosition { get; set; }
        public string LeverView { get; set; }
        private static string[] leverViews = new string[2] { "-", "+" };
        public Lever()
        {
            this.LeverPosition = (int)LEVERPOSITION.OFF;
            this.LeverView = leverViews[0];
        }
        /// <summary>
        /// Изменить характеристики клетки на обратные
        /// </summary>
        public void ChangePositionToOpposite()
        {
            if(this.LeverPosition == (int)LEVERPOSITION.OFF) { this.LeverPosition = (int)LEVERPOSITION.ON; this.LeverView = leverViews[1]; return; }
            if(this.LeverPosition == (int)LEVERPOSITION.ON) { this.LeverPosition = (int)LEVERPOSITION.OFF; this.LeverView = leverViews[0]; }
        }
    }
}
