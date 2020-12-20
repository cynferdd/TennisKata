module Tests

open System
open Xunit
open Tennis
open Swensen.Unquote

[<Fact>]
let ``Avantage au gagnant quand Deuce`` () =
    Score.FromDeuce P1 =! Avantage P1
    //test <@ Avantage P1 = Score.FromDeuce P1 @>

[<Fact>]
let ``Victoire ne change pas m�me avec un gagnant`` () =
    Score.FromVictoire P1 P2 =! Victoire P1
    Score.FromVictoire P1 P1 =! Victoire P1

[<Fact>]
let `` Si on perd l'avantage, on revient � Deuce`` () =
    Score.FromAvantage P1 P2 =! Deuce


[<Fact>]
let `` Si on gagne l'avantage, on a une victoire`` () =
    Score.FromAvantage P1 P1 =! Victoire P1

[<Fact>]
let ``Si le joueur qui a 40 gagne le point, il gagne la partie`` () =
    let quarante = {
        JoueurAQuarante = P1
        PointsAutreJoueur = Zero
    }
    Score.FromQuarante quarante P1 =! Victoire P1

[<Fact>]
let `` Si on a 40-30 et que le joueur � 30 gagne, on a un deuce`` () =
    let quarante = {
        JoueurAQuarante = P1
        PointsAutreJoueur = Trente
    }
    Score.FromQuarante quarante P2 =! Deuce

[<Fact>]
let `` Si on a 40-0 et que le joueur � 0 gagne, on a 40-15`` () =
    let quarante = {
        JoueurAQuarante = P1
        PointsAutreJoueur = Zero
    }
    Score.FromQuarante quarante P2 =! Quarante { JoueurAQuarante = P1;  PointsAutreJoueur = Quinze}

[<Fact>]
let `` Si on a 40-15 et que le joueur � 15 gagne, on a 40-30`` () =
    let quarante = {
        JoueurAQuarante = P1
        PointsAutreJoueur = Quinze
    }
    Score.FromQuarante quarante P2 =! Quarante { JoueurAQuarante = P1;  PointsAutreJoueur = Trente}

[<Fact>]
let `` Si on a 0-0 et qu'on a un gagnant, on passe � 15-0`` () =
    let points = {
        P1 = Zero
        P2 = Zero
    }
    Score.FromPoints points P1 =! Points { P1 = Quinze; P2 = Zero}
    Score.FromPoints points P2 =! Points { P1 = Zero; P2 = Quinze}

[<Fact>]
let `` Si on a 15-0 et qu'on a un gagnant, on passe � 30-0`` () =
    let points = {
        P1 = Quinze
        P2 = Zero
    }
    Score.FromPoints points P1 =! Points { P1 = Trente; P2 = Zero}
 
    Score.FromPoints {P1 = Zero; P2 = Quinze} P2 =! Points { P1 = Zero; P2 = Trente}

[<Fact>]
let `` Si on a 30-0 et qu'on a un gagnant, on passe � 40-0`` () =
    let points = {
        P1 = Trente
        P2 = Zero
    }
    Score.FromPoints points P1 =! Quarante {JoueurAQuarante = P1; PointsAutreJoueur = Zero}

    Score.FromPoints {P1 = Zero; P2 = Trente} P2 =! Quarante {JoueurAQuarante = P2; PointsAutreJoueur = Zero}
