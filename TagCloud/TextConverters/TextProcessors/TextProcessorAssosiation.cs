﻿using System.Collections.Generic;

namespace TagCloud.TextConverters.TextProcessors
{
    public static class TextProcessorAssosiation
    {
        private static readonly Dictionary<string, ITextProcessor> processors =
            new Dictionary<string, ITextProcessor>
            {
                ["paragraph"] = new ParagraphTextProcessor(),
                ["words"] = new WordsTextProcessor()
            };

        public static ITextProcessor GetProcessor(string name) =>
            processors.TryGetValue(name, out var processor) ? processor : null;
    }
}
