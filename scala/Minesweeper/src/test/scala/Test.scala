import org.scalatest.FlatSpec

/**
  * Created by fm on 25/01/2017.
  */
class Test extends FlatSpec {

  "My test" should "run through!" in {
    Minesweeper.main(Seq("huhu").toArray)

    assert(true)
  }

  "An empty gamefield" should "have no cells" in {
    val gamefield = GameField(Seq())

    assert(gamefield.cells.isEmpty)
  }

  "A one by one gamefield" should "contain one cell" in {
    val gameField = GameField(Seq(Line(".")))
    assert(gameField.cells.size == 1)
  }

  "A two by one gamefield with one mine" should "contain one cell with a mine" in {
    val gameField = GameField(Seq(Line(".*")))
    assert(gameField.cells.last == Mine)
  }

  "A two by one gamefield with one empty field and a mine" should "be read as such" in {
    val gameField = GameField(Seq(Line("*.")))
    assert(gameField.cells == Seq(Mine, Dangerous(1)))
  }

  "A three by one gamefield with a mine, and empty field and a mine" should "have a Dangerous Count of 2 for the empty field" in {
    val gameField = GameField(Seq(Line("*.*")))

    assert(gameField.cells == Seq(Mine, Dangerous(2), Mine))
  }

  "A one by two gamefield with no mines" should "contains all zeros" in {
    val gameField = GameField(Seq(Line("."), Line("*")))
    assert(gameField.cells == Seq(Dangerous(1), Mine))
  }

  "A one by two gamefield with one mine and an empty space" should "contains all zeros" in {
    val gameField = GameField(Seq(Line("*"), Line(".")))
    assert(gameField.cells == Seq(Mine, Dangerous(1)))
  }
}


