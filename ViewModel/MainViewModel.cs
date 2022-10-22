using Autofac;
using Game_Safe_of_the_pilot_brothers.Model;
using ReactiveUI;
using System.Windows.Input;
using ServicesMVVM;
using System.Collections.Generic;
using Gu.Wpf.DataGrid2D;
using Xceed.Wpf.Toolkit;
using System.Windows.Media;

namespace Game_Safe_of_the_pilot_brothers.ViewModel
{
    internal class MainViewModel: ReactiveObject
    {

        private int playingFieldSize;
        private List<List<string>>? playingField;
        private string? listHelp;

        private Game GameService { get; }
        public int PlayingFieldSize
        {
            get { return playingFieldSize; }
            set { this.RaiseAndSetIfChanged(ref playingFieldSize, value); }
        }
        public List<List<string>>? PlayingField
        {
            get { return playingField; }
            set { this.RaiseAndSetIfChanged(ref playingField, value); }
        }
        public string? ListHelp
        {
            get { return listHelp; }
            set { this.RaiseAndSetIfChanged(ref listHelp, value); }
        }

        /// <summary>
        /// Получить индексы теущей ячейки
        /// </summary>
        public RowColumnIndex Indexes 
        {
            get { return new RowColumnIndex(); }
            set
            {
                this.GameService.ChangePlayingField(value.Row, value.Column);
                this.PlayingField = CreatDataView();
                if (this.GameService.GameEnd())
                {
                    System.Windows.Style style = new(typeof(MessageBox));
                    style.Setters.Add(new System.Windows.Setter(MessageBox.FontFamilyProperty, new FontFamily("Algerian")));
                    style.Setters.Add(new System.Windows.Setter(MessageBox.FontSizeProperty, 30.0));
                    style.Setters.Add(new System.Windows.Setter(System.Windows.Controls.TextBox.TextAlignmentProperty, System.Windows.TextAlignment.Center));
                    MessageBox.Show(null, "Victory", "Hooray!", style);
                }
            }
        }
        public MainViewModel()
        {
            GameService = GameModule.Container.Resolve<Game>();
            this.playingFieldSize = 5;
            ListHelp = "";
        }

        /// <summary>
        /// Генерировать List<list<string>> из текущего"
        /// </summary>
        /// <returns></returns>
        private List<List<string>> CreatDataView()
        {
            var collection = GameService.PlayingField;
            List<List<string>> result = new();
            for (int i = 0; i < collection.Count; i++)
            {
                result.Add(new());
                for (int j = 0; j < collection.Count; j++)
                {
                    result[i].Add(collection[i][j].LeverView);
                }
            }
            return result;
        }

        #region commands
        //private RelayCommand? startNew;
        //private RelayCommand? startAgain;
        //private RelayCommand? help;

        public ICommand Help
        {
            get
            {
                return
                  (new RelayCommand(obj =>
                  {
                      if (ListHelp != "") { ListHelp = ""; return; }
                      var list = GameService.algorithmCreatingGame;
                      if(list.Count < 2) { return; }
                      ListHelp = "From the initial combination.\n";
                      for (int i = list.Count - 1; i >= 0; i--)
                      {
                          ListHelp += $"[{list[i][0] + 1}, {list[i][1] + 1}]\n";
                      }
                  }));
            }
        }
        /// <summary>
        /// сгенерировать игровое поле
        /// </summary>
        public ICommand StartNewGames
        {
            get
            {
                return
                  (new RelayCommand(obj =>
                  {
                      GameService.GetData(playingFieldSize);
                      GameService.CreatePlayingField();
                      PlayingField = CreatDataView();
                      ListHelp = "";
                  }));
            }
        }

        /// <summary>
        /// повторить начальную комбинацию текущего игрового поля
        /// </summary>
        public ICommand StartGameAgain
        {
            get
            {
                return
                  (new RelayCommand(obj =>
                  {
                      GameService.StartGameAgain();
                      PlayingField = CreatDataView();
                  }));
            }
        }
        #endregion
    }
}
