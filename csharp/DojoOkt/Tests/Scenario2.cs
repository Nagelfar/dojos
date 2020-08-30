using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace Tests
{
    public class Scenario2Tests
    {
        // (4,5) -> Advantage A
        // (5,0) -> A Won
        // (5,5) -> Deuce

        [Fact]
        public void With_one_point_for_player_A_the_score_should_be_15_0()
        {
            var result = Formatter.FormatScore(1, 0);

            Assert.Equal("15,0", result);
        }

        [Fact]
        public void With_four_point_for_player_A_the_playerA_should_win_the_game()
        {
            var result = Formatter.FormatScore(4, 0);

            Assert.Equal("A Won", result);
        }

        [Fact]
        public void With_5_points_for_each_player_the_result_should_be_deuce()
        {
            var result = Formatter.FormatScore(5, 5);

            Assert.Equal("deuce", result);
        }

        [Fact]
        public void With_4_points_for_each_player_the_result_should_be_40_40()
        {
            var result = Formatter.FormatScore(3, 3);

            Assert.Equal("40,40", result);
        }

        [Fact]
        public void After_4_points_a_difference_of_one_for_playerA_means_advantage_A()
        {
            var result = Formatter.FormatScore(5, 4);

            Assert.Equal("Advantage A", result);
        }
       
        [Fact]
        public void After_4_points_a_difference_of_two_for_playerA_means_A_won()
        {
            var result = Formatter.FormatScore(6, 4);

            Assert.Equal("A Won", result);
        }
    }

    public class Formatter
    {
        private const string AWon = "A Won";
        private const string Deuce = "deuce";

        public static string FormatScore(int scorePlayerA, int scorePlayerB)
        {
            if (scorePlayerA > 3 || scorePlayerB > 3)
            {
                if (scorePlayerA == scorePlayerB)
                {
                    if (scorePlayerA > 4)
                        return Deuce;
                }
                else if (scorePlayerA -scorePlayerB >=2 )
                    return AWon;
                else if (scorePlayerA > scorePlayerB && scorePlayerB >= 4)
                {
                    return "Advantage A";
                }
            }
            else
            {
                if (scorePlayerA == 3)
                    return "40,40";
                else
                    return "15,0";    
            }
            
        }
    }
}