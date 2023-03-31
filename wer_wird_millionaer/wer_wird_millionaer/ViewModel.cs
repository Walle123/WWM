using Bankschalter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace wer_wird_millionaer
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public Window window;
        public ICommand click_answer { get; private set; }
        public ICommand click_joker { get; private set; }
        public ICommand spiel_starten { get; private set; }
        public ICommand spiel_beenden { get; private set; }
        DispatcherTimer dt = new DispatcherTimer();
        DispatcherTimer spannungsTimer = new DispatcherTimer();
        DispatcherTimer delay = new DispatcherTimer();
        DispatcherTimer menuTimer = new DispatcherTimer();
        private string defaultColor = "#244095";
        private string correctColor = "#02fa1b";
        private string falseColor = "#f70217";
        private string spannungColor = "Gold";
        private string color_a;
        private string color_b;
        private string color_c;
        private string color_d;
        private string fragenText = "";
        private string antwort1Text = "";
        private string antwort2Text = "";
        private string antwort3Text = "";
        private string antwort4Text = "";
        private int runde = 0;
        private string rightAnswer = "";
        private string userAnswer = "";
        private string margin = "112 551 0 0";
        private string[] stufenFarben = new string[] {"black", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white" };
        private bool disabled = false;
        private bool[] jokers = new bool[] { true, true, true };
        private string[] jokers_cursor = new string[] { "Hand", "Hand", "Hand" };
        private string[] cursor = new string[] { "Hand", "Hand", "Hand", "Hand" };
        private string[] jokers_used = new string[] { "Hidden", "Hidden", "Hidden" };
        private int[] gewinnstufen = new int[] { 50, 100, 200, 300, 500, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 500000, 1000000 };
        private int[] sicherheitsStufen = new int[] { 50, 1000, 32000, 1000000 };
        private string messageBoxVisibility = "Visible";
        private string gameVisibility = "Hidden";
        private string[] boxMessages = new string[] { "Wer Wird Millionär", "", "Spiel Starten", "Beenden" };
        private int spannungsTicks = 9;
        private int currSpannungsTick = 0;
        Random rnd = new Random();
        Quiz quiz = new Quiz();
        public string GameVisibility
        {
            get { return gameVisibility; }
            set
            {
                gameVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameVisibility)));
            }
        }
        public string MessageBoxVisibility
        {
            get { return messageBoxVisibility; }
            set
            {
                messageBoxVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MessageBoxVisibility)));
            }
        }

        public string[] BoxMessages
        {
            get { return boxMessages; }
            set
            {
                boxMessages = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BoxMessages)));
            }
        }

        public string[] JokersUsed
        {
            get { return jokers_used; }
            set
            {
                jokers_used = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JokersUsed)));
            }
        }
        public string[] JokersCursor
        {
            get { return jokers_cursor; }
            set
            {
                jokers_cursor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JokersCursor)));
            }
        }

        public string[] Cursor
        {
            get { return cursor; }
            set
            {
                cursor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cursor)));
            }
        }

        public string Margin
        {
            get { return margin; }
            set
            {
                margin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Margin)));
            }
        }
        public string[] StufenFarben
        {
            get { return stufenFarben; }
            set
            {
                stufenFarben = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StufenFarben)));
            }
        }
        public string FragenText
        {
            get { return fragenText; }
            set
            {
                fragenText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FragenText)));
            }
        }
        public string Antwort1Text
        {
            get { return antwort1Text; }
            set
            {
                antwort1Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Antwort1Text)));
            }
        }
        public string Antwort2Text
        {
            get { return antwort2Text; }
            set
            {
                antwort2Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Antwort2Text)));
            }
        }
        public string Antwort3Text
        {
            get { return antwort3Text; }
            set
            {
                antwort3Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Antwort3Text)));
            }
        }
        public string Antwort4Text
        {
            get { return antwort4Text; }
            set
            {
                antwort4Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Antwort4Text)));
            }
        }
        public string Color_a
        {
            get { return color_a; }
            set
            {
                color_a = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_a)));
            }
        }
        public string Color_b
        {
            get { return color_b; }
            set
            {
                color_b = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_b)));
            }
        }
        public string Color_c
        {
            get { return color_c; }
            set
            {
                color_c = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_c)));
            }
        }
        public string Color_d
        {
            get { return color_d; }
            set
            {
                color_d = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_d)));
            }
        }

        public ViewModel(Window window)
        {
            this.window = window;
            click_answer = new RelayCommand<string>(CheckAnswer);
            click_joker = new RelayCommand<string>(Joker_click);
            spiel_starten = new RelayCommand<string>(SpielStarten);
            spiel_beenden = new RelayCommand<string>(SpielBeenden);
            dt.Interval = TimeSpan.FromSeconds(2);
            dt.Tick += ResetColor;
            spannungsTimer.Interval = TimeSpan.FromMilliseconds(100);
            spannungsTimer.Tick += ErzeugeSpannung;
            menuTimer.Interval = TimeSpan.FromSeconds(5);
            menuTimer.Tick += WrongAnswer;
            delay.Interval = TimeSpan.FromMilliseconds(50);
            delay.Tick += updateEigenschaften;
            color_a = defaultColor;
            color_b = defaultColor;
            color_c = defaultColor;
            color_d = defaultColor;
            update();
        }
        public void SpielStarten(string s)
        {
            quiz.usedQuestions = new List<Questions>();
            disabled = false;
            GameVisibility = "Visible";
            MessageBoxVisibility = "Hidden";
            color_a = defaultColor;
            color_b = defaultColor;
            color_c = defaultColor;
            color_d = defaultColor;
            currSpannungsTick = 0;
            runde = 0;
            StufenFarben = new string[] { "black", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white" };
            JokersCursor = new string[] { "Hand", "Hand", "Hand" };
            Cursor = new string[] { "Hand", "Hand", "Hand", "Hand" };
            JokersUsed = new string[] { "Hidden", "Hidden", "Hidden" };
            loadText();
            update();
        }
        public void SpielBeenden(string s)
        {
            window.Close();
        }
        public void update()
        {
            delay.Start();
        }

        public void updateEigenschaften(object? sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JokersCursor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cursor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_a)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_b)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_c)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_d)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JokersUsed)));
            delay.Stop();
        }

        private void ErzeugeSpannung(object? sender, EventArgs e)
        {
            if (currSpannungsTick == spannungsTicks - 1)
            {
                spannungsTimer.Interval = TimeSpan.FromSeconds(1);
            }
            if (currSpannungsTick % 2 == 0)
            {
                switch (userAnswer)
                {
                    case "a":
                        Color_a = spannungColor;
                        break;
                    case "b":
                        Color_b = spannungColor;
                        break;
                    case "c":
                        Color_c = spannungColor;
                        break;
                    case "d":
                        Color_d = spannungColor;
                        break;
                }
            }
            else
            {
                switch (userAnswer)
                {
                    case "a":
                        Color_a = defaultColor;
                        break;
                    case "b":
                        Color_b = defaultColor;
                        break;
                    case "c":
                        Color_c = defaultColor;
                        break;
                    case "d":
                        Color_d = defaultColor;
                        break;
                }
            }
            if (currSpannungsTick >= spannungsTicks)
            {
                spannungsTimer.Stop();
                CheckUserAnswer();
                spannungsTimer.Interval = TimeSpan.FromMilliseconds(100);
            }
            currSpannungsTick++;
        }
        public void WrongAnswer(object? sender, EventArgs e)
        {
            menuTimer.Stop();
            FragenText = "";
            GameVisibility = "Hidden";
            BoxMessages = new string[] { "Sie haben verloren", $"Sie haben {sicherheitsStufen[runde/5]}€ gewonnen", "Nochmal spielen", "Beenden" };
            MessageBoxVisibility = "Visible";
        }

        private void ResetColor(object? sender, EventArgs e)
        {
            dt.Stop();
            Color_a = defaultColor;
            Color_b = defaultColor;
            Color_c = defaultColor;
            Color_d = defaultColor;
            loadText();
            disabled = false;
            currSpannungsTick = 0;
            update();
        }

        public void CheckAnswer(string answer)
        {
            string antwortText = "";
            switch (answer)
            {
                case "a":
                    antwortText = Antwort1Text;
                    break;
                case "b":
                    antwortText= Antwort2Text;
                    break;
                case "c":
                    antwortText = Antwort3Text;
                    break;
                case "d":
                    antwortText = Antwort4Text;
                    break;
            }
            if (antwortText.Equals(String.Empty))
            {
                return;
            }
            if (disabled) return;
            disabled = true;
            Cursor = new string[] { "Arrow", "Arrow", "Arrow", "Arrow" };
            userAnswer = answer;
            spannungsTimer.Start();
        }
        public void CheckUserAnswer()
        {
            bool richtig = false;
            switch (userAnswer)
            {
                case "a":
                    richtig = rightAnswer.Equals(Antwort1Text);
                    Color_a = richtig ? correctColor : falseColor;
                    break;
                case "b":
                    richtig = rightAnswer.Equals(Antwort2Text);
                    Color_b = richtig ? correctColor : falseColor;
                    break;
                case "c":
                    richtig = rightAnswer.Equals(Antwort3Text);
                    Color_c = richtig ? correctColor : falseColor;
                    break;
                case "d":
                    richtig = rightAnswer.Equals(Antwort4Text);
                    Color_d = richtig ? correctColor : falseColor;
                    break;
            }
            if (richtig)
            {
                FragenText = $"Sehr gut, als nächstes spielen Sie um {gewinnstufen[runde]}€";
                SetStufe(runde);
                dt.Start();
                update();
            }
            else
            {
                FragenText = "Leider Falsch";
                if (rightAnswer.Equals(Antwort1Text)){
                    Color_a = correctColor;
                }
                if (rightAnswer.Equals(Antwort2Text)){
                    Color_b = correctColor;
                }
                if (rightAnswer.Equals(Antwort3Text)){
                    Color_c = correctColor;
                }
                if (rightAnswer.Equals(Antwort4Text)){
                    Color_d = correctColor;
                }

                menuTimer.Start();
            }
        }
        public void SetStufe(int i)
        {
            Margin = $"112 {551 - i*32} 0 0";
            string[] neu = new string[] { "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white" };
            neu[i]="black";
            StufenFarben = neu;
            update();
        }
        public void Joker_click(string s)
        {
            switch (s)
            {
                case "0":
                    if (jokers[0])
                    {
                        RemoveTwoAnswers();
                        jokers[0] = false;
                        JokersCursor[0] = "Arrow";
                        JokersUsed[0] = "Visible";
                    }
                    break;
                case "1":
                    if (jokers[1])
                    {
                        SkipJoker();
                        jokers[1] = false;
                        JokersCursor[1] = "Arrow";
                        JokersUsed[1] = "Visible";
                    }
                    break;
                case "2":
                    if (jokers[2])
                    {
                        jokers[2] = false;
                        JokersCursor[2] = "Arrow";
                        JokersUsed[2] = "Visible";
                    }
                    break;
            }
            update();
        }
        public void SkipJoker()
        {
            loadText();
            update();
        }
        
        public void RemoveTwoAnswers()
        {
            bool answer_1 = rightAnswer.Equals(Antwort1Text);
            bool answer_2 = rightAnswer.Equals(Antwort2Text);
            bool answer_3 = rightAnswer.Equals(Antwort3Text);
            bool answer_4 = rightAnswer.Equals(Antwort4Text);
            if (answer_1)
            {
                Antwort2Text = String.Empty;
                Antwort3Text = String.Empty;
                string[] t = Cursor;
                t[1] = "Arrow";
                t[2] = "Arrow";
                Cursor = t;
            }
            if (answer_2)
            {
                Antwort4Text = String.Empty;
                Antwort3Text = String.Empty;
                string[] t = Cursor;
                t[3] = "Arrow";
                t[2] = "Arrow";
                Cursor = t;
            }
            if (answer_3)
            {
                Antwort1Text = String.Empty;
                Antwort2Text = String.Empty;
                string[] t = Cursor;
                t[1] = "Arrow";
                t[0] = "Arrow";
                Cursor = t;
            }
            if (answer_4)
            {
                Antwort1Text = String.Empty;
                Antwort3Text = String.Empty;
                string[] t = Cursor;
                t[0] = "Arrow";
                t[2] = "Arrow";
                Cursor = t;
            }
            update();
        }

        public void loadText()
        {
            Cursor = new string[] { "Hand", "Hand", "Hand", "Hand" };
            Questions q;
            if (runde == 14)
            {
               q = quiz.getQuestionOfCategory(3);
            }
            else
            {
               q = quiz.getQuestionOfCategory(runde / 5);
            }
            FragenText = q.Prompt; // "Runde:" + runde / 5 + " _ " + 
            rightAnswer = q.Options[0];
            List<string> strings = new List<string>();
            while (strings.Count < 4)
            {
                string antwort = q.Options[rnd.Next(q.Options.Count)];
                strings.Add(antwort);
                q.Options.Remove(antwort);
            }
            Antwort1Text = strings[0];
            Antwort2Text = strings[1];
            Antwort3Text = strings[2];
            Antwort4Text = strings[3];
            runde++;
        }
        public event PropertyChangedEventHandler? PropertyChanged;


    }
}
