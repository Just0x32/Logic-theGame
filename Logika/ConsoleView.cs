using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Logika
{
    public class ConsoleView
    {
        #region [ From Model ]
        private static Model model = new Model();
        private static int MinDigit { get => Model.MinDigit; }
        private static int MaxDigit { get => Model.MaxDigit; }
        private static int CodeLength { get => Model.CodeLength; }
        private static int MaxAttempts { get => Model.MaxAttempts; }
        private static int AttemptsLeft { get => model.AttemptsLeft; }
        private static bool IsWin { get => model.IsWin; }
        #endregion

        #region [ Readonly text ]
        private readonly static string valueAndPositionMathesResult = "+";
        private readonly static string onlyValueMathesResult = "?";

        private readonly static string rulesMessage =
            "Игра \"Логика\"." + Environment.NewLine + Environment.NewLine +
            $"В этой игре нужно отгадать шифр из {CodeLength} цифр, каждая из которых имеет значение от {MinDigit} до {MaxDigit}. Цифры могут повторяться." + Environment.NewLine +
            $"Всего {MaxAttempts} попыток. После каждой попытки справа от введённой комбинации отображается результат соответствия:" + Environment.NewLine +
            "при угадывании каждой цифры и её положения в результате отображается \"+\"," + Environment.NewLine +
            "при угадывании каждой цифры, но не её положения - отображается \"?\"." + Environment.NewLine +
            $"Пример для шифра из {CodeLength} цифр: загадано \"12345\", результатом ввода шифра \"12354\" будет " +
            $"\"{valueAndPositionMathesResult + valueAndPositionMathesResult + valueAndPositionMathesResult + onlyValueMathesResult + onlyValueMathesResult}\"," +
            $" а шифра \"34543\" - \"{onlyValueMathesResult + onlyValueMathesResult + onlyValueMathesResult}\".";

        private readonly static string[] toHideRulesValidValues = new string[] { "С", "с", "H", "h", "C", "c" };
        private readonly static string toHideRulesMessage =
            $"Для скрытия правил введите \"{toHideRulesValidValues[0]}\"/\"{toHideRulesValidValues[2]}\" [рус./англ.].";

        private readonly static string[] toShowRulesValidValues = new string[] { "П", "п", "R", "r" };
        private readonly static string toShowRulesMessage =
            $"Для отображения правил введите \"{toShowRulesValidValues[0]}\"/\"{toShowRulesValidValues[2]}\" [рус./англ.]";

        private readonly static string[] toExitValidValues = new string[] { "В", "в", "E", "e" };
        private readonly static string toExitMessage =
            $"Для выхода введите \"{toExitValidValues[0]}\"/\"{toExitValidValues[2]}\" [рус./англ.] или закройте окно.";

        private readonly static string enterCodeMessage =
            "Введите комбинацию: ";

        private readonly static string[] lossWinMessages = new string[2] { "Шифр не разгадан... :(", "Шифр разгадан! :D" };

        private readonly static string toRestartMessage =
            $"Для игры сначала нажмите \"Enter\".";
        #endregion

        private static int[] guessCode = new int[CodeLength];
        private static string[] playAreaCodeAndResult;
        private readonly static string playAreaHeader;
        private readonly static string playAreaBorder;
        private static bool isRulesVisible = true;
        private static bool isExit = false;

        static ConsoleView()
        {
            playAreaHeader = GetPlayAreaHeader();
            playAreaBorder = GetPlayAreaBorder('-');

            string GetPlayAreaHeader()
            {
                StringBuilder sb = new StringBuilder();
                int spaceLength = CodeLength * 2 - 2;

                sb.Append("Шифр");

                for (int i = 0; i < spaceLength; i++)
                    sb.Append(" ");

                sb.Append("Результат");

                return sb.ToString();
            }
            string GetPlayAreaBorder(char separator)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < CodeLength; i++)
                    sb.Append(separator + " ");

                sb.Append(" ");

                for (int i = 0; i < CodeLength; i++)
                    sb.Append(" " + separator);

                return sb.ToString();
            }
        }

        static void Main(params string[] args)
        {
            CheckForRulesHiding();

            while (!isExit)
            {
                StartNewGame();

                while (!isExit && !IsWin && AttemptsLeft > 0)
                {
                    WriteText();
                    ReadText();
                }

                if (!isExit)
                {
                    WriteText();
                    ReadText();
                }
            }

            void CheckForRulesHiding()
            {
                foreach (var param in args)
                {
                    if (IsHidenRules(param))
                        break;
                }
            }
        }

        private static void StartNewGame()
        {
            model.StartGame();
            playAreaCodeAndResult = new string[CodeLength];
        }

        private static void WriteText()
        {
            Console.Clear();

            if (isRulesVisible)
            {
                WriteMessage(rulesMessage, 1);
                WriteMessage(toHideRulesMessage);
                WriteMessage(toExitMessage);
                WriteMessage(playAreaHeader, 0, 1);
                WriteMessage(playAreaBorder);
                WriteCompositeMessage(playAreaCodeAndResult);
                WriteMessage(playAreaBorder, 1);
            }
            else
            {
                WriteMessage(toShowRulesMessage);
                WriteMessage(toExitMessage);
                WriteMessage(playAreaHeader, 0, 1);
                WriteMessage(playAreaBorder);
                WriteCompositeMessage(playAreaCodeAndResult);
                WriteMessage(playAreaBorder, 1);
            }

            if (AttemptsLeft > 0 && !IsWin)
            {
                WriteMessage(enterCodeMessage, -1);
            }
            else
            {
                WriteMessage(lossWinMessages[Convert.ToInt32(IsWin)], 1);
                WriteMessage(toRestartMessage);
            }

            void WriteMessage(string message, int emptyLinesAfter = 0, int emptyLinesBefore = 0)
            {
                for (int i = 0; i < emptyLinesBefore; i++)
                    Console.Write(Environment.NewLine);

                Console.Write(message);

                for (int i = 0; i <= emptyLinesAfter; i++)
                    Console.Write(Environment.NewLine);
            }

            void WriteCompositeMessage(string[] message, int emptyLinesAfter = 0, int emptyLinesBefore = 0)
            {
                for (int i = 0; i < emptyLinesBefore; i++)
                    Console.Write(Environment.NewLine);

                for (int i = 0; i < message.Length - 1; i++)
                {
                    WriteMessage(message[i]);
                }

                WriteMessage(message[^1], emptyLinesAfter);
            }
        }

        private static void ReadText()
        {
            string value = Console.ReadLine();

            if (AttemptsLeft > 0 && !IsWin)
            {
                if (value.Length == 1)
                {
                    if (!IsHidenRules(value) && !IsShowedRules())
                        CheckForExit();
                }
                else if (value.Length == CodeLength && IsCode())
                {
                    CompareCode(guessCode);
                }
            }

            bool IsShowedRules()
            {
                foreach (var validValue in toShowRulesValidValues)
                {
                    if (value == validValue)
                    {
                        isRulesVisible = true;
                        return true;
                    }
                }

                return false;
            }

            void CheckForExit()
            {
                foreach (var validValue in toExitValidValues)
                {
                    if (value == validValue)
                        isExit = true;
                }
            }

            bool IsCode()
            {
                int parsedCode;

                if (int.TryParse(value, out parsedCode))
                {
                    for (int i = CodeLength - 1; i >= 0; i--)
                    {
                        guessCode[i] = parsedCode % 10;

                        if (guessCode[i] < MinDigit || guessCode[i] > MaxDigit)
                            return false;

                        parsedCode = parsedCode / 10;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static bool IsHidenRules(string value)
        {
            foreach (var validValue in toHideRulesValidValues)
            {
                if (value == validValue)
                {
                    isRulesVisible = false;
                    return true;
                }
            }

            return false;
        }

        private static void CompareCode(int[] code)
        {
            List<Model.Matches> matches = model.GetComparisonResults(code);
            playAreaCodeAndResult[MaxAttempts - AttemptsLeft - 1] = GetGuessCodeAndResult();

            string GetGuessCodeAndResult()
            {
                StringBuilder sb = new StringBuilder();

                foreach (var digit in code)
                    sb.Append(digit + " ");

                sb.Append(" ");

                foreach (var match in matches)
                {
                    if (match == Model.Matches.AtRightPlace)
                        sb.Append(" " + valueAndPositionMathesResult);
                    else if (match == Model.Matches.AtWrongPlace)
                        sb.Append(" " + onlyValueMathesResult);
                }

                return sb.ToString();
            }
        }
    }
}
