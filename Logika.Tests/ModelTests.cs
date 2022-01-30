using NUnit.Framework;
using ReflectionExtension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logika.Tests
{
    public class ModelTests
    {
        Model model = new Model();
        private static CodesAndResults[] codesAndResults;

        static ModelTests()
        {
            codesAndResults = new CodesAndResults[]
            {
                new CodesAndResults(new int[] { 1, 2, 3, 4, 5 }),
                new CodesAndResults(new int[] { 5, 4, 3, 2, 1 }),
                new CodesAndResults(new int[] { 1, 2, 3, 2, 1 }),
                new CodesAndResults(new int[] { 3, 2, 1, 2, 3 }),
            };
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace5Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);
            List<Model.Matches> result = model.GetComparisonResults(source.AtRightPlace5DigitsCode);

            bool areEqual = AreEqualMatchesLists(CodesAndResults.AtRightPlace5DigitsResult, result);
            Assert.IsTrue(areEqual);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace4Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace4DigitsCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace4DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace3Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace3DigitsCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace3DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace2Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace2DigitsCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace2DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace1Digit_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace1DigitCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace1DigitResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        private delegate List<Model.Matches> GetMatches(int[] code);

        private bool AreEqualSubtests(int[][] guessedCodes, GetMatches getMatchesMethod, List<Model.Matches> expectedResult, out string failedMessage)
        {
            int testQuantities = guessedCodes.Length;
            bool toReturn = true;
            StringBuilder failedMessageBuilder = new StringBuilder();

            for (int i = 0; i < testQuantities; i++)
            {
                List<Model.Matches> gottenResult = getMatchesMethod(guessedCodes[i]);
                bool areEqual = AreEqualMatchesLists(expectedResult, gottenResult);

                if (failedMessageBuilder.Length > 0)
                    failedMessageBuilder.Append(", ");

                failedMessageBuilder.Append(areEqual);

                if (!areEqual)
                    toReturn = false;
            }

            failedMessage = "Subtest results: " + failedMessageBuilder.ToString();
            return toReturn;
        }

        private bool AreEqualMatchesLists(List<Model.Matches> source, List<Model.Matches> result)
        {
            if (result.Count != source.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < source.Count; i++)
                {
                    if (result[i] != source[i])
                        return false;
                }
                return true;
            }
        }

        public class CodesAndResults
        {
            private static readonly int codeLength;
            private static readonly int minDigit;
            private static readonly int maxDigit;

            #region [ Codes ]
            public int[] SecretCode { get; private set; }
            public int[] AtRightPlace5DigitsCode { get => SecretCode; }
            public int[][] AtRightPlace4DigitsCodes { get; private set; }
            public int[][] AtRightPlace3DigitsCodes { get; private set; }
            public int[][] AtRightPlace2DigitsCodes { get; private set; }
            public int[][] AtRightPlace1DigitCodes { get; private set; }
            public int[][] AtWrongPlace5DigitsCodes { get; private set; }
            public int[][] AtWrongPlace4DigitsCodes { get; private set; }
            public int[][] AtWrongPlace3DigitsCodes { get; private set; }
            public int[][] AtWrongPlace2DigitsCodes { get; private set; }
            public int[][] AtWrongPlace1DigitCodes { get; private set; }
            public int[][] AtRightPlace3DigitsAtWrongPlace2DigitsCodes { get; private set; }
            public int[][] AtRightPlace3DigitsAtWrongPlace1DigitCodes { get; private set; }
            public int[][] AtRightPlace2DigitsAtWrongPlace3DigitsCodes { get; private set; }
            public int[][] AtRightPlace2DigitsAtWrongPlace2DigitsCodes { get; private set; }
            public int[][] AtRightPlace2DigitsAtWrongPlace1DigitCodes { get; private set; }
            public int[][] AtRightPlace1DigitAtWrongPlace4DigitsCodes { get; private set; }
            public int[][] AtRightPlace1DigitAtWrongPlace3DigitsCodes { get; private set; }
            public int[][] AtRightPlace1DigitAtWrongPlace2DigitsCodes { get; private set; }
            public int[][] AtRightPlace1DigitAtWrongPlace1DigitCodes { get; private set; }
            public int[] WrongDigitsCode { get; private set; }
            #endregion

            #region [ Results ]
            public static List<Model.Matches> AtRightPlace5DigitsResult { get; private set; }
            public static List<Model.Matches> AtRightPlace4DigitsResult { get; private set; }
            public static List<Model.Matches> AtRightPlace3DigitsResult { get; private set; }
            public static List<Model.Matches> AtRightPlace2DigitsResult { get; private set; }
            public static List<Model.Matches> AtRightPlace1DigitResult { get; private set; }
            public static List<Model.Matches> AtWrongPlace5DigitsResult { get; private set; }
            public static List<Model.Matches> AtWrongPlace4DigitsResult { get; private set; }
            public static List<Model.Matches> AtWrongPlace3DigitsResult { get; private set; }
            public static List<Model.Matches> AtWrongPlace2DigitsResult { get; private set; }
            public static List<Model.Matches> AtWrongPlace1DigitResult { get; private set; }
            public static List<Model.Matches> AtRightPlace3DigitsAtWrongPlace2DigitsResult { get; private set; }
            public static List<Model.Matches> AtRightPlace3DigitsAtWrongPlace1DigitResult { get; private set; }
            public static List<Model.Matches> AtRightPlace2DigitsAtWrongPlace3DigitsResult { get; private set; }
            public static List<Model.Matches> AtRightPlace2DigitsAtWrongPlace2DigitsResult { get; private set; }
            public static List<Model.Matches> AtRightPlace2DigitsAtWrongPlace1DigitResult { get; private set; }
            public static List<Model.Matches> AtRightPlace1DigitAtWrongPlace4DigitsResult { get; private set; }
            public static List<Model.Matches> AtRightPlace1DigitAtWrongPlace3DigitsResult { get; private set; }
            public static List<Model.Matches> AtRightPlace1DigitAtWrongPlace2DigitsResult { get; private set; }
            public static List<Model.Matches> AtRightPlace1DigitAtWrongPlace1DigitResult { get; private set; }
            public static List<Model.Matches> WrongDigitsResult { get; private set; }
            #endregion

            static CodesAndResults()
            {
                codeLength = 5;
                minDigit = 1;
                maxDigit = 8;
                GenerateAllResults();

                void GenerateAllResults()
                {
                    AtRightPlace5DigitsResult = GetResults(5, 0);
                    AtRightPlace4DigitsResult = GetResults(4, 0);
                    AtRightPlace3DigitsResult = GetResults(3, 0);
                    AtRightPlace2DigitsResult = GetResults(2, 0);
                    AtRightPlace1DigitResult = GetResults(1, 0);

                    AtWrongPlace5DigitsResult = GetResults(0, 5);
                    AtWrongPlace4DigitsResult = GetResults(0, 4);
                    AtWrongPlace3DigitsResult = GetResults(0, 3);
                    AtWrongPlace2DigitsResult = GetResults(0, 2);
                    AtWrongPlace1DigitResult = GetResults(0, 1);

                    AtRightPlace3DigitsAtWrongPlace2DigitsResult = GetResults(3, 2);
                    AtRightPlace3DigitsAtWrongPlace1DigitResult = GetResults(3, 1);
                    AtRightPlace2DigitsAtWrongPlace3DigitsResult = GetResults(2, 3);
                    AtRightPlace2DigitsAtWrongPlace2DigitsResult = GetResults(2, 2);
                    AtRightPlace2DigitsAtWrongPlace1DigitResult = GetResults(2, 1);

                    AtRightPlace1DigitAtWrongPlace4DigitsResult = GetResults(1, 4);
                    AtRightPlace1DigitAtWrongPlace3DigitsResult = GetResults(1, 3);
                    AtRightPlace1DigitAtWrongPlace2DigitsResult = GetResults(1, 2);
                    AtRightPlace1DigitAtWrongPlace1DigitResult = GetResults(1, 1);

                    WrongDigitsResult = GetResults(0, 0);

                    List<Model.Matches> GetResults(int atRightPlaceDigits, int atWrongPlaceDigits)
                    {
                        CheckArguments();

                        List<Model.Matches> toReturn = new List<Model.Matches>();

                        for (int i = 0; i < atRightPlaceDigits; i++)
                            toReturn.Add(Model.Matches.AtRightPlace);

                        for (int i = 0; i < atWrongPlaceDigits; i++)
                            toReturn.Add(Model.Matches.AtWrongPlace);

                        if (toReturn.Count == 0)
                            toReturn.Add(Model.Matches.None);

                        return toReturn;

                        void CheckArguments()
                        {
                            if (atRightPlaceDigits < 0 || atRightPlaceDigits > codeLength)
                                throw new ArgumentOutOfRangeException(nameof(atRightPlaceDigits));
                            if (atWrongPlaceDigits < 0 || atWrongPlaceDigits > codeLength)
                                throw new ArgumentOutOfRangeException(nameof(atWrongPlaceDigits));
                            if (atRightPlaceDigits + atWrongPlaceDigits > codeLength || (atRightPlaceDigits == codeLength - 1 && atWrongPlaceDigits == 1))
                                throw new ArgumentOutOfRangeException(nameof(atRightPlaceDigits) + ", " + nameof(atWrongPlaceDigits));
                        }
                    }
                }
            }

            public CodesAndResults(int[] secretCode)
            {
                CheckArguments(secretCode);
                SecretCode = secretCode;

                GenerateWrongDigitsCode();

                GenerateAllOnlyAtRightPlaceDigitsCodes();
                GenerateAllOnlyAtWrongPlaceDigitsCodes();
            }

            private void CheckArguments(int[] secretCode)
            {
                if (secretCode is null)
                    throw new ArgumentNullException(nameof(secretCode));
                if (secretCode.Length != codeLength)
                    throw new ArgumentOutOfRangeException(nameof(secretCode));
            }

            private void GenerateWrongDigitsCode()
            {
                List<int> wrongDigits = new List<int>();

                FindWrongDigits();
                WrongDigitsCode = GetWrongDigitsCode();

                void FindWrongDigits()
                {
                    bool isMatched;

                    for (int digit = minDigit; digit <= maxDigit; digit++)
                    {
                        isMatched = false;

                        for (int i = 0; i < codeLength; i++)
                        {
                            if (SecretCode[i] == digit)
                            {
                                isMatched = true;
                                break;
                            }
                        }

                        if (!isMatched)
                            wrongDigits.Add(digit);
                    }
                }

                int[] GetWrongDigitsCode()
                {
                    int[] toReturn = new int[codeLength];

                    for (int i = 0; i < codeLength; i++)
                        toReturn[i] = wrongDigits[i % wrongDigits.Count];

                    return toReturn;
                }
            }

            private void GenerateAllOnlyAtRightPlaceDigitsCodes()
            {
                AtRightPlace4DigitsCodes = GetAtRightPlaceFrom1To4DigitsCodes(4);
                AtRightPlace3DigitsCodes = GetAtRightPlaceFrom1To4DigitsCodes(3);
                AtRightPlace2DigitsCodes = GetAtRightPlaceFrom1To4DigitsCodes(2);
                AtRightPlace1DigitCodes = GetAtRightPlaceFrom1To4DigitsCodes(1);

                int[][] GetAtRightPlaceFrom1To4DigitsCodes(int digitQuatities)
                {
                    int offset = digitQuatities - 4;

                    int[][] toReturn = new int[codeLength + offset][];

                    for (int i = 0; i < codeLength + offset; i++)
                    {
                        toReturn[i] = new int[codeLength];

                        for (int j = 0; j < codeLength; j++)
                        {
                            if ((digitQuatities == 4 && i == j) ||
                                (digitQuatities == 3 && (i == j || i + 1 == j)) ||
                                (digitQuatities == 2 && (i == j || i + 1 == j || i + 2 == j)) ||
                                (digitQuatities == 1 && (i == j || i + 1 == j || i + 2 == j || i + 3 == j)))
                            {
                                toReturn[i][j] = WrongDigitsCode[j];
                            }
                            else
                            {
                                toReturn[i][j] = SecretCode[j];
                            }
                        }
                    }

                    return toReturn;
                }
            }

            private void GenerateAllOnlyAtWrongPlaceDigitsCodes()
            {
                //
            }
        }
    }
}