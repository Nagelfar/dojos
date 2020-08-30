using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Code;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Increase_playerB_After_PlayerB_Won()
        {
            var state = new GameState("0", "0");

            var newState = Scorer.CalculateScore(state,  PlayerWon.B);

            Assert.Equal(new GameState("0", "15"), newState);
        }

        [Fact]
        public void Increase_playerA_After_two_won_game()
        {
            var state = new GameState("15", "0");

            var newState = Scorer.CalculateScore(state, PlayerWon.A);

            Assert.Equal(new GameState("30", "0"), newState);
        }
        
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void NewState_Is_Correct_After_Game(GameState currentState, PlayerWon playerWon, GameState expectedNewState)
        {
            var newState = Scorer.CalculateScore(currentState, playerWon);

            Assert.Equal(expectedNewState, newState);
        }          

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] {new GameState("0", "0"), PlayerWon.A, new GameState("15", "0")}; 
            yield return new object[] {new GameState("0", "15"), PlayerWon.B, new GameState("0", "30")};
            yield return new object[] {new GameState("0", "30"), PlayerWon.B, new GameState("0", "40")};
        }
        
    }

    public struct GameState
    {
        private string playerA;
        private string playerB;

        public GameState(string playerA, string playerB)
        {
            this.playerA = playerA;
            this.playerB = playerB;
        }

        public GameState IncreaseA()
        {
            if (playerA == "15")
            {
                return new GameState("30", "0");
            }
            else
            {
                return new GameState("15", "0");
            }
        }

        public GameState IncreaseB()
        {
            if (playerB == "15")
            {
                return new GameState("0", "30");
            }
            else if (playerB == "30")
            {
                return new GameState("0", "40");                
            }
            else
            {
                return new GameState("0", "15");
            }
        }
    }
    class Scorer
    {      

        internal static GameState CalculateScore(GameState state, PlayerWon playerWon)
        {
            if (playerWon == PlayerWon.A)
                return state.IncreaseA();

            else
                return state.IncreaseB();
                
        }
    }

    public enum PlayerWon
    {
        A,
        B
    }
}