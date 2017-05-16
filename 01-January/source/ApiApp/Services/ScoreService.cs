using System.Linq;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace WebApplication.Services
{
    public class ScoreService
    {

        private Dictionary<string, int> bonusWords = new Dictionary<string, int>()
        {
            ["amazing"] = 50,
            ["classy"] = 20,
            ["clown"] = 40,
            ["tremendous"] = 10,
            ["handful"] = 10,
            ["eminent"] = 10,
            ["sudden"] = 50,
            ["us"] = 50,
            ["usa"] = 50,
            ["waste"] = 50,
            ["nasty"] = 40,
            ["nice"] = 30,
            ["hell"] = 10,
            ["fake"] = 50,
            ["mess"] = 30,
            ["mexico"] = 20,
            ["weak"] = 30,
            ["traitor"] = 10,
            ["horrible"] = 40,
            ["great"] = 30,
            ["america"] = 10,
            ["american"] = 30
        };

        public int GetScoreForSentence(string sentence)
        {

            var words = Regex.Replace(sentence, @"\p{P}", "")
            .ToLower()
            .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            var wordCount = words.Count();
            var lengthScore = words
                .Select(word => GetLengthScore(word))
                .Sum();

            var bonusScore = words
                .Select(w => GetBonusScore(w))
                .Sum();

            var finalScore = (lengthScore / wordCount) + bonusScore;

            return (int)finalScore;
        }

        private int GetBonusScore(string word)
        {

            if (this.bonusWords.ContainsKey(word))
            {
                return this.bonusWords[word];
            }

            return 0;
        }

        private int GetLengthScore(string word)
        {
            var extraLetterPenalty = 10;
            var score = 50;
            var lettersDelta = 6 - word.Count();
            score += lettersDelta * extraLetterPenalty;

            return Math.Max(0, score);
        }
    }
}