using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace wer_wird_millionaer
{
    public class Quiz
    {
        private List<Questions> Easy_Questions { get; set; }
        private List<Questions> Medium_Questions { get; set; }
        private List<Questions> Hard_Questions { get; set; }
        private List<Questions> Killer_Questions { get; set; }
        //internal List<Questions> Questions { get => Questions; set => Questions = value; }
        private Random rnd = new Random();
        private List<Questions> usedQuestions = new List<Questions>();
        public Questions getQuestionOfCategory(int i)
        {
            Questions q = null;
            switch (i)
            {
                case 0:
                    do
                    {
                        q = Easy_Questions[rnd.Next(Easy_Questions.Count)];
                    } while (usedQuestions.Contains(q));
                    break;
                case 1:
                    do
                    {
                        q = Medium_Questions[rnd.Next(Medium_Questions.Count)];
                    } while (usedQuestions.Contains(q));
                    break;
                case 2:
                    do
                    {
                        q = Hard_Questions[rnd.Next(Hard_Questions.Count)];
                    } while (usedQuestions.Contains(q));
                    break;
                case 3:
                    do
                    {
                        q = Killer_Questions[rnd.Next(Killer_Questions.Count)];
                    } while (usedQuestions.Contains(q));
                    break;
                default:
                    return null;
            }
            usedQuestions.Add(q);
            return q;
        }

        public Quiz()
        {
            Easy_Questions = new List<Questions>();
            // Add Eazy questions
            AddEasyQuestions();

            Medium_Questions = new List<Questions>();
            // Add Medium questions
            AddMediumQuestions();

            Hard_Questions = new List<Questions>();
            // Add Hard questions
            AddHardQuestions();

            Killer_Questions = new List<Questions>();
            // Add Killer questions
            AddKillerQuestions();
        }

        private void AddEasyQuestions()
        {
            Easy_Questions.Add(new Questions("Der erste Mensch auf dem Mond war?", new List<string> { "Lance Armstrong", "Neil Armstrong", "Bruce Beinsoft", "Arnold Beinsoft" }));
            Easy_Questions.Add(new Questions("Nach dem Kinderlied \"Backe backe Kuchen\" kommt welche Zutat nicht in einen Kuchen?", new List<string> { "Nüsse", "Eier", "Salz", "Butter" }));
            Easy_Questions.Add(new Questions("Vorsorge ist besser als...", new List<string> { "Nachsorge", "Vorglück", "Hinterangst", "Oberglück" }));
            Easy_Questions.Add(new Questions("Rhythmus hat man im?", new List<string> { "Blut", "Leber", "Milz", "Urin" }));
            Easy_Questions.Add(new Questions("Ein Junge, der zu allerlei Streichen aufgelegt ist, ist ein...", new List<string> { "Lausbub", "Mausbängel", "Flohjunge", "Zeckentyp" }));
            Easy_Questions.Add(new Questions("Wer wenig Geld hat zählt als?", new List<string> { "Arm", "Bein", "Hand", "Kopf" }));
            Easy_Questions.Add(new Questions("die Redewendung Jacke wie", new List<string> { "Hose", "Bluse", "Rock", "T-Shirt" }));
            Easy_Questions.Add(new Questions("Wer darf etwas, was die meisten anderen nicht dürfen?", new List<string> { "Befugte", "Berillte", "Belückte", "Bespaltete" }));
            Easy_Questions.Add(new Questions("Man könnte auch „Hälfte einer Hälfte“ oder „zwei Achtel“ sagen...?", new List<string> { "statt „Viertel“", "won „Gebiet“", "rant „Bezirk“", "ords „Teil“" }));
            Easy_Questions.Add(new Questions("Welche Endung wird bei Abkürzungen wie „staatl.“ oder „dienstl.“ weggelassen ?", new List<string> { "ich", "weiß", "es", "nicht" }));
            Easy_Questions.Add(new Questions("1 / 4 ist...?", new List<string> { "ein Bruch", "bank Raub", "dieb Stahl", "scheck Betrug" }));
            Easy_Questions.Add(new Questions("Wenn man eins von zwei Löchern im Reifen flickt, dann wird er ...?", new List<string> { "dichter", "maler", "bildhauer", "sänger" }));
            Easy_Questions.Add(new Questions("Wobei handelt es sich um ein beliebtes Getränk an kalten Tagen?", new List<string> { "heiße Zitrone", "call me strawberry", "nennt mich Kirsche", "bin die Banane" }));
            Easy_Questions.Add(new Questions("Was kommt in Ostasien häufig auf den Tisch ?", new List<string> { "Soja", "Sonicht", "Soschoneher", "Sovielleicht" }));
            Easy_Questions.Add(new Questions("Scotty, der Ingenieur des Raumschiffs Enterprise, war bei den \"Ausflügen\" meist nicht dabei, weil er ...?", new List<string> { "beamte", "angestellte", "selbstständige", "vorgesetzte" }));
        }

        private void AddMediumQuestions()
        {
            Medium_Questions.Add(new Questions("Der Bürgermeister welcher Stadt rief einen Wettbewerb über 1Mio. Euro aus um zu beweisen, dass es seine Stadt gibt:", new List<string> { "Bielefeld", "Hamburg", "Rostock", "München" }));
            Medium_Questions.Add(new Questions("Der Mensch besitzt 7 Halswirbel, wie viele Halswirbel besitzt die Giraffe?", new List<string> { "7", "4", "12", "19" }));
            Medium_Questions.Add(new Questions("Hippopotomonstrosesquippedaliophobie beschreibt die Angst vor", new List<string> { "langen Worten", "Reptilien", "Verdursten", "kleinen Räumen" }));
            Medium_Questions.Add(new Questions("1835 fuhr der erste Deutsche Zug mit dem Namen ______ von Nürnberg nach Fürth", new List<string> { "Adler", "Falke", "Milan", "Bussard" }));
            Medium_Questions.Add(new Questions("Mithilfe der Fibonacci-Folge kann das Aussehen von was bestimmt werden", new List<string> { "Blüten", "Stämmen", "Stielen", "Wurzeln" }));
            Medium_Questions.Add(new Questions("Die Erstellung einer Projektarbeit ist eine ", new List<string> { "Sisyphusarbeit", "Sisiphusarbeit", "Sissifusarbeit", "Sissyfhusarbeit" }));
            Medium_Questions.Add(new Questions("Bei Weitsichtigkeit hilft welche Linse", new List<string> { "Konvexe", "Konträre", "Konkave", "Konfluxe" }));
            Medium_Questions.Add(new Questions("Die Banane, die man bei uns als Obst in jedem Supermarkt kaufen kann, heißt mit \"vollem Namen\"...?", new List<string> { "Dessertbanane", "Aperitifbanane", "Vorspeisenbanane", "Hauptgangbanane" }));
            Medium_Questions.Add(new Questions("Wovon können auch wahrheitsliebende Menschen betroffen sein?", new List<string> { "Drehschwindel", "Kreisellüge", "Schraubenschummel", "Rotierflunker" }));
            Medium_Questions.Add(new Questions("Was wird im größten Hit eines Trios aus Großenkneten ganze 83 Mal wiederholt?", new List<string> { "Da", "Hier", "Dort", "Jetzt" }));
            Medium_Questions.Add(new Questions("Was absolvierten 1820 in Preußen 590, dagegen 2021 in Deutschland über 300.000 Menschen?", new List<string> { "Abitur", "Führerscheinprüfung", "IQ-Test", "DSDS-Recall" }));
            Medium_Questions.Add(new Questions("Bei der Fußball-Weltmeisterschaft in Katar fiel was - wie im Vorfeld angekündigt - ungewöhnlich aus?", new List<string> { "lange Nachspielzeiten", "gemähte Rasenflächen", "harte Bälle", "hässliche Trikots" }));
        }

        private void AddHardQuestions()
        {
            Hard_Questions.Add(new Questions("Die Fäkalien welches Tieres kommen dem geometrieschen Objekt eines Würfels am nähesten?", new List<string> { "Wombat", "Palmendieb", "Faultier", "Koala" }));
            Hard_Questions.Add(new Questions("Mit sinkendem Luftdruck sinkt der Siedepunkt von Flüssigkeiten. Bei welcher Temperatur kocht Wasser auf dem Mount Everest", new List<string> { "72", "93", "100", "107" }));
            Hard_Questions.Add(new Questions("Für kleineräumige Karten wird folgendes Koordinatensystem verwendet", new List<string> { "transversale Mercator-Projektion", "universale Steleon-Projektion", "transversale interdirekttionelle-Projektion", "diagonal-laterale-Projektion" }));
            Hard_Questions.Add(new Questions("Der längste Fluss in Baden-Württemberg ist", new List<string> { "der Rhein", "die Donau", "der Neckar", "die Jagst" }));
            Hard_Questions.Add(new Questions("Was macht sich gut in der Blumenvase?", new List<string> { "Spraynelken", "Geltulpen", "Pastenrosen", "Pulverlilien" }));
            Hard_Questions.Add(new Questions("Vom wem erhielt Deutschland beim Eurovision Song Contest seit 1957 bislang insgesamt die meisten Punkte?", new List<string> { "Spanien", "Schweiz", "Türkei", "Dänemark" }));
            Hard_Questions.Add(new Questions("Welches Land gehört nicht zu den ständigen Mitgliedern des UN-Sicherheitsrates?", new List<string> { "Deutschland", "Frankreich", "USA", "Großbrittanien" }));
            Hard_Questions.Add(new Questions("Wer tötet weder Lamm noch Zicklein, denn Eicheln und Beeren genügen ihm zum Essen?", new List<string> { "Frankensteins Monster", "Erlkönig", "Glöckner von Notre-Dame", "Mr. Hyde" }));
            Hard_Questions.Add(new Questions("Die schwarzfelligen Vertreter welcher Tiere werden umgangssprachlich gleichermaßen als schwarze Panther bezeichnet?", new List<string> { "Leopard & Jaguar", "Gepard & Puma", "Puma & Leopard", "Jaguar & Gepard" }));
        }

        private void AddKillerQuestions()
        {
            Killer_Questions.Add(new Questions("Der Mpemba-Effekt beschreibt", new List<string> { "warmes Wasser gefriert schneller als kaltes", "im Vaccum sinkt die Temperatur konstant zur Dichte", "der Elektronenfluss ist proportional abhängig von der Temperatur", "die Lichtbrechung in argongesättigten Räumen" }));
            Killer_Questions.Add(new Questions("Welcher Fisch verbindet sich nach dem Geschlechtsakt lebenslang untrennbar mit dem Weibchen", new List<string> { "Anglerfisch", "Scheibenbauch", "Vampirtintenfisch", "Blobfisch" }));
            Killer_Questions.Add(new Questions("Welcher Künstler trat 1985 bei den Live-Aid Konzerten nicht auf", new List<string> { "Bruce Springsteen", "Bryan Adams", "Bryan Ferry", "Bob Dylan" }));
            Killer_Questions.Add(new Questions("Die klassische, genormte Euro-Palette EPAL 1 besteht aus 78 Nägeln, neun Klötzen und insgesamt wie vielen Brettern?", new List<string> { "11", "10", "9", "12" }));
            Killer_Questions.Add(new Questions("Wer bekam 1954 den Chemie- und 1962 den Friedensnobelpreis?", new List<string> { "Linus Pauling", "Otto Hahn", "Pearl S. Buck", "Albert Schweitzer" }));
            Killer_Questions.Add(new Questions("Welches chemische Element macht mehr als die Hälfte der Masse eines menschlichen Körpers aus?", new List<string> { "Sauerstoff", "Kohlenstoff", "Kalzium", "Eisen" }));
            Killer_Questions.Add(new Questions("Wie hieß die erste deutsche Briefmarke, die 1849 in Bayern herausgegeben wurde?", new List<string> { "Schwarzer Einser", "Roter Zweier", "Gelber Dreier", "Blauer Vierer" }));
            Killer_Questions.Add(new Questions("Das Nagel-Schreckenberg-Modell liefert eine Erklärung für die Entstehung von...?", new List<string> { "Verkehrsstaus", "Sandwüsten", "Grippewellen", "Börsencrashs" }));
            Killer_Questions.Add(new Questions("Wer sollte sich mit der „20 nach vier“-Stellung auskennen?", new List<string> { "Kellner", "Fahrlehrer", "Karatemeister", "Landschaftsarchitekt" }));
            Killer_Questions.Add(new Questions("Die Entfernung von der Hauptstadt Berlin zum Erdmittelpunkt ist ungefähr so groß wie zwischen Berlin und ...?", new List<string> { "New York", "Tokio", "Kapstadt", "Moskau" }));
            Killer_Questions.Add(new Questions("Aus insgesamt wie vielen Steinchen besteht der klassische von Ernő Rubik erfundene Zauberwürfel?", new List<string> { "26", "22", "24", "28" }));
            Killer_Questions.Add(new Questions("Welches dieser Grimm'schen Märchen beginnt NICHT, wirklich NICHT mit „Es war einmal ...“?", new List<string> { "Rumpelstilzchen", "Hans im Glück", "Die Sterntaler", "Rotkäppchen" }));
        }
    }
    }