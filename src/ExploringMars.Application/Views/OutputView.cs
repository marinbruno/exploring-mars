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
                              "The probes landing positions are:\n" +
                              firstProbeLandingPosition + "\n" +
                              secondProbeLandingPosition);
        }
    }
}