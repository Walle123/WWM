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
        private string defaultColor = "#244095";
        private string correctColor = "LightGreen";
        private string falseColor = "#ff5959";
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
        Random rnd = new Random();
        Quiz quiz = new Quiz();
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
            color_a = defaultColor;
            color_b = defaultColor;
            color_c = defaultColor;
            color_d = defaultColor;
            loadText();
        }

        private void ResetColor(object? sender, EventArgs e)
        {
            dt.Stop();
            Color_a = defaultColor;
            Color_b = defaultColor;
            Color_c = defaultColor;
            Color_d = defaultColor;
            loadText();
        }

        public void CheckAnswer(string answer)
        {
            Random rnd = new Random();
            bool richtig = false;
            switch (answer)
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
            dt.Start(); // färbt buttons zurück
            if (richtig)
            {
                FragenText = "Nice bro";
            }
            else
            {
                FragenText = "lol";
            }
        }
        
        public void loadText()
        {
            Questions q = quiz.getQuestionOfCategory(runde/3);
            FragenText = "Runde:" + runde / 3 + " _ " + q.Prompt;
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
