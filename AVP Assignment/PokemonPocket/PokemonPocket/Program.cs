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
            Random rdm = new Random();
            
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
                        foreach (var i in db) {
                            if (functions.checkEvolve(i.Name.ToLower(), db, pokemonMasters)) {
                                var evolveto = pokemonMasters.Where(p=> p.Name.ToLower() == i.Name.ToLower()).Select(p=> p.EvolveTo).First();
                                Console.WriteLine($"{i.Name} --> {evolveto}"); 
                                // Prints x times based on x number of pokemon with same type (FIX)
                            }
                        }
                    }
                }    
                else if (option == "4") {   // Evolve Pokemon
                    if (functions.checkDbEvolve(db, pokemonMasters)) { // If there are pokemon that can be evolved 
                        Console.WriteLine("Enter Pokemon's Name to Evolve: ");
                        string pokemon_name = Console.ReadLine().ToLower();
                        if (functions.checkEvolve(pokemon_name ,db, pokemonMasters)) { // If given pokemon can evolve
                            functions.Evolve(pokemon_name, db, pokemonMasters); // Evolve function
                        }
                        else {
                            Console.WriteLine($"{pokemon_name} cannot be evolved"); 
                        }
                    }
                    else {
                        Console.WriteLine("No pokemon can evolve.");
                    }
                }
                else if (option == "5") {   // Battling Pokemon
                    // Create a list of pokemon that can be fought
                    List<Pokemon> battle_pokemon = new List<Pokemon>();
                    // Create pokemon to fight with random HP 
                    var battle_pikachu = new Pikachu("Pikachu", rdm.Next(10, 90), 0);
                    var battle_eevee = new Pikachu("Eevee", rdm.Next(10, 90), 0);
                    var battle_char = new Pikachu("Charmander", rdm.Next(10, 90), 0);
                    // Decrement respective pokemon count to compensate for initialisation 
                    battle_pikachu.decCount();
                    battle_eevee.decCount();
                    battle_char.decCount();
                    // Add pokemon to battle list
                    battle_pokemon.Add(battle_pikachu);
                    battle_pokemon.Add(battle_eevee);
                    battle_pokemon.Add(battle_char);

                    Boolean battle = true;
                    while (battle) {
                        // Prompt user for chosen pokemon to use in battle
                        Console.WriteLine("Choose Pokemon to use in battle: ");
                        var pokemon_name = Console.ReadLine().ToLower();
                        if (functions.checkName(pokemon_name, db)) {
                            // Declare a random pokemon to battle against
                            int opp_ind = rdm.Next(battle_pokemon.Count);
                            var opp = battle_pokemon[opp_ind];
                            // Take the first (most hp) pokemon in db with the given name
                            var pokemon = db.Where(p=>p.Name.ToLower() == pokemon_name).First();

                            // Combat 
                            while (pokemon.HP > 0 || opp.HP > 0) {
                                // Opponent attacks first 
                                Console.WriteLine($"{opp.Name} (Opp) used {opp.Skill}!");
                                pokemon.calculateDamage(opp.Skill_Dmg); // Pokemon takes damage
                                if (pokemon.HP == 0) {  // If pokemon fainted, player loses
                                    Console.WriteLine($"{pokemon.Name} has fainted");
                                    Console.WriteLine("You Lose!");
                                }
                                else {
                                    // Show player's pokemon HP left
                                    Console.WriteLine($"{pokemon.Name} has {pokemon.HP} left!");
                                    // Player's pokemon attacks opponent
                                    Console.WriteLine($"{pokemon.Name} used {pokemon.Skill}!");
                                    opp.calculateDamage(pokemon.Skill_Dmg); // Opponent takes damage
                                    if (opp.HP == 0) { // If opponent pokemon fainted, player wins
                                        Console.WriteLine($"{opp.Name} has fainted!");
                                        Console.WriteLine("You Win!")
                                    }
                                    else {
                                        // Show opponent HP left
                                        Console.WriteLine($"{opp.Name} has {op.HP} left!")
                                    }
                                }
                            }
                            // Ask player if they want to continue
                            Console.WriteLine("Do you want to continue (Y/N): ");
                            string continue = Console.ReadLine().ToLower();
                            // if player does not enter yes, break while loop 
                            if (continue != "y") {
                                battle = false;
                            }
                        }
                    }
                }
                else if (option == "6") { // Heal Pokemon that has been damaged or fainted 
                
                }
                else {
                    Environment.Exit(0);
                }
            }













        }
    }
}
