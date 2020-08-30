module Program

type NutritionFacts = {
  Fat: int
  Salt: double
  Calories: int
}

type DietCompatibility = {
  Vegan: bool
  Vegetarian: bool
  Pescetarian: bool
}

type MealFacts = {
  NutritionFacts: NutritionFacts
  DietCompatibility: DietCompatibility
  Organic: bool
}

let mealFacts _ = 
    {
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

let isVegan ingredients = 
  ingredients
  |> List.forall (fun i -> i.DietCompatibility.Vegan)
  
