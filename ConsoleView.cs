using System;

namespace Logic
{
    public class ConsoleView
    {
        #region [ Readonly fields ]
        private readonly static int minDigit = 1;
        private readonly static int maxDigit = 8;
        private readonly static string rulesMessage =
            "Игра \"Логика\"." + Environment.NewLine + Environment.NewLine +
            $"В этой игре нужно отгадать шифр из 5 цифр, каждая из которых имеет значение от {minDigit} до {maxDigit}. Цифры могут повторяться." + Environment.NewLine +
            "Всего 5 попыток. После каждой попытки справа от введённой комбинации отображается результат соответствия:" + Environment.NewLine +
            "при угадывании каждой цифры и её положения в результате отображается \"+\"," + Environment.NewLine +
            "при угадывании каждой цифры, но не её положения - отображается \"?\"." + Environment.NewLine +
            "Например: загадано \"12345\", результатом ввода шифра \"12354\" будет \"+++??\", а шифра \"34543\" - \"???\".";

        private readonly static string[] toHideRulesValidValues = new string[] { "С", "с", "H", "h", "C", "c" };
        private readonly static string toHideRulesMessage =
            $"Для сокрытия правил введите \"{toHideRulesValidValues[0]}\"/\"{toHideRulesValidValues[2]}\" [рус./англ.].";

        private readonly static string[] toShowRulesValidValues = new string[] { "П", "п", "R", "r" };
        private readonly static string toShowRulesMessage =
            $"Для отображения правил введите \"{toShowRulesValidValues[0]}\"/\"{toShowRulesValidValues[2]}\" [рус./англ.]";

        private readonly static string[] toExitValidValues = new string[] { "В", "в", "E", "e" };
        private readonly static string toExitMessage =
            $"Для выхода введите \"{toExitValidValues[0]}\"/\"{toExitValidValues[2]}\" [рус./англ.] или закройте окно.";

        private readonly static string enterCodeMessage =
            "Введите комбинацию: ";
        #endregion

        private static Model model = new Model();
        private static int[] inputCodeDigits = new int[5];
        private static string[] playArea = new string[5] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
        private static bool isRulesVisible = true;
        private static bool isExit = false;

        static void Main(string[] args)
        {
            while (!isExit)
            {
                WriteText();
                ReadText();
            }
        }

        private static void WriteText()
        {
            ClearScreen();

            if (isRulesVisible)
            {
                WriteMessage(true, rulesMessage);
                WriteMessage(false, toHideRulesMessage);
                WriteMessage(true, toExitMessage);
                WriteMessage(true, playArea);
                WriteMessage(false, enterCodeMessage);
            }
            else
            {
                WriteMessage(false, toShowRulesMessage);
                WriteMessage(true, toExitMessage);
                WriteMessage(true, playArea);
                WriteMessage(false, enterCodeMessage);
            }

            void WriteMessage(bool addLine, params string[] message)
            {
                foreach (var part in message)
                    Console.WriteLine(part);

                if (addLine)
                    Console.WriteLine(Environment.NewLine);
            }

            void ClearScreen() => Console.Clear();
        }

        private static void ReadText()
        {
            string value = Console.ReadLine();

            if (value.Length == 1)
            {
                if (!IsHideRules() && !IsShowRules())
                    CheckForExit();
            }
            else if (value.Length == 5 && IsCode())
            {
                EnterCode(inputCodeDigits);
            }

            bool IsHideRules()
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

            bool IsShowRules()
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
                    for (int i = 4; i >= 0; i--)
                    {
                        inputCodeDigits[i] = parsedCode % 10;

                        if (inputCodeDigits[i] < minDigit || inputCodeDigits[i] > maxDigit)
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

        private static void EnterCode(int[] code)
        {
            // Передать цифры в модель
        }
    }
}
