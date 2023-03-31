using Bankschalter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace wer_wird_millionaer
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public ICommand click_answer { get; private set; }
        DispatcherTimer dt = new DispatcherTimer();
        DispatcherTimer spannungsTimer = new DispatcherTimer();
        DispatcherTimer delay = new DispatcherTimer();
        private string defaultColor = "#244095";
        private string correctColor = "LightGreen";
        private string falseColor = "#ff5959";
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
        private string cursor = "Hand";
        private int spannungsTicks = 5;
        private int currSpannungsTick = 0;
        Random rnd = new Random();
        Quiz quiz = new Quiz();

        public string Cursor
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

        public ViewModel()
        {
            click_answer = new RelayCommand<string>(CheckAnswer);
            dt.Interval = TimeSpan.FromSeconds(2);
            dt.Tick += ResetColor;
            spannungsTimer.Interval = TimeSpan.FromMilliseconds(200);
            spannungsTimer.Tick += ErzeugeSpannung;
            delay.Interval = TimeSpan.FromMilliseconds(50);
            delay.Tick += CheckUserAnswer;
            color_a = defaultColor;
            color_b = defaultColor;
            color_c = defaultColor;
            color_d = defaultColor;
            loadText();
        }

        private void ErzeugeSpannung(object? sender, EventArgs e)
        {
            if (currSpannungsTick>=spannungsTicks)
            {
                spannungsTimer.Stop();
                delay.Start();
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
            currSpannungsTick++;
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
            Cursor = "Hand";
            currSpannungsTick = 0;
        }

        public void CheckAnswer(string answer)
        {
            if (disabled) return;
            disabled = true;
            Cursor = "Arrow";
            spannungsTimer.Start();
            userAnswer = answer;
        }
        public void CheckUserAnswer(object? sender, EventArgs e)
        {
            delay.Stop();
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
                FragenText = "Nice bro";
                SetStufe(runde);
            }
            else
            {
                FragenText = "lol";
            }
            dt.Start();
        }
        public void SetStufe(int i)
        {
            Margin = $"112 {551 - i*32} 0 0";
            string[] neu = new string[] { "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white" };
            neu[i]="black";
            StufenFarben = neu;
        }
        

        public void loadText()
        {
            Questions q = quiz.getQuestionOfCategory(runde / 5);
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
