using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploringMars.Application.Views
{
    public class OutputView
    {
        public static void AnswerUserProbesLandingPositions(List<List<string>> probesLandingPositions)
        {
            var firstProbeLandingPosition = probesLandingPositions.First();
            var secondProbeLandingPosition = probesLandingPositions.Last();

            Console.WriteLine("\n***********************************************\n" +
                              "The probes landing positions are:\n");
            
            Console.WriteLine(firstProbeLandingPosition[0] + " " + firstProbeLandingPosition[1] + " " + firstProbeLandingPosition[2]);
            Console.WriteLine(secondProbeLandingPosition[0] + " " + secondProbeLandingPosition[1] + " " + secondProbeLandingPosition[2]);
        }
    }
}