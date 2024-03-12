using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;

namespace MatchesGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //private readonly MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            //viewModel = new MainViewModel();
            DataContext = this;

            GetMatchItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SimpleCommand StartCommand => new SimpleCommand(Start);
        public SimpleCommand<object> PickCommand => new SimpleCommand<object>(Pick);
        public SimpleCommand OKCommand => new SimpleCommand(OK);
        public SimpleCommand ExitCommand => new SimpleCommand(Exit);


        public string gameRule = "游戏规则：每人可以在一轮内, 在任意行拿任意根火柴, 但不能跨行, 拿最后一根火柴的人即为输家!";
        public string GameRule
        {
            get { return gameRule; }
            set { gameRule = value; OnPropertyChanged(); }
        }

        public string tips = "请按 开始游戏 按钮";
        public string Tips
        {
            get { return tips; }
            set { tips = value; OnPropertyChanged(); }
        }

        private List<Match> matches;

        private ObservableCollection<ObservableCollection<Match>> matchItems = new() { new(), new(), new() };
        public ObservableCollection<ObservableCollection<Match>> MatchItems
        {
            get { return matchItems; }
            set { matchItems = value; OnPropertyChanged(); }
        }

        private bool isStarted;
        public bool IsStarted
        {
            get { return isStarted; }
            set { isStarted = value; OnPropertyChanged(); }
        }

        private Player player1;
        private Player player2;

        private Player currentPlayer;
        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; OnPropertyChanged(); }
        }

        private void GetMatchItems()
        {
            matches = new List<Match>();
            int[] array = { 3, 5, 7 };
            int id = 1;
            for (int i = 0; i < array.Length; i++)
            {
                List<Match> _matches = new List<Match>();
                for (int j = 1; j <= array[i]; j++)
                {
                    Match match = new Match
                    {
                        Id = id,
                        RowNumber = i + 1,
                    };
                    _matches.Add(match);
                    id += 1;
                }
                matches.AddRange(_matches);
                MatchItems[i] = new ObservableCollection<Match>(_matches);
            }
        }

        private void Start()
        {
            GetMatchItems();
            IsStarted = true;
            player1 = new Player(1);
            player2 = new Player(2);
            CurrentPlayer = player1;
            Tips = $"请 {CurrentPlayer.Name} 选取火柴";
        }

        private void Pick(object obj)
        {
            if (CurrentPlayer == null)
            {
                MessageBox.Show("请先按开始游戏按钮！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (obj is Match match)
            {
                if (CurrentPlayer.SelectingMatches.Any(t => t.RowNumber != match.RowNumber))
                {
                    MessageBox.Show("不能跨行选取哦！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                match.IsSelected = true;
                match.PlayerId = CurrentPlayer.Id;
                CurrentPlayer.SelectingMatches.Add(match);
                CurrentPlayer.SelectedMatches.Add(match);
            }

            if (!matches.Any(t => !t.IsSelected))
            {
                Tips = $"{CurrentPlayer.Name} 输了！";
                IsStarted = false;
            }
        }

        private void OK()
        {
            if (!CurrentPlayer.SelectingMatches.Any())
            {
                MessageBox.Show("你还没有选取火柴！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CurrentPlayer = CurrentPlayer.Id == 1 ? player2 : player1;
            CurrentPlayer.SelectingMatches.Clear();
            Tips = $"请 {CurrentPlayer.Name} 选取火柴";
        }

        private void Exit()
        {
            if (MessageBox.Show("确定要退出游戏吗?", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}