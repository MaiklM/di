﻿using System;
using System.Drawing;
using TagCloud.TextConverters.TextReaders;
using TagCloud.TextConverters.TextProcessors;
using TagCloud.WordsMetrics;
using TagCloud.PointGetters;
using TagCloud.Visualization;
using TagCloud.Visualization.WordsColorings;
using TagCloud.CloudLayoters;

namespace TagCloud.Clients
{
    internal class ConsoleClient : IClient
    {
        private readonly ITextReader reader = new TextReaderTxt();
        private readonly ITextProcessor processor;
        private readonly IWordsMetric metric;
        private readonly ICloudLayoter layoter;

        internal ConsoleClient(ITextProcessor processor, 
            IWordsMetric metric, ICloudLayoter layoter)
        {
            this.processor = processor;
            this.metric = metric;
            this.layoter = layoter;
        }

        public void Run()
        {
            string answear;
            Console.WriteLine("Hello, I'm your personal visualization client");
            while (true)
            {
                Console.WriteLine("Please, write path to file with words or \"exit\" to exit");
                answear = Console.ReadLine();
                if (answear == "exit")
                    break;
                var text = reader.ReadText(answear);
                if (text == null)
                {
                    Console.WriteLine("Something was wrong, Please try again");
                    continue;
                }
                var info = ReadInfo();
                Console.WriteLine("Please write path to save picture");
                var path = Console.ReadLine();
                Visualize(text, path, info);
                Console.WriteLine("Picture save");
                Console.WriteLine();
            }
        }

        private VisualizationInfo ReadInfo()
        {
            Console.WriteLine("Write 3 numbers from 0 to 255 between space");
            Console.WriteLine("For example: 255 176 0");
            var colorRGB = Console.ReadLine();
            Console.WriteLine("Please write font");
            var font = Console.ReadLine();
            Console.WriteLine("Please write 2 number for size picture");
            var sizeString = Console.ReadLine();
            var size = VisualizationInfo.ReadSize(sizeString);
            return new VisualizationInfo(new WordsColoringLineBringhtness(Color.FromArgb(255, 255, 0, 0)), size, font);
        }

        public void Visualize(string text, string picturePath, VisualizationInfo info)
        {
            var tagCloud = AlgorithmTagCloud.GetTagCloud(text, layoter, processor, metric);
            TagCloudVisualization.Visualize(tagCloud, picturePath, info);
        }
    }
}
