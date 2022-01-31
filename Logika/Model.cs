using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public class Model
    {
        public readonly static int MinDigit = 1;
        public readonly static int MaxDigit = 8;
        public readonly static int CodeLength = 5;
        private int[] secretCode = new int[CodeLength];

        public readonly static int MaxAttempts = 5;
        public int AttemptsLeft { get; private set; }
        public bool IsWin { get; private set; }

        public void StartGame()
        {
            GenerateNewCode();
            IsWin = false;
            AttemptsLeft = MaxAttempts;
        }

        private void GenerateNewCode()
        {
            Random random = new Random();

            for (int i = 0; i < secretCode.Length; i++)
                secretCode[i] = random.Next(MinDigit, MaxDigit + 1);
        }

        public List<Matches> GetComparisonResults(int[] guessedCode)
        {
            List<Matches> result = new List<Matches>();
            bool[] isMatchedGuessedDigit = new bool[CodeLength];
            bool[] isMatchedSecretDigit = new bool[CodeLength];

            CheckForValueAndPositionMatches();

            this.IsWin = IsWin();

            if (!this.IsWin)
            {
                CheckForOnlyValueMatches();

                if (result.Count == 0)
                    result.Add(Matches.None);

                AttemptsLeft--;
            }
            
            return result;

            void CheckForValueAndPositionMatches()
            {
                for (int i = 0; i < CodeLength; i++)
                {
                    if (!isMatchedGuessedDigit[i] && guessedCode[i] == secretCode[i])
                    {
                        result.Add(Matches.AtRightPlace);
                        isMatchedGuessedDigit[i] = true;
                        isMatchedSecretDigit[i] = true;
                    }
                }
            }

            bool IsWin()
            {
                if (result.Count == 5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            void CheckForOnlyValueMatches()
            {
                for (int i = 0; i < CodeLength; i++)
                {
                    if (!isMatchedGuessedDigit[i])
                    {
                        for (int j = 0; j < CodeLength; j++)
                        {
                            if (!isMatchedSecretDigit[j] && guessedCode[i] == secretCode[j])
                            {
                                result.Add(Matches.AtWrongPlace);
                                isMatchedGuessedDigit[i] = true;
                                isMatchedSecretDigit[j] = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public enum Matches
        {
            None,
            AtWrongPlace,
            AtRightPlace
        }
    }
}
