using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LemmaSharp.Classes;
using OpenNLP.Tools.PosTagger;
using OpenNLP.Tools.Tokenize;

namespace TagsCloudVisualization.TagGeneration
{
    internal class WordExtractor : ITextExtractor
    {
        private readonly string modelPath = Path.Combine(
        Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
        "..",
        "..",
        "model"        
        );
        private readonly HashSet<string> illegalPartsOfSpeech = new HashSet<string>
        {
            "CC", "CD", "DT", "EX", "IN", "LS", "MD", "RP", "SYM", "TO", "UH",
            "PDT", "POS", "WDT", "WP", "WP$", "WRB", "PRP", "PRP$"
        };
        private readonly HashSet<string> illegalWords = new HashSet<string>
        {
            "be", "is", "are", "so", "do", "have", "go", "not", "yes", "no", "get", "mr", "ms", "mrs", "sir"
        };

        private Result<IEnumerable<string>> RemoveIllegalWords(string text)
        {
            var tokenizer = new EnglishRuleBasedTokenizer(splitOnHyphen:false);

            if (!File.Exists(Path.Combine(modelPath, "EnglishPOS.nbin")))
                return Result.Fail<IEnumerable<string>>("file EnglishPOS.nbin not found");
            var posTagger = new EnglishMaximumEntropyPosTagger(Path.Combine(modelPath, "EnglishPOS.nbin"));
            var tokens = tokenizer.Tokenize(text);
            var partsOfSpeech = posTagger.Tag(tokens);

            if (!File.Exists(Path.Combine(modelPath, "full7z-multext-en.lem")))
                return Result.Fail<IEnumerable<string>>("file full7z-multext-en.lem not found");
            var stream = File.OpenRead(Path.Combine(modelPath, "full7z-multext-en.lem"));
            var lemmatizer = new Lemmatizer(stream);

            return Result.Ok(tokens.Zip(partsOfSpeech, Tuple.Create)
                .Where(x => !illegalPartsOfSpeech.Contains(x.Item2))
                .Select(x => lemmatizer.Lemmatize(x.Item1.ToLower()))
                .Where(x => !illegalWords.Contains(x)));
        }

        private static Result<string> RemovePunctuation(string str)
        {
            return Result.Ok(new string(str.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray()));
        }

        public Result<IEnumerable<string>> ExtractWords(string text)
        {
            return RemoveIllegalWords(RemovePunctuation(text).Value);
        }
    }
}
