/**
  * Created by fm on 25/01/2017.
  */
object Minesweeper {

  def main(args: Array[String]) = {
    println("hello")
  }
}

case class GameField(lines: Seq[Line]) {

  val emptyLine ={
    if (!lines.isEmpty) {
      Line(lines.head.mineline.map(_ => Dangerous(0)))
    }
    else {
      Line(Seq())
    }

  }

  val lowerCells = lines.zipWithIndex.map(i => i._1 + lower(i._2))

  def lower(index: Int): Line = {
    val lower = index + 1

    if (lower < lines.size) {
      lines(lower)
    } else {
      emptyLine
    }
  }

  val upperCells = lines.zipWithIndex.map(i => upper(i._2) + i._1)

  def upper(index: Int): Line = {
    val upper = index - 1

    if (upper >= 0) {
      lines(upper)
    } else {
      emptyLine
    }
  }

  val cells: Seq[Cell] = lowerCells.zip(upperCells).map(tuple =>tuple._1 + tuple._2).flatMap(_.mineline)
}

object Line {

  def apply(cells: String): Line = {
    val prev = previous(cells) _
    val n = next(cells) _

    Line(cells.zipWithIndex.collect {
      case ('.', i) => Dangerous(prev(i) + n(i))
      case (Mine.identifier, _) => Mine
    })
  }

  def previous(cells: String)(index: Int): Int = {
    val previousIndex = index - 1

    if (previousIndex >= 0 && cells.charAt(previousIndex) == Mine.identifier) {
      1
    } else {
      0
    }
  }

  def next(cells: String)(index: Int): Int = {
    val nextIndex = index + 1

    if (nextIndex < cells.size && cells.charAt(nextIndex) == Mine.identifier) {
      1
    } else {
      0
    }
  }
}

case class Line(mineline: Seq[Cell]) {


  def +(line: Line): Line = {
    val newMineLine = mineline.zip(line.mineline).collect {
      case (Mine, Mine) => Mine
      case (Mine, Dangerous(_)) => Mine
      case (Dangerous(danger), Mine) => Dangerous(danger + 1)
      case (Dangerous(k), Dangerous(_)) => Dangerous(k)
    }

    Line(newMineLine)
  }
}

trait Cell

object Mine extends Cell {
  val identifier = '*'
}

case class Dangerous(danger: Int) extends Cell