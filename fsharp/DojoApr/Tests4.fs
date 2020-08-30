module Tests4

open System
open Xunit
open Program

// from https://github.com/cyriux/Monoidz-kata/blob/master/panini.md

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
    let bread = {
        NutritionFacts = {
            Fat = 2
            Salt = 0.2
            Calories = 100000            
        }
        DietCompatibility = {
            Vegan = true
            Vegetarian = true
            Pescetarian = true
        }
        Organic = true
    }
        
    let cheese = {
            NutritionFacts = {
                Fat = 80
                Salt = 0.0
                Calories = 20000            
            }
            DietCompatibility = {
                Vegan = false
                Vegetarian = true
                Pescetarian = true
            }
            Organic = false
        }
    let ham = {
            NutritionFacts = {
                Fat = 150
                Salt = 0.1
                Calories = 30000            
            }
            DietCompatibility = {
                Vegan = false
                Vegetarian = false
                Pescetarian = false
            }
            Organic = false
        }

    let paniniA = [
        bread, 2
        cheese, 1
        ham, 1
    ]

    let expectedNutritionFacts = {
        Fat = 234
        Salt = 0.5
        Calories = 250000            
    }
    
    [<Fact>]
    let ``when computing the mealFacts``() = 
        let expected = {
            NutritionFacts = {
                Fat = 234
                Salt = 0.5
                Calories = 250000            
            }
            DietCompatibility = {
                Vegan = false
                Vegetarian = false
                Pescetarian = false
            }
            Organic = false
        }
        Assert.Equal (expected, mealFacts paniniA)
  
    // [<Fact>]
    // let ``when checking whether it is vegan``() =
    //     Assert.False (isVegan paniniA)


    [<Fact>]
    let ``when checking whether ingredients are vegan``() =
         Assert.False (isVegan [bread;bread;ham;cheese])
            
    let paniniBreadOnly = [
        bread
    ]

    [<Fact>]
    let ``when checking whether the breadOnly panini it is vegan``() =
        let v = isVegan [bread]
        Assert.True (v)

    // let ``when checking whether it is vegetarian``() =`() =
    //     Assert.False <| isVegetarian paniniA

    // let ``when checking whether it is organic``() =
    //     Assert.False <| isOrganic paniniA
    // let ``when checking whether it is vegetarian``() =
    //     Assert.False <| isVegetarian paniniA

    // let ``when checking whether it is pescetarian``() =
    //     Assert.False <| isPescetarian paniniA

    // let ``when checking whether it is organic``() =
    //     Assert.False <| isOrganic paniniA

    // let ``when checking the nutricion facts``() =
    //     Assert.Equal (expectedNutritionFacts, getNutricionFacts paniniA)


(*

*** Timeframe
* Intro 15'
* Setup 5'
* First Coding 45' (troubleshooting tooling: 5')
* Retro 10'
* Second Coding 45' (troubleshooting tooling: 10')
* Retro 5'

*** User/ API Design
* Lot of discussions around API
* What's the API for the user?
* How are meals/ingredients created?
* Clash with real world model (would users work that way?)
* Most discussions about invoking the API, not so much about the actual implementation
* Chattiness? How granular should/can the API be?
* Granularity of API/model changed when writing tests

*** Process / Approach
* Discovering (data)structure while implementing
* Streamlined concepts / missing words / language from the outside
* Outside -> in approach: more design upfront
* Bottom-up: building up from small parts
* We missed a step when going from outside to the concrete implementation

*** Language/F#
* Structure of an F# program is interesting -> forces you in some direction (good or bad?)
* Anonymus-types good for prototyping vs half-ass-names
** replacing them with a proper type was easy
* F# pipes are nice

*** Tooling
* VS-Code Live Share
* Connection disruptions with VS-LS :-(
* Problems with F# test setup (missing Program.fs / main method)

*** General
* How should we teaser the learning goals in the future?
*)