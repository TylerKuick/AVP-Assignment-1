using System;
using System.Collections.Generic;
using System.Linq;
using Function;

namespace PokemonPocket
{
    class Program
    {
        static void Main(string[] args)
        {   
            //PokemonMaster list for checking pokemon evolution availability.    
            List<PokemonMaster> pokemonMasters = new List<PokemonMaster>(){
            new PokemonMaster("Pikachu", 2, "Raichu"),
            new PokemonMaster("Eevee", 3, "Flareon"),
            new PokemonMaster("Charmander", 1, "Charmeleon")
            };

            // Database 
            List<Pokemon> db = new List<Pokemon>() {
                new Pikachu("Pikachu", 50, 20),
                new Eevee("Eevee", 30, 10),
                new Eevee("Eevee", 60, 9),
                new Charmander("Charmander", 40, 0)
            };

            // Initialise Function
            Funct functions = new Funct();
            
            //Use "Environment.Exit(0);" if you want to implement an exit of the console program
            //Start your assignment 1 requirements below.
            Boolean loop = true;
            while (loop) {
                Console.WriteLine(@"
                ----------------------------------
                Welcome to Pokemon Pocket App
                ----------------------------------
                (1). Add Pokemon to Pocket
                (2). List pokemon(s) in my Pocket
                (3). Check if Pokemon can Evolve
                (4). Evolve Pokemon
                (5). Battle Pokemon 
                (6). Heal Pokemon
                ----------------------------------
                ");
                Console.WriteLine("Please only enter [1, 2, 3, 4, 5, 6] or Q to quit: ");
                string option = Console.ReadLine();
                if (option == "1") {
                    Console.WriteLine("Enter Pokemon's Name: ");
                    string pokemon_name = Console.ReadLine().ToLower();
                    if (functions.checkName(pokemon_name, db)){
                        Console.WriteLine("Enter Pokemon's HP: ");
                        int pokemon_hp = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Pokemon's Exp: ");
                        int pokemon_exp = int.Parse(Console.ReadLine());
                        if (pokemon_name.Equals("pikachu")){
                            db.Add(new Pikachu(pokemon_name, pokemon_hp, pokemon_exp)); 
                        }
                        else if (pokemon_name.Equals("eevee")) {
                            db.Add(new Eevee(pokemon_name, pokemon_hp, pokemon_exp));
                        }
                        else if (pokemon_name.Equals("charmander")) {
                            db.Add(new Charmander(pokemon_name, pokemon_hp, pokemon_exp));
                        }
                    }
                    else {
                        Console.WriteLine($"{pokemon_name} not found in the database!");
                    }
                }
                else if (option == "2") { // Show List of Pokemon in Pocket
                    // Sort the db by Descending HP Order
                    for (var i=db.Count-1; i>0; i--) {
                        for (var j=0; j<i; j++) {
                            if (j+1 < db.Count) {
                                if (db[j].HP < db[j+1].HP) {
                                    var tmp = db[j];
                                    db[j] = db[j+1];
                                    db[j+1] = tmp;
                                }
                            }
                        }
                    }
                    // Print each Pokemon's information 
                    foreach (var i in db){
                        Console.WriteLine(@$"
                        ----------------------------------
                        Pokemon Name: {i.Name}
                        Pokemon HP: {i.HP}
                        Pokemon Exp: {i.Exp}
                        ----------------------------------
                        ");
                    }
                }
                else if (option == "3") { // Check if Pokemon can Evolve
                    if (!functions.checkDbEvolve(db, pokemonMasters)) { // If no pokemon can be evolved 
                        Console.WriteLine("No pokemon can evolve.");
                    }
                    else {
                        
                    }
                }    
                else if (option == "4") {
                    if (functions.checkDbEvolve(db, pokemonMasters)) { // If there are pokemon that can be evolved 
                        Console.WriteLine("Enter Pokemon's Name to Evolve: ");
                        string pokemon_name = Console.ReadLine().ToLower();
                        if (functions.checkEvolve(pokemon_name ,db, pokemonMasters)) {
                            functions.Evolve(pokemon_name, db, pokemonMasters);
                        }
                        else {
                            Console.WriteLine($"{pokemon_name} cannot be evolved");
                        }
                        
                    }
                    else {
                        Console.WriteLine("No pokemon can evolve.");
                    }
                }
                else {
                    Environment.Exit(0);
                }
            }













        }
    }
}
