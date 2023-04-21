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
    public class ViewModel : INotifyPropertyChanged
    {
        public Window window;

        /// <summary>
        /// Benutzer hat Antwort angeklickt
        /// </summary>
        public ICommand click_answer { get; private set; }

        /// <summary>
        /// Benutzer hat Joker geklickt
        /// </summary>
        public ICommand click_joker { get; private set; }

        /// <summary>
        /// Benutzer hat Spiel Starten geklickt
        /// </summary>
        public ICommand spiel_starten { get; private set; }

        /// <summary>
        /// Benutzer hat Spiel Beenden geklickt
        /// </summary>
        public ICommand spiel_beenden { get; private set; }

        /// <summary>
        /// Timer für das Reseten nach richtiger Antwort
        /// </summary>
        DispatcherTimer dt = new DispatcherTimer();

        /// <summary>
        /// Timer für flicker Effekt nach dem klicken einer Antwort
        /// </summary>
        DispatcherTimer blinkTimer = new DispatcherTimer();

        /// <summary>
        /// Hilfstimer für probleme mit Oberflächen updates
        /// </summary>
        DispatcherTimer delay = new DispatcherTimer();

        /// <summary>
        /// Timer für das öffnen des Menüs nach falscher Antwort
        /// </summary>
        DispatcherTimer menuTimer = new DispatcherTimer();
        private string defaultColor = "#244095";
        private string correctColor = "#02fa1b";
        private string falseColor = "#f70217";
        private string blinkColor = "Gold";
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
        private string[] alteJokerCursor;
        private string[] boxMessages = new string[] { "Wer Wird Millionär", "", "Spiel Starten", "Beenden" };
        private int blinkTicks = 9;
        private int currBlinkTick = 0;
        Random rnd = new Random();

        /// <summary>
        /// Beinhaltet alle Questions
        /// </summary>
        Quiz quiz = new Quiz();

        /// <summary>
        /// Sichtbarkeit der Spielelemente der Oberfläche
        /// </summary>
        public string GameVisibility
        {
            get { return gameVisibility; }
            set
            {
                gameVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameVisibility)));
            }
        }

        /// <summary>
        /// Sichtbarkeit des Menüs
        /// </summary>
        public string MessageBoxVisibility
        {
            get { return messageBoxVisibility; }
            set
            {
                messageBoxVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MessageBoxVisibility)));
            }
        }

        /// <summary>
        /// Inhalt des Menüs
        /// </summary>
        public string[] BoxMessages
        {
            get { return boxMessages; }
            set
            {
                boxMessages = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BoxMessages)));
            }
        }

        /// <summary>
        /// Sichtbarkeit von Joker benutzt Bild
        /// </summary>
        public string[] JokersUsed
        {
            get { return jokers_used; }
            set
            {
                jokers_used = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JokersUsed)));
            }
        }

        /// <summary>
        /// Cursor aussehen bei den Jokern
        /// </summary>
        public string[] JokersCursor
        {
            get { return jokers_cursor; }
            set
            {
                jokers_cursor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JokersCursor)));
            }
        }

        /// <summary>
        /// Cursor aussehen bei den Antworten
        /// </summary>
        public string[] Cursor
        {
            get { return cursor; }
            set
            {
                cursor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cursor)));
            }
        }

        /// <summary>
        /// Margin des Polygons im Gewinnmenü
        /// </summary>
        public string Margin
        {
            get { return margin; }
            set
            {
                margin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Margin)));
            }
        }

        /// <summary>
        /// Farbe der Gewinnstufen im Gewinnmenü
        /// </summary>
        public string[] StufenFarben
        {
            get { return stufenFarben; }
            set
            {
                stufenFarben = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StufenFarben)));
            }
        }

        /// <summary>
        /// Text der Frage in Benutzeroberfläche
        /// </summary>
        public string FragenText
        {
            get { return fragenText; }
            set
            {
                fragenText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FragenText)));
            }
        }

        /// <summary>
        /// Text der ersten Antwort in Benutzeroberfläche
        /// </summary>
        public string Antwort1Text
        {
            get { return antwort1Text; }
            set
            {
                antwort1Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Antwort1Text)));
            }
        }

        /// <summary>
        /// Text der zweiten Antwort in Benutzeroberfläche
        /// </summary>
        public string Antwort2Text
        {
            get { return antwort2Text; }
            set
            {
                antwort2Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Antwort2Text)));
            }
        }

        /// <summary>
        /// Text der dritten Antwort in Benutzeroberfläche
        /// </summary>
        public string Antwort3Text
        {
            get { return antwort3Text; }
            set
            {
                antwort3Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Antwort3Text)));
            }
        }

        /// <summary>
        /// Text der vierten Antwort in Benutzeroberfläche
        /// </summary>
        public string Antwort4Text
        {
            get { return antwort4Text; }
            set
            {
                antwort4Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Antwort4Text)));
            }
        }

        /// <summary>
        /// Farbe der ersten Antwort in Benutzeroberfläche
        /// </summary>
        public string Color_a
        {
            get { return color_a; }
            set
            {
                color_a = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_a)));
            }
        }

        /// <summary>
        /// Farbe der zweiten Antwort in Benutzeroberfläche
        /// </summary>
        public string Color_b
        {
            get { return color_b; }
            set
            {
                color_b = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_b)));
            }
        }

        /// <summary>
        /// Farbe der dritten Antwort in Benutzeroberfläche
        /// </summary>
        public string Color_c
        {
            get { return color_c; }
            set
            {
                color_c = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_c)));
            }
        }

        /// <summary>
        /// Farbe der vierten Antwort in Benutzeroberfläche
        /// </summary>
        public string Color_d
        {
            get { return color_d; }
            set
            {
                color_d = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_d)));
            }
        }
        /// <summary>
        /// Initiieren des Programs
        /// </summary>
        /// <param name="window"></param>
        public ViewModel(Window window)
        {
            this.window = window;
            click_answer = new RelayCommand<string>(CheckAnswer);
            click_joker = new RelayCommand<string>(Joker_click);
            spiel_starten = new RelayCommand<string>(SpielStarten);
            spiel_beenden = new RelayCommand<string>(SpielBeenden);
            dt.Interval = TimeSpan.FromSeconds(2);
            dt.Tick += ResetColor;
            blinkTimer.Interval = TimeSpan.FromMilliseconds(100);
            blinkTimer.Tick += BlinkAnswer;
            menuTimer.Interval = TimeSpan.FromSeconds(5);
            menuTimer.Tick += WrongAnswer;
            delay.Interval = TimeSpan.FromMilliseconds(50);
            delay.Tick += updateEigenschaften;
            update();
        }

        /// <summary>
        /// Setzt alle Variablen für den Spielbeginn
        /// </summary>
        /// <param name="s"></param>
        public void SpielStarten(string s)
        {
            quiz.usedQuestions = new List<Questions>();
            disabled = false;
            GameVisibility = "Visible";
            MessageBoxVisibility = "Hidden";
            Color_a = defaultColor;
            Color_b = defaultColor;
            Color_c = defaultColor;
            Color_d = defaultColor;
            SetStufe(0);
            runde = 0;
            currBlinkTick = 0;
            jokers = new bool[] { true, true, true };
            alteJokerCursor = new string[] { "Hand", "Hand", "Hand" };
            StufenFarben = new string[] { "black", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white" };
            JokersCursor = new string[] { "Hand", "Hand", "Hand" };
            Cursor = new string[] { "Hand", "Hand", "Hand", "Hand" };
            JokersUsed = new string[] { "Hidden", "Hidden", "Hidden" };
            loadText();
            update();
        }

        /// <summary>
        /// Schließt das Fenster
        /// </summary>
        /// <param name="s"></param>
        public void SpielBeenden(string s)
        {
            window.Close();
        }

        /// <summary>
        /// Behebt Oberflächen update Probleme
        /// </summary>
        public void update()
        {
            delay.Start();
        }

        /// <summary>
        /// Behebt Oberflächen update Probleme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void updateEigenschaften(object? sender, EventArgs e)
        {
            delay.Stop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JokersCursor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cursor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_a)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_b)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_c)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color_d)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JokersUsed)));
        }

        /// <summary>
        /// Lässt vom Benutzer geklickte Antwort blinken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlinkAnswer(object? sender, EventArgs e)
        {
            if (currBlinkTick == blinkTicks - 1)
            {
                blinkTimer.Interval = TimeSpan.FromSeconds(1);
            }
            if (currBlinkTick % 2 == 0)
            {
                switch (userAnswer)
                {
                    case "a":
                        Color_a = blinkColor;
                        break;
                    case "b":
                        Color_b = blinkColor;
                        break;
                    case "c":
                        Color_c = blinkColor;
                        break;
                    case "d":
                        Color_d = blinkColor;
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
            if (currBlinkTick >= blinkTicks)
            {
                blinkTimer.Stop();
                CheckUserAnswer();
                blinkTimer.Interval = TimeSpan.FromMilliseconds(100);
            }
            currBlinkTick++;
        }

        /// <summary>
        /// Öffnet das Menü bei falscher Antwort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WrongAnswer(object? sender, EventArgs e)
        {
            menuTimer.Stop();
            FragenText = "";
            GameVisibility = "Hidden";
            BoxMessages = new string[] { "Sie haben verloren", $"Sie haben {sicherheitsStufen[Math.Max(0,runde-1)/5]}€ gewonnen", "Nochmal spielen", "Beenden" };
            JokersUsed = new string[] { "Hidden", "Hidden", "Hidden" };
            MessageBoxVisibility = "Visible";
        }

        /// <summary>
        /// Setzt Antwort Farben nach richtiger Antwort zurück
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetColor(object? sender, EventArgs e)
        {
            dt.Stop();
            Color_a = defaultColor;
            Color_b = defaultColor;
            Color_c = defaultColor;
            Color_d = defaultColor;
            loadText();
            disabled = false;
            currBlinkTick = 0;
            update();
        }

        /// <summary>
        /// Wird nach klicken auf Antwort ausgeführt und verhindert weitere Klicks
        /// </summary>
        /// <param name="answer"></param>
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
            JokersCursor = new string[] { "Arrow", "Arrow", "Arrow" };
            userAnswer = answer;
            blinkTimer.Start();
        }

        /// <summary>
        /// Checkt ob die geklickte Antwort richtig ist
        /// </summary>
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
                if (runde >= 14)
                {
                    MessageBoxVisibility = "Visible";
                    BoxMessages = new string[]{ "Wer Wird Millionär", "Herzlichen Glückwunsch, Sie haben die Million geknackt!", "Nochmal spielen", "Beenden" };
                    GameVisibility = "Hidden";
                    FragenText = "";
                    JokersUsed = new string[] { "Hidden", "Hidden", "Hidden" };
                }
                else
                {
                    runde++;
                    FragenText = $"Sehr gut, als nächstes spielen Sie um {gewinnstufen[runde]}€";
                    SetStufe(runde);
                    dt.Start();
                    update();
                }
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

        /// <summary>
        /// Setzt Margin für Anzeige der Gewinnstufe im Gewinnmenü
        /// </summary>
        /// <param name="i"></param>
        public void SetStufe(int i)
        {
            Margin = $"112 {551 - i*32} 0 0";
            string[] neu = new string[] { "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white", "white" };
            neu[i]="black";
            StufenFarben = neu;
            update();
        }

        /// <summary>
        /// Wird beim klicken von Jokern ausgeführt
        /// </summary>
        /// <param name="s"></param>
        public void Joker_click(string s)
        {
            if (disabled) return;
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

        /// <summary>
        /// Überspringt eine Frage einmalig (JOKER)
        /// </summary>
        public void SkipJoker()
        {
            loadText();
            update();
        }
        
        /// <summary>
        /// Entfernt zwei Antwortmöglichkeiten einmalig (JOKER)
        /// </summary>
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

        /// <summary>
        /// Lädt die nächste Frage und Antwortmöglichkeiten aus dem Quiz Objekt
        /// </summary>
        public void loadText()
        {
            JokersCursor = alteJokerCursor;
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
            //FragenText = quiz.usedQuestions.Count+": "+q.Prompt;
            string kategorie;
            switch (runde / 5)
            {
                case 0:
                    kategorie = "Easy";
                    break;
                case 1:
                    kategorie = "Medium";
                    break;
                case 2:
                    kategorie = "Hard";
                    break;
                case 3:
                    kategorie = "Impossible";
                    break;
                default:
                    kategorie = "Undefined";
                    break;
            }
            if (runde == 14) kategorie = "Impossible";
            // FragenText = $"UsedQuestion Count: {quiz.usedQuestions.Count}, Runde:{runde}, Kategorie: {kategorie}, Frage: {q.Prompt}";
            FragenText = $"{q.Prompt}";
            rightAnswer = q.Options[0];
            List<string> strings = new List<string>();
            List<string> optionsCopy = q.Options.ToArray().ToList();
            while (strings.Count < 4)
            {
                string antwort = optionsCopy[rnd.Next(optionsCopy.Count)];
                strings.Add(antwort);
                optionsCopy.Remove(antwort);
            }
            Antwort1Text = strings[0];
            Antwort2Text = strings[1];
            Antwort3Text = strings[2];
            Antwort4Text = strings[3];
        }

        /// <summary>
        /// Wird für das Updaten der Benutzeroberfläche benötigt
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


    }
}
