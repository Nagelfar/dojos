module Tests

open System
open Xunit
open Implementation
open System

type Player = X | Y
type Column = First|Middle|Last
type Row = Upper|Middle|Lower
type Position = {column: Column; row:Row; Player: Player option}

type Winner = Player of Player | Draw | Undecided

type Board = Position list

let winnerInEntries entries =
    match entries with
    | [Some(X) ; Some(X); Some(X)]-> Player(X)
    | [Some(Y) ; Some(Y); Some(Y)]-> Player(Y)
    | e when List.exists (fun x-> x = None) e -> Undecided
    | _ -> Draw
    
let isWon options =
    match options with
    | e when List.exists (fun p -> p = Player(X)) e -> Player(X)
    | e when List.exists (fun p -> p = Player(Y)) e -> Player(Y)
    | e when List.exists (fun p -> p = Draw) e -> Draw
    | _ -> Undecided
    

let findPlayers board condition =
    board
        |> List.filter condition
        |> List.map (fun p->p.Player)

let winnerInRow (board:Board) = 
    let playersInRow row = 
        board
        |> List.filter (fun p->p.row = row)
        |> List.map (fun p->p.Player)

    [Upper;Middle;Lower]
    |> List.map (playersInRow >> winnerInEntries)
    
let winnerInColumn board =
    let playersInColumn col =
        board 
        |> List.filter (fun b -> b.column = col)
        |> List.map (fun p -> p.Player)
    
    [First;Column.Middle;Last]
    |> List.map (playersInColumn >> winnerInEntries)


let whoIsWinner board =
    let byRow =
        isWon (winnerInRow board)
    let byColumn =
        isWon (winnerInColumn board)

    match byRow with
        | Winner.Player (p) -> Player(p)
        | _ -> byColumn

type InputValue = X|Y|O
// type InputRow = (InputValue,InputValue,InputValue)
        
let parseValue r =
    match r with
    | X -> Some(Player.X)
    | Y -> Some(Player.Y)
    | O -> None

let buildPosition row (entries: Player option array ) =
    [        
        {column = First; row= row; Player = entries.[0]};
        {column = Column.Middle; row= row; Player = entries.[1]};
        {column = Last; row= row; Player = entries.[2]};
    ] 

let buildRow items row =
    items 
        |> Array.map parseValue
        |> buildPosition row

let buildBoard (first, middle, last) =
    [
        buildRow first Row.Upper; 
        buildRow middle Row.Middle;
        buildRow last Row.Lower
    ] |> List.collect id 

let emptyRow = [|O;O;O|]

[<Fact>]
let ``XXX in one row should lead to X`` () =
    let board = buildBoard (
            [|X;X;X|],
            emptyRow,
            emptyRow
        )
    Assert.Equal(Winner.Player(Player.X), whoIsWinner board)

[<Fact>]
let ``XXX in one column should lead to X`` () =
    let board = buildBoard (
        [|X;O;O|],
        [|X;O;O|],
        [|X;O;O|]
        )
    Assert.Equal(Winner.Player(Player.X), whoIsWinner board)

[<Fact>]
let ``XXX in second column should lead to X`` () =
    let board = buildBoard (
        [|X;X;O|],
        [|Y;X;O|],
        [|X;X;O|]
        )
    Assert.Equal(Winner.Player(Player.X), whoIsWinner board)

[<Fact>]
let ``YYY in last column should lead to Y`` () =
    let board = buildBoard (
        [|X;Y;Y|],
        [|Y;X;Y|],
        [|X;X;Y|]
        )
    Assert.Equal(Winner.Player(Player.Y), whoIsWinner board)
