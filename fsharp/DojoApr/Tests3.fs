module Tests

open System
open Xunit


// Let’s talk about food, from the perspective of diet compatibilities & nutrition facts.
// You have ingredients: salad, cheese, tomato, bread, ham, fish… that you can combine in order to make a delicious Panini.
// Each ingredient can be described by 
// its diet compatibilities: vegan, vegetarian, or pescetarian, 
// whether it's organic or not,
// and it has its own nutrition facts: calories, fat, salt and carbohydrates amounts (and many more).
// There are additional food items like drinks and deserts which can help create a meal together with a panini.
// Task: We want to derive the diet compatibilities & nutrition facts of any kind of Panini and for any meal built from these ingredients and food items.

// Salad is vegan, vegetarian, and pescetarian. It’s organic. Its nutrition facts are fat:0, salt:0, calories: 50.
// Cheese is not vegan, but is vegetarian and pescetarian. It’s not organic. Its nutrition facts are fat:80, salt:0, calories: 20000.
// Bread is vegan, vegetarian, and pescetarian. It’s organic. Its nutrition facts are fat:2, salt:0.2, calories: 100000.
// Ham is not vegan, not vegetarian, and not pescetarian. It’s not organic. Its nutrition facts are fat:150, salt:0.1, calories: 30000.
// Panini "A" consists of 2 slices of bread, ham and cheese and is not vegan, not vegetarian, and not pescetarian. Its not organic. Its nutrition facts are fat:234, salt 0.5, calories: 250000 
// Panini "B" consists of 2 slices of bread, 3 leaves of salad and cheese. It’s is not vegan, but is vegetarian and pescetarian. It’s not organic. Its nutrition facts are fat: 84, salt: 0.2, calories: 120150 

module ``Panini A`` =

    let bread = ...
    let cheese = ...
    let ham = ...

    let paniniA = [
        bread, 2
        cheese, 1
        ham, 1
    ]

    let expectedNutritionFacts ={|
        Fat = 234
        Salt = 0.5
        Calories = 250000            
    |}

    let ``when computing the X``() = 
        let expected = {|
            NutritionFacts = expectedNutritionFacts
            DietCompatibility = {|
                Vegan = False
                Vegetarian = False
                Pescetarian = False
            |}
            Organic = False
        |}

    let ``when checking whether it is vegan``() =
        Assert.False <| isVegan paniniA    

    let ``when checking whether it is vegetarian``() =
        Assert.False <| isVegetarian paniniA

    let ``when checking whether it is organic``() =
        Assert.False <| isOrganic paniniA
    let ``when checking whether it is vegetarian``() =
        Assert.False <| isVegetarian paniniA

    let ``when checking whether it is pescetarian``() =
        Assert.False <| isPescetarian paniniA

    let ``when checking whether it is organic``() =
        Assert.False <| isOrganic paniniA

    let ``when checking the nutricion facts``() =
        Assert.Equal (expectedNutritionFacts, getNutricionFacts paniniA)


[<Fact>]
let ``My test`` () =
    Assert.True(true)
