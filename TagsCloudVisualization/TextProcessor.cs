using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LemmaSharp.Classes;
using OpenNLP.Tools.PosTagger;
using OpenNLP.Tools.Tokenize;

namespace TagsCloudVisualization
{
    class TextProcessor : ITextProcessor
    {
        private string modelPath = Path.Combine(
        Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
        "..",
        "..",
        "model"        
        );
        private HashSet<string> illegalPartsOfSpeech = new HashSet<string>
        {
            "CC", "CD", "DT", "EX", "IN", "LS", "MD", "RP", "SYM", "TO", "UH",
            "PDT", "POS", "WDT", "WP", "WP$", "WRB", "PRP", "PRP$"
        };
        private HashSet<string> illegalWords = new HashSet<string>
        {
            "be", "is", "are", "so", "do", "have", "go", "not", "yes", "no", "get", "mr", "ms", "mrs", "sir"
        };

        private IEnumerable<string> RemoveIllegalWords(string text)
        {
            var tokenizer = new EnglishRuleBasedTokenizer(splitOnHyphen:false);
            var posTagger = new EnglishMaximumEntropyPosTagger(Path.Combine(modelPath, "EnglishPOS.nbin"));
            var tokens = tokenizer.Tokenize(text);
            var partsOfSpeech = posTagger.Tag(tokens);
            var stream = File.OpenRead(Path.Combine(modelPath, "full7z-multext-en.lem"));
            var lemmatizer = new Lemmatizer(stream);

            return tokens.Zip(partsOfSpeech, Tuple.Create)
                .Where(x => !illegalPartsOfSpeech.Contains(x.Item2))
                .Select(x => lemmatizer.Lemmatize(x.Item1.ToLower()))
                .Where(x => !illegalWords.Contains(x));
        }

        private string RemovePunctuation(string str)
        {
            return new string(str.Where(c => !char.IsPunctuation(c)).ToArray());
        }

        public IEnumerable<string> ProcessText(string text)
        {
            return RemoveIllegalWords(RemovePunctuation(text));
        }
    }
}
