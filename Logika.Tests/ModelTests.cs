using NUnit.Framework;
using ReflectionExtension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Logika.Tests
{
    public class ModelTests
    {
        Model model = new Model();
        private static List<CodesAndResults> codesAndResults;

        static ModelTests()
        {
            int codeLength = CodesAndResults.CodeLength;
            int minDigit = CodesAndResults.MinDigit;
            int maxDigit = MaxDigit(true, true);

            codesAndResults = new List<CodesAndResults>();
            SetDigit(0);

            int MaxDigit(bool isSpeedTest = true, bool isBalancedTest = true)
            {
                if (isSpeedTest)
                {
                    return (CodesAndResults.MinDigit + CodesAndResults.MaxDigit) / 2;
                }
                else if (isBalancedTest)
                {
                    int optimalMaxDigit = CodesAndResults.MinDigit + CodesAndResults.CodeLength - 1;

                    if (CodesAndResults.MaxDigit > optimalMaxDigit)
                        return optimalMaxDigit;
                }

                return CodesAndResults.MaxDigit;
            }
            
            void SetDigit (int codeIndex)
            {
                CheckArguments();

                int[] secretCode = new int[codeLength];

                for (int digit = minDigit; digit <= maxDigit; digit++)
                {
                    secretCode[codeIndex] = digit;

                    if (codeIndex == codeLength - 1)
                    {
                        int[] currentSecretCode = new int[codeLength];
                        secretCode.CopyTo(currentSecretCode, 0);
                        codesAndResults.Add(new CodesAndResults(currentSecretCode));
                    }
                    else
                    {
                        SetDigit(codeIndex + 1);
                    }
                }

                void CheckArguments()
                {
                    if (codeIndex < 0 || codeIndex > codeLength)
                        throw new ArgumentOutOfRangeException(nameof(codeIndex));
                }
            }
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

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtWrongPlace5Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtWrongPlace5DigitsCodes, model.GetComparisonResults, CodesAndResults.AtWrongPlace5DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtWrongPlace4Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtWrongPlace4DigitsCodes, model.GetComparisonResults, CodesAndResults.AtWrongPlace4DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtWrongPlace3Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtWrongPlace3DigitsCodes, model.GetComparisonResults, CodesAndResults.AtWrongPlace3DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtWrongPlace2Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtWrongPlace2DigitsCodes, model.GetComparisonResults, CodesAndResults.AtWrongPlace2DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtWrongPlace1Digit_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtWrongPlace1DigitCodes, model.GetComparisonResults, CodesAndResults.AtWrongPlace1DigitResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace3DigitsAtWrongPlace2Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace3DigitsAtWrongPlace2DigitsCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace3DigitsAtWrongPlace2DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace3DigitsAtWrongPlace1Digit_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace3DigitsAtWrongPlace1DigitCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace3DigitsAtWrongPlace1DigitResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace2DigitsAtWrongPlace3Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace2DigitsAtWrongPlace3DigitsCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace2DigitsAtWrongPlace3DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace2DigitsAtWrongPlace2Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace2DigitsAtWrongPlace2DigitsCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace2DigitsAtWrongPlace2DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace2DigitsAtWrongPlace1Digit_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace2DigitsAtWrongPlace1DigitCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace2DigitsAtWrongPlace1DigitResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace1DigitAtWrongPlace4Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace1DigitAtWrongPlace4DigitsCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace1DigitAtWrongPlace4DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace1DigitAtWrongPlace3Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace1DigitAtWrongPlace3DigitsCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace1DigitAtWrongPlace3DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace1DigitAtWrongPlace2Digits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace1DigitAtWrongPlace2DigitsCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace1DigitAtWrongPlace2DigitsResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_AtRightPlace1DigitAtWrongPlace1Digit_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);

            string failedMessage;
            bool areEqual = AreEqualSubtests(source.AtRightPlace1DigitAtWrongPlace1DigitCodes, model.GetComparisonResults, CodesAndResults.AtRightPlace1DigitAtWrongPlace1DigitResult, out failedMessage);

            Assert.IsTrue(areEqual, failedMessage);
        }

        [Test, TestCaseSource(nameof(codesAndResults))]
        public void GetComparisonResults_WrongDigits_True(CodesAndResults source)
        {
            Reflection.SetFieldValue(typeof(Model), "secretCode", source.SecretCode, model);
            List<Model.Matches> result = model.GetComparisonResults(source.WrongDigitsCode);

            bool areEqual = AreEqualMatchesLists(CodesAndResults.WrongDigitsResult, result);
            Assert.IsTrue(areEqual);
        }

        private delegate List<Model.Matches> GetMatches(int[] code);

        private bool AreEqualSubtests(List<int[]> guessedCodes, GetMatches getMatchesMethod, List<Model.Matches> expectedResult, out string failedMessage)
        {
            int testQuantities = guessedCodes.Count;
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
            public static readonly int CodeLength;
            public static readonly int MinDigit;
            public static readonly int MaxDigit;

            #region [ Codes ]
            public int[] SecretCode { get; private set; }
            public int[] AtRightPlace5DigitsCode { get => SecretCode; }
            public List<int[]> AtRightPlace4DigitsCodes { get; private set; }
            public List<int[]> AtRightPlace3DigitsCodes { get; private set; }
            public List<int[]> AtRightPlace2DigitsCodes { get; private set; }
            public List<int[]> AtRightPlace1DigitCodes { get; private set; }
            public List<int[]> AtWrongPlace5DigitsCodes { get; private set; }
            public List<int[]> AtWrongPlace4DigitsCodes { get; private set; }
            public List<int[]> AtWrongPlace3DigitsCodes { get; private set; }
            public List<int[]> AtWrongPlace2DigitsCodes { get; private set; }
            public List<int[]> AtWrongPlace1DigitCodes { get; private set; }
            public List<int[]> AtRightPlace3DigitsAtWrongPlace2DigitsCodes { get; private set; }
            public List<int[]> AtRightPlace3DigitsAtWrongPlace1DigitCodes { get; private set; }
            public List<int[]> AtRightPlace2DigitsAtWrongPlace3DigitsCodes { get; private set; }
            public List<int[]> AtRightPlace2DigitsAtWrongPlace2DigitsCodes { get; private set; }
            public List<int[]> AtRightPlace2DigitsAtWrongPlace1DigitCodes { get; private set; }
            public List<int[]> AtRightPlace1DigitAtWrongPlace4DigitsCodes { get; private set; }
            public List<int[]> AtRightPlace1DigitAtWrongPlace3DigitsCodes { get; private set; }
            public List<int[]> AtRightPlace1DigitAtWrongPlace2DigitsCodes { get; private set; }
            public List<int[]> AtRightPlace1DigitAtWrongPlace1DigitCodes { get; private set; }
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
                CodeLength = 5;
                MinDigit = 1;
                MaxDigit = 8;
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
                            if (atRightPlaceDigits < 0 || atRightPlaceDigits > CodeLength)
                                throw new ArgumentOutOfRangeException(nameof(atRightPlaceDigits));
                            if (atWrongPlaceDigits < 0 || atWrongPlaceDigits > CodeLength)
                                throw new ArgumentOutOfRangeException(nameof(atWrongPlaceDigits));
                            if (atRightPlaceDigits + atWrongPlaceDigits > CodeLength || (atRightPlaceDigits == CodeLength - 1 && atWrongPlaceDigits == 1))
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
                GenerateAllWithRightDigitsCodes();
            }

            private void CheckArguments(int[] secretCode)
            {
                if (secretCode is null)
                    throw new ArgumentNullException(nameof(secretCode));
                if (secretCode.Length != CodeLength)
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

                    for (int digit = MinDigit; digit <= MaxDigit; digit++)
                    {
                        isMatched = false;

                        for (int i = 0; i < CodeLength; i++)
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
                    int[] toReturn = new int[CodeLength];

                    for (int i = 0; i < CodeLength; i++)
                        toReturn[i] = wrongDigits[i % wrongDigits.Count];

                    return toReturn;
                }
            }

            private void GenerateAllWithRightDigitsCodes()
            {
                AtRightPlace4DigitsCodes = GetWithRightDigitsCodes(4, 0);
                AtRightPlace3DigitsCodes = GetWithRightDigitsCodes(3, 0);
                AtRightPlace2DigitsCodes = GetWithRightDigitsCodes(2, 0);
                AtRightPlace1DigitCodes = GetWithRightDigitsCodes(1, 0);

                AtWrongPlace5DigitsCodes = GetWithRightDigitsCodes(0, 5);
                AtWrongPlace4DigitsCodes = GetWithRightDigitsCodes(0, 4);
                AtWrongPlace3DigitsCodes = GetWithRightDigitsCodes(0, 3);
                AtWrongPlace2DigitsCodes = GetWithRightDigitsCodes(0, 2);
                AtWrongPlace1DigitCodes = GetWithRightDigitsCodes(0, 1);

                AtRightPlace3DigitsAtWrongPlace2DigitsCodes = GetWithRightDigitsCodes(3, 2);
                AtRightPlace3DigitsAtWrongPlace1DigitCodes = GetWithRightDigitsCodes(3, 1);
                AtRightPlace2DigitsAtWrongPlace3DigitsCodes = GetWithRightDigitsCodes(2, 3);
                AtRightPlace2DigitsAtWrongPlace2DigitsCodes = GetWithRightDigitsCodes(2, 2);
                AtRightPlace2DigitsAtWrongPlace1DigitCodes = GetWithRightDigitsCodes(2, 1);
                AtRightPlace1DigitAtWrongPlace4DigitsCodes = GetWithRightDigitsCodes(1, 4);
                AtRightPlace1DigitAtWrongPlace3DigitsCodes = GetWithRightDigitsCodes(1, 3);
                AtRightPlace1DigitAtWrongPlace2DigitsCodes = GetWithRightDigitsCodes(1, 2);
                AtRightPlace1DigitAtWrongPlace1DigitCodes = GetWithRightDigitsCodes(1, 1);
            }

            private List<int[]> GetWithRightDigitsCodes(int atRightPlaceDigitQuantities, int atWrongPlaceDigitQuantities)
            {
                CheckArguments();

                List<int[]> generatedCodes = new List<int[]>();
                int[] atRightPlaceDigitIndexes = new int[atRightPlaceDigitQuantities];
                int[] atWrongPlaceDigitIndexes = new int[atWrongPlaceDigitQuantities];

                SetRightPlaceDigitIndex(atRightPlaceDigitQuantities, 0);

                return generatedCodes;

                void SetRightPlaceDigitIndex(int indexesLeft, int currentIndex)
                {
                    CheckArguments();

                    if (atRightPlaceDigitQuantities == 0)
                    {
                        SetWrongPlaceDigitIndex(atWrongPlaceDigitQuantities, 0);
                    }
                    else
                    {
                        for (int i = currentIndex; i <= CodeLength - indexesLeft; i++)
                        {
                            atRightPlaceDigitIndexes[atRightPlaceDigitQuantities - indexesLeft] = i;

                            if (indexesLeft == 1)
                            {
                                SetWrongPlaceDigitIndex(atWrongPlaceDigitQuantities, GetFirstFreeFromRightPlaceDigitIndex());
                            }
                            else
                            {
                                SetRightPlaceDigitIndex(indexesLeft - 1, i + 1);
                            }
                        }
                    }

                    void CheckArguments()
                    {
                        if ((indexesLeft < 1 && atRightPlaceDigitQuantities > 0) || indexesLeft > atRightPlaceDigitQuantities)
                            throw new ArgumentOutOfRangeException(nameof(indexesLeft));

                        if (currentIndex < 0 || currentIndex >= CodeLength)
                            throw new ArgumentOutOfRangeException(nameof(currentIndex));
                    }

                    int GetFirstFreeFromRightPlaceDigitIndex()
                    {
                        int i = 0;
                        int j = 0;

                        while (i < CodeLength)
                        {
                            if (j == atRightPlaceDigitQuantities || i != atRightPlaceDigitIndexes[j])
                            {
                                return i;
                            }
                            else
                            {
                                i++;
                                j++;
                            }
                        }

                        return -1;
                    }
                }

                void SetWrongPlaceDigitIndex(int indexesLeft, int currentIndex)
                {
                    CheckArguments();

                    if (atWrongPlaceDigitQuantities == 0)
                    {
                        GenerateCodes();
                    }
                    else
                    {
                        for (int i = currentIndex; i <= CodeLength - indexesLeft; i++)
                        {
                            bool isAvailableIndex = true;

                            for (int j = 0; j < atRightPlaceDigitQuantities; j++)
                            {
                                if (atRightPlaceDigitIndexes[j] == i)
                                {
                                    isAvailableIndex = false;
                                    break;
                                }
                            }

                            if (isAvailableIndex)
                            {
                                atWrongPlaceDigitIndexes[atWrongPlaceDigitQuantities - indexesLeft] = i;

                                if (indexesLeft == 1)
                                {
                                    GenerateCodes();
                                }
                                else
                                {
                                    SetWrongPlaceDigitIndex(indexesLeft - 1, i + 1);
                                }
                            }
                        }
                    }

                    void CheckArguments()
                    {
                        if ((indexesLeft < 1 && atWrongPlaceDigitQuantities > 0) || indexesLeft > atWrongPlaceDigitQuantities)
                            throw new ArgumentOutOfRangeException(nameof(indexesLeft));

                        if (currentIndex < 0 || currentIndex >= CodeLength)
                            throw new ArgumentOutOfRangeException(nameof(currentIndex));
                    }
                }

                void GenerateCodes()
                {
                    int generationCounter = 0;
                    bool[] areAtRightPlaceDigits = AreRightDigits(atRightPlaceDigitIndexes);
                    bool[] areAtWrongPlaceDigits = AreRightDigits(atWrongPlaceDigitIndexes);

                    if (atWrongPlaceDigitQuantities > 0)
                        FillWithAtWrongPlaceDigits();

                    if (atRightPlaceDigitQuantities > 0)
                        FillWithAtRightPlaceDigits();

                    if (atRightPlaceDigitQuantities + atWrongPlaceDigitQuantities < CodeLength)
                        FillWithWrongDigits();

                    bool[] AreRightDigits(int[] digitIndexes)
                    {
                        bool[] areRightDigits = new bool[CodeLength];

                        for (int i = 0; i < digitIndexes.Length; i++)
                            areRightDigits[digitIndexes[i]] = true;

                        return areRightDigits;
                    }

                    void FillWithAtWrongPlaceDigits()
                    {
                        bool[] areBusyByAnotherAtWrongPlaceDigit = new bool[CodeLength];
                        int[] onlyAtWrongPlaceDigits = new int[CodeLength];

                        TryFindAtWrongPlaceDigit(atWrongPlaceDigitQuantities);

                        void TryFindAtWrongPlaceDigit(int indexesLeft)
                        {
                            CheckArguments();

                            for (int i = 0; i < CodeLength; i++)
                            {
                                if (!areAtRightPlaceDigits[i] && !areBusyByAnotherAtWrongPlaceDigit[i] && SecretCode[atWrongPlaceDigitIndexes[atWrongPlaceDigitQuantities - indexesLeft]] != SecretCode[i])
                                {
                                    onlyAtWrongPlaceDigits[atWrongPlaceDigitIndexes[atWrongPlaceDigitQuantities - indexesLeft]] = SecretCode[i];
                                    areBusyByAnotherAtWrongPlaceDigit[i] = true;

                                    if (indexesLeft > 1)
                                    {
                                        TryFindAtWrongPlaceDigit(indexesLeft - 1);
                                    }
                                    else
                                    {
                                        generatedCodes.Add(CodeCopy(onlyAtWrongPlaceDigits));
                                        generationCounter++;
                                    }

                                    onlyAtWrongPlaceDigits[atWrongPlaceDigitIndexes[atWrongPlaceDigitQuantities - indexesLeft]] = 0;
                                    areBusyByAnotherAtWrongPlaceDigit[i] = false;
                                }
                            }

                            void CheckArguments()
                            {
                                if ((indexesLeft < 1 && atWrongPlaceDigitQuantities > 0) || indexesLeft > atWrongPlaceDigitQuantities)
                                    throw new ArgumentOutOfRangeException(nameof(indexesLeft));
                            }

                            int[] CodeCopy(int[] code)
                            {
                                int[] copy = new int[code.Length];

                                for (int i = 0; i < code.Length; i++)
                                    copy[i] = code[i];

                                return copy;
                            }
                        }
                    }

                    void FillWithAtRightPlaceDigits()
                    {
                        if (generationCounter == 0 && atWrongPlaceDigitQuantities == 0)
                        {
                            generatedCodes.Add(new int[CodeLength]);
                            generationCounter++;
                        }

                        for (int i = 0; i < atRightPlaceDigitQuantities; i++)
                            for (int j = 0; j < generationCounter; j++)
                                generatedCodes[generatedCodes.Count - generationCounter + j][atRightPlaceDigitIndexes[i]] = SecretCode[atRightPlaceDigitIndexes[i]];
                    }

                    void FillWithWrongDigits()
                    {
                        if (generationCounter == 0 && atWrongPlaceDigitQuantities == 0)
                        {
                            generatedCodes.Add(new int[CodeLength]);
                            generationCounter++;
                        }

                        for (int i = 0; i < CodeLength; i++)
                        {
                            if (!areAtRightPlaceDigits[i] && !areAtWrongPlaceDigits[i])
                            {
                                for (int j = 0; j < generationCounter; j++)
                                    generatedCodes[generatedCodes.Count - generationCounter + j][i] = WrongDigitsCode[i];
                            }
                        }
                    }
                }

                void CheckArguments()
                {
                    if (atRightPlaceDigitQuantities < 0 || atRightPlaceDigitQuantities > CodeLength)
                        throw new ArgumentOutOfRangeException(nameof(atRightPlaceDigitQuantities));

                    if (atWrongPlaceDigitQuantities < 0 || atWrongPlaceDigitQuantities > CodeLength)
                        throw new ArgumentOutOfRangeException(nameof(atWrongPlaceDigitQuantities));

                    if (atRightPlaceDigitQuantities + atWrongPlaceDigitQuantities > 5 || atRightPlaceDigitQuantities + atWrongPlaceDigitQuantities == 0 ||
                        (atRightPlaceDigitQuantities == CodeLength - 1 && atWrongPlaceDigitQuantities == 1))
                        throw new ArgumentOutOfRangeException(nameof(atRightPlaceDigitQuantities) + ", " + nameof(atWrongPlaceDigitQuantities));
                }
            }
        }
    }
}